﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ship_ShipEmulator
{
    public partial class ShipEmulatorView : Form
    {
        private UdpClient mGpsUDPClient;
        private UdpClient mRpmUDLClient;
        private Thread mThread_Send;
        private Random random = new Random();
        private bool mIsRunning = false;
        private bool mIsIncreasing;
        private int mGpsPort = 2323;
        private int mRpmPort = 2424;
        private int mHz = 10;
        private int mRpm;
        private double mLatitude = 37.2328660;
        private double mLongitude = 131.8654529;
        public ShipEmulatorView()
        {
            InitializeComponent();
            
        }


        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!mIsRunning) 
            {
                mRpm = random.Next(500, 1500);
                int time = 1000 / mHz; // 1hz는 1초에 몇 번 진동하는지 여부.
                mGpsUDPClient = new UdpClient();
                mRpmUDLClient = new UdpClient();
                mIsRunning = true;

                mThread_Send = new Thread(Send);
                mThread_Send.IsBackground = true;
                mThread_Send.Start();

                Button_Start.Enabled = false;
                Button_Stop.Enabled = true;
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            if (mIsRunning) 
            {
                mRpm = 0;
                mIsRunning = false;
                mThread_Send.Join();
                mGpsUDPClient.Close();
                mRpmUDLClient.Close();
                Button_Start.Enabled = true;
                Button_Stop.Enabled = false;
            }
        }

        private string AddGPS()
        {
            mLatitude += (random.NextDouble() - 0.5) * 0.03;
            mLongitude += (random.NextDouble() - 0.5) * 0.03;
            string time = DateTime.UtcNow.ToString("HHmmss.fff");

            int Degree_Latitude = (int)mLatitude;
            double Minute_Latitude = (mLatitude - Degree_Latitude) * 60;
            string NMEA_Latitude = $"{Degree_Latitude}{Minute_Latitude:00.0000},N";

            int Degree_Longitude = (int)mLongitude;
            double Minute_Longitude = (mLongitude - Degree_Longitude) * 60;
            string NMEA_Longitude = $"{Degree_Longitude}{Minute_Longitude:00.0000},E";

            return $"$GPGGA,{time},{NMEA_Latitude},{NMEA_Longitude},1,03,23,23,M,23,M,0.0,0000*23";
        }



        private int AddRPM()
        {

            if (mIsIncreasing)
            {
                mRpm += random.Next(0, 20);
                if (mRpm >= 1490)
                {
                    mIsIncreasing = false;
                }
            }
            else
            {
                mRpm -= random.Next(0, 20);
                if (mRpm <= 510)
                {
                    mIsIncreasing = true;
                }
            }

            return mRpm;
        }

        private void Send()
        {
            string Gps;
            int Rpm;
            int time;
            byte[] GpsData;
            byte[] RpmData;


            while (mIsRunning)
            {
                time = 1000 / mHz;
                Gps = AddGPS();
                Rpm = AddRPM();

                GpsData = Encoding.UTF8.GetBytes(Gps);
                RpmData = BitConverter.GetBytes(Rpm); // int값을 보내려면 이렇게 보내야함. 이후 수신에서 BitConverter.ToInt32(receivedBytes, 0); 로 받을 수 있음 

                mGpsUDPClient.Send(GpsData, GpsData.Length, "127.0.0.1", mGpsPort);
                mRpmUDLClient.Send(RpmData, RpmData.Length, "127.0.0.1", mRpmPort);

                //Console.WriteLine(Gps);
                //Console.WriteLine(mGpsPort);
                //Console.WriteLine(Rpm);
                //Console.WriteLine(mRpmPort);
                //Console.WriteLine(mHz);
                //Console.WriteLine(time);
                Thread.Sleep(time);
            }
        }

        private void Button_Change_PortGPS_Click(object sender, EventArgs e)
        {
            if(TextBox_Change_portGPS.Text != "")
            {
                mGpsPort = int.Parse(TextBox_Change_portGPS.Text);
                Label_Text_PortGPS.Text = mGpsPort.ToString();
            }
        }

        private void Button_Change_PortRPM_Click(object sender, EventArgs e)
        {
            if (TextBox_Change_portRPM.Text != "")
            {
                mRpmPort = int.Parse(TextBox_Change_portRPM.Text);
                Label_Text_PortRPM.Text = mRpmPort.ToString();
            }
        }

        private void Button_Change_HZ_Click(object sender, EventArgs e)
        {
            if (TextBox_Change_HZ.Text != "")
            {
                mHz = int.Parse(TextBox_Change_HZ.Text);
                Label_Text_HZ.Text = mHz.ToString();
            }
        }

    }
}
