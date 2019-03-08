using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class TinyDragon : AnimateActor
    {
        int Num;
        public int hp = 10;

        public TinyDragon() { }

        public TinyDragon(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size) {
        }

        public void Motion(int num) {
            if (Num == num) { return; }
            Num = num;
            this.ClearImages();
            switch (num) {
                case 0: this.AddImage("tinyDragon", 3); break;
                case 1: this.AddImage("tinyDragon_burn", 2); break;
            }
        }
    }
}
