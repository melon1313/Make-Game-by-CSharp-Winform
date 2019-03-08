using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SuperDragon
{
    class Fire : AnimateActor
    {
        bool isLEFT = true;
        private int x, y;

        private float state;
        public float State
        {
            get { return state; }
            set { state = value; }
        }

        public Fire(string fileFirstName, int picturesNum, Point center, float size, float widthsize, float heightsize) : base(fileFirstName, picturesNum, center, size)
        {

        }

        //public Fire(Image image)
        //{
        //    base.img = image,
        //        x y
        //        width height
        //}

        public void shakemouse(MouseEventArgs e, float sizeChange)
        {
            if (e.X > x)//(當滑鼠往右邊移動)
            {
                //判斷是否和當前滑鼠移動的方向相反。如果相反，則產生輸出火力
                if (isLEFT && widthsize <= 1)
                {
                    widthsize += sizeChange;
                    heightsize += sizeChange;
                }
                isLEFT = false; //記錄當前方向(朝右移動)
                x = e.X; //記錄下本次座標
                y = e.Y;
            }
            else if (e.X < x)
            {
                if (!isLEFT && widthsize <= 1)
                {
                    widthsize += sizeChange;
                    heightsize += sizeChange;
                }

                isLEFT = true;
                x = e.X;
                y = e.Y;
            }
            else //相等的時候沒事發生，僅把座標記錄起來
            {
                x = e.X;
                y = e.Y;
            }

        }
    }
}
