using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace ShipEmulator.Models
{
    internal class DotMarker : GMapMarker
    {
        private System.Drawing.Brush brush;
        private System.Drawing.Pen pen;
        private int radius;
        private static List<Point> points = new List<Point>();

        public static GMapOverlay MarkerOverlay { get; set; }


        public DotMarker(PointLatLng p, System.Drawing.Color color) : base(p) //부모 생성자 호출 방법 
        {
            this.brush = new SolidBrush(color);
            this.pen = new System.Drawing.Pen(color);
            this.radius = 5;
        }

        public override void OnRender(Graphics g)
        {
            // 원의 시작 점 보정을 위해서 반지름의 절반 만큼을 빼서 보정함. 
            g.FillEllipse(brush, new Rectangle(LocalPosition.X - radius / 2, LocalPosition.Y - radius / 2, radius, radius));

            if (MarkerOverlay != null && this == MarkerOverlay.Markers.Last())
            {
                points.Add(new Point(LocalPosition.X, LocalPosition.Y));
            }

            if (points.Count >= 5)
            {
                int last = points.Count;

                    g.DrawLine(pen, points[last-5], points[last - 4]);
                    g.DrawLine(pen, points[last - 4], points[last - 3]);
                    g.DrawLine(pen, points[last - 3], points[last - 2]);
                    g.DrawLine(pen, points[last - 2], points[last-1]);
            }
        }
    }
}
