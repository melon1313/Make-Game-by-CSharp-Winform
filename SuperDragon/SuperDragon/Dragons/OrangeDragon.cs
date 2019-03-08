using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class OrangeDragon : AnimateActor
    {
        public int HP = 10;     //怪物血量(點擊次數)
        public int Enemys = 7;  //怪物總數
        public OrangeDragon() { }
        int Num;

        public OrangeDragon(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size)
        { }


        //橘色恐龍生命套裝
        public void Life() {            
            if (Enemys <= 0) { return; }
            if (HP <= 0) { this.Center = new Point(1200, 600); Enemys--; HP = 10; Global.target = Enemys; }
            HP--;
        }

        //橘色恐龍動作套裝
        public void Motion(int num) {
            if(Num == num) { return; }
            Num = num;
            this.ClearImages();
            switch (num) {
                case 0: this.AddImage("orangedragon", 2); break;
                case 1: this.AddImage("orange_throw", 3); break;
                case 2: this.AddImage("orange_hurt", 3); break;
            }
        }
    }
}
