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
            int total = 40;
            float start = 155;
            float final = 235;

            int active = (int)((rpm - mMinRPM) / (float)(mMaxRPM - mMinRPM) * total);

            for (int i = 0; i < total; i++)
            {
                float angle = start + (i * (final / total));
                DrawSegment(g, 300, 300, 175, angle, Color.DarkGray, 4);
            }

            for (int i = 0; i < active; i++)
            {
                float angle = start + (i * (final / total));
                DrawSegment(g, 300, 300, 175, angle, Color.Blue, 4);
                Invalidate();
            }
            DrawRPMLabels(g, 300, 300, 200, -120, +120); // 실제로 가장 위에 0을 놓았을 때 각도가 155부터 235가 아닌 -120도에서 +120도임
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

        // RPM 게이지에 0, 1/4, 1/3, 1/2, 3/4, 2/3, 1 지점에 레이블을 그려 현재 RPM이 어느 정도 위치에 있는지 확인 시켜주는 함수  
        private void DrawRPMLabels(Graphics g, int x, int y, int r, float start, float final)
        {
            Font font = new Font("Pretendard", 13); // 여기서부터 지금 변경해야함 
            Brush brush = Brushes.Black;

            float[] location = { 0f, 1f / 4, 1f / 3, 1f / 2, 2f / 3, 3f / 4, 1f };
            foreach (float fraction in location)
            {
                int rpmValue = (int)(mMinRPM + (mMaxRPM - mMinRPM) * fraction);
                float angle = start + (fraction * (final - start));

                double radian = (angle - 90) * Math.PI / 180;
                int X = x + (int)((r + 20) * Math.Cos(radian));
                int Y = y + (int)((r + 20) * Math.Sin(radian));

                string text = rpmValue.ToString();

                X -= 20;
                Y -= 10;

                g.DrawString(text, font, brush, X, Y);
            }
        }



    }
}
