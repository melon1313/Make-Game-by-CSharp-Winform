using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class YellowBird : AnimateActor
    {
        int Num;
        public int HP = 2;     //怪物血量(點擊次數)
        public int Enemys = 5;  //怪物總數

        public YellowBird() { }

        public YellowBird(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size){
        }
        //丟石頭套裝
        public bool Throw() {
            if (HP <= 0) { return true; }
            HP--;
            return false;
        }


        //動作套裝
        public void Motion(int num) {
            if (Num == num) { return; }
            Num = num;
            this.ClearImages();
            switch (num) {
                case 0: this.AddImage("YellowBird", 3); break;
                case 1: this.AddImage("YellowBird_Hit", 3); break;
            }

        }
    }
}
