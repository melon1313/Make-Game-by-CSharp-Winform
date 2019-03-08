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
    class Stage03 {
        //-------------速度控制-------------//
        int jumpt, touch;   // t:每次跳時間
        bool isjump = false;
        bool ispower = false;

        //--------載入遊戲狀態與工具--------//
        int step = 0;
        Music music;
        TurnPage turn;
        CountTime countTime; // 數秒數變數

        //-------------角色宣告-------------//
        AnimateActor river, river2, winroad;
        ContinuousActor background, grid;
        MainCharactor mainCharacter;
        PinkDragon pinkDragon;
        const int yellow_NUM = 2;
        YellowDragon[] yellowDragon;
        HealthPt[] healthPts;

        //-------------說明頁物件-------------//
        AnimateActor explain, ExplainPlayBtn, rNextBtn, lNextBtn;
        int explainPage = 0;


        public Stage03() {
            music = new Music("stage03.mp3");
            turn = new TurnPage();
            countTime = new CountTime();

            river = new AnimateActor("river", 14, new Point(560, 550), 2.3f);
            river2 = new AnimateActor("river", 14, new Point(560, 550), 2.3f);

            background = new ContinuousActor("GridWall", 4, new Point(100, 340), 1f, 880, 100, 0);
            grid = new ContinuousActor("grid", 4, new Point(195, 700), 2, 500, 100, 0);
            winroad = new AnimateActor("finish", 1, new Point(1820, 530), 1.8f);//520到底

            mainCharacter = new MainCharactor("dragon", 3, new Point(420, 430), 8); // size 6
            pinkDragon = new PinkDragon("Pinkdragon", 3, new Point(1990, 550), 6);
            yellowDragon = new YellowDragon[yellow_NUM];
            healthPts = new HealthPt[yellow_NUM];

            for (int i = 0; i < yellow_NUM; i++)
            {
                yellowDragon[i] = new YellowDragon("yellowdragon", 2, new Point(555, 600), 4);
                healthPts[i] = new HealthPt("ProgressBar10", 1, new Point(1200, 500 + 100 * i), 4);
            }

            // 說明頁物件
            explain = new AnimateActor("Stage03explain0", 1, new Point(550, 410), 2);
            ExplainPlayBtn = new AnimateActor("exPlay", 1, new Point(550, 630), 1); // 550 600
            rNextBtn = new AnimateActor("Rnext", 1, new Point(1000, 430), 1.3f);
            lNextBtn = new AnimateActor("graExpLBtn0", 1, new Point(100, 430), 1.3f);
        }

        public void Paint(Graphics g)
        { 
            //---------------- 背景類
            background.Paint(g, new Point(100, 100));
            river.Paint(g);
            grid.Paint(g, new Point(100, 600));

            switch (step)
            {
                case 0:
                    //說明頁
                    if (explain != null && ExplainPlayBtn != null)
                    {
                        explain.Paint(g);
                        
                        // 說明頁切換
                        switch (explainPage)
                        {
                            case 0:
                                mainCharacter.Paint(g);
                                break;
                            case 1:
                                mainCharacter.Paint(g);
                                break;
                            case 2:
                                yellowDragon[0].Paint(g);
                                break;
                        }

                        river.Paint(g);
                        river2.Paint(g);

                        ExplainPlayBtn.Paint(g);
                        rNextBtn.Paint(g);
                        lNextBtn.Paint(g);
                    }
                    break;
                case 1:
                    //---------------- MainCharacter
                    if (yellowDragon[0].Enemys == 0 && yellowDragon[1].Enemys == 0) {
                        winroad.Paint(g);
                        pinkDragon.Paint(g);
                    }
                    mainCharacter.Paint(g);

                    //---------------- Enemys
                    for (int i = 0; i < yellow_NUM; i++) {
                        if (yellowDragon[i] != null) {
                            yellowDragon[i].Paint(g);
                        }
                    }
                    //---------------- healthPts 血條
                    for (int i = 0; i < yellow_NUM; i++) {
                        if (healthPts[i] != null) {
                            healthPts[i].Paint(g);
                        }
                    }

                    if (yellowDragon[0].Enemys != 0 || yellowDragon[1].Enemys != 0) {
                        river2.Paint(g);
                    }
                    break;
            }
           
            //---------------- 換頁效果
            if (step == 0 || step == 2 || step == 3) turn.Paint(g);
        }

        bool isUp;
        bool mainCanJump;

        public void Action() {
            switch (step) {
                //遊戲進場說明
                case 0:
                    Thread.Sleep(100);
                    river.Action();
                    river2.AntiAction();
                    // 說明頁切換
                    switch (explainPage)
                    {
                        case 0:
                            mainCharacter.Action();
                            // mainCharater 動作
                            if (mainCanJump)
                            {
                                if (mainCharacter.Center.Y >= 280 && touch == 0) {
                                    mainCharacter.Move(0, -3 * jumpt);
                                    jumpt++;
                                } else if (mainCharacter.Center.Y <= 430) {
                                    touch = -1;
                                    mainCharacter.Move(0, 3 * jumpt);
                                    jumpt--;
                                    if (mainCharacter.Center.Y > 430) {
                                        mainCanJump = false;
                                        jumpt = 0;
                                        touch = 0;
                                    }
                                }   
                            }
                            
                            break;

                        case 1:
                            mainCharacter.Action();
                            break;

                        case 2:
                            // yellowDragon 動作
                            yellowDragon[0].Action();
                            if(yellowDragon[0].Center.Y <= 350){ isUp = false; }
                            else if(yellowDragon[0].Center.Y >= 600) { isUp = true; }
                            if (isUp){ yellowDragon[0].Move(0, -20); }
                            else if (!isUp){ yellowDragon[0].Move(0, 20); }
                            break;
                    }


                    break;
                //遊戲開始
                case 1:
                    countTime.StartT();
                    Thread.Sleep(90);
                    music.PlayMusic(4);
                    if (yellowDragon[0].Enemys == 0 && yellowDragon[1].Enemys == 0) {

                        if (winroad.Center.X < 520) {
                            pinkDragon.Action();
                            pinkDragon.Move(-4, 0);
                            if (pinkDragon.isClick(mainCharacter.Center.X+50, mainCharacter.Center.Y)) {
                                step = 4;
                            }
                        } else {
                            winroad.Move(-10, 0);
                            pinkDragon.Move(-10, 0);
                        }
                    }

                    // 所有圖片動畫
                    if (winroad.Center.X > 520) {
                        river.Action();
                        river2.AntiAction();
                        grid.Action(-160, new Point(1900, 700), -10, 0);
                        background.Action(-440, new Point(1700, 340), -3, 0);
                    }
                    mainCharacter.Action();

                    //---------------- Enemys
                    for (int i = 0; i < yellow_NUM; i++) {
                        if (yellowDragon[i] != null) {
                            yellowDragon[i].Action();
                            yellowDragon[i].Jump(grid.continuousActors[grid.continuousActors.Count - 1].Center, i);
                            if (yellowDragon[i].isClick(mainCharacter.Center.X, mainCharacter.Center.Y)) {
                                Global.HP -= 1;
                                music.PlayMusic(1);
                                if (Global.HP <= 0) step = 3;
                                mainCharacter.Motion(2);
                            }
                            healthPts[i].Center = new Point(yellowDragon[i].Center.X, yellowDragon[i].Center.Y - 120);
                            healthPts[i].Health(yellowDragon[i].HP, 5);
                        }
                    }

                    // 恐龍是否走在路上
                    touch = 0;
                    for (int i = 0; i < grid.continuousActors.Count; i++) {
                        if (grid.continuousActors[i].isClick(mainCharacter.Center.X - 15, mainCharacter.Center.Y + 60))
                            touch++;
                    }

                    // 恐龍動作(跳/墜落)
                    if (ispower && jumpt > -10) jumpt--;
                    if (isjump) {

                        if (jumpt < 2) {
                            mainCharacter.Move(0, -30 + jumpt);
                        } else {
                            isjump = false;
                            jumpt = 0;
                        }

                        jumpt++;
                    } else if (winroad.Center.X <= 750) {
                        mainCharacter.Center = new Point(mainCharacter.Center.X + 4, 550);

                    } else if (touch == 0) {
                        mainCharacter.GMove(0, 3);
                    }

                    // 恐龍扣血
                    if (mainCharacter.Center.Y > 950) {
                        Global.HP -= 1;
                        music.ContiPlayMusic("drop.wav"); 
                        if (Global.HP <= 0) step = 3;
                        mainCharacter.Motion(2);
                        mainCharacter.Center = new Point(190, 190);
                        mainCharacter.t = 0;
                        isjump = false;
                    }

                    // 計時直到碰見粉紅
                    if (!pinkDragon.isClick(mainCharacter.Center.X, mainCharacter.Center.Y)) {
                        countTime.EndT();
                    }
                    break;

                //遊戲勝場
                case 2:
                    music.StopMusic("cave2.wav");
                    turn.Turn(pageName.mainPage);
                    break;

                //遊戲敗場
                case 3:
                    music.StopMusic("cave2.wav");
                    turn.Turn(pageName.mainPage);
                    break;
                case 4:
                    music.PlayMusic("Pass.mp3");
                    Ranker ranker = new Ranker();
                    ranker.Load();
                    ranker.Clear();
                    ranker.ShowRank(0);
                    step = 2;
                    break;
            }

        }

        public void MouseDown(MouseEventArgs e) {
            switch (step) {
                // 說明頁
                case 0:
                   
                    // 說明頁按鈕切換
                    if (rNextBtn.isClick(e.X, e.Y) && (explainPage == 0 || explainPage == 1))
                    {
                        music.PlayMusic("concern.mp3");
                        explainPage++;
                        rNextBtn.Size = 1.5f;
                    }
                    if (lNextBtn.isClick(e.X, e.Y) && (explainPage == 1 || explainPage == 2))
                    {
                        music.PlayMusic("concern.mp3");
                        explainPage--;
                        lNextBtn.Size = 1.5f;
                    }
                    // 說明頁 子頁 切換
                    switch (explainPage)
                    {
                        case 0:
                            explain.ClearImages();
                            explain.AddImage("Stage03explain0", 1);

                            lNextBtn.ClearImages();
                            lNextBtn.AddImage("graExpLBtn0", 1);

                            rNextBtn.ClearImages();
                            rNextBtn.AddImage("Rnext", 1);

                            // mainCharacter位置重設
                            mainCharacter.Size = 8;
                            mainCharacter.Center = new Point(420, 430);
                            mainCharacter.Motion(0);

                            if(mainCharacter.isClick(e.X, e.Y))
                            {
                                music.PlayMusic("Power.mp3");
                                mainCharacter.Motion(5);
                            }

                            mainCanJump = false;
                            break;

                        case 1:
                            explain.ClearImages();
                            explain.AddImage("Stage03explain1", 1);

                            rNextBtn.ClearImages();
                            rNextBtn.AddImage("Rnext", 1);

                            lNextBtn.ClearImages();
                            lNextBtn.AddImage("Lnext", 1);

                            // mainCharacter位置重設
                            mainCharacter.Size = 8;
                            mainCharacter.Center = new Point(565, 550);
                            mainCharacter.Motion(2);

                            break;
                        case 2:
                            explain.ClearImages();
                            explain.AddImage("Stage03explain2", 1);

                            rNextBtn.ClearImages();
                            rNextBtn.AddImage("graExpRBtn0", 1);

                            lNextBtn.ClearImages();
                            lNextBtn.AddImage("Lnext", 1);

                            // yellowDragon[0]
                            yellowDragon[0].Size = 4;
                            yellowDragon[0].Center = new Point(555, 500);

                            if(yellowDragon[0].isClick(e.X, e.Y))
                            {
                                yellowDragon[0].Motion(1);
                            }


                            break;
                    }

                    if (ExplainPlayBtn.isClick(e.X, e.Y))
                    {
                        music.PlayMusic("concern.mp3");
                        ExplainPlayBtn.Size = 1.2f;

                        // mainCharater 位置、 圖片重設
                        mainCharacter.Size = 6;
                        mainCharacter.Center = new Point(190, 590);
                        mainCharacter.Motion(0);

                        // river 、 river2 位置、圖片重設
                        river.Size = 1;
                        river.Center = new Point(500, 750);
                        river2.Size = 1;
                        river2.Center = new Point(500, 750);

                        // yellowDragon 位置、圖片重設
                        for (int i = 0; i < yellow_NUM; i++)
                        {
                            yellowDragon[i] = new YellowDragon("yellowdragon", 2, new Point(1200, 500 + 100 * i), 3);
                        }

                        step = 1;
                        

                    }


                    break;

                case 1:
                    //攻擊敵人
                    for (int i = 0; i < yellow_NUM; i++) {
                        if (yellowDragon[i].isClick(e.X, e.Y)) {
                            yellowDragon[i].Life();
                            yellowDragon[i].Motion(1);
                        }
                    }
                    //續力開始
                    if (mainCharacter.isClick(e.X, e.Y) && mainCharacter.Center.Y > 545 && touch > 0 && !isjump) {
                        music.PlayMusic("Power.mp3");
                        ispower = true;
                        mainCharacter.Motion(5);
                    }
                    break;
            }
        }

        public void MouseUp(MouseEventArgs e) {
            switch (step) {
                // 說明頁
                case 0:
                    if (ExplainPlayBtn.isClick(e.X, e.Y))
                    {
                        ExplainPlayBtn.Size = 1;
                        explain = null;
                        ExplainPlayBtn = null;
                    }
                    if (rNextBtn.isClick(e.X, e.Y))
                    {
                        rNextBtn.Size = 1.3f;
                    }
                    if (lNextBtn.isClick(e.X, e.Y))
                    {
                        lNextBtn.Size = 1.3f;
                    }

                    // 說明頁 子頁 切換
                    switch (explainPage)
                    {
                        case 0:
                            if (mainCharacter.isClick(e.X, e.Y))
                            {
                                mainCharacter.Motion(0);
                                mainCanJump = true;
                            }
                            break;
                        case 2:
                            if(yellowDragon[0].isClick(e.X, e.Y))
                            {
                                yellowDragon[0].Motion(0);
                            }
                            break;
                    }
                    break;

                case 1:
                    //續力結束
                    ispower = false;
                    //跳躍開始
                    if (mainCharacter.isClick(e.X, e.Y) && mainCharacter.Center.Y > 545 && touch > 0) {
                        isjump = true;
                        mainCharacter.Motion(0);
                    }
                    break;
            }
        }
    }
}
