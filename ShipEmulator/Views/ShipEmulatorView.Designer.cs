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
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_Explanation_Sentence
            // 
            this.Label_Explanation_Sentence.AutoSize = true;
            this.Label_Explanation_Sentence.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_Sentence.Location = new System.Drawing.Point(104, 27);
            this.Label_Explanation_Sentence.Name = "Label_Explanation_Sentence";
            this.Label_Explanation_Sentence.Size = new System.Drawing.Size(114, 29);
            this.Label_Explanation_Sentence.TabIndex = 0;
            this.Label_Explanation_Sentence.Text = "Sentence";
            // 
            // Label_Explanation_Longitude
            // 
            this.Label_Explanation_Longitude.AutoSize = true;
            this.Label_Explanation_Longitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_Longitude.Location = new System.Drawing.Point(654, 75);
            this.Label_Explanation_Longitude.Name = "Label_Explanation_Longitude";
            this.Label_Explanation_Longitude.Size = new System.Drawing.Size(55, 29);
            this.Label_Explanation_Longitude.TabIndex = 1;
            this.Label_Explanation_Longitude.Text = "경도";
            // 
            // Label_Explanation_Latitude
            // 
            this.Label_Explanation_Latitude.AutoSize = true;
            this.Label_Explanation_Latitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_Latitude.Location = new System.Drawing.Point(382, 75);
            this.Label_Explanation_Latitude.Name = "Label_Explanation_Latitude";
            this.Label_Explanation_Latitude.Size = new System.Drawing.Size(55, 29);
            this.Label_Explanation_Latitude.TabIndex = 2;
            this.Label_Explanation_Latitude.Text = "위도";
            // 
            // Label_Text_Sentence
            // 
            this.Label_Text_Sentence.AutoSize = true;
            this.Label_Text_Sentence.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_Sentence.Location = new System.Drawing.Point(224, 31);
            this.Label_Text_Sentence.Name = "Label_Text_Sentence";
            this.Label_Text_Sentence.Size = new System.Drawing.Size(909, 29);
            this.Label_Text_Sentence.TabIndex = 3;
            this.Label_Text_Sentence.Text = "$GPGGA,114455.532,3735.0079,N,12701.6446,E,1,03,7.9,48.8,M,19.6,M,0.0,0000*48";
            // 
            // Label_Text_Latitude
            // 
            this.Label_Text_Latitude.AutoSize = true;
            this.Label_Text_Latitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_Latitude.Location = new System.Drawing.Point(456, 75);
            this.Label_Text_Latitude.Name = "Label_Text_Latitude";
            this.Label_Text_Latitude.Size = new System.Drawing.Size(124, 29);
            this.Label_Text_Latitude.TabIndex = 4;
            this.Label_Text_Latitude.Text = "37.5664도";
            // 
            // Label_Text_Longitude
            // 
            this.Label_Text_Longitude.AutoSize = true;
            this.Label_Text_Longitude.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_Longitude.Location = new System.Drawing.Point(715, 75);
            this.Label_Text_Longitude.Name = "Label_Text_Longitude";
            this.Label_Text_Longitude.Size = new System.Drawing.Size(133, 29);
            this.Label_Text_Longitude.TabIndex = 5;
            this.Label_Text_Longitude.Text = "126.9777도";
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(109, 126);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1024, 332);
            this.gMapControl1.TabIndex = 6;
            this.gMapControl1.Zoom = 0D;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 475);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1024, 202);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ShipEmulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 689);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.Label_Text_Longitude);
            this.Controls.Add(this.Label_Text_Latitude);
            this.Controls.Add(this.Label_Text_Sentence);
            this.Controls.Add(this.Label_Explanation_Latitude);
            this.Controls.Add(this.Label_Explanation_Longitude);
            this.Controls.Add(this.Label_Explanation_Sentence);
            this.Name = "ShipEmulatorView";
            this.Text = "ShipEmulator";
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
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Button button1;
    }
}

