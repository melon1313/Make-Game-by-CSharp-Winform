using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class Foot
    {
        Actor[] foot = new Actor[11];
        Random randomizer = new Random();

        public Foot()
        {
            //foot
            for (int i = 0; i <= 10; i++)
            {
                int j = randomizer.Next(0, 10); //8
                foot[i] = new Actor("flower" + j, new Point(0, 0), 10); //foot 20
                //foot[i].AddOneImage("foot0" + j + ".png");
                //foot[i].AddOneImage("flower0" + j + ".png");
                if (i % 2 == 0) foot[i].Center = new Point(i * 80, 700 - 25);
                else foot[i].Center = new Point(i * 80, 700);
            }

        }

        public void Paint(Graphics g, Actor actor)
        {
            if (actor.Center.X >= 55) foot[0].Paint(g);
            for (int i = 1; i <= 10; i++) if (actor.Center.X >= (135 + 85 * (i - 1))) foot[i].Paint(g);
        }
    }
}
