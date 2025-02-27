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
        private string mGPGGA;
        private double mLatitude = 37.2328660;
        private double mLongitude = 131.8654529;
        private System.Windows.Forms.Timer mTimer_UI;

        public ShipEmulatorView()
        {
            InitializeComponent();
            mTimer_UI = new System.Windows.Forms.Timer();
            mTimer_UI.Interval = 100;
            mTimer_UI.Tick += Timer_UI;
        }

        // 송신시작 버튼을 클릭하였을 때 불러오는 함수 
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
                mTimer_UI.Start();

                Button_Start.Enabled = false;
                Button_Stop.Enabled = true;

            }
        }

        // 송신 종료 버튼을 클릭하였을 때 불러오는 함수 
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            if (mIsRunning) 
            {
                mRpm = 0;
                mIsRunning = false;
                mThread_Send.Join();
                mGpsUDPClient.Close();
                mRpmUDLClient.Close();

                mTimer_UI.Stop();
                Button_Start.Enabled = true;
                Button_Stop.Enabled = false;

            }
        }

        // 선박 애뮬레이터로서 GPS 정보를 NMEA-0183 GPGGA 형식에 맞춰 랜덤 발생 시키는 함수 
        private string AddGPS()
        {
            // 선박이 움직이는 것처럼 어느 정도 범위를 제한하여 난수 발생
            mLatitude += (random.NextDouble() - 0.5) * mRpm * 0.00001;
            mLongitude += (random.NextDouble() - 0.5) * mRpm * 0.00001;
            string time = DateTime.UtcNow.ToString("HHmmss.fff");

            int Degree_Latitude = (int)mLatitude;
            double Minute_Latitude = (mLatitude - Degree_Latitude) * 60;
            string NMEA_Latitude = $"{Degree_Latitude}{Minute_Latitude:00.0000},N";

            int Degree_Longitude = (int)mLongitude;
            double Minute_Longitude = (mLongitude - Degree_Longitude) * 60;
            string NMEA_Longitude = $"{Degree_Longitude}{Minute_Longitude:00.0000},E";

            mGPGGA = $"$GPGGA,{time},{NMEA_Latitude},{NMEA_Longitude},1,03,23,23,M,23,M,0.0,0000*23";
            return mGPGGA;
        }


        // 선박 애뮬레이터로서 RPM을 MIN-MAX 사이를 이동하는 랜덤 수 발생시키는 함수 
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

        // 만든 데이터를 UDP Client에 태워 전송하는 함수 
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

        // GPS 포트 번호 변경 버튼 클릭시 실행되는 함수 
        private async void Button_Change_PortGPS_Click(object sender, EventArgs e)
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
                        await udpClient.SendAsync(GPSPort, GPSPort.Length, "127.0.0.1", 50505);
                    }
                }
            }
        }

        // RPM 포트 번호 변경 버튼 클릭시 실행되는 함수 
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

        // 전송주파수 변경 버튼 클릭시 실행되는 함수 
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

        private void Timer_UI(object sender, EventArgs e)
        {
            if (mIsRunning)
            {
                Label_Text_Sentence.Text = mGPGGA;
                Label_Text_RPM.Text = mRpm.ToString("0000");
                Label_Text_Latitude.Text = $"{ChangeGPSLoacation(mGPGGA.Split(',')[2], mGPGGA.Split(',')[3]).ToString("F6")}도";
                Label_Text_Longitude.Text = $"{ChangeGPSLoacation(mGPGGA.Split(',')[4], mGPGGA.Split(',')[5]).ToString("F6")}도";
            }
        }
    }
}
