using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SuperDragon
{
    class Actor
    {
        private static readonly object key = new object();
        //-------------圖片基本設定-------------//
        public string fileFirstName;
        public Image img;
        public bool Canpaint = true;
        private float width, height, size;
        protected Point center;  // 圖片正中心座標設定
        protected float gdiPercentage;
        public float Size {
            set {
                size = value;
                width = img.Width * (gdiPercentage / size);
                height = img.Height * (gdiPercentage / size);
            }
            get { return size; }
        }
        public Point Center {
            get { return center; }
            set { center = value; }
        }

        //-------------中心旋轉-------------//
        private int _angle;
        public int _Angle { get { return _angle; } set { _angle = value; } }
        private Matrix _m1 = new Matrix();
        private Matrix _m0 = new Matrix();

        //--------------Region--------------//
        public Region myRegion;
        private GraphicsPath myGraphicsPath;
        protected Point[] inputpoint1 = new Point[4];
        private float sita;
        public float Sita {
            get { return sita; }
        }

        //-------------- Actor建構子 --------------//
        public Actor() { }
        public Actor(string fileFirstName, Point center, float size) {
            gdiPercentage = 1 / getScalingFactor(); // 螢幕縮放倍率的倒數
            this.fileFirstName = fileFirstName;
            img = new Bitmap(fileFirstName + "00.png");

            // 中心位置隨著 gdi 改變
            this.center = new Point((int)(center.X * gdiPercentage), (int)(center.Y * gdiPercentage));
            this.size = size;
            SetRegion();

            width = img.Width * (gdiPercentage / size);
            height = img.Height * (gdiPercentage / size);
        }

        //-------------- Actor.Paint 在 Form1.Paint 裡重複刷新 --------------//
        public void Paint(Graphics g) {
            lock (key) {
                if (Canpaint) {
                    sita = _angle;
                    SetRegion();

                    _m1.RotateAt(_angle, inputpoint1[0]);
                    g.Transform = _m1;
                    g.DrawImage(img, inputpoint1[0].X, inputpoint1[0].Y, width, height);

                    _m1 = _m0.Clone();
                    g.Transform = _m0;

                    //Pen pen = new Pen(Color.Aqua);
                    //g.DrawPolygon(pen, inputpoint1);
                }
            }
        }
        //-----------------圖片點擊判斷-----------------//  
        public bool isClick(int x, int y) {
            lock (key) {
                try {
                    if (myRegion.IsVisible(x, y)) return true;
                    else return false;
                } catch {
                    Console.WriteLine("範圍錯誤!");
                    return false;
                }
            }
        }

        //--------------------- Region 設定 ---------------------//
        public void SetRegion() {
            lock (key) {
                // 旋轉座標位置
                // x = (x1 - x2)cosθ - (y1 - y2)sinθ + x2 (x1:原始座標點 , x2:中心座標點)
                // y = (y1 - y2)cosθ + (x1 - x2)sinθ + y2
                sita = Convert.ToSingle(_angle * Math.PI / 180);
                // 點擊區域計算
                if (myGraphicsPath == null) myGraphicsPath = new GraphicsPath();
                if (myRegion==null) myRegion = new Region();
                myGraphicsPath.Reset();
                inputpoint1[0] = new Point(Convert.ToInt32(-img.Width * (gdiPercentage / size) / 2.0 * Math.Cos(sita) + img.Height * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.X), Convert.ToInt32(-img.Height * (gdiPercentage / size) / 2.0 * Math.Cos(sita) - img.Width * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.Y));
                inputpoint1[1] = new Point(Convert.ToInt32(-img.Width * (gdiPercentage / size) / 2.0 * Math.Cos(sita) - img.Height * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.X), Convert.ToInt32(img.Height * (gdiPercentage / size) / 2.0 * Math.Cos(sita) - img.Width * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.Y));
                inputpoint1[2] = new Point(Convert.ToInt32(img.Width * (gdiPercentage / size) / 2.0 * Math.Cos(sita) - img.Height * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.X), Convert.ToInt32(img.Height * (gdiPercentage / size) / 2.0 * Math.Cos(sita) + img.Width * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.Y));
                inputpoint1[3] = new Point(Convert.ToInt32(img.Width * (gdiPercentage / size) / 2.0 * Math.Cos(sita) + img.Height * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.X), Convert.ToInt32(-img.Height * (gdiPercentage / size) / 2.0 * Math.Cos(sita) + img.Width * (gdiPercentage / size) / 2.0 * Math.Sin(sita) + center.Y));
                try {
                    myGraphicsPath.AddPolygon(inputpoint1);
                    myRegion.MakeEmpty();
                    myRegion.Union(myGraphicsPath);
                } catch {
                    Console.WriteLine("區域錯誤!");
                }
            }
            
        }

        //--------------------- 計算螢幕GDI ---------------------//
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }
        private float getScalingFactor() {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            float screenWidth = Screen.PrimaryScreen.Bounds.Width;
            float screenHeight = Screen.PrimaryScreen.Bounds.Height;
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            float ScreenScale =  (screenWidth/ screenHeight)/ (1100/768);

            return 1;//ScreenScalingFactor; // 1.25 = 125%
        }
        //------------------------------------------------------//
    }
}
