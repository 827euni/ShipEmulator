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
using ShipEmulator.Models;

namespace ShipEmulator
{
    public partial class ShipEmulatorView : Form
    {
        private UdpClient mGpsUDPClient;
        private UdpClient mRpmUDLClient;
        private Thread mThread_Gps;
        private Thread mThread_Rpm;
        private bool mIsRunning = false;
        private int mGpsPort = 2323;
        private int mRpmPort = 2424;

        DatabaseHelper mDatabaseHelper = new DatabaseHelper();

        public ShipEmulatorView()
        {
            InitializeComponent();
        }

        private void ShipEmulatorView_Load(object sender, EventArgs e)
        {
            // 구글 맵을 불러와서 화면에 로드하는 함수
            gMap_Main.MapProvider = GMapProviders.GoogleMap;
            gMap_Main.Position = new PointLatLng(37.2328660, 131.8654529);
            gMap_Main.MinZoom = 10;
            gMap_Main.MaxZoom = 30;
            gMap_Main.Zoom = 13;
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!mIsRunning)
            {
                mIsRunning = true;
                mGpsUDPClient = new UdpClient(mGpsPort);
                mRpmUDLClient = new UdpClient(mRpmPort);

                mThread_Gps = new Thread(GetGpsData);
                mThread_Rpm = new Thread(GetRpmData);

                mThread_Gps.IsBackground = true;
                mThread_Rpm.IsBackground = true;

                mThread_Gps.Start();
                mThread_Rpm.Start();

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

        private void GetGpsData()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, mGpsPort);
            byte[] getBytes;
            string gpsData;
            try
            {
                while (mIsRunning)
                {
                    getBytes = mGpsUDPClient.Receive(ref point);
                    gpsData = Encoding.UTF8.GetString(getBytes);


                    GPS gps = new GPS()
                    {
                        GPS_TIME = ChangeDateTime(gpsData.Split(',')[1]),
                        GPS_Latitude = ChangeGPSLoacation(gpsData.Split(',')[2], gpsData.Split(',')[3]),
                        GPS_Longitude = ChangeGPSLoacation(gpsData.Split(',')[4], gpsData.Split(',')[5])
                    };

                    mDatabaseHelper.AddGPS(gps);

                    Invoke(new Action(() =>
                    {
                        Label_Text_Sentence.Text = $"{gpsData}";
                        Label_Text_Latitude.Text = $"{ChangeGPSLoacation(gpsData.Split(',')[2], gpsData.Split(',')[3]).ToString("F6")}도";
                        Label_Text_Longitude.Text = $"{ChangeGPSLoacation(gpsData.Split(',')[4], gpsData.Split(',')[5]).ToString("F6")}도";
                    }));
                }
            }
            finally
            {
                mGpsUDPClient.Close();
            }
        }

        private void GetRpmData()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, mRpmPort);
            byte[] getBytes;
            int rpmData;

            try
            {

                while (mIsRunning)
                {
                    getBytes = mRpmUDLClient.Receive(ref point);
                    rpmData = BitConverter.ToInt32(getBytes, 0);

                    mDatabaseHelper.AddRPM(rpmData);

                    Invoke(new Action(() =>
                    {
                        Label_Text_RPM.Text = $"{rpmData}";
                    }));
                }
            }
            finally
            {
                mRpmUDLClient.Close();
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

        public void AddPoint(GMapControl gmap, double lat, double lng)
        {

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

