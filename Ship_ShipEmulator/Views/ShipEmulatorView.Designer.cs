namespace Ship_ShipEmulator
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
            this.Button_Start = new System.Windows.Forms.Button();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Label_Change_GPS = new System.Windows.Forms.Label();
            this.Label_Text_PortGPS = new System.Windows.Forms.Label();
            this.Label_Explanation_PortGPS = new System.Windows.Forms.Label();
            this.Label_Text_PortRPM = new System.Windows.Forms.Label();
            this.Label_Explanation_PortRPM = new System.Windows.Forms.Label();
            this.Label_Text_HZ = new System.Windows.Forms.Label();
            this.Label_Explanation_HZ = new System.Windows.Forms.Label();
            this.Label_Change_RPM = new System.Windows.Forms.Label();
            this.Label_Change_HZ = new System.Windows.Forms.Label();
            this.TextBox_Change_portGPS = new System.Windows.Forms.TextBox();
            this.TextBox_Change_portRPM = new System.Windows.Forms.TextBox();
            this.TextBox_Change_HZ = new System.Windows.Forms.TextBox();
            this.Button_Change_PortGPS = new System.Windows.Forms.Button();
            this.Button_Change_PortRPM = new System.Windows.Forms.Button();
            this.Button_Change_HZ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_Start
            // 
            this.Button_Start.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_Start.Font = new System.Drawing.Font("Pretendard Variable ExtraBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_Start.Location = new System.Drawing.Point(164, 144);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(235, 89);
            this.Button_Start.TabIndex = 0;
            this.Button_Start.Text = "송신 시작";
            this.Button_Start.UseVisualStyleBackColor = false;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // Button_Stop
            // 
            this.Button_Stop.Font = new System.Drawing.Font("Pretendard Variable ExtraBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_Stop.Location = new System.Drawing.Point(430, 144);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(235, 89);
            this.Button_Stop.TabIndex = 1;
            this.Button_Stop.Text = "송신 종료";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Label_Change_GPS
            // 
            this.Label_Change_GPS.AutoSize = true;
            this.Label_Change_GPS.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Change_GPS.Location = new System.Drawing.Point(175, 287);
            this.Label_Change_GPS.Name = "Label_Change_GPS";
            this.Label_Change_GPS.Size = new System.Drawing.Size(102, 29);
            this.Label_Change_GPS.TabIndex = 5;
            this.Label_Change_GPS.Text = "GPS포트";
            // 
            // Label_Text_PortGPS
            // 
            this.Label_Text_PortGPS.AutoSize = true;
            this.Label_Text_PortGPS.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_PortGPS.Location = new System.Drawing.Point(722, 9);
            this.Label_Text_PortGPS.Name = "Label_Text_PortGPS";
            this.Label_Text_PortGPS.Size = new System.Drawing.Size(71, 29);
            this.Label_Text_PortGPS.TabIndex = 8;
            this.Label_Text_PortGPS.Text = "2323";
            // 
            // Label_Explanation_PortGPS
            // 
            this.Label_Explanation_PortGPS.AutoSize = true;
            this.Label_Explanation_PortGPS.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_PortGPS.Location = new System.Drawing.Point(599, 9);
            this.Label_Explanation_PortGPS.Name = "Label_Explanation_PortGPS";
            this.Label_Explanation_PortGPS.Size = new System.Drawing.Size(102, 29);
            this.Label_Explanation_PortGPS.TabIndex = 7;
            this.Label_Explanation_PortGPS.Text = "GPS포트";
            this.Label_Explanation_PortGPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Text_PortRPM
            // 
            this.Label_Text_PortRPM.AutoSize = true;
            this.Label_Text_PortRPM.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_PortRPM.Location = new System.Drawing.Point(722, 47);
            this.Label_Text_PortRPM.Name = "Label_Text_PortRPM";
            this.Label_Text_PortRPM.Size = new System.Drawing.Size(71, 29);
            this.Label_Text_PortRPM.TabIndex = 10;
            this.Label_Text_PortRPM.Text = "2424";
            // 
            // Label_Explanation_PortRPM
            // 
            this.Label_Explanation_PortRPM.AutoSize = true;
            this.Label_Explanation_PortRPM.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_PortRPM.Location = new System.Drawing.Point(599, 47);
            this.Label_Explanation_PortRPM.Name = "Label_Explanation_PortRPM";
            this.Label_Explanation_PortRPM.Size = new System.Drawing.Size(105, 29);
            this.Label_Explanation_PortRPM.TabIndex = 9;
            this.Label_Explanation_PortRPM.Text = "RMS포트";
            this.Label_Explanation_PortRPM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Text_HZ
            // 
            this.Label_Text_HZ.AutoSize = true;
            this.Label_Text_HZ.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Text_HZ.Location = new System.Drawing.Point(751, 86);
            this.Label_Text_HZ.Name = "Label_Text_HZ";
            this.Label_Text_HZ.Size = new System.Drawing.Size(38, 29);
            this.Label_Text_HZ.TabIndex = 12;
            this.Label_Text_HZ.Text = "10";
            // 
            // Label_Explanation_HZ
            // 
            this.Label_Explanation_HZ.AutoSize = true;
            this.Label_Explanation_HZ.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Explanation_HZ.Location = new System.Drawing.Point(586, 86);
            this.Label_Explanation_HZ.Name = "Label_Explanation_HZ";
            this.Label_Explanation_HZ.Size = new System.Drawing.Size(118, 29);
            this.Label_Explanation_HZ.TabIndex = 11;
            this.Label_Explanation_HZ.Text = "전송주파수";
            this.Label_Explanation_HZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label_Change_RPM
            // 
            this.Label_Change_RPM.AutoSize = true;
            this.Label_Change_RPM.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Change_RPM.Location = new System.Drawing.Point(172, 334);
            this.Label_Change_RPM.Name = "Label_Change_RPM";
            this.Label_Change_RPM.Size = new System.Drawing.Size(105, 29);
            this.Label_Change_RPM.TabIndex = 13;
            this.Label_Change_RPM.Text = "RPM포트";
            // 
            // Label_Change_HZ
            // 
            this.Label_Change_HZ.AutoSize = true;
            this.Label_Change_HZ.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Change_HZ.Location = new System.Drawing.Point(159, 382);
            this.Label_Change_HZ.Name = "Label_Change_HZ";
            this.Label_Change_HZ.Size = new System.Drawing.Size(118, 29);
            this.Label_Change_HZ.TabIndex = 14;
            this.Label_Change_HZ.Text = "전송주파수";
            // 
            // TextBox_Change_portGPS
            // 
            this.TextBox_Change_portGPS.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextBox_Change_portGPS.Location = new System.Drawing.Point(307, 287);
            this.TextBox_Change_portGPS.Name = "TextBox_Change_portGPS";
            this.TextBox_Change_portGPS.Size = new System.Drawing.Size(219, 36);
            this.TextBox_Change_portGPS.TabIndex = 20;
            this.TextBox_Change_portGPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextBox_Change_portRPM
            // 
            this.TextBox_Change_portRPM.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextBox_Change_portRPM.Location = new System.Drawing.Point(307, 334);
            this.TextBox_Change_portRPM.Name = "TextBox_Change_portRPM";
            this.TextBox_Change_portRPM.Size = new System.Drawing.Size(219, 36);
            this.TextBox_Change_portRPM.TabIndex = 21;
            this.TextBox_Change_portRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextBox_Change_HZ
            // 
            this.TextBox_Change_HZ.Font = new System.Drawing.Font("Pretendard Variable", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextBox_Change_HZ.Location = new System.Drawing.Point(307, 375);
            this.TextBox_Change_HZ.Name = "TextBox_Change_HZ";
            this.TextBox_Change_HZ.Size = new System.Drawing.Size(219, 36);
            this.TextBox_Change_HZ.TabIndex = 22;
            this.TextBox_Change_HZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Button_Change_PortGPS
            // 
            this.Button_Change_PortGPS.Font = new System.Drawing.Font("Pretendard Variable SemiBold", 14F, System.Drawing.FontStyle.Bold);
            this.Button_Change_PortGPS.Location = new System.Drawing.Point(541, 287);
            this.Button_Change_PortGPS.Name = "Button_Change_PortGPS";
            this.Button_Change_PortGPS.Size = new System.Drawing.Size(75, 36);
            this.Button_Change_PortGPS.TabIndex = 23;
            this.Button_Change_PortGPS.Text = "변경";
            this.Button_Change_PortGPS.UseVisualStyleBackColor = true;
            this.Button_Change_PortGPS.Click += new System.EventHandler(this.Button_Change_PortGPS_Click);
            // 
            // Button_Change_PortRPM
            // 
            this.Button_Change_PortRPM.Font = new System.Drawing.Font("Pretendard Variable SemiBold", 14F, System.Drawing.FontStyle.Bold);
            this.Button_Change_PortRPM.Location = new System.Drawing.Point(541, 334);
            this.Button_Change_PortRPM.Name = "Button_Change_PortRPM";
            this.Button_Change_PortRPM.Size = new System.Drawing.Size(75, 36);
            this.Button_Change_PortRPM.TabIndex = 24;
            this.Button_Change_PortRPM.Text = "변경";
            this.Button_Change_PortRPM.UseVisualStyleBackColor = true;
            this.Button_Change_PortRPM.Click += new System.EventHandler(this.Button_Change_PortRPM_Click);
            // 
            // Button_Change_HZ
            // 
            this.Button_Change_HZ.Font = new System.Drawing.Font("Pretendard Variable SemiBold", 14F, System.Drawing.FontStyle.Bold);
            this.Button_Change_HZ.Location = new System.Drawing.Point(541, 375);
            this.Button_Change_HZ.Name = "Button_Change_HZ";
            this.Button_Change_HZ.Size = new System.Drawing.Size(75, 36);
            this.Button_Change_HZ.TabIndex = 25;
            this.Button_Change_HZ.Text = "변경";
            this.Button_Change_HZ.UseVisualStyleBackColor = true;
            this.Button_Change_HZ.Click += new System.EventHandler(this.Button_Change_HZ_Click);
            // 
            // ShipEmulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Button_Change_HZ);
            this.Controls.Add(this.Button_Change_PortRPM);
            this.Controls.Add(this.Button_Change_PortGPS);
            this.Controls.Add(this.TextBox_Change_HZ);
            this.Controls.Add(this.TextBox_Change_portRPM);
            this.Controls.Add(this.TextBox_Change_portGPS);
            this.Controls.Add(this.Label_Change_HZ);
            this.Controls.Add(this.Label_Change_RPM);
            this.Controls.Add(this.Label_Text_HZ);
            this.Controls.Add(this.Label_Explanation_HZ);
            this.Controls.Add(this.Label_Text_PortRPM);
            this.Controls.Add(this.Label_Explanation_PortRPM);
            this.Controls.Add(this.Label_Text_PortGPS);
            this.Controls.Add(this.Label_Explanation_PortGPS);
            this.Controls.Add(this.Label_Change_GPS);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.Button_Start);
            this.Name = "ShipEmulatorView";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Label Label_Change_GPS;
        private System.Windows.Forms.Label Label_Text_PortGPS;
        private System.Windows.Forms.Label Label_Explanation_PortGPS;
        private System.Windows.Forms.Label Label_Text_PortRPM;
        private System.Windows.Forms.Label Label_Explanation_PortRPM;
        private System.Windows.Forms.Label Label_Text_HZ;
        private System.Windows.Forms.Label Label_Explanation_HZ;
        private System.Windows.Forms.Label Label_Change_RPM;
        private System.Windows.Forms.Label Label_Change_HZ;
        private System.Windows.Forms.TextBox TextBox_Change_portGPS;
        private System.Windows.Forms.TextBox TextBox_Change_portRPM;
        private System.Windows.Forms.TextBox TextBox_Change_HZ;
        private System.Windows.Forms.Button Button_Change_PortGPS;
        private System.Windows.Forms.Button Button_Change_PortRPM;
        private System.Windows.Forms.Button Button_Change_HZ;
    }
}

