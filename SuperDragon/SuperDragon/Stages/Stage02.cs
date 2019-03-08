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
    class Stage02
    {
        Music music;
        int step = 0;

        AnimateActor pinkBk; // pinBk 為背景, gun 為靶心
        MainCharactor mainCharator; // 主角
        BrownDragon brownDragon; // 大 Boss

        const int TINYDRAGON_NUM = 5; // 小恐龍最大數量
        TinyDragon[] tinyDragons; // 小恐龍的陣列

        const int Fire_MaX_Num = 30; // 火焰最大數量
        AnimateActor[] fires; // 火焰陣列

        const int HEALTHPTS_NUM = 5; // 小恐龍血條最大數量
        HealthPt[] healthPts; // 小恐龍血條陣列

        HealthPt brownDragonHealthPt; // brownDragon 血條
        int brownDragonImgWidth, brownDragonImgHeight;

        ContinuousActor clouds;

        Random randomizer; // 隨機變數
        CountTime countTime; // 數秒數變數
        TurnPage turn; //換頁

        Point cloudsCenter;
        int healthPtCount;
        const int BROWNDRAGON_Max_HP = 100;

        //-------------說明頁物件-------------//
        AnimateActor explain, ExplainPlayBtn, rNextBtn, lNextBtn;

        //-------------- Stage02 建構子 ---------------//
        public Stage02()
        {
            music = new Music("stage2.mp3");

            turn = new TurnPage();
            randomizer = new Random();
            countTime = new CountTime();

            tinyDragons = new TinyDragon[TINYDRAGON_NUM];
            fires = new AnimateActor[Fire_MaX_Num];

            healthPts = new HealthPt[HEALTHPTS_NUM];
            brownDragonHealthPt = new HealthPt("ProgressBar10", 1, new Point(500, 500), 3);
            brownDragonHealthPt.hp = BROWNDRAGON_Max_HP;
            brownDragon = new BrownDragon("browndragon", 2, new Point(1550, 500), 2); //950
            brownDragonImgWidth = brownDragon.brownDragon.img.Width;
            brownDragonImgHeight = brownDragon.brownDragon.img.Height;

            clouds = new ContinuousActor("clouds", 7, new Point(100, 100), 1, 400, 100, 5);

            mainCharator = new MainCharactor("flydragon", 2, new Point(200, 400), 5f);
           
            for (int i = 0; i < TINYDRAGON_NUM; i++)
            {
                tinyDragons[i] = new TinyDragon("tinyDragon", 3, new Point(1000 + 50 * i * randomizer.Next(1, 5), 0 + 50 * i * randomizer.Next(1, 10)), 3);
                healthPts[i] = new HealthPt("ProgressBar10", 1, new Point(500, 500), 6);
            }

            pinkBk = new AnimateActor("pinkBk", 1, new Point(500, 390), 1.5f);
            cloudsCenter = new Point(100, 100);

            healthPtCount = 0; // 記數 : tinyDragon 的死亡數量
            //MessageBox.Show("" + tinyDragons[0].img.Width);

            //------說明頁
            

            if (Global.Mode > 0)
                explain = new AnimateActor("Stage02explain0", 1, new Point(550, 410), 2);
            else
                explain = new AnimateActor("story21", 1, new Point(550, 410), 2);
            ExplainPlayBtn = new AnimateActor("exPlay", 1, new Point(550, 630), 1);    
            rNextBtn = new AnimateActor("Rnext", 1, new Point(1000, 450), 1.3f);
            lNextBtn = new AnimateActor("graExpLBtn0", 1, new Point(100, 450), 1.3f);

            Global.target = 1;
        }

        
        //-------------- 放在 Form1.Paint 裡 ---------------//
        public void Paint(Graphics g)
        {
           
            //------ pinkBackground
            pinkBk.Paint(g);
            if(step == 1)
            {
                //----------------clouds
                clouds.Paint(g, cloudsCenter);

                //------ tinyDragons
                for (int i = 0; i < TINYDRAGON_NUM; i++)
                {
                    if(tinyDragons[i] != null)
                    {
                        tinyDragons[i].Paint(g);
                    }
                }

                //------ healthPts 血條
                for (int i = 0; i < HEALTHPTS_NUM; i++)
                {
                    if(healthPts[i] != null)
                    {
                        healthPts[i].Paint(g);
                    }
                }

                //------ brownDragon 血條
                if(brownDragonHealthPt != null && healthPtCount >= 0)
                {
                    brownDragonHealthPt.Paint(g);
                }

                //------ mainCharator & gun
                if(mainCharator != null)
                {
                    mainCharator.Paint(g);
                }
            
                //------ brownDragon
                if (brownDragon != null && healthPtCount >= 0)
                {
                    brownDragon._Paint(g);
                }

                //------ fires
                for(int i = 0; i < Fire_MaX_Num; i++)
                {
                    if(fires[i] != null)
                    {
                        fires[i].Paint(g);
                    }
                }
            }

            //換頁效果
            if (step == 0 || step == 2 || step == 3) turn.Paint(g);

            //------ 說明頁
            if (explain != null && ExplainPlayBtn != null)
            {
                explain.Paint(g);
                ExplainPlayBtn.Paint(g);
                //rNextBtn.Paint(g);
                //lNextBtn.Paint(g);
            }
        }

        //-------------- 計算兩物件距離 ---------------//
        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        //-------------- 放在 Form1.Animate 裡 ---------------//
        public void Action()
        {
            switch (step) {
                //遊戲進場
                case 0:
                    Thread.Sleep(80);
                    //step = 1;
                    break;
                //遊戲開始
                case 1:
                    countTime.StartT();
                    Thread.Sleep(70);

                    //------ maincharator 被射線打到
                    if (brownDragon != null && brownDragon.brownDragon.Center.X <= 950 && brownDragon.myEndPoint.X <= 300) {
                        if (mainCharator.isClick(brownDragon.brownDragon.Center.X - 800
                            , brownDragon.brownDragon.Center.Y)) {
                            if (countTime.MilSec(1000))
                            {
                                mainCharator.Motion(6);
                                brownDragon.Motion(0);
                                music.PlayMusic(5);
                                Global.HP--;
                            }
                        }
                    }

                    //----------------  clouds
                    cloudsCenter = new Point(1300, 150 + 100 * randomizer.Next(1, 5));
                    clouds.Action(-150, cloudsCenter, -1, 0);


                    //------  mainCharator
                    mainCharator.Action();

                    //------ brownDragon
                    if (brownDragon != null) {
                        brownDragon.Fly();
                    }

                    //--------- fire & progressBar
                    for (int i = 0; i < Fire_MaX_Num; i++) {
                        if (fires[i] != null) {
                            fires[i].Action();
                            //music.PlayMusic("fireAttack.mp3");
                            fires[i].Center = new Point(fires[i].Center.X + 30, fires[i].Center.Y);

                            if (fires[i].Center.X >= 1100) {
                                fires[i] = null;
                            }
                        }
                        if (fires[i] != null) {
                            for (int j = 0; j < TINYDRAGON_NUM; j++) {
                                if (healthPts[j] != null && tinyDragons[j] != null) {
                                    if (tinyDragons[j].isClick(fires[i].Center.X + 10, fires[i].Center.Y)) // 如果小恐龍碰到火焰則...
                                    {
                                        fires[i] = null; // 火焰消失
                                        music.PlayMusic(6); // 小恐龍發出燃燒的聲音
                                        tinyDragons[j].Motion(1); // 小恐龍圖變成燃燒的恐龍圖
                                        tinyDragons[j].hp--; // tinyDragon 被火焰打到 hp 值減一
                                        healthPts[j].hp--;
                                        healthPts[j].Health(tinyDragons[j].hp, 10); // 血條依 tinyDragon 的 hp 值做變化
                                        if (healthPts[j].hp == 0) // 如果血條為 0, 則...
                                        {
                                            healthPts[j] = null; // 血條為 null
                                            tinyDragons[j] = null; // 小恐龍為 null
                                            healthPtCount++;
                                        }
                                        break;
                                    }
                                }

                            }
                        }


                        //------ 火焰打到 brownDragon
                        if (fires[i] != null && brownDragon != null) {   // 如果 brownDragon 被火焰打到, 則...
                            for (int k = 0; k < brownDragonImgHeight / 4; k++) {
                                if (fires[i].isClick(brownDragon.brownDragon.Center.X - (brownDragonImgWidth / 4),
                                    brownDragon.brownDragon.Center.Y - (brownDragonImgHeight / 16) + k) && brownDragon.hp != 0) {
                                    music.PlayMusic(6);
                                    brownDragon.Motion(1); // browmDragon 受傷圖
                                    brownDragon.hp--; // brownDragon 生命值減少
                                    brownDragonHealthPt.hp--; // 血條減少
                                    brownDragonHealthPt.Health(brownDragon.hp, BROWNDRAGON_Max_HP); // 血條依 brownDragon 比例調整
                                    fires[i] = null; // 火焰消失
                                    if (brownDragon.hp == 0) {
                                        brownDragon = null; // 如果 brownDragon 生命值為 0 則設為 null
                                        brownDragonHealthPt = null; // 血條設為 null
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    //-------- 血條跟著 brownDragon
                    if (brownDragonHealthPt != null && brownDragon != null) {
                        brownDragonHealthPt.Center = new Point(brownDragon.brownDragon.Center.X - 80, brownDragon.brownDragon.Center.Y - 100);
                    }


                    //--------- tinyDragon
                    for (int i = 0; i < TINYDRAGON_NUM; i++) {
                        if (healthPts[i] != null && tinyDragons[i] != null) {
                            healthPts[i].Center = new Point(tinyDragons[i].Center.X - 10, tinyDragons[i].Center.Y - 60);
                        }

                        if (tinyDragons[i] != null) {
                            tinyDragons[i].Action();
                            if (mainCharator.isClick(tinyDragons[i].Center.X - 47, tinyDragons[i].Center.Y) && tinyDragons[i].hp != 0) // 利用 size 過濾碰撞條件
                            {
                                if (countTime.Sec(1))
                                {
                                    mainCharator.Motion(6);
                                    Global.HP -= 1;
                                    music.PlayMusic(1);
                                } // 等待一秒再扣 HP
                            }
                        }
                    }

                    //--------- tinyDragon 追蹤 maincharator
                    for (int i = 0; i < TINYDRAGON_NUM; i++) {
                        if (tinyDragons[i] != null) {
                            double dist = Distance(tinyDragons[i].Center, mainCharator.Center); // 計算 tinyDragon 與 maincharator 的距離
                            if (tinyDragons[i].Center.X <= mainCharator.Center.X + 170) {
                                tinyDragons[i].Center = new Point(tinyDragons[i].Center.X - 20, tinyDragons[i].Center.Y);
                            } else {
                                int r = randomizer.Next(8, 20);
                                tinyDragons[i].Center = new Point((int)(tinyDragons[i].Center.X + (mainCharator.Center.X - tinyDragons[i].Center.X) * r / dist),
                                (int)(tinyDragons[i].Center.Y + (mainCharator.Center.Y - tinyDragons[i].Center.Y) * r / dist));
                            }

                            if (tinyDragons[i].Center.X <= -30) {
                                tinyDragons[i].Center = new Point(1000 + 50 * i * randomizer.Next(8, 20), 0 + 50 * i * randomizer.Next(0, 5));
                            }
                        }

                    }

                    // 如果 hp 為 0 則回到 turntablePage
                    if (Global.HP <= 0) {
                        step = 3;
                    }

                    //------ 判斷轉場 進下一關
                    if (brownDragon == null /*&& healthPtCount == 5*/) {
                        Global.target = 0;
                        step = 2;
                    }
                    countTime.EndT();
                    break;
                //遊戲勝場
                case 2:
                    turn.TurnWin(pageName.level02);
                    break;
                //遊戲敗場
                case 3:
                    turn.TurnGameOver(pageName.mainPage);
                    break;
            }
        }

        //-------------- 放在 Form1.MouseMove 裡 ---------------//
        public void MouseMove(MouseEventArgs e)
        {
            switch (step)
            {
                //遊戲開始
                case 1:
                    mainCharator.Center = new Point(mainCharator.Center.X, e.Y);
                    break;
            }
        }

        //int explainPage = 0;
        //-------------- 放在 Form1.MouseDown 裡 ---------------//
        public void MouseDown(MouseEventArgs e)
        {
            switch (step)
            {
                // 說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y)) {
                        music.PlayMusic("concern.mp3");
                        ExplainPlayBtn.Size = 1.2f;
                        explain = null;
                        ExplainPlayBtn = null;
                        step = 1;
                    }
                    break;

                //遊戲開始
                case 1:
                    for (int i = 0; i < Fire_MaX_Num; i++) {
                        if (fires[i] == null) {
                            music.PlayMusicMuti();
                            mainCharator.Motion(4);
                            fires[i] = new AnimateActor("fire", 4, new Point(mainCharator.Center.X + 60, mainCharator.Center.Y - 40), 1.5f);
                            GC.Collect();
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

        //-------------- 放在 Form1.MouseUp 裡 ---------------//
        public void MouseUp(MouseEventArgs e)
        {
            switch (step) {
                //說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y)) {
                        ExplainPlayBtn.Size = 1;
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
                    mainCharator.Motion(3);
                    break;
            }
        }
    }
}
