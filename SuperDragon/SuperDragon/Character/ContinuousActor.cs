using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class ContinuousActor
    {
        public List<AnimateActor> continuousActors;
        string fileFirstName;
        int listNum, distX, distY, r;
        float size;
        Point location;

        Random randomizer;


        public ContinuousActor(string fileFirstName, int listNum, Point location, float size, int distX, int distY,int r)
        {
            continuousActors = new List<AnimateActor>();
            randomizer = new Random();
            this.listNum = listNum;
            this.distX = distX;
            this.distY = distY;
            this.r = r;
            this.size = size;
            this.location = location;
            SetAnimateActorList(continuousActors, fileFirstName, listNum, location, size, distX, distY, r);
        }

        // 隨機背景方法
        private void SetAnimateActorList(List<AnimateActor> animates, string fileFirstName, int listNum, Point center, float size, int distX, int distY, int r) {
            int k1 = 0; // 判斷圖片是否連續出現
            for (int i = 0; i < listNum; i++) {
                int j = new Random().Next(0, listNum);
                while (j == k1) j = new Random().Next(0, listNum);
                k1 = j;
                animates.Add(new AnimateActor(fileFirstName + j, 1, new Point(center.X + i * distX, center.Y + distY * randomizer.Next(0, r)), size));
            }
        }

        public void Paint(Graphics g, Point newLocation) {
            if (continuousActors.Count == 0) {
                SetAnimateActorList(continuousActors, fileFirstName, listNum, newLocation, size, distX, distY, r);
            } else {
                foreach (AnimateActor mt in continuousActors) {
                    mt.Paint(g);
                }
            }
        }

        public void Action(int xRange, Point newCenter, int xSteps, int ySteps) {
            foreach (AnimateActor act in continuousActors) {
                if (act.Center.X <= xRange) {
                    act.ClearImages();
                    act.Center = newCenter;
                }
                act.Move(xSteps, ySteps);
            }
        }
    }
}
