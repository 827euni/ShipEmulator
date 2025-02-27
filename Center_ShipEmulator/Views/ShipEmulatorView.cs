using System;
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
        private UdpClient mRpmUDLClient;
        private UdpClient mChangePortGPS;
        private UdpClient mChangePortRPM;
        private Thread mThread_Gps;
        private Thread mThread_Rpm;
        private bool mIsRunning = false;
        private int mGpsPort = 2323;
        private int mRpmPort = 2424;
        private volatile string mGPGGA;
        private int mRpm;
        private int mGetGPSPortData;

        DatabaseHelper mDatabaseHelper = new DatabaseHelper();
        private List<PointLatLng> pointsList;
        public GMapOverlay DrawPoint;
        private System.Timers.Timer mTimer_UI;



        public ShipEmulatorView()
        {
            InitializeComponent();
            StartChangeGpsPort();
            mTimer_UI = new System.Timers.Timer(100);
            mTimer_UI.Elapsed += Timer_UI;
            mTimer_UI.AutoReset = true;
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
            gMap_Main.Zoom = 13;
            gMap_Main.ShowCenter = false;

            DrawPoint = new GMapOverlay("point");
            gMap_Main.Overlays.Add(DrawPoint);
            DotMarker.MarkerOverlay = DrawPoint;

            pointsList = new List<PointLatLng>();
        }

        // 마커가 지도 범위를 넘어갈 경우 해당 점에 맞춰 지도가 이동하는 함수
        private void Shift_Map(PointLatLng marking)
        {
            RectLatLng viewArea = gMap_Main.ViewArea;

            if (!viewArea.Contains(marking))
            {
                gMap_Main.Position = marking;
            }
        }

        // 수신 시작 버튼 클릭 시 불러오는 함수 
        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!mIsRunning)
            {
                mIsRunning = true;
                DrawPoint.Markers.Clear();
                //Thread ChangeGpsPort = new Thread(GetChangeGPSPort);
                //Thread ChangeRPMPort = new Thread(GetGhangeRPMPort);

                mThread_Gps = new Thread(GetGpsData);
                mThread_Rpm = new Thread(GetRpmData);

                mThread_Gps.IsBackground = true;
                mThread_Rpm.IsBackground = true;
                //ChangeGpsPort.IsBackground = true;
                //ChangeRPMPort.IsBackground = true;

                mThread_Gps.Start();
                mThread_Rpm.Start();
                //ChangeGpsPort.Start();
                //ChangeRPMPort.Start();

                mTimer_UI.Start();

                Button_Start.Enabled = false;
                Button_Stop.Enabled = true;
            }
        }

        // 수신 종료 버튼을 클릭 시 불러오는 함수 
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            if (mIsRunning)
            {
                mIsRunning = false;

                mTimer_UI.Stop();

                Button_Start.Enabled = true;
                Button_Stop.Enabled = false;

            }
        }

        //// 선박에서 GPS 포트가 변경되었을 때 그것을 감지하는 함수 
        //private void GetChangeGPSPort()
        //{
        //    if (mChangePortGPS == null)
        //    {
        //        mChangePortGPS = new UdpClient(50505);
        //    }
        //    IPEndPoint point = new IPEndPoint(IPAddress.Any, 50505);
        //    byte[] getBytes;
        //    int GPSPortData;

        //    while (true) // 송신, 수신 여부와 상관없이 반드시 포트 변경은 감지해야함.
        //    {
        //        getBytes = mChangePortGPS.Receive(ref point);
        //        GPSPortData = BitConverter.ToInt32(getBytes, 0);

        //        Invoke(new Action(() =>
        //        {
        //            if (mGpsPort != GPSPortData)
        //            {
        //                mGpsPort = GPSPortData;
        //                Label_Text_PortGPS.Text = mGpsPort.ToString();
        //                RestartGps();
        //            }
        //        }));

        //    }
        //}

        //// 선박에서 RPM 포트가 변경되었을 때 그것을 감지하는 함수 
        //private void GetGhangeRPMPort()
        //{

        //    if (mChangePortRPM == null)
        //    {
        //        mChangePortRPM = new UdpClient(50506);
        //    }
        //    IPEndPoint point = new IPEndPoint(IPAddress.Any, 50506);
        //    byte[] getBytes;
        //    int RPMPortData;

        //    while (true) // 송신, 수신 여부와 상관없이 반드시 포트 변경은 감지해야함.
        //    {
        //        getBytes = mChangePortRPM.Receive(ref point);
        //        RPMPortData = BitConverter.ToInt32(getBytes, 0);

        //        Invoke(new Action(() =>
        //        {
        //            if (mRpmPort != RPMPortData)
        //            {
        //                mRpmPort = RPMPortData;
        //                Label_Text_PortRPM.Text = mRpmPort.ToString();
        //                RestartRPM();
        //            }
        //        }));

        //    }
        //}

        // 포트 변경 이후 GPS UDP 클라이언트 재시작시키는 함수 
        private void RestartGps()
        {
            if (mIsRunning)
            {
                if (mGpsUDPClient != null)
                {
                    mGpsUDPClient.Close();
                    mGpsUDPClient = null;
                }
                mGpsUDPClient = new UdpClient(mGpsPort);
            }
        }

        // 포트 변경 이후 RPM UDP 클라이언트 재시작시키는 함수 
        private void RestartRPM()
        {
            if (mIsRunning)
            {
                if (mRpmUDLClient != null)
                {
                    mRpmUDLClient.Close();
                    mRpmUDLClient = null;
                }
                mRpmUDLClient = new UdpClient(mRpmPort);

            }
        }

        // 선박으로부터 GPS 데이터를 받아와 데이터 파싱하여 DB에 저장하고, 화면에 텍스트로 보여주는 함수 
        private void GetGpsData()
        {
            Console.WriteLine($"GET{mGpsPort}");
            IPEndPoint point = new IPEndPoint(IPAddress.Any, mGpsPort);
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

                        


                        //Invoke(new Action(() =>
                        //{
                        //    Label_Text_Sentence.Text = $"{getGPS}";
                        //    Label_Text_Latitude.Text = $"{Latitude.ToString("F6")}도";
                        //    Label_Text_Longitude.Text = $"{Longitude.ToString("F6")}도";
                        //}));

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
                mGpsUDPClient.Close();
                mGpsUDPClient = null;
            }
        }

        // 선박으로부터 RPM 데이터를 받아와 DB에 저장하고, 화면에 텍스트로 보여주는 함수 
        private void GetRpmData()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, mRpmPort);
            byte[] getBytes;
            int rpmData;

            try
            {
                mRpmUDLClient = new UdpClient(mRpmPort);
                mRpmUDLClient.Client.ReceiveTimeout = 1000;
                while (mIsRunning)
                {
                    try
                    {
                        getBytes = mRpmUDLClient.Receive(ref point);
                        rpmData = BitConverter.ToInt32(getBytes, 0);
                        mDatabaseHelper.AddRPM(rpmData);
                        Gauge.rpm = rpmData;

                        mRpm = rpmData;

                        //Invoke(new Action(() =>
                        //{
                        //    Label_Text_RPM.Text = $"{rpmData.ToString("0000")}";
                        //}));

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
                mRpmUDLClient.Close();
                mRpmUDLClient = null;
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

        private void Timer_UI(object sender, ElapsedEventArgs e)
        {

            if (!mIsRunning || string.IsNullOrEmpty(mGPGGA)) return;

            try
            {
                string[] gpsData = mGPGGA.Split(',');

                if (gpsData.Length < 6) return;

                if (Label_Text_Sentence.InvokeRequired)
                {
                    Label_Text_Sentence.Invoke(new Action(() =>
                    {
                        UpdateUI(gpsData);
                    }));
                }
                else
                {
                    UpdateUI(gpsData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateUI(string[] gpsData)
        {
            Label_Text_Sentence.Text = mGPGGA;
            Label_Text_Latitude.Text = $"{ChangeGPSLoacation(gpsData[2], gpsData[3]):F6}도";
            Label_Text_Longitude.Text = $"{ChangeGPSLoacation(gpsData[4], gpsData[5]):F6}도";
            Label_Text_RPM.Text = $"{mRpm:0000}";
            Label_Text_PortGPS.Text = mGetGPSPortData.ToString();
        }

        // GPS 포트 번호 변경 버튼 클릭시 실행되는 함수 
        private void Button_Change_PortGPS_Click(object sender, EventArgs e)
        {
            if (mIsRunning)
            {
                if (TextBox_Change_portGPS.Text != "")
                {
                    if (int.Parse(TextBox_Change_portGPS.Text) == 50505 || int.Parse(TextBox_Change_portGPS.Text) == 50506)
                    {
                        MessageBox.Show("사용할 수 없는 포트 번호입니다.");
                    }
                    else if (int.Parse(TextBox_Change_portGPS.Text) == mRpmPort)
                    {
                        MessageBox.Show("GPS포트와 RPM포트는 동일할 수 없습니다.");
                    }
                    else
                    {
                        mGpsPort = int.Parse(TextBox_Change_portGPS.Text);
                        RestartGps();
                        Label_Text_PortGPS.Text = "0"; //여기 수정해야함. 
                    }
                }
            }
            else
            {
                Button_Change_PortGPS.Enabled = false;
            }
        }


        private void Button_Change_PortRPM_Click(object sender, EventArgs e)
        {

        }

        private async void StartChangeGpsPort()
        {
            if (mChangePortGPS == null)
            {
                mChangePortGPS = new UdpClient(50505);
                await GetChangeGpsPort();
            }
        }

        private async Task GetChangeGpsPort() 
        {
            UdpReceiveResult getGpsPort = await mChangePortGPS.ReceiveAsync();
            mGetGPSPortData = BitConverter.ToInt32(getGpsPort.Buffer, 0);

            await GetChangeGpsPort();
        }

    }
}

