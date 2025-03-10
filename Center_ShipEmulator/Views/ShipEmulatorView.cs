﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using ShipEmulator.Models;

namespace ShipEmulator
{
    public partial class ShipEmulatorView : Form
    {
        private UdpClient mGpsUDPClient;
        private UdpClient mRpmUDPClient;
        private UdpClient mChangePortGPS;
        private UdpClient mChangePortRPM;
        private Thread mThread_Gps;
        private Thread mThread_Rpm;
        private bool mIsRunning = false;
        private int mGpsPort = 2323;
        private int mRpmPort = 2424;

        private string mGPGGA = "$GPGGA,114455.532,3735.0079,N,12701.6446,E,1,03,7.9,48.8,M,19.6,M,0.0,0000*23";
        private int mRpm = 1224;

        private int mGetGPSPortData = 2323;
        private int mGetRPMPortData = 2424;

        private DateTime mGetGpsTime;
        private DateTime mGetRpmTime;

        private DateTime? mConnectGpsTime;
        private DateTime? mConnectRpmTime;

        DatabaseHelper mDatabaseHelper = new DatabaseHelper();
        private List<PointLatLng> pointsList;
        public GMapOverlay DrawPoint;
        private System.Timers.Timer mTimer_UI;



        public ShipEmulatorView()
        {
            InitializeComponent();
            StartChangeGpsPort();
            StartChangeRpmPort();
            mTimer_UI = new System.Timers.Timer(100);
            mTimer_UI.Elapsed += Timer_UI;
            mTimer_UI.AutoReset = true;
            mTimer_UI.Start();
        }

        // 해당 폼이 로드 될 때 불러오는 함수 
        private void ShipEmulatorView_Load(object sender, EventArgs e)
        {
            // 구글 맵을 불러와서 화면에 로드하는 함수
            gMap_Main.MapProvider = GMapProviders.GoogleMap;
            gMap_Main.DragButton = MouseButtons.Left;
            gMap_Main.Position = new PointLatLng(37.2328660, 131.8654529);
            gMap_Main.MinZoom = 5;
            gMap_Main.MaxZoom = 50;
            gMap_Main.Zoom = 14;
            gMap_Main.ShowCenter = false;

            DrawPoint = new GMapOverlay("point");
            gMap_Main.Overlays.Add(DrawPoint);
            DotMarker.MarkerOverlay = DrawPoint;

            pointsList = new List<PointLatLng>();

            mGetGpsTime = DateTime.Now.AddSeconds(-5);
            mGetRpmTime = DateTime.Now.AddSeconds(-5);


        }

        // 마커가 지도 범위를 넘어갈 경우 해당 점에 맞춰 지도가 이동하는 함수
        private void Shift_Map(PointLatLng marking)
        {
            RectLatLng viewArea = gMap_Main.ViewArea;

            // 마킹 위치가 현재 뷰 영역 밖에 있을 때만 이동
            if (!viewArea.Contains(marking))
            {
                double step = 0.005;
                
                // 천천히 움직이므로 이러한 제한을 주기 전보다 훨씬 더 부드럽게 이동할 수 있음 
                if (gMap_Main.Position.Lat < marking.Lat)
                    gMap_Main.Position = new PointLatLng(gMap_Main.Position.Lat + step, gMap_Main.Position.Lng);
                else if (gMap_Main.Position.Lat > marking.Lat)
                    gMap_Main.Position = new PointLatLng(gMap_Main.Position.Lat - step, gMap_Main.Position.Lng);

                if (gMap_Main.Position.Lng < marking.Lng)
                    gMap_Main.Position = new PointLatLng(gMap_Main.Position.Lat, gMap_Main.Position.Lng + step);
                else if (gMap_Main.Position.Lng > marking.Lng)
                    gMap_Main.Position = new PointLatLng(gMap_Main.Position.Lat, gMap_Main.Position.Lng - step);
            }
        }


