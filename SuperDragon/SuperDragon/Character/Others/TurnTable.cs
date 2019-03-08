using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace SuperDragon
{
    class TurnTable
    {
        Music music;
        //-------------載入角色-------------//
        AnimateActor clock, cycle, gobtn;

        //-------------動畫計時-------------//
        CountTime newt = new CountTime();

        //-------------隨機轉盤-------------//
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        int n = 0; //隨機狀態(轉盤轉動的秒數)
        int t = 0; //轉盤加/減速時間
        int k = 0; //轉盤結果回報

        //--------------------------------//
        public bool turtableisVisble = false;
        private int clickCount = 0;

        //-------------建構子(初始化)-------------//
        public TurnTable() {
            music = new Music();
            //轉盤提醒 : 比例約1:1:2 ;以中心座標來放位置。
            clock = new AnimateActor("true", 1, new Point(760, 485), 1.5f);
            cycle = new AnimateActor("cycle", 1, new Point(760, 485),1.5f);
            gobtn = new AnimateActor("transparent", 1, new Point(758, 485), 3f);
        }

        //-------------Form 呼叫的 Paint-------------//
        public void paint(Graphics g) {
            clock.Paint(g);
            cycle.Paint(g);
            gobtn.Paint(g);
        }
        //-------------Thread 裡面的 動作-------------//
        public bool turnEnd = false;

        public void rotates() {
            //一定要睡覺，不然視窗會關不起來! (有close的按鈕下)

            //轉盤的主程序
            switch (n) {
                case 1:
                    if (!newt.Sec(3)) { cycle._Angle += 3 * t; t++;  }
                    else n = 4;
                    break;

                case 2:
                    if (!newt.Sec(4)) { cycle._Angle += 2 * t; t++;  }
                    else n = 4;
                    break;

                case 3:
                    if (!newt.Sec(5)) { cycle._Angle += t; t++; }
                    else n = 4;
                    break;

                case 4:
                    if (t > 0) { cycle._Angle += t; t--;  music.ContiPlayMusic("Anti_Loop.mp3");
                    } else {
                        music.StopMusic("Anti_Loop.mp3");
                        music.StopMusic("Loop.mp3");
                        n = 0;
                        gobtn.Size = 3f;
                        if(cycle._Angle % 360 == 0 || cycle._Angle % 360 == 120 || cycle._Angle % 360 == 240)
                        {
                            n = ran.Next(1, 4);
                            gobtn.Size = 4f;
                        }
                        else if (cycle._Angle % 360 > 240)
                        {
                            k = 3;                         // 結果數值。
                            Global.HP = 8;
                            turnEnd = true;
                        }
                        else if (cycle._Angle % 360 > 120)
                        {
                            k = 5;
                            Global.HP = 10;
                            turnEnd = true;
                            //MessageBox.Show(k.ToString());
                        }
                        else if (cycle._Angle % 360 > 0)
                        {
                            k = 4;
                            Global.HP = 9;
                            turnEnd = true;
                            //MessageBox.Show(k.ToString());
                        }
                    }
                    break;
            }
        }

        //-------------Form 呼叫的 MouseDown MouseUp-------------//
        public void randomnumDown(MouseEventArgs e) {
            //當按鈕按下，轉盤會開始轉動 
            if (gobtn.isClick(e.X, e.Y) && clickCount == 0) {
                music.ContiPlayMusic("Loop.mp3");
                n = ran.Next(1, 4);
                gobtn.Size = 4f; //做出轉盤按下後縮小的感覺，可以利用Size來綁住讓Button不能按。
                clickCount++;
            }
        }

        public void randomnumUp(MouseEventArgs e) {
            if (gobtn.isClick(e.X, e.Y) && clickCount == 0) {
                gobtn.Size = 3f;    
            }
        }

    }
}
