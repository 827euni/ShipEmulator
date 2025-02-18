using System;
using System.Collections.Generic;
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


    }
}