        // 수신 시작 버튼 클릭 시 불러오는 함수 
        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!mIsRunning)
            {
                mIsRunning = true;
                DrawPoint.Markers.Clear();

                Button_Start.Enabled = false;
                Button_Stop.Enabled = true;

                mThread_Gps = new Thread(GetGpsData);
                mThread_Rpm = new Thread(GetRpmData);

                mThread_Gps.IsBackground = true;
                mThread_Rpm.IsBackground = true;

                mThread_Gps.Start();
                mThread_Rpm.Start();

                Button_Change_PortGPS.Enabled = false;
                Button_Change_PortRPM.Enabled = false;
            }
        }

        // 수신 종료 버튼을 클릭 시 불러오는 함수 
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            if (mIsRunning)
            {
                mIsRunning = false;

                Button_Start.Enabled = true;
                Button_Stop.Enabled = false;

                Button_Change_PortGPS.Enabled = true;
                Button_Change_PortRPM.Enabled = true;

            }
        }

        // 포트 변경 이후 GPS UDP 클라이언트 재시작시키는 함수 
        private void RestartGps()
        {
                if (mGpsUDPClient != null)
                {
                    mGpsUDPClient.Close();
                    mGpsUDPClient = null;
                }
                mGpsUDPClient = new UdpClient(mGpsPort);
        }

        // 포트 변경 이후 RPM UDP 클라이언트 재시작시키는 함수 
        private void RestartRPM()
        {
            if (mIsRunning)
            {
                if (mRpmUDPClient != null)
                {
                    mRpmUDPClient.Close();
                    mRpmUDPClient = null;
                }
                mRpmUDPClient = new UdpClient(mRpmPort);

            }
        }

        // 선박으로부터 GPS 데이터를 받아와 데이터 파싱하여 DB에 저장하고, 화면에 텍스트로 보여주는 함수 
        private void GetGpsData()
        {
            Console.WriteLine($"GET{mGpsPort}");
            IPEndPoint point = new IPEndPoint(IPAddress.Parse("192.168.0.53"), mGpsPort);
            byte[] getBytes;
            string getGPS;
            Decimal Latitude;
            Decimal Longitude;

            try
            {
                if (mGpsUDPClient == null)
                {
                    mGpsUDPClient = new UdpClient(mGpsPort);
                }
                mGpsUDPClient.Client.ReceiveTimeout = 1000;

                while (mIsRunning)
                {
                    try
                    {
                        getBytes = mGpsUDPClient.Receive(ref point);
                        mGetGpsTime = DateTime.Now;
                        if (mConnectGpsTime == null)
                        {
                            mConnectGpsTime = DateTime.Now;
                        }
                        getGPS = Encoding.UTF8.GetString(getBytes);
                        Latitude = ChangeGPSLoacation(getGPS.Split(',')[2], getGPS.Split(',')[3]);
                        Longitude = ChangeGPSLoacation(getGPS.Split(',')[4], getGPS.Split(',')[5]);

                        GPS gps = new GPS()
                        {
                            GPS_TIME = ChangeDateTime(getGPS.Split(',')[1]),
                            GPS_Latitude = Latitude,
                            GPS_Longitude = Longitude
                        };

                        mDatabaseHelper.AddGPS(gps);
                        AddPoint(Latitude, Longitude);

                        mGPGGA = getGPS;
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.TimedOut)
                        {
                            continue;
                        }
                    }
                }
            }
            finally
            {
                if (mGpsUDPClient != null)
                {
                    mGpsUDPClient.Close();
                    mGpsUDPClient = null;
                }
            }
        }

        // 선박으로부터 RPM 데이터를 받아와 DB에 저장하고, 화면에 텍스트로 보여주는 함수 
        private void GetRpmData()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse("192.168.0.53"), mRpmPort);
            byte[] getBytes;
            int rpmData;
            try
            {
                if (mRpmUDPClient == null)
                {
                    mRpmUDPClient = new UdpClient(mRpmPort);
                }
                mRpmUDPClient.Client.ReceiveTimeout = 1000;
                while (mIsRunning)
                {
                    try
                    {
                        getBytes = mRpmUDPClient.Receive(ref point);
                        mGetRpmTime = DateTime.Now;
                        if (mConnectRpmTime == null)
                        {
                            mConnectRpmTime = DateTime.Now;
                        }
                        rpmData = BitConverter.ToInt32(getBytes, 0);
                        mDatabaseHelper.AddRPM(rpmData);
                        Gauge.rpm = rpmData;

                        mRpm = rpmData;
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.TimedOut)
                        {
                            continue;
                        }
                    }
                }
            }
            finally
            {
                if (mRpmUDPClient != null)
                {
                    mRpmUDPClient.Close();
                    mRpmUDPClient = null;
                }
            }
        }

        // hhmmss.fff 형태의 데이터 파싱 데이터 일부를 utc로 만드는 함수 
        private DateTime ChangeDateTime(string time)
        {
            int hour = int.Parse(time.Substring(0, 2));
            int minute = int.Parse(time.Substring(2, 2));
            int second = int.Parse(time.Substring(4, 2));
            int millisecond = 0;
            if (time.Contains("."))
            {
                string[] timeParts = time.Split('.');
                if (timeParts.Length > 1)
                {
                    millisecond = int.Parse(timeParts[1].Substring(0, 3));
                }
            }
            DateTime changeTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day,
                                                    hour, minute, second, millisecond, DateTimeKind.Utc);

            return changeTime;
        }

        // 지도에 점 찍는 함수 
        private void AddPoint(Decimal latitude, Decimal longitude)
        {
            gMap_Main.Invoke(new MethodInvoker(() =>
            {
                DotMarker point = new DotMarker(new PointLatLng((double)latitude, (double)longitude), System.Drawing.Color.Blue);
                DrawPoint.Markers.Add(point);
                Shift_Map(point.Position);
                gMap_Main.Refresh();
            }));
        }

        // GPS 데이터 중 위도와 경도에 있어서 NMEA 신호를 일반적인 위경도의 단위로 변환하는 함수 
        private Decimal ChangeGPSLoacation(string location, string direction)
        {
            if (!string.IsNullOrEmpty(location))
            {
                Decimal num = Decimal.Parse(location);
                Decimal degree = Math.Floor(num / 100);
                Decimal minute = num % 100;

                Decimal decimalDegrees = degree + (minute / 60);

                if (direction == "S" || direction == "W")
                {
                    decimalDegrees *= -1;
                }

                return decimalDegrees;
            }

            return 0;
        }

        // 수신 중일 때는 폼을 닫지 못하게 하는 함수 
        private void ShipEmulatorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mIsRunning)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        // 스레드 내에서 Lable을 바꾸는 것이 아닌, 타이머를 사용해서 0.1초에 UI를 새로 그릴 수 있도록 타이머를 호출해주는 함수 
        private void Timer_UI(object sender, ElapsedEventArgs e)
        {

            if (string.IsNullOrEmpty(mGPGGA)) return;

            try
            {
                string[] gpsData = mGPGGA.Split(',');

                if (gpsData.Length < 6) return;

                if (Label_Text_Sentence.InvokeRequired)
                {
                    Label_Text_Sentence.Invoke(new Action(() =>
                    {
                        UpdateUI();
                    }));
                }
                else
                {
                    UpdateUI();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // UI를 변화시킬 때 0.1초에 한 번 사용하는 함수.
        private void UpdateUI()
        {
            Label_Text_Sentence.Text = mGPGGA;
            Label_Text_Latitude.Text = $"{ChangeGPSLoacation(mGPGGA.Split(',')[2], mGPGGA.Split(',')[3]):F6}도";
            Label_Text_Longitude.Text = $"{ChangeGPSLoacation(mGPGGA.Split(',')[4], mGPGGA.Split(',')[5]):F6}도";
            Label_Text_RPM.Text = $"{mRpm:0000}";
            Label_Text_PortGPS.Text = $"센터 : {mGpsPort.ToString()}";
            Label_Text_PortRPM.Text = $"센터 : {mRpmPort.ToString()}";
            Label_Text_ShipPortGPS.Text = $"선박 : {mGetGPSPortData.ToString()}";
            Label_Text_ShipPortRPM.Text = $"선박 : {mGetRPMPortData.ToString()}";

            if (mGpsPort == mGetGPSPortData)
            {
                Label_Text_PortGPS.ForeColor = Color.DimGray;
                Label_Text_ShipPortGPS.ForeColor = Color.DimGray;
            }

            else 
            {
                Label_Text_PortGPS.ForeColor = Color.IndianRed;
                Label_Text_ShipPortGPS.ForeColor = Color.IndianRed;
            }

            if (mRpmPort == mGetRPMPortData)
            {
                Label_Text_PortRPM.ForeColor = Color.DimGray;
                Label_Text_ShipPortRPM.ForeColor = Color.DimGray;
            }

            else
            {
                Label_Text_PortRPM.ForeColor = Color.IndianRed;
                Label_Text_ShipPortRPM.ForeColor = Color.IndianRed;
            }

            if ((DateTime.Now - mGetGpsTime).TotalSeconds >= 3)
            {
                Label_Connection_GPS.ForeColor = Color.IndianRed;
            }
            else if (mConnectGpsTime.HasValue && (DateTime.Now - mConnectGpsTime.Value).TotalSeconds >= 3)
            {
                Label_Connection_GPS.ForeColor = Color.Green;
                mConnectGpsTime = null;
            }

            if ((DateTime.Now - mGetRpmTime).TotalSeconds >= 3)
            {
                Label_Connection_RPM.ForeColor = Color.IndianRed;
            }
            else if (mConnectRpmTime.HasValue && (DateTime.Now - mConnectRpmTime.Value).TotalSeconds >= 3)
            {
                Label_Connection_RPM.ForeColor = Color.Green;
                mConnectRpmTime = null;
            }



        }

        // GPS 포트 번호 변경 버튼 클릭시 실행되는 함수 
        private void Button_Change_PortGPS_Click(object sender, EventArgs e)
        {
            if (TextBox_Change_portGPS.Text != "")
            {
                if (int.Parse(TextBox_Change_portGPS.Text) == 50505 || int.Parse(TextBox_Change_portGPS.Text) == 50506)
                {
                    ShowMessage("사용할 수 없는 포트 번호입니다.", "gps");
                }
                else if (int.Parse(TextBox_Change_portGPS.Text) == mRpmPort)
                {
                    ShowMessage("GPS포트와 RPM포트는 동일할 수 없습니다.", "gps");
                }
                else if (int.Parse(TextBox_Change_portGPS.Text) == mGetRPMPortData)
                {
                    ShowMessage("선박에서 RPM포트로 사용하고 있는 포트입니다.", "gps");
                }
                else
                {
                    mGpsPort = int.Parse(TextBox_Change_portGPS.Text);
                    message_gpsPort.Visible = false;
                    RestartGps();
                    UpdateUI();
                }
            }
        }

        // RPM 포트 번호 변경 버튼 클릭시 실행되는 함수 
        private void Button_Change_PortRPM_Click(object sender, EventArgs e)
        {
            if (TextBox_Change_portRPM.Text != "")
            {
                if (int.Parse(TextBox_Change_portRPM.Text) == 50505 || int.Parse(TextBox_Change_portRPM.Text) == 50506)
                {
                    ShowMessage("사용할 수 없는 포트 번호입니다.", "rpm");
                }
                else if (int.Parse(TextBox_Change_portRPM.Text) == mGpsPort)
                {
                    ShowMessage("GPS포트와 RPM포트는 동일할 수 없습니다.", "rpm");
                }

                else if (int.Parse(TextBox_Change_portRPM.Text) == mGetGPSPortData)
                {
                    ShowMessage("선박에서 GPS포트로 사용하고 있는 포트입니다.", "rpm");
                }

                else
                {
                    mRpmPort = int.Parse(TextBox_Change_portRPM.Text);
                    message_rpmPort.Visible = false;
                    RestartRPM();
                    UpdateUI();
                }

            }
        }

        // 포트 번호 변경에 대해서 문제가 생길 경우 UI적으로 전시해주는 함수 
        private void ShowMessage(string message, string type)
        {
            if (type == "gps")
            {
                message_gpsPort.Visible = true;
                message_gpsPort.Text = message;
            }

            if (type == "rpm")
            {
                message_rpmPort.Visible = true;
                message_rpmPort.Text = message;
            }
        }

        // 비동기 프로그래밍을 사용하여 선박에서 변화된 GPS 포트를 감지하는 함수 
        private async void StartChangeGpsPort()
        {
            if (mChangePortGPS == null)
            {
                mChangePortGPS = new UdpClient(50505);
                await GetChangeGpsPort();
            }
        }

        // 비동기 프로그래밍을 사용하여 선박에서 변화된 RPM 포트를 감지하는 함수 
        private async void StartChangeRpmPort()
        {
            if (mChangePortRPM == null)
            {
                mChangePortRPM = new UdpClient(50506);
                await GetChangeRpmPort();
            }
        }

        // 감지한 GPS포트를 받아 변환 후 Label로 정보를 뿌려주는 함수 
        private async Task GetChangeGpsPort() 
        {
            UdpReceiveResult getGpsPort = await mChangePortGPS.ReceiveAsync();
            mGetGPSPortData = BitConverter.ToInt32(getGpsPort.Buffer, 0);

            await GetChangeGpsPort();
        }

        // 감지한 RPM포트를 받아 변환 후 Label로 정보를 뿌려주는 함수 
        private async Task GetChangeRpmPort()
        {
            UdpReceiveResult getRpmPort = await mChangePortRPM.ReceiveAsync();
            mGetRPMPortData = BitConverter.ToInt32(getRpmPort.Buffer, 0);

            await GetChangeRpmPort();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

