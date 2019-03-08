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
    class Stage01 {
        //-------------動畫計時-------------//
        Music music;
        CountTime newt;
        int step = 0;
        int speed = 9;
        //-------------隨機位置-------------//
        Random randomizer = new Random(Guid.NewGuid().GetHashCode());
        //-------------角色宣告-------------//
        TurnPage turn = new TurnPage();
        MainCharactor mainCharactor;
        OrangeDragon orangeDragon;
        YellowBird yellowBird;
        HealthPt healthyellow, healthorange;
        AnimateActor background, grass, playButton, bone, rainbow, stone;
        ContinuousActor clouds, mountains, grasses;

        //-------------判斷閃避-------------//
        bool escaption = false;

        //-------------說明頁物件-------------//
        AnimateActor explain, ExplainPlayBtn, oneHp01, oneHp02, rNextBtn, lNextBtn;
        bool canPaintHp01, canPaintHp02, isAttackOrange;
        int explainPage = 0;
        
        public Stage01() {
            music = new Music("stage1.mp3");

            // 隨機背景
            clouds = new ContinuousActor("clouds", 7, new Point(100, 100), 1, 400, 80, 5);
            mountains = new ContinuousActor("mountain", 6, new Point(100, 600), 1.2f, 350, 80, 0);
            grasses = new ContinuousActor("Grass", 6, new Point(100, 658), 1.8f, 200, 80, 0);

            // 固定背景與物體
            background = new AnimateActor("bluesky", 1, new Point(800, 300), 1.3f);
            grass = new AnimateActor("Grass11", 1, new Point(1000, 430), 1f);
            rainbow = new AnimateActor("rainbow1", 1, new Point(1800, 300), 0.3f);

            // 動態互動物體
            stone = new AnimateActor("stone", 1, new Point(1100, 200), 3f);
            bone = new AnimateActor("bone", 1, new Point(750, 450), 3);

            // 怪物們
            mainCharactor = new MainCharactor("dragon", 3, new Point(350, 500), 6);
            orangeDragon = new OrangeDragon("orangedragon", 2, new Point(750, 500), 3);
            yellowBird = new YellowBird("YellowBird", 3, new Point(1100, 100), 4);

            // 生命Bar
            healthyellow = new HealthPt("ProgressBar10", 1, new Point(1100, 0), 3);
            healthorange = new HealthPt("ProgressBar10", 1, new Point(1100, 500), 3);

            // 測試用按鈕
            playButton = new AnimateActor("Play1", 1, new Point(1000, 430), 15);
            newt = new CountTime();

            // 說明頁物件
            if(Global.Mode>0)
                explain = new AnimateActor("explain2", 1, new Point(550, 410), 2);
            else
                explain = new AnimateActor("story11", 1, new Point(550, 410), 2);
            ExplainPlayBtn = new AnimateActor("exPlay", 1, new Point(550, 630), 1); // 550 600
            oneHp01 = new AnimateActor("oneHp", 1, new Point(450, 390), 1);
            oneHp02 = new AnimateActor("oneHp", 1, new Point(750, 390), 1);
            rNextBtn = new AnimateActor("Rnext", 1, new Point(1000, 430), 1.3f);
            lNextBtn = new AnimateActor("graExpLBtn0", 1, new Point(100, 430), 1.3f);
            canPaintHp01 = false;
            canPaintHp02 = false;
            isAttackOrange = false;

            Global.target = orangeDragon.Enemys;

            
        }

        //---------------- 加在 Form1.Paint 裡 ----------------//
        public void Paint(Graphics g) {
            //----------------背景
            background.Paint(g);
            clouds.Paint(g, new Point(100, 100));
            mountains.Paint(g, new Point(100, 600));
            if (orangeDragon.Enemys <= 0) {
                rainbow.Paint(g);
            }
            grass.Paint(g);
            grasses.Paint(g, new Point(100, 658));


            //----------------MainCharacter
            if(step == 1 && explain == null && ExplainPlayBtn == null) {
                mainCharactor.Paint(g);
            }
            //----------------Enemys
            if (orangeDragon.Enemys > 0 && step != 0) {

                orangeDragon.Paint(g);
                //----------------Items & Character
                stone.Paint(g);
                yellowBird.Paint(g);
                bone.Paint(g);
                //----------------LifePoint
                healthyellow.Paint(g);
                healthorange.Paint(g);
            }

            //換頁效果
            if (step == 0 || step == 2 || step == 3) turn.Paint(g);
            //說明頁
            if (explain != null && ExplainPlayBtn != null ) {

                explain.Paint(g);
                if(explainPage == 1 && Global.Mode > 0) {
                    mainCharactor.Paint(g);
                    orangeDragon.Paint(g);
                    bone.Paint(g);
                }
                
                ExplainPlayBtn.Paint(g);
                rNextBtn.Paint(g);
                lNextBtn.Paint(g);
                if (canPaintHp01 && Global.Mode > 0) {
                    oneHp01.Paint(g);
                    if (newt.MilSec(200)) { canPaintHp01 = false; }
                }
                if (canPaintHp02 && Global.Mode > 0) {
                    oneHp02.Paint(g);
                    if (newt.MilSec(100)) { canPaintHp02 = false; }
                }
            }
        }

        //---------------- 加在 Form1.Thread 裡 ----------------//
        public void Move() {
            switch (step) {
                //遊戲進場說明
                case 0:
                    Thread.Sleep(80);
                    if (explainPage == 1 && Global.Mode > 0) {
                        mainCharactor.Action();

                        //敵人的動作
                        if (!isAttackOrange) {
                            orangeDragon.Action();
                            if (newt.MilSec(500)) {
                                orangeDragon.Motion(1);
                            }
                        }
                        //敵人丟骨頭
                        bone._Angle += 9;
                        bone.Move(-15 - speed, 0);

                        if (mainCharactor.isClick(bone.Center.X, bone.Center.Y)) {
                            
                            //如果沒防禦
                            if (!escaption) {
                                music.PlayMusic(1);
                                mainCharactor.Motion(2);
                                canPaintHp01 = true;
                            }
                            bone.Center = new Point(orangeDragon.Center.X - 50, orangeDragon.Center.Y - 20);
                            music.PlayMusic(3);
                        }
                    }
                    break;
                
                //遊戲開始
                case 1:

                    newt.StartT();
                    Thread.Sleep(50);

                    //----------------  背景移動
                    mountains.Action(-200, new Point(1800, 600), -3, 0);
                    grasses.Action(-150, new Point(1200, 658), -3-speed, 0);
                    clouds.Action(-150, new Point(1300, 150 + 80 * randomizer.Next(1, 5)), -3, 0);

                    if (orangeDragon.Enemys > 0) {
                        // 黃色小鳥 : 會丟石頭或生命蛋
                        yellowBird.Action();
                        // 黃色小鳥移動與刷新
                        if (yellowBird.Center.X < 0 && (stone.Center.X < 0)) {
                            yellowBird.HP = 2;
                            yellowBird.Motion(0);
                            yellowBird.Center = new Point(1300, 100);
                            stone.Center = yellowBird.Center;
                        } else yellowBird.Move(-8 - speed, 0);

                        // 黃色小鳥丟石頭與否
                        if (yellowBird.HP > 0) {
                            //當小鳥超過主角會自動釋放物體
                            if (yellowBird.Center.X <= mainCharactor.Center.X - (-3 - speed) * (20 + speed)) {
                                //當石頭砸到主角
                                if (stone.isClick(mainCharactor.Center.X, mainCharactor.Center.Y - 50)) {
                                    mainCharactor._Angle += 5;
                                    Global.HP -= 2;
                                    music.PlayMusic(6);
                                    stone.Center = new Point(-100, 0);
                                    stone.t = 0;
                                    if (Global.HP <= 0) step = 3;
                                }
                                //石頭落下狀態
                                if (stone.Center.Y < 700) {
                                    music.ContiPlayMusic("Stonrfalling.mp3");
                                    stone.GMove(-3 - speed, 1);
                                } else {
                                    stone.Move(-5 - speed, 0);
                                }
                            } else {
                                //石頭沒超過主角，和鳥一起移動
                                stone.Center = new Point(yellowBird.Center.X, yellowBird.Center.Y + 70);
                            }
                        } else {
                            //小鳥被點到，石頭自然釋出
                            if (stone.isClick(orangeDragon.Center.X, orangeDragon.Center.Y)) {  
                                music.PlayMusic(1);
                                orangeDragon.HP -= 4;
                                orangeDragon.Life();
                            }
                            if (stone.Center.Y < 700) {
                                music.ContiPlayMusic("Stonrfalling.mp3");
                                stone.GMove(-3 - speed, 1);
                                if (stone.Center.Y > 680) music.PlayMusic(6);
                            } else {
                                music.StopMusic("Stonrfalling.mp3");
                                stone.Move(-5 - speed, 0);
                            }
                        }
                    }

                    //主角的動作
                    mainCharactor.Action();
                    if (mainCharactor._Angle > 0) mainCharactor._Angle -= 1;
                    if (orangeDragon.Enemys <= 0 && rainbow.isClick(mainCharactor.Center.X - 250, mainCharactor.Center.Y)) {
                        mainCharactor.Move(-2 - speed, -5 - speed);
                        if (mainCharactor.Center.Y <= 100) { step = 2; }
                    }

                    if (orangeDragon.Enemys > 0) {
                        //敵人的動作
                        orangeDragon.Action();
                        orangeDragon.Move(-5 - speed, 0);
                        bone._Angle += 9;
                        bone.Move(-20 - speed, 0);

                        if (orangeDragon.Center.X <= 0) {
                            orangeDragon.Center = new Point(1200, 600);
                            bone.Center = new Point(orangeDragon.Center.X - 50, orangeDragon.Center.Y - 20);
                        }

                        //點主角防禦骨頭攻擊
                        if (mainCharactor.isClick(bone.Center.X, bone.Center.Y)) {
                            //如果沒防禦
                            if (!escaption) {
                                music.PlayMusic(1);
                                Global.HP -= 1;
                                if (Global.HP <= 0) step = 3;
                                mainCharactor.Motion(2);
                            }
                            bone.Center = new Point(orangeDragon.Center.X - 50, orangeDragon.Center.Y - 20);
                            music.PlayMusic(3);
                            orangeDragon.Motion(1);
                        } else if (!orangeDragon.isClick(bone.Center.X, bone.Center.Y) && orangeDragon.images.Count == 3) {
                            orangeDragon.Motion(0);
                        }

                        healthyellow.Health(yellowBird.HP, 2);
                        healthorange.Health(orangeDragon.HP, 10);
                        healthyellow.Center = new Point(yellowBird.Center.X, yellowBird.Center.Y - 100);
                        healthorange.Center = new Point(orangeDragon.Center.X, orangeDragon.Center.Y - 100);
                    }

                    //怪物打完一定數量啟動換關卡機制
                    if (orangeDragon.Enemys <= 0) {
                        Form1.labelx.Invoke((Action)delegate { Form1.labelx.Text = Global.TIME.ToString("000.0") + " Sec            " + "              X " + Global.target; });
                        Thread.Sleep(25);
                        rainbow.Move(-4 - speed, 0);
                        mainCharactor.Move(4 + speed, 0);
                    } else {
                        newt.EndT();
                    }
                    speed = 9 - orangeDragon.Enemys;
                    break;

                //遊戲勝場
                case 2:
                    turn.TurnWin(pageName.level01);
                    break;
                //遊戲敗場
                case 3:
                    turn.TurnGameOver(pageName.mainPage);
                    break;
            }
        }
        
        //---------- MouseDouwn -------------//
        public void MouseDown(MouseEventArgs e) {
            switch (step) {
                // 說明頁
                case 0:
                    if (Form1.labely.Visible == true) {
                        Form1.labely.Visible = false;
                        Form1.labelz.Visible = false;
                    }
                    if (ExplainPlayBtn.isClick(e.X, e.Y)) {
                        music.PlayMusic("concern.mp3");
                        ExplainPlayBtn.Size = 1.2f;
                        explain = null;
                        ExplainPlayBtn = null;

                        mainCharactor.Center = new Point(195, 600);
                        orangeDragon.Center = new Point(1100, 600);
                        yellowBird.Center = new Point(1100, 100);
                        bone.Center = new Point(1050, 580);

                        mainCharactor.Motion(0);
                        orangeDragon.Motion(0);
                        
                        step = 1;
                    }
                    if (mainCharactor.isClick(e.X, e.Y) && explainPage == 1) {
                        mainCharactor.Motion(1);
                        escaption = true;
                        canPaintHp01 = false;
                    }
                    if (orangeDragon.isClick(e.X, e.Y) && explainPage == 1) {
                        orangeDragon.Motion(2);
                        canPaintHp02 = true;
                        isAttackOrange = true;
                    }
                    // 說明頁按鈕切換
                    if (rNextBtn.isClick(e.X, e.Y) && (Global.Mode == 0 ? (explainPage == 0 || explainPage == 1 || explainPage == 2) : (explainPage == 0 || explainPage == 1))) {
                        music.PlayMusic("concern.mp3");
                        explainPage++;
                        rNextBtn.Size = 1.5f;
                    }
                    if (lNextBtn.isClick(e.X, e.Y) && (Global.Mode == 0 ? (explainPage == 1 || explainPage == 2 || explainPage == 3) : (explainPage == 1 || explainPage == 2))) {
                        music.PlayMusic("concern.mp3");
                        explainPage--;
                        lNextBtn.Size = 1.5f;
                    }
                    // 說明頁切換
                    if (rNextBtn.isClick(e.X, e.Y)|| lNextBtn.isClick(e.X, e.Y)) { 
                        switch (explainPage) {
                            case 0:
                                explain.ClearImages();
                                if (Global.Mode > 0) {
                                    explain.AddImage("explain2", 1);
                                } else {
                                    explain.AddImage("story11", 1);
                                }
                                lNextBtn.ClearImages();
                                lNextBtn.AddImage("graExpLBtn0", 1);
                                rNextBtn.ClearImages();
                                rNextBtn.AddImage("Rnext", 1);
                                break;

                            case 1:
                                explain.ClearImages();
                                if (Global.Mode > 0) {
                                    explain.AddImage("explain0", 1);
                                } else {
                                    explain.AddImage("story12", 1);
                                }
                                rNextBtn.ClearImages();
                                rNextBtn.AddImage("Rnext", 1);
                                lNextBtn.ClearImages();
                                lNextBtn.AddImage("Lnext", 1);
                                break;
                            case 2:
                                explain.ClearImages();
                                rNextBtn.ClearImages();
                                if (Global.Mode > 0) {
                                    explain.AddImage("explain1", 1);
                                    rNextBtn.AddImage("graExpRBtn0", 1);
                                } else {
                                    explain.AddImage("story13", 1);
                                    rNextBtn.AddImage("Rnext", 1);
                                }
                                lNextBtn.ClearImages();
                                lNextBtn.AddImage("Lnext", 1);
                                break;
                            case 3:
                                explain.ClearImages();
                                explain.AddImage("story14", 1);
                                rNextBtn.ClearImages();
                                rNextBtn.AddImage("graExpRBtn0", 1);
                                lNextBtn.ClearImages();
                                lNextBtn.AddImage("Lnext", 1);
                                break;
                        }
                    }
                    break;

                //遊戲開始
                case 1:
                    if (playButton.isClick(e.X, e.Y)) {
                        step = 2;
                    }
                    if (mainCharactor.isClick(e.X, e.Y)) {
                        mainCharactor.Motion(1);
                        escaption = true;
                    }
                    if (orangeDragon.isClick(e.X, e.Y)) {
                        orangeDragon.Motion(2);
                        orangeDragon.Life();
                        music.PlayMusic("AnemyHit.mp3");
                    }
                    if (yellowBird.isClick(e.X, e.Y)) {
                        yellowBird.Motion(1);
                        yellowBird.Throw();
                        music.PlayMusic("AnemyHit.mp3");
                    }
                    break;
                //遊戲敗場
                case 3:
                    turn.MouseDown(e, pageName.turntablePage);
                    break;
            }
        }
        //---------- MouseUp -------------//
        public void MouseUp(MouseEventArgs e) {
            switch (step) {
                // 說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y))
                    {
                        ExplainPlayBtn.Size = 1;
                    }
                    if (escaption) {
                        escaption = false;
                        mainCharactor.Motion(0);
                    }
                    if (orangeDragon.isClick(e.X, e.Y) && explainPage == 1 && Global.Mode>0) {
                        orangeDragon.Motion(1);
                        isAttackOrange = false;
                    }
                    if (rNextBtn.isClick(e.X, e.Y)) {
                        rNextBtn.Size = 1.3f;
                    }
                    if (lNextBtn.isClick(e.X, e.Y)) { 
                        lNextBtn.Size = 1.3f;
                    }

                    break;

                //遊戲開始
                case 1:
                    if (escaption) {
                        escaption = false;
                        mainCharactor.Motion(0);
                    }
                    break;
            }
        }
    }
}
