using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SuperDragon
{
    public partial class Form1 : Form 
    {
        Thread thread;
        public static Label labelx,labely,labelz;
        static MainpageStage MainPage;
        static TurnTableStage turntableStage;
        static Stage01 stage01;
        static Level01 level01;
        static Stage02 stage02;
        static Level02 level02;
        static Stage03 stage03;

        public Form1() {
            InitializeComponent();
            Size = new Size(1100, 768);
            labelx = this.label1;
            labely = this.NAME;
            labelz = this.TIME;
            MainPage = new MainpageStage();
            this.Cursor = new Cursor("icon\\mouse.ico");

            /************初始化( 測試時方便跳觀用 )************/
            //turntableStage = new TurnTableStage();
            //stage01 = new Stage01();
            //level01 = new Level01();
            //stage02 = new Stage02();
            //level02 = new Level02();
            //stage03 = new Stage03();
            /**************************************************/
        }

        private void Form1_Load(object sender, EventArgs e) {
            DoubleBuffered = true;
            thread = new Thread(RunAnimate);
            thread.Start();
            label1.Visible = false;
            this.KeyPress += nameTextbox_KeyPress;
        }
        int removeCount = 0;
        private void Form1_Paint(object sender, PaintEventArgs e) {
            switch (Global.page) {
                case pageName.mainPage :
                    MainPage.DrawImages(e.Graphics);
                    label1.Visible = false;
                    break;
                case pageName.turntablePage :
                    removeCount = 0;
                    turntableStage.Paint(e.Graphics);
                    this.Controls.Add(Music.track);
                    this.Controls.Add(turntableStage.nameTextbox);
                    this.Controls.Add(turntableStage.pageLabel);
                    this.Controls.Add(turntableStage.turnLabel);
                    break;
                case pageName.stage01 :
                    label1.Visible = false;
                    stage01.Paint(e.Graphics);
                    if(removeCount == 0) {
                        this.Controls.Remove(Music.track);
                        this.Controls.Remove(turntableStage.nameTextbox);
                        this.Controls.Remove(turntableStage.pageLabel);
                        this.Controls.Remove(turntableStage.turnLabel);
                        removeCount++;
                    }
                    if (!TurnPage.isGameOver) {
                        Global.setEggs(e.Graphics);
                        label1.Visible = true;
                    }
                    break;
                case pageName.level01 :
                    label1.Visible = false;
                    level01.Paint(e.Graphics);
                    if (!TurnPage.isGameOver) {
                        Global.setEggs(e.Graphics);
                        label1.Visible = true;
                    }
                    break;
                case pageName.stage02 :
                    label1.Visible = false;
                    stage02.Paint(e.Graphics);
                    if (removeCount == 0) {
                        this.Controls.Remove(Music.track);
                        this.Controls.Remove(turntableStage.nameTextbox);
                        this.Controls.Remove(turntableStage.pageLabel);
                        this.Controls.Remove(turntableStage.turnLabel);
                        removeCount++;
                    }
                    if (!TurnPage.isGameOver) {
                        Global.setEggs(e.Graphics);
                        label1.Visible = true;
                    }
                    break;
                case pageName.level02 :
                    label1.Visible = false;
                    level02.Paint(e.Graphics);
                    if (!TurnPage.isGameOver) {
                        Global.setEggs(e.Graphics);
                        label1.Visible = true;
                    }
                    break;
                case pageName.stage03 :
                    label1.Visible = false;
                    stage03.Paint(e.Graphics);
                    if (!TurnPage.isGameOver) {
                        Global.setEggs(e.Graphics);
                        label1.Visible = true;
                    }
                    break;
            }
        }

        public static void NewStage(pageName pageName) {
            if (pageName == pageName.mainPage) { MainPage = new MainpageStage(); }
            if (pageName == pageName.turntablePage) { turntableStage = new TurnTableStage(); stage01 = null; stage02 = null; stage03 = null;  }
            if (pageName == pageName.stage01) {
                stage01 = new Stage01();
                level01 = null;
                MainPage = null;
            }
            if (pageName == pageName.level01) { level01 = new Level01(); turntableStage = null; }
            if (pageName == pageName.stage02) {
                stage02 = new Stage02();
                level02 = null;
                MainPage = null;
            }
            if (pageName == pageName.level02) { level02 = new Level02(); turntableStage = null; }
            if (pageName == pageName.stage03) { stage03 = new Stage03(); }
            GC.Collect();
        }

        public void RunAnimate() {
            while (true) {
                switch (Global.page) {
                    case pageName.mainPage :
                        MainPage.Action();
                        break;
                    case pageName.turntablePage :
                        turntableStage.Action();
                        break;
                    case pageName.stage01 :
                        stage01.Move();
                        break;
                    case pageName.level01:
                        level01.Move();
                        break;
                    case pageName.stage02:
                            stage02.Action();
                        break;
                    case pageName.level02:
                        level02.Move();
                        break;
                    case pageName.stage03:
                        stage03.Action();
                        break;
                }
                Invalidate();
            }                
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            switch (Global.page) {
                case pageName.turntablePage: turntableStage.MouseMove(e);  break;
                case pageName.stage02: stage02.MouseMove(e); break;
                case pageName.level02: level02.MouseMove(e); break;
            }
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Cursor = new Cursor("icon\\mouse_click.ico");
                switch (Global.page) {
                    case pageName.mainPage : MainPage.AcotrisClickedByMouseDown(e); break;
                    case pageName.turntablePage : turntableStage.MouseDown(e);      break;
                    case pageName.stage01 : stage01.MouseDown(e); break;
                    case pageName.level01 : level01.MouseDown(e); break;
                    case pageName.stage02 : stage02.MouseDown(e); break;
                    case pageName.level02 : level02.MouseDown(e); break;
                    case pageName.stage03 : stage03.MouseDown(e); break;
                }
            }
        }


        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            this.Cursor = new Cursor("icon\\mouse.ico");
            switch (Global.page) {
                case pageName.mainPage : MainPage.AcotrisClickedByMouseUp(e); break;
                case pageName.turntablePage : turntableStage.MouseUp(e); break;
                case pageName.stage01 : stage01.MouseUp(e); break;
                case pageName.level01 : level01.MouseUp(e); break;
                case pageName.stage02 : stage02.MouseUp(e); break;
                case pageName.level02 : level02.MouseUp(e); break;
                case pageName.stage03 : stage03.MouseUp(e); break;
            }           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                thread.Abort();
                Close();
            }
        }

        public void nameTextbox_KeyPress(object sender, KeyPressEventArgs e) {
            // e.KeyChar == (Char)44 -----> ,
            if (e.KeyChar == (Char)44) {
                e.Handled = true;
            } else {
                e.Handled = false;
            }
        }
    }
}
