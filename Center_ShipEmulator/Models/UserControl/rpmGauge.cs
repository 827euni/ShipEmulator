using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ShipEmulator.Models;

namespace ShipEmulator
{
    public partial class rpmGauge : UserControl
    {

        int mMinRPM = 500;
        int mMaxRPM = 1500;
        public int rpm;


        public rpmGauge()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        // GDI+를 사용하여 게이지를 랜더링하는 함수 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(SystemColors.Control);
            int total = 50;
            float start = 155;
            float final = 235;

            int active = (int)((rpm - mMinRPM) / (float)(mMaxRPM - mMinRPM) * total);

            for (int i = 0; i < total; i++)
            {
                float angle = start + (i * (final / total));
                DrawSegment(g, 200, 200, 175, angle, Color.DarkGray, 4);
            }

            for (int i = 0; i < active; i++)
            {
                float angle = start + (i * (final / total));
                DrawSegment(g, 200, 200, 175, angle, Color.Blue, 4);
                Invalidate();
            }
        }

        // 게이지의 직선 부분을 그려내는 함수 

        private void DrawSegment(Graphics g, int x, int y, int r, float ang, Color color, int size)
        {
            double radian = ang * Math.PI / 180;
            int startX = x + (int)((r) * Math.Cos(radian));
            int endX = x + (int)((r + 20) * Math.Cos(radian));
            int startY = y + (int)((r) * Math.Sin(radian));
            int endY = y + (int)((r + 20) * Math.Sin(radian));

            using (Pen pen = new Pen(color, size))
            {
                g.DrawLine(pen, startX, startY, endX, endY);
            }
        }
    }
}
