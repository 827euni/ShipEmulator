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

        DatabaseHelper mDatabaseHelper = new DatabaseHelper();
        private List<PointLatLng> pointsList;
        public GMapOverlay DrawPoint;



        public ShipEmulatorView()
        {
            InitializeComponent();
        }

        private void ShipEmulatorView_Load(object sender, EventArgs e)
        {
            // 구글 맵을 불러와서 화면에 로드하는 함수
            gMap_Main.MapProvider = GMapProviders.GoogleMap;
            gMap_Main.DragButton = MouseButtons.Left;
            gMap_Main.Position = new PointLatLng(37.2328660, 131.8654529);
            gMap_Main.MinZoom = 5;
            gMap_Main.MaxZoom = 50;
            gMap_Main.Zoom = 12;
            gMap_Main.ShowCenter = false;

            DrawPoint = new GMapOverlay("point");
            gMap_Main.Overlays.Add(DrawPoint);

            pointsList = new List<PointLatLng>();
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!mIsRunning)
            {
                mIsRunning = true;
                DrawPoint.Markers.Clear();
                Thread ChangeGpsPort = new Thread(GetChangeGPSPort);
                Thread ChangeRPMPort = new Thread(GetGhangeRPMPort);

                mThread_Gps = new Thread(GetGpsData);
                mThread_Rpm = new Thread(GetRpmData);

                mThread_Gps.IsBackground = true;
                mThread_Rpm.IsBackground = true;
                ChangeGpsPort.IsBackground = true;
                ChangeRPMPort.IsBackground = true;

                mThread_Gps.Start();
                mThread_Rpm.Start();
                ChangeGpsPort.Start();
                ChangeRPMPort.Start();

                Button_Start.Enabled = false;
                Button_Stop.Enabled = true;
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            if (mIsRunning)
            {
                mIsRunning = false;

                Button_Start.Enabled = true;
                Button_Stop.Enabled = false;
            }
        }

        private void GetChangeGPSPort()
        {
            if (mChangePortGPS == null)
            {
                mChangePortGPS = new UdpClient(50505);
            }
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 50505);
            byte[] getBytes;
            int GPSPortData;

            while (true) // 송신, 수신 여부와 상관없이 반드시 포트 변경은 감지해야함.
            {
                getBytes = mChangePortGPS.Receive(ref point);
                GPSPortData = BitConverter.ToInt32(getBytes, 0);

                Invoke(new Action(() =>
                {
                    if (mGpsPort != GPSPortData)
                    {
                        mGpsPort = GPSPortData;
                        Label_Text_PortGPS.Text = mGpsPort.ToString();
                        RestartGps();
                    }
                }));

            }
        }



        private void GetGhangeRPMPort()
        {

            if (mChangePortRPM == null)
            {
                mChangePortRPM = new UdpClient(50506);
            }
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 50506);
            byte[] getBytes;
            int RPMPortData;

            while (true) // 송신, 수신 여부와 상관없이 반드시 포트 변경은 감지해야함.
            {
                getBytes = mChangePortRPM.Receive(ref point);
                RPMPortData = BitConverter.ToInt32(getBytes, 0);

                Invoke(new Action(() =>
                {
                    if (mRpmPort != RPMPortData)
                    {
                        mRpmPort = RPMPortData;
                        Label_Text_PortRPM.Text = mRpmPort.ToString();
                        RestartRPM();
                    }
                }));

            }
        }

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

        private void GetGpsData()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, mGpsPort);
            byte[] getBytes;
            string gpsData;
            Decimal Latitude;
            Decimal Longitude;

            try
            {
                if(mGpsUDPClient == null)
                {
                    mGpsUDPClient = new UdpClient(mGpsPort);
                }
                mGpsUDPClient.Client.ReceiveTimeout = 1000;

                while (mIsRunning)
                {
                    try
                    {
                        getBytes = mGpsUDPClient.Receive(ref point);
                        gpsData = Encoding.UTF8.GetString(getBytes);
                        Latitude = ChangeGPSLoacation(gpsData.Split(',')[2], gpsData.Split(',')[3]);
                        Longitude = ChangeGPSLoacation(gpsData.Split(',')[4], gpsData.Split(',')[5]);

                        GPS gps = new GPS()
                        {
                            GPS_TIME = ChangeDateTime(gpsData.Split(',')[1]),
                            GPS_Latitude = Latitude,
                            GPS_Longitude = Longitude
                        };

                        mDatabaseHelper.AddGPS(gps);
                        AddPoint(Latitude, Longitude);

                        Invoke(new Action(() =>
                        {
                            Label_Text_Sentence.Text = $"{gpsData}";
                            Label_Text_Latitude.Text = $"{Latitude.ToString("F6")}도";
                            Label_Text_Longitude.Text = $"{Longitude.ToString("F6")}도";
                        }));

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



                        Invoke(new Action(() =>
                        {
                            Label_Text_RPM.Text = $"{rpmData.ToString("0000")}";
                        }));

                        Gauge.rpm = rpmData;
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

        private void AddPoint(Decimal latitude, Decimal longitude)
        {
            DotMarker point = new DotMarker(new PointLatLng((double)latitude, (double)longitude), System.Drawing.Color.Blue);
            DrawPoint.Markers.Add(point);
            DrawPoint.Control.Invalidate();
        }

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

    }
}

