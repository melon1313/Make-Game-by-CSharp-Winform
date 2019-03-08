using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon {
    class Button : AnimateActor {

        int Num;

        public Button(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size) {

        }

        public void Motion(int num) {
            if (Num == num) { return; }
            Num = num;
            this.ClearImages();
            switch (num) {
                case 0: this.AddImage("redButton0", 2); break;
                case 1: this.AddImage("redButton1", 1); break;
                case 2: this.AddImage("blueButton0", 2); break;
                case 3: this.AddImage("blueButton1", 1); break;
                case 4: this.AddImage("greenButton0", 2); break;
                case 5: this.AddImage("greenButton1", 1); break;
                case 6: this.AddImage("purpleButton0", 2); break;
                case 7: this.AddImage("purpleButton1", 1); break;

                case 8: this.AddImage("redPage", 1); break;
                case 9: this.AddImage("bluePage", 1); break;
                case 10: this.AddImage("greenPage", 1); break;
                case 11: this.AddImage("purplePage", 1); break;

                case 12: this.AddImage("rank0", 1); break;
                case 13: this.AddImage("rank1", 1); break;
                case 14: this.AddImage("rank2", 1); break;

            }
        }
    }
}
