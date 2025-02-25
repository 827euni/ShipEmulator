namespace ShipEmulator
{
    partial class ShipEmulatorView
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_Explanation_Sentence = new System.Windows.Forms.Label();
            this.Label_Explanation_Longitude = new System.Windows.Forms.Label();
            this.Label_Explanation_Latitude = new System.Windows.Forms.Label();
            this.Label_Text_Sentence = new System.Windows.Forms.Label();
            this.Label_Text_Latitude = new System.Windows.Forms.Label();
            this.Label_Text_Longitude = new System.Windows.Forms.Label();
            this.gMap_Main = new GMap.NET.WindowsForms.GMapControl();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Button_Start = new System.Windows.Forms.Button();
            this.Label_Text_RPM = new System.Windows.Forms.Label();
            this.Label_Text_PortRPM = new System.Windows.Forms.Label();
            this.Label_Explanation_PortRPM = new System.Windows.Forms.Label();
            this.Label_Text_PortGPS = new System.Windows.Forms.Label();
            this.Label_Explanation_PortGPS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Gauge = new ShipEmulator.rpmGauge();
            this.SuspendLayout();
            // 
            // Label_Explanation_Sentence
            // 
            this.Label_Explanation_Sentence.AutoSize = true;
            this.Label_Explanation_Sentence.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_Sentence.Location = new System.Drawing.Point(103, 71);
            this.Label_Explanation_Sentence.Name = "Label_Explanation_Sentence";
            this.Label_Explanation_Sentence.Size = new System.Drawing.Size(114, 29);
            this.Label_Explanation_Sentence.TabIndex = 0;
            this.Label_Explanation_Sentence.Text = "Sentence";
            // 
            // Label_Explanation_Longitude
            // 
            this.Label_Explanation_Longitude.AutoSize = true;
            this.Label_Explanation_Longitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_Longitude.Location = new System.Drawing.Point(663, 116);
            this.Label_Explanation_Longitude.Name = "Label_Explanation_Longitude";
            this.Label_Explanation_Longitude.Size = new System.Drawing.Size(55, 29);
            this.Label_Explanation_Longitude.TabIndex = 1;
            this.Label_Explanation_Longitude.Text = "경도";
            // 
            // Label_Explanation_Latitude
            // 
            this.Label_Explanation_Latitude.AutoSize = true;
            this.Label_Explanation_Latitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_Latitude.Location = new System.Drawing.Point(391, 116);
            this.Label_Explanation_Latitude.Name = "Label_Explanation_Latitude";
            this.Label_Explanation_Latitude.Size = new System.Drawing.Size(55, 29);
            this.Label_Explanation_Latitude.TabIndex = 2;
            this.Label_Explanation_Latitude.Text = "위도";
            // 
            // Label_Text_Sentence
            // 
            this.Label_Text_Sentence.AutoSize = true;
            this.Label_Text_Sentence.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_Sentence.Location = new System.Drawing.Point(223, 75);
            this.Label_Text_Sentence.Name = "Label_Text_Sentence";
            this.Label_Text_Sentence.Size = new System.Drawing.Size(908, 29);
            this.Label_Text_Sentence.TabIndex = 3;
            this.Label_Text_Sentence.Text = "$GPGGA,114455.532,3735.0079,N,12701.6446,E,1,03,7.9,48.8,M,19.6,M,0.0,0000*23";
            // 
            // Label_Text_Latitude
            // 
            this.Label_Text_Latitude.AutoSize = true;
            this.Label_Text_Latitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_Latitude.Location = new System.Drawing.Point(465, 116);
            this.Label_Text_Latitude.Name = "Label_Text_Latitude";
            this.Label_Text_Latitude.Size = new System.Drawing.Size(153, 29);
            this.Label_Text_Latitude.TabIndex = 4;
            this.Label_Text_Latitude.Text = "37.232866도";
            // 
            // Label_Text_Longitude
            // 
            this.Label_Text_Longitude.AutoSize = true;
            this.Label_Text_Longitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_Longitude.Location = new System.Drawing.Point(724, 116);
            this.Label_Text_Longitude.Name = "Label_Text_Longitude";
            this.Label_Text_Longitude.Size = new System.Drawing.Size(162, 29);
            this.Label_Text_Longitude.TabIndex = 5;
            this.Label_Text_Longitude.Text = "131.865452도";
            // 
            // gMap_Main
            // 
            this.gMap_Main.Bearing = 0F;
            this.gMap_Main.CanDragMap = true;
            this.gMap_Main.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap_Main.GrayScaleMode = false;
            this.gMap_Main.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap_Main.LevelsKeepInMemory = 5;
            this.gMap_Main.Location = new System.Drawing.Point(108, 170);
            this.gMap_Main.MarkersEnabled = true;
            this.gMap_Main.MaxZoom = 2;
            this.gMap_Main.MinZoom = 2;
            this.gMap_Main.MouseWheelZoomEnabled = true;
            this.gMap_Main.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap_Main.Name = "gMap_Main";
            this.gMap_Main.NegativeMode = false;
            this.gMap_Main.PolygonsEnabled = true;
            this.gMap_Main.RetryLoadTile = 0;
            this.gMap_Main.RoutesEnabled = true;
            this.gMap_Main.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap_Main.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap_Main.ShowTileGridLines = false;
            this.gMap_Main.Size = new System.Drawing.Size(1024, 371);
            this.gMap_Main.TabIndex = 6;
            this.gMap_Main.Zoom = 0D;
            // 
            // Button_Stop
            // 
            this.Button_Stop.Font = new System.Drawing.Font("Pretendard Variable ExtraBold", 16F, System.Drawing.FontStyle.Bold);
            this.Button_Stop.Location = new System.Drawing.Point(1124, 12);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(118, 45);
            this.Button_Stop.TabIndex = 9;
            this.Button_Stop.Text = "수신 종료";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Button_Start
            // 
            this.Button_Start.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_Start.Font = new System.Drawing.Font("Pretendard Variable ExtraBold", 16F, System.Drawing.FontStyle.Bold);
            this.Button_Start.Location = new System.Drawing.Point(1000, 12);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(118, 45);
            this.Button_Start.TabIndex = 8;
            this.Button_Start.Text = "수신 시작";
            this.Button_Start.UseVisualStyleBackColor = false;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // Label_Text_RPM
            // 
            this.Label_Text_RPM.AutoSize = true;
            this.Label_Text_RPM.Font = new System.Drawing.Font("Pretendard Variable Black", 70F, System.Drawing.FontStyle.Bold);
            this.Label_Text_RPM.Location = new System.Drawing.Point(494, 673);
            this.Label_Text_RPM.Name = "Label_Text_RPM";
            this.Label_Text_RPM.Size = new System.Drawing.Size(257, 113);
            this.Label_Text_RPM.TabIndex = 10;
            this.Label_Text_RPM.Text = "1224";
            this.Label_Text_RPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Text_PortRPM
            // 
            this.Label_Text_PortRPM.AutoSize = true;
            this.Label_Text_PortRPM.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_PortRPM.Location = new System.Drawing.Point(641, 824);
            this.Label_Text_PortRPM.Name = "Label_Text_PortRPM";
            this.Label_Text_PortRPM.Size = new System.Drawing.Size(71, 29);
            this.Label_Text_PortRPM.TabIndex = 16;
            this.Label_Text_PortRPM.Tag = "  ";
            this.Label_Text_PortRPM.Text = "2424";
            this.Label_Text_PortRPM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Explanation_PortRPM
            // 
            this.Label_Explanation_PortRPM.AutoSize = true;
            this.Label_Explanation_PortRPM.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_PortRPM.Location = new System.Drawing.Point(518, 824);
            this.Label_Explanation_PortRPM.Name = "Label_Explanation_PortRPM";
            this.Label_Explanation_PortRPM.Size = new System.Drawing.Size(105, 29);
            this.Label_Explanation_PortRPM.TabIndex = 15;
            this.Label_Explanation_PortRPM.Text = "RPM포트";
            this.Label_Explanation_PortRPM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Text_PortGPS
            // 
            this.Label_Text_PortGPS.AutoSize = true;
            this.Label_Text_PortGPS.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_PortGPS.Location = new System.Drawing.Point(641, 786);
            this.Label_Text_PortGPS.Name = "Label_Text_PortGPS";
            this.Label_Text_PortGPS.Size = new System.Drawing.Size(71, 29);
            this.Label_Text_PortGPS.TabIndex = 14;
            this.Label_Text_PortGPS.Text = "2323";
            this.Label_Text_PortGPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Explanation_PortGPS
            // 
            this.Label_Explanation_PortGPS.AutoSize = true;
            this.Label_Explanation_PortGPS.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_PortGPS.Location = new System.Drawing.Point(518, 786);
            this.Label_Explanation_PortGPS.Name = "Label_Explanation_PortGPS";
            this.Label_Explanation_PortGPS.Size = new System.Drawing.Size(102, 29);
            this.Label_Explanation_PortGPS.TabIndex = 13;
            this.Label_Explanation_PortGPS.Text = "GPS포트";
            this.Label_Explanation_PortGPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pretendard Variable", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(461, 853);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "500";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Pretendard Variable", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(709, 853);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "1500";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Gauge
            // 
            this.Gauge.Location = new System.Drawing.Point(419, 569);
            this.Gauge.Name = "Gauge";
            this.Gauge.Size = new System.Drawing.Size(416, 322);
            this.Gauge.TabIndex = 11;
            // 
            // ShipEmulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 903);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label_Text_PortRPM);
            this.Controls.Add(this.Label_Explanation_PortRPM);
            this.Controls.Add(this.Label_Text_PortGPS);
            this.Controls.Add(this.Label_Explanation_PortGPS);
            this.Controls.Add(this.Label_Text_RPM);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.Button_Start);
            this.Controls.Add(this.gMap_Main);
            this.Controls.Add(this.Label_Text_Longitude);
            this.Controls.Add(this.Label_Text_Latitude);
            this.Controls.Add(this.Label_Text_Sentence);
            this.Controls.Add(this.Label_Explanation_Latitude);
            this.Controls.Add(this.Label_Explanation_Longitude);
            this.Controls.Add(this.Label_Explanation_Sentence);
            this.Controls.Add(this.Gauge);
            this.Name = "ShipEmulatorView";
            this.Text = "CENTER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShipEmulatorView_FormClosing);
            this.Load += new System.EventHandler(this.ShipEmulatorView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Explanation_Sentence;
        private System.Windows.Forms.Label Label_Explanation_Longitude;
        private System.Windows.Forms.Label Label_Explanation_Latitude;
        private System.Windows.Forms.Label Label_Text_Sentence;
        private System.Windows.Forms.Label Label_Text_Latitude;
        private System.Windows.Forms.Label Label_Text_Longitude;
        private GMap.NET.WindowsForms.GMapControl gMap_Main;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.Label Label_Text_RPM;
        private rpmGauge Gauge;
        private System.Windows.Forms.Label Label_Text_PortRPM;
        private System.Windows.Forms.Label Label_Explanation_PortRPM;
        private System.Windows.Forms.Label Label_Text_PortGPS;
        private System.Windows.Forms.Label Label_Explanation_PortGPS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

