using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon {
    class PinkDragon : AnimateActor {
        public PinkDragon() { }

        public PinkDragon(string fileFirstName, int picturesNum, Point center, float size)
               : base(fileFirstName, picturesNum, center, size) {
        }
    }
}
