using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipEmulator.Models
{
    internal class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            connectionString = "Server=EA-PC\\SQLEXPRESS;Database=ShipEmulator;Integrated Security=True;"; // 회사 데스크탑/노트북
        }

        // 서버측에서 받은 GPS를 데이터베이스에 저장하는 함수
        public void AddGPS(GPS gps) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO GPS (GPS_TIME, GPS_Latitude, GPS_Longitude) VALUES (@GPS_TIME, @GPS_Latitude, @GPS_Longitude)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GPS_TIME", gps.GPS_TIME);
                command.Parameters.AddWithValue("@GPS_Latitude", gps.GPS_Latitude);
                command.Parameters.AddWithValue("@GPS_Longitude", gps.GPS_Longitude);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // 서버측에서 받은 RPM을 데이터베이스에 저장하는 함수 
        public void AddRPM(int rpm)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO RPM (RPM_TIME, RPM_RPM) VALUES (@RPM_TIME, @RPM_RPM)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RPM_TIME",DateTime.UtcNow);
                command.Parameters.AddWithValue("@RPM_RPM", rpm);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

