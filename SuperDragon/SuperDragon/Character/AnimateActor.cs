using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace SuperDragon
{
    class AnimateActor : Actor
    {
        public List<Image> images = new List<Image>();
        public int picturesNum;
        object taking = new object();
        Image image;

        private int i = 0 ; //  Action()方法的圖片計數
        public int t = 0; // 時間

        protected Dictionary<string, int> movements = new Dictionary<string, int>();
        public float widthsize, heightsize;

        //------------------ AnimateActor 建構子 ------------------//
        public AnimateActor() { }
        public AnimateActor(string fileFirstName, int picturesNum, Point center, float size) : base(fileFirstName, center, size)
        {
            this.picturesNum = picturesNum;
            this.fileFirstName = fileFirstName;
            AddImage(fileFirstName, picturesNum);
        }


        //------------------ 新增、刪除 圖片方法 ------------------//
        public void AddImage(string fileFirstName, int picturesNum) {
            for (int i = 0; i < picturesNum; i++) {
                Bitmap img = new Bitmap(fileFirstName + "0" + i + ".png");
                images.Add(img);
                if (images.Count == 1) base.img = img;
            }
        }

        public void ClearImages() {
            images.Clear();
        }

        //------------------ 圖片動畫 ------------------//
        public void Action() {
            try {
                if (i < images.Count) {
                    base.img = images[i];
                    i++;
                } else
                    i = 0;
            } catch {
                Console.WriteLine("動畫錯誤!");
            }

        }
        //------------------ 圖片動畫 ------------------//
        public void AntiAction() {
            if (i >=0) {
                base.img = images[i];
                i--;
            } else i = images.Count-1;
        }
        //------------------ 圖片翻轉 ------------------//
        public void ReAction() {
            if (i < images.Count) {
                image = images[i].Clone() as Image;
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
                base.img = image;
                i++;
            } else i = 0;
        }

        //---------------------------------------------//

        public void Move(int x, int y) {
            t = 0;
            Center = new Point(center.X + x, center.Y + y);
        }
        public void GMove(int x, int y) {
            t++;
            Center = new Point(center.X + x, center.Y + y*t);
        }


        //----------------- 需移動調整 -----------------//
        public void CloudMove(Point x, int y, int z) {
            lock (taking) {
                center = x;
                if (center.X + y < 960 && center.X + y > 120) {
                    Center = new Point(center.X + y, center.Y + z - 90);
                    t = 0;
                } else {
                    t++;
                    Center = new Point(center.X + y, center.Y + z + 40 * t - 90);
                }
            }
        }

        //------------------ 加入Form1.Paint裡 ------------------//
        public void SetWidthHeight(Graphics g, float widthsize, float heightsize)
        {
            //Pen pen = new Pen(Color.Aqua);
            //g.DrawPolygon(pen, inputpoint1);
            this.widthsize = widthsize;
            this.heightsize = heightsize;
            if (Canpaint) {
                SetRegion();       
                g.DrawImage(img, inputpoint1[0].X, inputpoint1[0].Y, (int)(img.Width * gdiPercentage * widthsize), (int)(img.Height * gdiPercentage * heightsize));
            }
        }
    }
}
