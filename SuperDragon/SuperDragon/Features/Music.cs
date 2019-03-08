using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices; // 命名空間提供各種支援COM Interop 和平台叫用服務的成員
using WMPLib;
//using System.Media;

namespace SuperDragon {
    class Music {

        int loop;
        static string Playing;
        public static TrackBar track;
        SoundPlayer hurt, fire;

        //一般建構子
        public Music() {
            hurt = new SoundPlayer("hurt.wav");
            hurt.Load();
        }
        //背景音樂建構子
        public Music(string song) {
            mciMusic(Playing, "stop");
            mciMusic(song, "play", "repeat");
            Playing = song;
            hurt = new SoundPlayer("hurt.wav");
            fire = new SoundPlayer("fire.wav");
            hurt.Load();
            fire.Load();
        }
        //預設音量建構子
        public Music(int num) {
            track = new TrackBar();
            track.Orientation = Orientation.Horizontal;
            track.AutoSize = false;
            track.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(68)))), ((int)(((byte)(16)))));
            track.Location = new Point(165, 25);
            track.TickStyle = TickStyle.BottomRight;
            track.Size = new Size(170, 30);
            track.Scroll += new EventHandler(this.trackBar1_Scroll);
            track.Value = num;
        }

        //---- DllImport直接調用以下這些功能 ----//
        //---------------------------------------//
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        /// <summary>
        /// 使用mciSendString播放音樂
        /// </summary>
        /// <param name="name">檔案名稱</param>
        /// <param name="command">命令</param>
        public static void mciMusic(string name, string command) {
            StringBuilder shortpath = new StringBuilder();
            int result = GetShortPathName(name, shortpath, shortpath.Capacity);
            name = shortpath.ToString();
            string buf = string.Empty;

            mciSendString(command + " " + name, buf, buf.Length, 0); //播放 
        }

        private static void mciMusic(string name, string command, string command2) {
            StringBuilder shortpath = new StringBuilder();
            int result = GetShortPathName(name, shortpath, shortpath.Capacity);
            name = shortpath.ToString();
            string buf = string.Empty;

            mciSendString(command + " " + name + " " + command2, buf, buf.Length, 0); //播放 
        }

        /// <summary>
        /// Returns volume from 0 to 10、取得音量
        /// </summary>
        /// <returns>Volume from 0 to 10</returns>
        public static int GetVolume() {
            uint CurrVol = 0;
            waveOutGetVolume(IntPtr.Zero, out CurrVol);
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            int volume = CalcVol / (ushort.MaxValue / 10);
            return volume;
        }

        /// <summary>
        /// Sets volume from 0 to 10 (將音量分割成十等分) 、設定音量
        /// </summary>
        /// <param name="volume">Volume from 0 to 10</param>
        public static void SetVolume(int volume) {
            int NewVolume = ((ushort.MaxValue / 10) * volume);
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        //利用現有的Bar去調整音量。
        private void trackBar1_Scroll(object sender, EventArgs e) {
            SetVolume(track.Value);
        }

        //一般點擊音效
        public void Click() {
            mciMusic(@"decision.mp3", "stop");
            mciMusic(@"decision.mp3", "play");
            //player.controls.play();
        }

        //連續執行音效
        public void PlayMusic(string music) {
            mciMusic(music, "stop");
            mciMusic(music, "play");
        }
        public void PlayMusic(int n) {
            switch (n) {
                case 1:
                    hurt.Play();
                    break;
                case 2:
                    fire.Play();
                    break;
                case 3:
                    PlayMusic("sword1.wav");
                    break;
                case 4:
                    mciMusic("cave2.wav", "play");
                    break;
                case 5:
                    mciMusic("hit.wav", "play");
                    break;
                case 6:
                    mciMusic("punch.mp3", "play");
                    break;
            }

        }
        //中斷執行音效
        public void ContiPlayMusic(string music) {
            mciMusic(music, "play");
        }
        public void StopMusic(string music) {
            mciMusic(music, "stop");
        }
        public void PlayMusicMuti() {
            if (loop > 2) loop = 0;
            mciMusic("fireAttack" + loop + ".mp3", "stop");
            mciMusic("fireAttack" + loop + ".mp3", "play");
            loop++;
        }
    }
}
