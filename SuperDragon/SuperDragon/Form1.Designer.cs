namespace SuperDragon
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.NAME = new System.Windows.Forms.Label();
            this.TIME = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(137, 119);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "000.0 SEC";
            // 
            // NAME
            // 
            this.NAME.AutoSize = true;
            this.NAME.BackColor = System.Drawing.Color.Transparent;
            this.NAME.Font = new System.Drawing.Font("落落の汤圆体", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAME.ForeColor = System.Drawing.Color.Ivory;
            this.NAME.Location = new System.Drawing.Point(693, 229);
            this.NAME.Name = "NAME";
            this.NAME.Size = new System.Drawing.Size(383, 37);
            this.NAME.TabIndex = 1;
            this.NAME.Text = "  排名(Rank)    名字(Name)\r\n";
            this.NAME.Visible = false;
            // 
            // TIME
            // 
            this.TIME.AutoSize = true;
            this.TIME.BackColor = System.Drawing.Color.Transparent;
            this.TIME.Font = new System.Drawing.Font("落落の汤圆体", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TIME.ForeColor = System.Drawing.Color.Ivory;
            this.TIME.Location = new System.Drawing.Point(1125, 229);
            this.TIME.Name = "TIME";
            this.TIME.Size = new System.Drawing.Size(230, 37);
            this.TIME.TabIndex = 2;
            this.TIME.Text = "挑戰時間(Time)\r\n";
            this.TIME.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 788);
            this.Controls.Add(this.TIME);
            this.Controls.Add(this.NAME);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NAME;
        private System.Windows.Forms.Label TIME;
    }
}

