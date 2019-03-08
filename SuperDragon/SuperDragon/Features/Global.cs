using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class Global
    {
        public static pageName page = pageName.mainPage;
        public static string Name = "None";
        public static int Mode = 0;
        public static int target = 0;
        public static float TIME = 0;
        static int hp = 20;
        public static int HP {
            get { return hp; }
            set { hp = value;}
        }
        static Actor bar;
        static List<Actor> eggs, targetlist;

        static void refresh() {
            eggs = null;
            bar = null;
            bar = new Actor("Bar", new Point(250, 80), 1f);
            eggs = new List<Actor>();
            eggs.Add(new Actor("egg", new Point(120, 50), 8));
            int width = (int)(eggs[0].img.Width / eggs[0].Size) + 13;
            for (int i = 1; i < Global.HP; i++) {
                eggs.Add(new Actor("egg", new Point(120 + width * i, 50), 8));
            }

            targetlist = new List<Actor>();
            for (int i = 0; i < 6; i++) {
                targetlist.Add(new Actor("target" + i, new Point(398, 105), 1.9f));
            }
        }

        public static void setEggs(Graphics g) {
            if (bar == null || eggs == null) refresh();
            bar.Paint(g);
            int m = 0;
            foreach (Actor actor in eggs) {
                if (m < hp) actor.Paint(g);
                m++;
            }
            switch(page) {
                case pageName.level01:
                    targetlist[1].Paint(g);
                    break;
                case pageName.level02:
                    targetlist[4].Paint(g);
                    break;
                case pageName.stage01:
                    targetlist[0].Paint(g);
                    break;
                case pageName.stage02:
                    targetlist[3].Paint(g);
                    break;
                case pageName.stage03:
                    targetlist[5].Paint(g);
                    break;
            }      
        }
    }
}