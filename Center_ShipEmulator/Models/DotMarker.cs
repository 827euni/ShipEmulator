using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace ShipEmulator.Models
{
    internal class DotMarker : GMapMarker
    {
        private Brush brush;
        private int radius;

        public DotMarker(PointLatLng p, Color color) : base(p) //부모 생성자 호출 방법 
        {
            this.brush = new SolidBrush(color);
            this.radius = 5;
        }

        public override void OnRender(Graphics g)
        {
            g.FillEllipse(brush, new Rectangle(LocalPosition.X - (radius / 2),LocalPosition.Y - (radius / 2),radius, radius)); // 너비, 높이
            //점이 왼쪽 상단을 중심으로 그려지기 때문에 중앙으로 가져와야하고, 이는 radius의 절반만큼의 보정값을 가짐. 
        }
    }
}
