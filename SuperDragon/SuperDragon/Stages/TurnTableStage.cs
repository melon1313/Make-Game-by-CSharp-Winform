using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace SuperDragon
{
    class TurnTableStage {
        // 功能
        Ranker ranker;
        Music music;
        TurnPage turn;
        TurnTable turnTable;
        // 繪圖
        Actor background, wood, rankpic;
        AnimateActor Page, redButton, blueButton, greenButton, purpleButton, nextButtonLeft, nextButtonRight, cloud;
        AnimateActor greenDragon, orangeDragon, bird, tinyDragon, brownDragon, blackDragon, yellowDragon;
        AnimateActor intro0, intro1, intro2, intro3, intro4, intro5, intro6;
        AnimateActor mode01, mode02, storyMode, storyModeExplain;
        AnimateActor rBtn0, rBtn1, rBtn2;
        // 控件
        public TextBox nameTextbox;
        public Label pageLabel, turnLabel;
        // 流程判斷
        int step = 0;
        int btn, click;
        int actorChange = 0; // 角色瀏覽標籤

        //------------ TurnTableStage 建構子 ----------// 
        public TurnTableStage() {
            turn = new TurnPage();
            ranker = new Ranker();
            music = new Music(5);
            turnTable = new TurnTable();
            click = 1;
            Global.TIME = 0;
            ranker.Load();

            wood = new Actor("wood", new Point(250, 390), 0.9f);
            background = new Actor("mainpageBackground", new Point(550, 394), 1.5f);
            rankpic = new Button("rank0", 1, new Point(795, 350), 1.3f);

            Page = new Button("redPage", 1, new Point(760, 365), 1.06f);
            redButton = new Button("redButton0", 2, new Point(150, 160), 1.5f);
            blueButton = new Button("blueButton0", 2, new Point(150, 460), 1.5f);
            greenButton = new Button("greenButton0", 2, new Point(250, 310), 1.5f);
            purpleButton = new Button("purpleButton0", 2, new Point(250, 610), 1.5f);

            nextButtonLeft = new AnimateActor("nextButton0", 1, new Point(710, 670), 13);
            nextButtonRight = new AnimateActor("nextButton1", 1, new Point(805, 670), 13);
            intro0 = new AnimateActor("intro0", 1, new Point(765, 200), 1);
            intro1 = new AnimateActor("intro1", 1, new Point(765, 200), 1);
            intro2 = new AnimateActor("intro2", 1, new Point(765, 200), 1);
            intro3 = new AnimateActor("intro3", 1, new Point(765, 200), 1);
            intro4 = new AnimateActor("intro4", 1, new Point(765, 200), 1);
            intro5 = new AnimateActor("intro5", 1, new Point(765, 200), 1);
            intro6 = new AnimateActor("intro6", 1, new Point(765, 200), 1);

            greenDragon = new MainCharactor("dragon", 2, new Point(775, 500), 4.5f);
            orangeDragon = new OrangeDragon("orangedragon", 2, new Point(770, 500), 2.5f);
            bird = new YellowBird("YellowBird_Hit", 3, new Point(775, 500), 2.5f);
            tinyDragon = new TinyDragon("tinyDragon", 3, new Point(760, 500), 2f);
            brownDragon = new AnimateActor("browndragon", 2, new Point(750, 500), 2.5f);
            blackDragon = new BlackDragon("blackdragon", 2, new Point(735, 500), 5);
            yellowDragon = new YellowDragon("yellowdragon", 2, new Point(755, 500), 2.5f);

           
            nameTextbox = new TextBox();
            SetNameTextbox();
            pageLabel = new Label();
            SetPageLabel();
            turnLabel = new Label();
            SetTurnLabel();

            mode01 = new AnimateActor("Mode0", 1, new Point(550, 600), 2f);
            mode02 = new AnimateActor("Mode1", 1, new Point(760, 600), 2f);
            storyMode = new AnimateActor("Story0", 1, new Point(970, 600), 2f); ;
            storyModeExplain = new AnimateActor("modeStory0", 1, new Point(750, 335), 2.55f);

            cloud = new AnimateActor("cloud", 1, new Point(760, 55), 1);
            // 模式按鈕
            switch (Global.Mode)
            {
                case 0:
                    rBtn0 = new AnimateActor("rrBtn0", 1, new Point(550, 175), 1.5f);
                    rBtn1 = new AnimateActor("rBtn1", 1, new Point(750, 175), 1.5f);
                    rBtn2 = new AnimateActor("rBtn2", 1, new Point(960, 175), 1.5f);
                    break;
                case 1:
                    rBtn0 = new AnimateActor("rBtn0", 1, new Point(550, 175), 1.5f);
                    rBtn1 = new AnimateActor("rrBtn1", 1, new Point(750, 175), 1.5f);
                    rBtn2 = new AnimateActor("rBtn2", 1, new Point(960, 175), 1.5f);
                    break;
                case 2:
                    rBtn0 = new AnimateActor("rBtn0", 1, new Point(550, 175), 1.5f);
                    rBtn1 = new AnimateActor("rBtn1", 1, new Point(750, 175), 1.5f);
                    rBtn2 = new AnimateActor("rrBtn2", 1, new Point(960, 175), 1.5f);
                    break;
            }
            
        }

        //------------ 設定 Textbox ----------//
        public void SetNameTextbox() {
            nameTextbox.ShortcutsEnabled = false;  // 不啟用快速鍵
            nameTextbox.BackColor = System.Drawing.Color.White;
            nameTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            nameTextbox.Font = new System.Drawing.Font("落落の汤圆体", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nameTextbox.ForeColor = System.Drawing.Color.Silver;
            nameTextbox.Location = new System.Drawing.Point(541, 24);
            nameTextbox.MaxLength = 12;
            nameTextbox.Name = "textBox1";
            nameTextbox.Size = new Size(448, 80);
            nameTextbox.TabIndex = 1;
            nameTextbox.Text = "Your Name";
            nameTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        }

       
        //------------ 設定 pageLabel ----------//
        public void SetPageLabel() {
            pageLabel.AutoSize = true;
            pageLabel.BackColor = System.Drawing.Color.Transparent;
            pageLabel.Font = new System.Drawing.Font("Showcard Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pageLabel.ForeColor = System.Drawing.Color.Crimson;
            pageLabel.Location = new System.Drawing.Point(736, 628);
            pageLabel.Name = "label2";
            pageLabel.Size = new System.Drawing.Size(48, 60);
            pageLabel.TabIndex = 1;
            pageLabel.Text = "2";
            pageLabel.Visible = false;
        }
        public void SetTurnLabel() {
            turnLabel.BackColor = System.Drawing.Color.Transparent;
            turnLabel.Font = new System.Drawing.Font("落落の汤圆体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            turnLabel.ForeColor = System.Drawing.Color.Ivory;
            turnLabel.Location = new System.Drawing.Point(536, 200);
            turnLabel.Name = "label2";
            turnLabel.Size = new System.Drawing.Size(460, 70);
            turnLabel.TabIndex = 1;
            turnLabel.Text = string.Format("Click GO to Start");
            turnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }
        // 排行榜列印
        public void PrintScore() {
            Form1.labely.Text = "排名(Rank)    名字(Name)\r\n";
            Form1.labelz.Text = "挑戰時間(Time) \r\n";
            //排行榜的顯示筆數上限
            int count = 0;
            int i = 0;
            foreach (Score a in ranker.rank) {
                if (a.mode == actorChange) count++;
            }
            if (count > 10) count = 10;
            //印出排行榜
            foreach (Score a in ranker.rank) {
                if (a.mode == actorChange) {
                    if (count > 0) {
                        Form1.labely.Text += "\r\n" + "   No." + (i + 1).ToString("00") + "           " + a.name;
                        Form1.labelz.Text += "\r\n" + "       " + a.spand_time.ToString("000.0");
                    } else break;
                    count--;
                    i++;
                }
            }
        }
        //------------ 放在 Form1.Paint ----------//
        public void Paint(Graphics g) {
            background.Paint(g);
            wood.Paint(g);
            Page.Paint(g);
            redButton.Paint(g);
            blueButton.Paint(g);
            greenButton.Paint(g);
            purpleButton.Paint(g);
            if (click != 2 && click != 3 && click != 4) {
                turnTable.paint(g);
                cloud.Paint(g);
                rBtn0.Paint(g);
                rBtn1.Paint(g);
                rBtn2.Paint(g);
            }
            
            if (click == 2) {
                if (actorChange != 0) { nextButtonLeft.Paint(g); }
                if (actorChange != 6) { nextButtonRight.Paint(g); }
                switch (actorChange) {
                    case 0: greenDragon.Paint(g); intro0.Paint(g); break;
                    case 1: orangeDragon.Paint(g); intro2.Paint(g); break;
                    case 2: bird.Paint(g); intro3.Paint(g); break;
                    case 3: tinyDragon.Paint(g); intro4.Paint(g); break;
                    case 4: brownDragon.Paint(g); intro1.Paint(g); break;
                    case 5: blackDragon.Paint(g); intro5.Paint(g); break;
                    case 6: yellowDragon.Paint(g); intro6.Paint(g); break;
                }
            } else if (click == 3) {
                rankpic.Paint(g);
                if (actorChange != 0) { nextButtonLeft.Paint(g); }
                if (actorChange != 2) { nextButtonRight.Paint(g); }
            } else if (click == 4) {
                storyModeExplain.Paint(g);
                mode01.Paint(g);
                mode02.Paint(g);
                storyMode.Paint(g);
            }
            //換頁效果
            if (step == 0 || step == 2 || step == 3) turn.Paint(g);
        }

        //------------ 放在 Form1 裡的 Runanimate() ----------//
        public void Action() {
            switch (step) {
                //遊戲進場
                case 0:
                    step = 1;
                    break;
                //遊戲開始
                case 1:
                    Thread.Sleep(150);
                    turnTable.rotates();

                    if (click == 1) {
                    } else if (click == 2) {
                        switch (actorChange) {
                            case 0: greenDragon.Action(); break;
                            case 1: orangeDragon.Action(); break;
                            case 2: bird.Action(); break;
                            case 3: tinyDragon.Action(); break;
                            case 4: brownDragon.Action(); break;
                            case 5: blackDragon.Action(); break;
                            case 6: yellowDragon.Action(); break;
                        }
                    }

                    if (btn == 1) {
                        redButton.Action();
                    } else if (btn == 2) {
                        blueButton.Action();
                    } else if (btn == 3) {
                        greenButton.Action();
                    } else if (btn == 4) {
                        purpleButton.Action();
                    }

                    if (turnTable.turnEnd) {
                        turnLabel.Invoke((Action)delegate { turnLabel.Text = "You win " + (Global.HP) + " eggs (HP)"; });
                        turnLabel.Invoke((Action)delegate { turnLabel.Location = new System.Drawing.Point(541, 200); });
                    }
                    if (turnTable.turnEnd) step = 2;
                    break;

                //遊戲出場
                case 2:
                    Global.Name = nameTextbox.Text.ToString();
                    if (Global.Mode == 2) {
                        turn.TurnWin(pageName.stage02);
                    } else {
                        turn.TurnWin(pageName.stage01);
                    }
                    break;
            }
        }

        //------------ 放在 Form1.MouseDown ----------//
        public void MouseDown(MouseEventArgs e) {
            switch (step) {
                //遊戲開始
                case 1:
                    ((Button)redButton).Motion(0);
                    ((Button)blueButton).Motion(2);
                    ((Button)greenButton).Motion(4);
                    ((Button)purpleButton).Motion(6);
                    if (purpleButton.isClick(e.X, e.Y)) {
                        if (click == 4) break;
                        click = 4;
                        music.Click();
                        actorChange = 0;
                        ((Button)Page).Motion(11);
                        ((Button)purpleButton).Motion(7);
                        nameTextbox.Invoke((Action)delegate { nameTextbox.Visible = false; });
                        pageLabel.Invoke((Action)delegate { pageLabel.Visible = false; });
                        turnLabel.Invoke((Action)delegate { turnLabel.Visible = false; });
                    } else if (redButton.isClick(e.X, e.Y)) {
                        if (click == 1) break;
                        click = 1;
                        music.Click();
                        actorChange = 0;
                        ((Button)Page).Motion(8);
                        ((Button)redButton).Motion(1);
                        nameTextbox.Invoke((Action)delegate { nameTextbox.Visible = true; });
                        pageLabel.Invoke((Action)delegate { pageLabel.Visible = false; });
                        turnLabel.Invoke((Action)delegate { turnLabel.Visible = true; });
                    } else if (greenButton.isClick(e.X, e.Y)) {
                        if (click == 2) break;
                        click = 2;
                        music.Click();
                        ((Button)Page).Motion(10);
                        ((Button)greenButton).Motion(5);
                        nameTextbox.Invoke((Action)delegate { nameTextbox.Visible = false; });
                        actorChange = 0;
                        pageLabel.Invoke((Action)delegate { pageLabel.Visible = true; pageLabel.Text = actorChange.ToString(); });
                        Thread.Sleep(100);
                        turnLabel.Invoke((Action)delegate { turnLabel.Visible = false; });

                    } else if (blueButton.isClick(e.X, e.Y)) {
                        if (click == 3) break;
                        click = 3;
                        music.Click();
                        ((Button)Page).Motion(9);
                        ((Button)blueButton).Motion(3);
                        ((Button)rankpic).Motion(actorChange + 12);
                        nameTextbox.Invoke((Action)delegate { nameTextbox.Visible = false; });
                        actorChange = 0;
                        pageLabel.Invoke((Action)delegate { pageLabel.Visible = true; pageLabel.Text = actorChange.ToString(); });
                        Thread.Sleep(100);
                        turnLabel.Invoke((Action)delegate { turnLabel.Visible = false; });
                    }

                    if (click != 3) {
                        Form1.labely.Visible = false;
                        Form1.labelz.Visible = false;
                    }

                    if (click == 1) {
                       
                    }

                    //----- 模式 Radio Button
                    if(click == 1) {
                        //轉盤與模式選擇
                        if(rBtn2.isClick(e.X, e.Y)) {
                            music.PlayMusic("concern.mp3");
                            Global.Mode = 2;
                            rBtn2.ClearImages();
                            rBtn2.AddImage("rrBtn2", 1);
                            rBtn0.ClearImages();
                            rBtn0.AddImage("rBtn0", 1);
                            rBtn1.ClearImages();
                            rBtn1.AddImage("rBtn1", 1);
                        } else if (rBtn1.isClick(e.X, e.Y)) {
                            music.PlayMusic("concern.mp3");
                            Global.Mode = 1;
                            rBtn2.ClearImages();
                            rBtn2.AddImage("rBtn2", 1);
                            rBtn0.ClearImages();
                            rBtn0.AddImage("rBtn0", 1);
                            rBtn1.ClearImages();
                            rBtn1.AddImage("rrBtn1", 1);
                        } else if(rBtn0.isClick(e.X, e.Y)) {
                            music.PlayMusic("concern.mp3");
                            Global.Mode = 0;
                            rBtn2.ClearImages();
                            rBtn2.AddImage("rBtn2", 1);
                            rBtn0.ClearImages();
                            rBtn0.AddImage("rrBtn0", 1);
                            rBtn1.ClearImages();
                            rBtn1.AddImage("rBtn1", 1);
                        }
                        turnTable.randomnumDown(e);
                    } else if (click == 2) {
                        // 選單與角色瀏覽設定
                        if (nextButtonRight.isClick(e.X, e.Y) && actorChange < 6) {
                            music.PlayMusic("concern.mp3");
                            actorChange++;
                            nextButtonRight.Size = 15;
                            pageLabel.Invoke((Action)delegate { pageLabel.Text = actorChange.ToString(); });
                        } else if (nextButtonLeft.isClick(e.X, e.Y) && actorChange > 0) {
                            music.PlayMusic("concern.mp3");
                            actorChange--;
                            nextButtonLeft.Size = 15;
                            pageLabel.Invoke((Action)delegate { pageLabel.Text = actorChange.ToString(); });
                        }
                    } else if (click == 3) {
                        // Rank 排行榜瀏覽
                        Form1.labely.Visible = true;
                        Form1.labelz.Visible = true;
                        PrintScore();
                        if (nextButtonRight.isClick(e.X, e.Y) && actorChange < 2) {
                            music.PlayMusic("concern.mp3");
                            actorChange++;
                            ((Button)rankpic).Motion(actorChange + 12);
                            PrintScore();
                            nextButtonRight.Size = 15;
                            pageLabel.Invoke((Action)delegate { pageLabel.Text = actorChange.ToString(); });
                        } else if (nextButtonLeft.isClick(e.X, e.Y) && actorChange > 0) {
                            music.PlayMusic("concern.mp3");
                            actorChange--;
                            ((Button)rankpic).Motion(actorChange + 12);
                            PrintScore();
                            nextButtonLeft.Size = 15;
                            pageLabel.Invoke((Action)delegate { pageLabel.Text = actorChange.ToString(); });
                        }
                    }
                    


                    //------ Mode 按紐
                    if (mode01.isClick(e.X, e.Y) && click == 4) {
                        music.PlayMusic("concern.mp3");
                        mode01.ClearImages();
                        mode01.AddImage("SpurStge1", 1);
                        mode02.ClearImages();
                        mode02.AddImage("Mode1", 1);
                        storyMode.ClearImages();
                        storyMode.AddImage("Story0", 1);
                        storyModeExplain.ClearImages();
                        storyModeExplain.AddImage("modeStory0", 1);
                    } else if (mode02.isClick(e.X, e.Y) && click == 4) {
                        music.PlayMusic("concern.mp3");
                        mode01.ClearImages();
                        mode01.AddImage("Mode0", 1);
                        mode02.ClearImages();
                        mode02.AddImage("SpurStge2", 1);
                        storyMode.ClearImages();
                        storyMode.AddImage("Story0", 1);
                        storyModeExplain.ClearImages();
                        storyModeExplain.AddImage("modeStory1", 1);
                    } else if (storyMode.isClick(e.X, e.Y) && click == 4) {
                        music.PlayMusic("concern.mp3");
                        mode01.ClearImages();
                        mode01.AddImage("Mode0", 1);
                        mode02.ClearImages();
                        mode02.AddImage("Mode1", 1);
                        storyMode.ClearImages();
                        storyMode.AddImage("SpurStge3", 1);
                        storyModeExplain.ClearImages();
                        storyModeExplain.AddImage("modeStory2", 1);
                    }
                    //------ 角色 Demo
                    switch (actorChange) {
                        case 0:
                            if (greenDragon.isClick(e.X, e.Y) && click == 2) {
                                ((MainCharactor)greenDragon).Motion(1);
                            }
                            break;
                        case 1:
                            if (orangeDragon.isClick(e.X, e.Y) && click == 2) {
                                ((OrangeDragon)orangeDragon).Motion(2);
                            }
                            break;
                        case 2:
                            if (bird.isClick(e.X, e.Y) && click == 2) {
                                ((YellowBird)bird).Motion(0);
                            }
                            break;
                        case 3:
                            if (tinyDragon.isClick(e.X, e.Y) && click == 2) {
                                ((TinyDragon)tinyDragon).Motion(1);
                            }
                            break;
                        case 4:
                            if (brownDragon.isClick(e.X, e.Y) && click == 2) {
                                brownDragon.ClearImages();
                                brownDragon.AddImage("browndragon_hurt", 3);
                            }
                            break;
                        case 5:
                            if (blackDragon.isClick(e.X, e.Y) && click == 2) {
                                ((BlackDragon)blackDragon).Motion(1);
                            }
                            break;
                        case 6:
                            if (yellowDragon.isClick(e.X, e.Y) && click == 2) {
                                ((YellowDragon)yellowDragon).Motion(1);
                            }
                            break;
                    }
                    break;
            }
        }

        //------------ 放在 Form1.Up ----------//
        public void MouseUp(MouseEventArgs e) {
            switch (step) {
                //遊戲開始
                case 1:
                    if (click == 1) {
                        turnTable.randomnumDown(e);
                    }
                    if (nextButtonRight.isClick(e.X, e.Y)) {
                        nextButtonRight.Size = 13;
                    }
                    if (nextButtonLeft.isClick(e.X, e.Y)) {
                        nextButtonLeft.Size = 13;
                    }

                    //------ 角色 Demo
                    switch (actorChange) {
                        case 0:
                            if (greenDragon.isClick(e.X, e.Y) && click == 2) {
                                ((MainCharactor)greenDragon).Motion(0);
                            }
                            break;
                        case 1:
                            if (orangeDragon.isClick(e.X, e.Y) && click == 2) {
                                ((OrangeDragon)orangeDragon).Motion(0);
                            }
                            break;
                        case 2:
                            if (bird.isClick(e.X, e.Y) && click == 2) {
                                ((YellowBird)bird).Motion(1);
                            }
                            break;
                        case 3:
                            if (tinyDragon.isClick(e.X, e.Y) && click == 2) {
                                ((TinyDragon)tinyDragon).Motion(0);
                            }
                            break;
                        case 4:
                            if (brownDragon.isClick(e.X, e.Y) && click == 2) {
                                brownDragon.ClearImages();
                                brownDragon.AddImage("browndragon", 2);
                            }
                            break;
                        case 5:
                            if (blackDragon.isClick(e.X, e.Y) && click == 2) {
                                ((BlackDragon)blackDragon).Motion(0);
                            }
                            break;
                        case 6:
                            if (yellowDragon.isClick(e.X, e.Y) && click == 2) {
                                ((YellowDragon)yellowDragon).Motion(0);
                            }
                            break;
                    }
                    break;
            }
        }

        public void MouseMove(MouseEventArgs e) {
            switch (step) {
                //遊戲開始
                case 1:
                    if (nextButtonLeft.isClick(e.X, e.Y)) {
                        nextButtonLeft.Size = 11;
                        nextButtonRight.Size = 13;
                    } else if (nextButtonRight.isClick(e.X, e.Y)) {
                        nextButtonRight.Size = 11;
                        nextButtonLeft.Size = 13;
                    }
                    if (redButton.isClick(e.X, e.Y))
                        btn = 1;
                    else if (blueButton.isClick(e.X, e.Y))
                        btn = 2;
                    else if (greenButton.isClick(e.X, e.Y))
                        btn = 3;
                    else if (purpleButton.isClick(e.X, e.Y))
                        btn = 4;
                    else
                        btn = 0;
                    break;
                case 2:
                    Form1.labely.Visible = false;
                    Form1.labelz.Visible = false;
                    break;
            }
        }
        
    }
}
