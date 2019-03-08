namespace SuperDragon {
    partial class ResultForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Rank = new System.Windows.Forms.Label();
            this.btncls = new System.Windows.Forms.Button();
            this.Times = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Rank
            // 
            this.Rank.AutoSize = true;
            this.Rank.BackColor = System.Drawing.Color.Transparent;
            this.Rank.Font = new System.Drawing.Font("落落の汤圆体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rank.ForeColor = System.Drawing.Color.White;
            this.Rank.Location = new System.Drawing.Point(47, 88);
            this.Rank.Name = "Rank";
            this.Rank.Size = new System.Drawing.Size(222, 31);
            this.Rank.TabIndex = 1;
            this.Rank.Text = "  Rank           Name\r\n";
            // 
            // btncls
            // 
            this.btncls.BackColor = System.Drawing.Color.SlateGray;
            this.btncls.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncls.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btncls.Location = new System.Drawing.Point(184, 488);
            this.btncls.Name = "btncls";
            this.btncls.Size = new System.Drawing.Size(107, 35);
            this.btncls.TabIndex = 2;
            this.btncls.Text = "Close";
            this.btncls.UseVisualStyleBackColor = false;
            this.btncls.Click += new System.EventHandler(this.btncls_Click);
            // 
            // Times
            // 
            this.Times.AutoSize = true;
            this.Times.BackColor = System.Drawing.Color.Transparent;
            this.Times.Font = new System.Drawing.Font("落落の汤圆体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Times.ForeColor = System.Drawing.Color.White;
            this.Times.Location = new System.Drawing.Point(336, 88);
            this.Times.Name = "Times";
            this.Times.Size = new System.Drawing.Size(127, 31);
            this.Times.TabIndex = 3;
            this.Times.Text = "TIME(Sec)\r\n";
            // 
            // ResultForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(476, 537);
            this.ControlBox = false;
            this.Controls.Add(this.Times);
            this.Controls.Add(this.btncls);
            this.Controls.Add(this.Rank);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResultForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ResultForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ResultForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ResultForm_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ResultForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Rank;
        private System.Windows.Forms.Button btncls;
        private System.Windows.Forms.Label Times;
    }
}