using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;


namespace SuperDragon {
    class Level02 {
        Music music;
        //-------------載入角色-------------//
        CountTime countTime;
        TurnPage turn;
        AnimateActor background, holegrass;
        Fire redfire, bluefire;
        MainCharactor mainCharactor;
        BlackDragon blackDragon;
        ContinuousActor clouds;

        //-------------說明頁物件-------------//
        AnimateActor explain, ExplainPlayBtn, rNextBtn, lNextBtn;

        bool douwn;
        int downCount, step;
        int explainPage = 0;
        Random randomizer;

        public Level02() {
            music = new Music();
            countTime = new CountTime();
            turn = new TurnPage();
            randomizer = new Random();

            background = new AnimateActor("sunsetBk", 1, new Point(697, 330), 1.5f);
            holegrass = new AnimateActor("holegrass", 1, new Point(247, 360), 1.1f);
            mainCharactor = new MainCharactor("DragonFire1", 2, new Point(120, 550), 4.5f);
            blackDragon = new BlackDragon("blackdragon", 2, new Point(970, 550), 4.5f);
            redfire = new Fire("redfire", 3, new Point(385, 465), 1.8f, 0.5f, 1);
            bluefire = new Fire("BlueFire", 3, new Point(740, 465), 1.8f, 0.5f, 1);
            clouds = new ContinuousActor("sunsetCloud", 4, new Point(100, 0), 1.7f, 400, 30, 5);

            douwn = false;
            downCount = 0;

            //------說明頁
            if (Global.Mode > 0) {
                explain = new AnimateActor("Level02explain0", 3, new Point(550, 410), 2);
                rNextBtn = new AnimateActor("graExpRBtn0", 1, new Point(1000, 450), 1.3f);
            } else {
                explain = new AnimateActor("story22", 1, new Point(550, 410), 2);
                rNextBtn = new AnimateActor("Rnext", 1, new Point(1000, 450), 1.3f);
            }
            ExplainPlayBtn = new AnimateActor("exPlay", 1, new Point(550, 630), 1);
            
            lNextBtn = new AnimateActor("graExpLBtn0", 1, new Point(100, 450), 1.3f);

            Global.target = 1;
        }

        //---------------- 加在 Form1.Paint 裡 ----------------//
        public void Paint(Graphics g) {
            background.Paint(g);
            holegrass.Paint(g);
            switch (step) {
                //遊戲進場
                case 0:
                    //------ 說明頁
                    if (explain != null && ExplainPlayBtn != null) {
                        explain.Paint(g);
                        ExplainPlayBtn.Paint(g);
                        rNextBtn.Paint(g);
                        lNextBtn.Paint(g);
                    }
                    break;
                case 1:
                case 4:
                    //----------------  clouds
                    Thread.Sleep(30);
                    clouds.Paint(g, new Point(100, 100));
                    mainCharactor.Paint(g);
                    blackDragon.Paint(g);

                    //---------------- fire
                    if (bluefire.widthsize >= 0 && bluefire.widthsize <= 1
                        && bluefire.heightsize >= 0.5 && bluefire.heightsize <= 1.5
                        && redfire.widthsize >= 0 && redfire.widthsize <= 1
                        && redfire.heightsize >= 0.5 && redfire.heightsize <= 1.5) {
                        douwn = true;

                        bluefire.widthsize += 0.01f;
                        bluefire.heightsize += 0.01f;
                        bluefire.Center = new Point(redfire.Center.X + Convert.ToInt32(redfire.img.Width * redfire.widthsize), redfire.Center.Y);

                        redfire.widthsize -= 0.01f;
                        redfire.heightsize -= 0.01f;

                    }

                    if (douwn) {
                        bluefire.SetWidthHeight(g, bluefire.widthsize, bluefire.heightsize);
                        redfire.SetWidthHeight(g, redfire.widthsize, redfire.heightsize);
                        douwn = false;
                    } else if (downCount == 0) {
                        redfire.Center = new Point(385, 465);
                        bluefire.Center = new Point(740, 465);
                        bluefire.SetWidthHeight(g, 0.5f, 1);
                        redfire.SetWidthHeight(g, 0.5f, 1);
                        downCount++;
                    } else {
                        if (redfire.widthsize >= 1) {
                            redfire.Center = new Point(redfire.Center.X + 30, redfire.Center.Y);
                            redfire.SetWidthHeight(g, 1, 1.5f);

                            if (redfire.Center.X >= 1000) {
                                Global.target = 0;
                                blackDragon.Motion(1);
                                if (countTime.Sec(2) && step != 5) {
                                    if (Global.Mode == 2)
                                        step = 4;
                                    else
                                        step = 2;
                                }
                            }
                        }

                        if (bluefire.widthsize >= 1) {
                            bluefire.Center = new Point(bluefire.Center.X - 30, bluefire.Center.Y);
                            bluefire.SetWidthHeight(g, 1, 1.5f);

                            if (bluefire.Center.X <= -100) {
                                mainCharactor.Motion(7);
                                if (countTime.Sec(2)) {
                                    step = 3;
                                }

                            }
                        }
                    }
                    break;
                case 2:
                case 3:
                case 5:
                    turn.Paint(g);
                    break;
            }
        }


