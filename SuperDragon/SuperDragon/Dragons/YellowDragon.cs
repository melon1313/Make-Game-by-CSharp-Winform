using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon {
    class YellowDragon : AnimateActor {
        int t1, t2, Num;
        bool isjump = false;
        public int HP = 5;     //怪物血量(點擊次數)
        public int Enemys = 3;  //怪物總數
        public YellowDragon() { }

        public YellowDragon(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size) {

        }

        //恐龍跳躍
        public void Jump(Point grid, int i) {
            if (this.center.Y > 800) isjump = true;
            else if (this.center.Y < 300) isjump = false;

            if (this.center.Y > 650) {
                HP = 5;
                Motion(0);
            }
            if (HP >= 0) {
                if (isjump) {
                    this.center = new Point(grid.X - 260 + 520 * i, this.center.Y - 10 * t1);
                    t2 = 0;
                    t1++;
                } else {
                    this.center = new Point(grid.X - 260 + 520 * i, this.center.Y + 10 * t2);
                    t1 = 0;
                    t2++;
                }
            } else if (grid.X > 1200) {
                HP = 5;
            }
        }

        //恐龍生命
        public void Life() {
            if (HP < 0) { return; }
            if (HP <= 0) { this.Center = new Point(1200, 600); Enemys--; }
            HP--;
        }

        //恐龍動作
        public void Motion(int num) {
            if (Num == num) return;
            Num = num;
            this.ClearImages();
            switch (num) {
                case 0: this.AddImage("yellowdragon", 2); break;
                case 1: this.AddImage("yellowdragon_hurt", 3); break;
            }
        }
    }
}
