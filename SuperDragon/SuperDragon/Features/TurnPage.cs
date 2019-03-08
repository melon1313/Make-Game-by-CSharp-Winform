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
    class TurnPage {
        public int count = 0;
        //-------------載入角色-------------//
        AnimateActor empty;

        //-------------GameOver-------------//
        AnimateActor gameOver;
        public static bool isGameOver;

        public TurnPage() {
            empty = new AnimateActor("empty", 11, new Point(200, 200), 0.01f);

            // GameOver
            gameOver = new AnimateActor("gameOver", 5, new Point(560, 380), 1.5f);
            isGameOver = false;

        }

        //------------- Paint-------------//
        public void Paint(Graphics g) {
            empty.Paint(g);

            if (isGameOver && count == 11){
                gameOver.Paint(g);
            }
            
        }

        //------------- Thread ( 淡入 )-------------//
        public void TurnGameOver(pageName page) {
            
            isGameOver = true;
            if(count == 12) {
                isGameOver = false;
                Global.page = page;
            }
            else if (count >= 11) {
                Thread.Sleep(50);
                gameOver.Action();
            } else if (count == 0) {
                Form1.NewStage(page);
                Thread.Sleep(40);
                count++;
            } else {
                Thread.Sleep(40);
                empty.Action();
                count++;
            }
        }

        public void TurnWin(pageName page){
            isGameOver = false;
            if (count >= 11) {
               Global.page = page;
            }
            else if (count == 0){
                Form1.NewStage(page);
                Thread.Sleep(40);
                count++;
            }
            else{
                Thread.Sleep(40);
                empty.Action();
                count++;
            }
        }

        //------------- MouseDown -------------//
        public void MouseDown(MouseEventArgs e, pageName page)
        {
            if(gameOver.isClick(e.X, e.Y) && isGameOver)
            {
                isGameOver = false;
                count++;
            }
        }
    }
}
