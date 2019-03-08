using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class BlackDragon : AnimateActor
    {
        int Num;

        public BlackDragon(){}

        public BlackDragon(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size)
        {
            
        }

        public void Motion(int num)
        {
            if (Num == num) { return; }
            Num = num;
            this.ClearImages();
            switch (num)
            {
                case 0: this.AddImage("blackdragon", 2); break;
                case 1: this.AddImage("blackdragon_hurt", 3); break;
            }
        }
    }
}
