using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SuperDragon
{
    class MainCharactor : AnimateActor
    {
        //private int i = 0; //  Action()方法的圖片計數
        int Num, jumpt;

        //跳躍與續力
        public bool isjump = false;
        public bool ispower = false;

        protected int indexer = 0;
        AnimateActor actor = new AnimateActor();

        public MainCharactor(){}
        public MainCharactor(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size){}

        public void Jump(int speed,int bound) {
            if (ispower && jumpt > -10) jumpt--;
            if (isjump) {
                if (jumpt < 2) {
                    this.Move(0, speed + jumpt);
                } else {
                    isjump = false;
                    jumpt = 0;
                }
                jumpt++;
            } else {
                if (this.Center.Y <= bound)
                    this.GMove(0, 3);
            }
        }

        public void Jump2(int speed, int bound) {
            if (ispower && jumpt > -10) jumpt--;
            if (isjump) {
                if (jumpt < 2) {
                    this.Move(0, speed + jumpt);
                } else {
                    isjump = false;
                    jumpt = 0;
                }
                jumpt++;
            } else {
                if (bound==0)
                    this.GMove(0, 3);
            }
        }

        public void Motion(int num) {
            if(Num == num) { return; }

            Num = num;
            this.ClearImages();

            switch (num) {    
                case 0: this.AddImage("dragon", 3); break;
                case 1: this.AddImage("dragon_shield", 2); break;
                case 2: this.AddImage("dragon_hurt", 3); break;
                case 3: this.AddImage("flydragon", 2);  break;
                case 4: this.AddImage("DragonFire", 2);  break;
                case 5: this.AddImage("dragon_power", 4); break;
                case 6: this.AddImage("flyddragon_hurt", 2); break;
                case 7: this.AddImage("dragon_burn", 3); break;
            }
        }
    }
}
    

