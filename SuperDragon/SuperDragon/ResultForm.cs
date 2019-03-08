using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperDragon {
    public partial class ResultForm : Form {
        List<Score> rank = new List<Score>();
        int mode;
        Button Background;

        internal ResultForm(List<Score> a,int b) {
            InitializeComponent();
            Background = new Button("rank" + b, 1, new Point(260, 255), 1.8f);
            rank = a;
            mode = b;
        }

        private void btncls_Click(object sender, EventArgs e) {
                this.Close();
        }

        private void ResultForm_Load(object sender, EventArgs e) {
            ShowRank();
            this.Cursor = new Cursor("icon\\mouse.ico");
        }

        public void ShowRank() {
            
            //排行榜的顯示筆數上限
            int count = 0;
            int i = 0;
            foreach (Score a in rank) {
                if (a.mode == mode) count++;
            }
            if (count > 10) count = 10;


            //印出排行榜
            foreach (Score a in rank) {
                if (a.mode == mode) {
                    if(count>0) {
                        Rank.Text += "\r\n" + " No. " + (i + 1).ToString("00") + "   " + $"{ a.name,-20}";
                        Times.Text += "\r\n  " + a.spand_time.ToString("000.0");
                    }
                    else break;
                    count--;
                    i++;
                }
            }
        }

        private void ResultForm_Paint(object sender, PaintEventArgs e) {
            Background.Paint(e.Graphics);
        }

        private void ResultForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor("icon\\mouse_click.ico");
        }

        private void ResultForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor("icon\\mouse.ico");
        }
    }
}