        //---------------- 加在 Form1.Thread 裡 ----------------//
        public void Move() {
            switch (step) {
                //遊戲進場
                case 0:
                    Thread.Sleep(100);
                    if (explain != null) {
                        explain.Action();
                    }

                    break;
                //遊戲開始
                case 1:
                    music.ContiPlayMusic("fire2.mp3");
                    //在Thread裡面的動作記得要睡覺。
                    countTime.StartT();
                    Thread.Sleep(50);
                    mainCharactor.Action();
                    blackDragon.Action();
                    redfire.Action();
                    bluefire.Action();

                    //background.Center = new Point(background.Center.X + 30, background.Center.Y - 30);
                    //background.Center = new Point(background.Center.X - 30, background.Center.Y + 30);

                    //----------------  clouds
                    clouds.Action(-150, new Point(1300, 0 + 50 * randomizer.Next(1, 5)), -1, 0);
                    countTime.EndT();
                    break;
                //遊戲勝場
                case 2:
                    music.StopMusic("fire2.mp3");
                    turn.TurnWin(pageName.stage03);
                    music.PlayMusic("burned.mp3");
                    break;
                //遊戲敗場
                case 3:
                    music.StopMusic("fire2.mp3");

                    turn.TurnGameOver(pageName.mainPage);
                    break;
                //遊戲過關
                case 4:
                    music.PlayMusic("Pass.mp3");
                    Ranker ranker = new Ranker();
                    ranker.Load();
                    ranker.Clear();
                    ranker.ShowRank(2);
                    step = 5;
                    break;
                case 5:
                    turn.TurnWin(pageName.mainPage);
                    break;


            }

        }


        //---------------- 加在 Form1.MouseMove 裡 ----------------//
        public void MouseMove(MouseEventArgs e) {
            switch (step) {
                //遊戲開始
                case 1:
                    if (redfire.widthsize >= 1 || bluefire.widthsize >= 1) {
                    } else {
                        redfire.shakemouse(e, 0.02f);
                        bluefire.shakemouse(e, -0.02f);
                        bluefire.Center = new Point(redfire.Center.X + Convert.ToInt32(redfire.img.Width * redfire.widthsize), redfire.Center.Y);
                    }
                    break;
            }
        }

        //---------------- 加在 Form1.MouseDown 裡 ----------------//
        public void MouseDown(MouseEventArgs e) {
            switch (step) {
                // 說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y)) {
                        music.PlayMusic("concern.mp3");
                        ExplainPlayBtn.Size = 1.2f;
                        explain = null;
                        ExplainPlayBtn = null;
                        step = 1;
                    }
                    // 說明頁按鈕切換
                    if (rNextBtn.isClick(e.X, e.Y) && (Global.Mode == 0 ? explainPage == 0  : false)) {
                        music.PlayMusic("concern.mp3");
                        explainPage++;
                        rNextBtn.Size = 1.5f;
                    }
                    if (lNextBtn.isClick(e.X, e.Y) && (Global.Mode == 0 ? explainPage == 1 : false)) {
                        music.PlayMusic("concern.mp3");
                        explainPage--;
                        lNextBtn.Size = 1.5f;
                    }
                    if ((rNextBtn.isClick(e.X, e.Y) || lNextBtn.isClick(e.X, e.Y)) && Global.Mode == 0) {
                        switch (explainPage) {
                            case 0:
                                explain.ClearImages();
                                explain.AddImage("story22", 1);
                                lNextBtn.ClearImages();
                                lNextBtn.AddImage("graExpLBtn0", 1);
                                rNextBtn.ClearImages();
                                rNextBtn.AddImage("Rnext", 1);
                                break;
                            case 1:
                                explain.ClearImages();
                                explain.AddImage("story23", 1);
                                rNextBtn.ClearImages();
                                rNextBtn.AddImage("graExpRBtn0", 1);
                                lNextBtn.ClearImages();
                                lNextBtn.AddImage("Lnext", 1);
                                break;
                        }
                    }
                    break;
                //遊戲敗場
                case 3:
                    turn.MouseDown(e, pageName.turntablePage);
                    break;
            }
        }

        //---------------- 加在 Form1.MouseUp 裡 ----------------//
        public void MouseUp(MouseEventArgs e) {
            switch (step) {
                //說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y)) {
                        ExplainPlayBtn.Size = 1;
                    }
                    if (rNextBtn.isClick(e.X, e.Y) && explainPage == 0) {
                        //music.PlayMusic("concern.mp3");
                        rNextBtn.Size = 1.3f;
                    }
                    if (lNextBtn.isClick(e.X, e.Y) && explainPage == 1) {
                        //music.PlayMusic("concern.mp3");
                        lNextBtn.Size = 1.3f;
                    }
                    break;


            }
        }
    }
}
