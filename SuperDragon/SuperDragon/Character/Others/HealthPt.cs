using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class HealthPt  : AnimateActor
    {
        float a;
        public HealthPt() { }
        public int hp;

        public HealthPt(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size)
        {
            hp = 10;
        }
        public void Health(int num,int max)
        {
            a = max / 10f;

            this.ClearImages();
            switch (num / a)
            {
                case 0: this.AddImage("ProgressBar0", 1); break;
                case 1: this.AddImage("ProgressBar1", 1); break;
                case 2: this.AddImage("ProgressBar2", 1); break;
                case 3: this.AddImage("ProgressBar3", 1); break;
                case 4: this.AddImage("ProgressBar4", 1); break;
                case 5: this.AddImage("ProgressBar5", 1); break;
                case 6: this.AddImage("ProgressBar6", 1); break;
                case 7: this.AddImage("ProgressBar7", 1); break;
                case 8: this.AddImage("ProgressBar8", 1); break;
                case 9: this.AddImage("ProgressBar9", 1); break;
                case 10: this.AddImage("ProgressBar10",1); break;
            }
        }
    }
}
