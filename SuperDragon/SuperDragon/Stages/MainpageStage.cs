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
    class MainpageStage {
        // 角色載入
        Music music;
        TurnPage turn;
        Foot foot;
        MainCharactor dragon;
        Actor background;
        AnimateActor playButton, superragonLogo;

        // 流程判斷
        bool mainPageisStart;
        int super_count, step;

        public MainpageStage() {
            music = new Music("ep2.mp3");
            turn = new TurnPage();
            foot = new Foot();
            background = new Actor("mainpageBackground", new Point(550, 394), 1.5f);
            dragon = new MainCharactor("dragon", 2, new Point(0, 625), 7);
            playButton = new AnimateActor("skip", 1, new Point(1030, 700), 1.2f);
            superragonLogo = new AnimateActor("Logo", 1, new Point(530, -200), 2);

            mainPageisStart = true;
            super_count = 0;
            step = 0;
        }

        public void DrawImages(Graphics g) {
            background.Paint(g);
            foot.Paint(g, dragon);
            dragon.Paint(g);
            playButton.Paint(g);
            superragonLogo.Paint(g);
            turn.Paint(g);
        }

        public void Action() {
            switch (step) {

                //遊戲開始
                case 0:

                    Thread.Sleep(100);
                    dragon.Action();

                    if (dragon.Center.X <= 850) dragon.Move(7, 0);

                    // SuperDragon Logo
                    if (dragon.Center.X >= 500) {
                        if (superragonLogo.Center.Y <= 0) { superragonLogo.Move(0, 500); } else if (superragonLogo.Center.Y == 300 && (super_count == 0 || super_count == 1)) { superragonLogo.Move(0, -30); } else if (superragonLogo.Center.Y == 270 && (super_count == 0)) { superragonLogo.Move(0, 30); super_count++; }
                    }

                    if (!mainPageisStart) step = 1;
                    break;

                //遊戲進場
                case 1:
                    turn.TurnWin(pageName.turntablePage);
                    break;
            }
        }

        //---------- MouseDouwn -------------//
        public void AcotrisClickedByMouseDown(MouseEventArgs e) {
            if (playButton.isClick(e.X, e.Y)) {
                playButton.Size = 1.8f;
                music.PlayMusic("concern.mp3");
                mainPageisStart = false;
            }
        }

        //---------- MouseUp -------------//
        public void AcotrisClickedByMouseUp(MouseEventArgs e) {
            if (playButton.isClick(e.X, e.Y)) {
                
                playButton.Size = 1.2f;
                
            }
        }
    }
}
