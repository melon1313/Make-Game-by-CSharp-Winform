using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace SuperDragon
{
    class BrownDragon : AnimateActor
    {
        CountTime countTime;
        Music music;

        public int hp;
        public AnimateActor brownDragon;

        Pen pen;
        public Point myStartPoint, myEndPoint;

        bool isAttack;
        bool up = true;
        int Num = 0;

        public BrownDragon(){}

        public BrownDragon(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size)
        {
            hp = 100;
            countTime = new CountTime();
            music = new Music();
            brownDragon = new AnimateActor(fileFirstName, picturesNum, center, size);
            pen = new Pen(Color.Red, 5);
            myStartPoint = new Point(860, 510);
            myEndPoint = new Point(860, 510);

            isAttack = false;
        }


        public void _Paint(Graphics g)
        {
            isAttack = true;
            brownDragon.Paint(g);
            if(brownDragon.Center.X <= 950)
            {
                g.DrawLine(pen, myStartPoint, myEndPoint);
            }
        }


        
        public void Fly()
        {
            brownDragon.Action();
            if (brownDragon.Center.X >= 950)
            {
                brownDragon.Center = new Point(brownDragon.Center.X - 5, brownDragon.Center.Y);
            }
            else
            {
                //------ 發射射線
                if(myEndPoint.X >= 0)
                {
                    music.ContiPlayMusic("RaysAttack.mp3");
                    myEndPoint.X -= 50;
                }
                else
                {
                    music.StopMusic("RaysAttack.mp3");
                    myEndPoint.X = 860;
                }
           
                //------ 恐龍上下移動
                if (brownDragon.Center.Y <= 50 )
                {
                    up = false; 
                }

                if (!up)
                {
                    brownDragon.Center = new Point(brownDragon.Center.X, brownDragon.Center.Y + 5);
                    myStartPoint.Y += 5;
                    myEndPoint.Y += 5;
                }

                if (brownDragon.Center.Y >= 650 )
                {
                    up = true;
                }

                if (up)
                {
                    brownDragon.Center = new Point(brownDragon.Center.X, brownDragon.Center.Y - 5);
                    myStartPoint.Y -= 5;
                    myEndPoint.Y -= 5;
                }
            }

        }

        public void Motion(int num)
        {
            if (Num == num) { return; }
            Num = num;
            brownDragon.ClearImages();
            switch (num)
            {
                case 0: brownDragon.AddImage("browndragon", 2); break;
                case 1: brownDragon.AddImage("browndragon_hurt", 3); break;

            }
        }
    }
}
