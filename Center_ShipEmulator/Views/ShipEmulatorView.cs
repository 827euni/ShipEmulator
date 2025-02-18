using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;

namespace ShipEmulator
{
    public partial class ShipEmulatorView : Form
    {
        public ShipEmulatorView()
        {
            InitializeComponent();
        }

        private void ShipEmulatorView_Load(object sender, EventArgs e)
        {
            // 구글 맵을 불러와서 화면에 로드하는 함수
            gMap_Main.MapProvider = GMapProviders.GoogleMap;
            gMap_Main.Position = new PointLatLng(37.481063, 126.879302);
            gMap_Main.MinZoom = 10;
            gMap_Main.MaxZoom = 30;
            gMap_Main.Zoom = 18;
        }
       
    }
}
