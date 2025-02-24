using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
                Button_Change_PortGPS.Enabled = true;
                Button_Change_PortRPM.Enabled = true;
                Button_Change_HZ.Enabled = true;
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
                Button_Change_PortGPS.Enabled = false;
                Button_Change_PortRPM.Enabled = false;
                Button_Change_HZ.Enabled = false;
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
                RpmData = BitConverter.GetBytes(Rpm);

                mGpsUDPClient.Send(GpsData, GpsData.Length, "127.0.0.1", mGpsPort);
                mRpmUDLClient.Send(RpmData, RpmData.Length, "127.0.0.1", mRpmPort);
                Thread.Sleep(time);
            }
        }

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
                        Label_Text_PortGPS.Text = mGpsPort.ToString();
                        using (UdpClient udpClient = new UdpClient())
                        {
                            byte[] GPSPort = BitConverter.GetBytes(mGpsPort);
                            udpClient.Send(GPSPort, GPSPort.Length, "127.0.0.1", 50505);
                        }
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
            if (TextBox_Change_portRPM.Text != "")
            {
                if(int.Parse(TextBox_Change_portRPM.Text) == 50505 || int.Parse(TextBox_Change_portRPM.Text) == 50506)
                {
                    MessageBox.Show("사용할 수 없는 포트 번호입니다.");
                }
                else if (int.Parse(TextBox_Change_portRPM.Text) == mGpsPort)
                {
                    MessageBox.Show("GPS포트와 RPM포트는 동일할 수 없습니다.");
                }
                else
                {
                    mRpmPort = int.Parse(TextBox_Change_portRPM.Text);
                    Label_Text_PortRPM.Text = mRpmPort.ToString();
                    using (UdpClient udpClient = new UdpClient())
                    {
                        byte[] RPMPort = BitConverter.GetBytes(mRpmPort);
                        udpClient.Send(RPMPort, RPMPort.Length, "127.0.0.1", 50506);
                    }
                }
            }
        }

        private void Button_Change_HZ_Click(object sender, EventArgs e)
        {
            if (TextBox_Change_HZ.Text != "")
            {

                if(int.Parse(TextBox_Change_HZ.Text) >= 1 && int.Parse(TextBox_Change_HZ.Text) <= 10)
                {
                    mHz = int.Parse(TextBox_Change_HZ.Text);
                    Label_Text_HZ.Text = mHz.ToString();
                }
                else
                {
                    MessageBox.Show("1부터 10까지의 값을 작성해주세요.");
                }
            }
        }

    }
}
