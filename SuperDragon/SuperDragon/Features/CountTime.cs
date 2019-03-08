using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SuperDragon {
    class CountTime {
        private DateTime time_start;
        private DateTime gettimer;
        private int timestar = 0;
        public void StartT() {
            time_start = DateTime.Now;
        }

        public void EndT() {
            DateTime time_end = DateTime.Now; //計時結束 取得目前累計時間
            string result1 = ((TimeSpan)(time_end - time_start)).TotalMilliseconds.ToString();
            Global.TIME += Convert.ToSingle(result1) / 1000f;
            Form1.labelx.Invoke((Action)delegate { Form1.labelx.Text = Global.TIME.ToString("000.0") + " Sec            " + "              X "+ Global.target; });
            Thread.Sleep(30);
            //return result1;
        }

        public bool Sec(int S) {
            if (timestar == 0) {
                gettimer = DateTime.Now.AddSeconds(S);
                timestar = 1;
            }
            if (DateTime.Now.Second >= gettimer.Second) { timestar = 0; return true; }
            return false;
        }

        public bool MilSec(double S) {
            if (timestar == 0) {
                gettimer = DateTime.Now.AddMilliseconds(S);
                timestar = 1;
            }
            if (DateTime.Now >= gettimer) { timestar = 0; return true; }
            return false;
        }
    }
}
