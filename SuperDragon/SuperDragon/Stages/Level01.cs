using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace SuperDragon {
    class Level01 {
        Music music;
        //-------------載入角色-------------//
        CountTime countTime;
        TurnPage turn;
        MainCharactor mainCharactor;
        AnimateActor Cloud, WingL, WingR, stone;
        YellowBird yellowBird;
        Actor Background, RainBow;
        //-------------速度控制-------------//
        int a, t1, t2;   //a :板子上移動 、 t1、t2:加速度
        int step = 0;      //遊戲進度

        //-------------說明頁物件-------------//
        AnimateActor explain, ExplainPlayBtn, rNextBtn, lNextBtn;

        //-------------建構子-------------//
        public Level01() {
            music = new Music();
            countTime = new CountTime();
            turn = new TurnPage();
            yellowBird = new YellowBird("YellowBird", 3, new Point(1100, 100), 6);
            Background = new Actor("pink", new Point(690, 300), 1.2f);
            RainBow = new Actor("rainbow", new Point(550, 640), 1.2f);
            Cloud = new AnimateActor("clouds", 1, new Point(550, 430), 2);
            WingL = new AnimateActor("wing0", 2, new Point(570, 470), 1.7f);
            WingR = new AnimateActor("wing1", 2, new Point(540, 365), 1.2f);

            mainCharactor = new MainCharactor("dragon", 3, new Point(690, 220), 8);
            stone = new AnimateActor("stone", 1, new Point(1100, 200), 5f);

            //------說明頁
            if (Global.Mode > 0) {
                explain = new AnimateActor("Level01explain0", 1, new Point(550, 410), 2);
                rNextBtn = new AnimateActor("Rnext", 1, new Point(1000, 430), 1.3f);
            } else {
                explain = new AnimateActor("story15", 1, new Point(550, 410), 2);
                rNextBtn = new AnimateActor("graExpRBtn0", 1, new Point(1000, 430), 1.3f);
            }
                ExplainPlayBtn = new AnimateActor("exPlay", 1, new Point(550, 630), 1);
            
            lNextBtn = new AnimateActor("graExpLBtn0", 1, new Point(100, 430), 1.3f);

            Global.target = 2;
        }
        //-------------Form 呼叫的 Paint-------------//
        public void Paint(Graphics g) {
            Background.Paint(g);

            //------ 遊戲開始
            if(step == 1)
            {
                RainBow.Paint(g);
                if (WingL != null) WingL.Paint(g);
                if (WingR != null) WingR.Paint(g);
                Cloud.Paint(g);
                mainCharactor.Paint(g);
                stone.Paint(g);
                yellowBird.Paint(g);
            }

            //------ 換頁效果
            if (step == 5 || step == 2 || step == 3) turn.Paint(g);

            //------ 說明頁
            if (explain != null && ExplainPlayBtn != null) {
                explain.Paint(g);
                ExplainPlayBtn.Paint(g);
                if (Global.Mode != 0) {
                    rNextBtn.Paint(g);
                    lNextBtn.Paint(g);
                }
                if(explainPage == 1) {
                    WingL.Paint(g);
                    WingR.Paint(g);
                }
            }
        }

        int explainPage = 0;
        //-------------Form 呼叫的 MouseDown-------------//
        public void MouseDown(MouseEventArgs e) {
            switch (step) {
                // 說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y))
                    {
                        music.PlayMusic("concern.mp3");
                        ExplainPlayBtn.Size = 1.2f;
                        explain = null;
                        ExplainPlayBtn = null;
                        WingL = null;
                        WingR = null;
                        WingL = new AnimateActor("wing0", 2, new Point(200, 320), 1.5f);
                        WingR = new AnimateActor("wing1", 2, new Point(900, 320), 1.5f);
                        step = 1;
                    }
                    // 說明頁按鈕切換
                    if (rNextBtn.isClick(e.X, e.Y) && explainPage == 0 && Global.Mode>0)
                    {
                        music.PlayMusic("concern.mp3");
                        explain.ClearImages();
                        explain.AddImage("Level01explain1", 1);

                        explainPage = 1;
                        rNextBtn.Size = 1.5f;

                        rNextBtn.ClearImages();
                        rNextBtn.AddImage("graExpRBtn0", 1);

                        lNextBtn.ClearImages();
                        lNextBtn.AddImage("Lnext", 1);
                    }
                    if (lNextBtn.isClick(e.X, e.Y) && explainPage == 1 && Global.Mode > 0)
                    {
                        music.PlayMusic("concern.mp3");
                        explain.ClearImages();
                        explain.AddImage("Level01explain0", 1);

                        explainPage = 0;
                        lNextBtn.Size = 1.5f;

                        lNextBtn.ClearImages();
                        lNextBtn.AddImage("graExpLBtn0", 1);

                        rNextBtn.ClearImages();
                        rNextBtn.AddImage("Rnext", 1);
                    }
                    break;
                //遊戲開始
                case 1:
                    if (Cloud.isClick(e.X, e.Y)) {
                        if (e.X > Cloud.Center.X) {
                            music.PlayMusic("cloudsClick.mp3");
                            Cloud._Angle += Convert.ToInt32(Math.Sqrt(Math.Pow(e.X - Cloud.Center.X, 2) + Math.Pow(e.Y - Cloud.Center.Y, 2)) / 50);
                        }else if (e.X < Cloud.Center.X){
                            music.PlayMusic("cloudsClick.mp3");
                            Cloud._Angle -= Convert.ToInt32(Math.Sqrt(Math.Pow(e.X - Cloud.Center.X, 2) + Math.Pow(e.Y - Cloud.Center.Y, 2)) / 50);
                        }
                            
                    }
                    break;

                //遊戲敗場
                case 3:
                    turn.MouseDown(e, pageName.turntablePage);
                    break;
            }

        }

        //-------------Form 呼叫的 MouseUp-------------//
        public void MouseUp(MouseEventArgs e)
        {
            switch (step)
            {
                //說明頁
                case 0:
                if (ExplainPlayBtn.isClick(e.X, e.Y))
                {
                    ExplainPlayBtn.Size = 1;
                   
                }
                if (rNextBtn.isClick(e.X, e.Y))
                {
                        rNextBtn.Size = 1.3f;
                }
                if (lNextBtn.isClick(e.X, e.Y))
                {
                        lNextBtn.Size = 1.3f;
                }
                break;
            }
            
        }

        //-------------Thread 裡面的 動作-------------//
        public void Move() {
            switch (step) {
                //遊戲進場
                case 0:
                    Thread.Sleep(70);
                    if (WingL != null)
                        WingL.Action();
                    if (WingR != null)
                        WingR.Action();
                    break;
                //遊戲開始
                case 1:
                    //在Thread裡面的動作記得要睡覺。
                    countTime.StartT();
                    Thread.Sleep(70);

                    //角色的翻轉動作。
                    if (Cloud._Angle >= 0)
                        mainCharactor.Action();
                    else
                        mainCharactor.ReAction();

                    //角色的動態圖
                    if (WingL != null) WingL.Action();
                    if (WingR != null) WingR.Action();
                    yellowBird.Action();

                    //翅膀是否碰到
                    if ( WingL != null )
                        if (WingL.isClick(mainCharactor.Center.X, mainCharactor.Center.Y)) {
                            music.ContiPlayMusic("getWings.mp3");
                            Global.target -= 1;
                            WingL = null; 
                        }
                    if ( WingR != null) 
                        if (WingR.isClick(mainCharactor.Center.X, mainCharactor.Center.Y)) {
                            music.ContiPlayMusic("getWings.mp3");
                            Global.target -= 1;
                            WingR = null;
                        }

                    if (WingL == null && WingR == null) {
                        music.PlayMusic("change.wav");
                        mainCharactor.Motion(3);
                        if (Global.Mode == 1)
                            step = 4;
                        else
                            step = 2;
                    }

                    // 黃色小鳥移動與刷新
                    if (yellowBird.Center.X < 0 && (stone.Center.Y > 500)) {
                        yellowBird.HP = 2;
                        yellowBird.Motion(0);
                        yellowBird.Center = new Point(1300, 100);
                        stone.t = 0;
                        stone.Center = yellowBird.Center;
                    } else yellowBird.Move(-20, 0);

                    // 黃色小鳥丟石頭與否
                    if (stone.Center.Y <= yellowBird.Center.Y + 100) {
                        //當小鳥超過主角會自動釋放物體
                        if (yellowBird.Center.X <= mainCharactor.Center.X && stone.Center.X > 0) {
                            if (!countTime.Sec(1)) music.ContiPlayMusic("Stonrfalling.mp3");
                            stone.GMove(0, 1);
                        } else
                            //石頭沒超過主角，和鳥一起移動
                            stone.Center = new Point(yellowBird.Center.X, yellowBird.Center.Y + 50);
                    } else {
                        //石頭自然釋出
                        if (stone.isClick(mainCharactor.Center.X, mainCharactor.Center.Y - 50)) {
                            if (countTime.MilSec(500)) {
                                Global.HP -= 2;
                                music.PlayMusic(1);
                            }
                            music.StopMusic("Stonrfalling.mp3");
                            
                            if (Global.HP <= 0) step = 3;
                        }
                        if (Cloud.isClick(stone.Center.X, stone.Center.Y - 50)) {
                            music.PlayMusic("punch.mp3");
                            if (stone.Center.X > Cloud.Center.X)
                                Cloud._Angle += Convert.ToInt32(Math.Sqrt(Math.Pow(stone.Center.X - Cloud.Center.X, 2) + Math.Pow(stone.Center.Y - Cloud.Center.Y, 2)) / 60);
                            else
                                Cloud._Angle += -Convert.ToInt32(Math.Sqrt(Math.Pow(stone.Center.X - Cloud.Center.X, 2) + Math.Pow(stone.Center.Y - Cloud.Center.Y, 2)) / 50);
                        }

                        stone.GMove(0, 1);
                    }

                    if (mainCharactor.Center.Y + Convert.ToInt32(-a * Math.Tan(mainCharactor.Sita)) > 900) {
                        Global.HP--;
                        if (Global.HP <= 0) {
                            step = 3;
                        }
                        a = 0;
                        t1 = t2 = 0;
                    }

                    //當板子開始有角度的時候
                    if (Cloud._Angle > 0) {
                        //角色在板子上的(加)速度
                        a -= Convert.ToInt32(10 * Math.Sin(mainCharactor.Sita)) * t1 / 2;
                        //角色的速度為零時，時間參數歸零，避免角色暴衝
                        if (Convert.ToInt32(10 * Math.Sin(mainCharactor.Sita)) * t1 / 2 == 0) t1 = 0;
                        //另一側的時間參數歸零
                        t2 = 0;
                        //時間參數是隨著執行次數而增加的
                        t1++;

                        //角色在斜面上移動，y的位置利用角度去算出確實位置，以保證角色維持在斜面上移動。
                        mainCharactor.CloudMove(Cloud.Center, -a, Convert.ToInt32(-a * Math.Tan(mainCharactor.Sita)));
                        //角色和斜面同角度。
                        mainCharactor._Angle = Cloud._Angle;
                    } else if (Cloud._Angle < 0) {
                        a -= Convert.ToInt32(10 * Math.Sin(mainCharactor.Sita)) * t2 / 2;
                        if (Convert.ToInt32(10 * Math.Sin(mainCharactor.Sita)) * t2 / 2 == 0) t2 = 0;

                        t1 = 0;
                        t2++;
                        mainCharactor.CloudMove(Cloud.Center, -a, Convert.ToInt32(-a * Math.Tan(mainCharactor.Sita)));
                        mainCharactor._Angle = Cloud._Angle;
                    } else if (Cloud._Angle == 0) {
                        mainCharactor._Angle = Cloud._Angle;
                        mainCharactor.CloudMove(Cloud.Center, -a, Convert.ToInt32(-a * Math.Tan(mainCharactor.Sita)));
                        t1 = t2 = 0;
                    }
                    countTime.EndT();
                    break;
                //遊戲勝場
                case 2:
                    turn.TurnWin(pageName.stage02);
                    break;
                //遊戲敗場
                case 3:
                    turn.TurnGameOver(pageName.mainPage);
                    break;
                //遊戲過關
                case 4:
                    music.PlayMusic("Pass.mp3");
                    Ranker ranker = new Ranker();
                    ranker.Load();
                    ranker.Clear();
                    ranker.ShowRank(1);
                    step = 5;
                    break;
                case 5:
                    turn.TurnWin(pageName.mainPage);
                    break;
            }
        }
    }
}
