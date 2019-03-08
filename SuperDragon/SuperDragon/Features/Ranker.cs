using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SuperDragon {

    class Ranker {
        public List<Score> rank = new List<Score>();
        string filer = "Game.bin";

        public void Save() {
            StreamWriter rd = new StreamWriter(filer);
            string line;
            for (int i = 0; i < rank.Count; i++) {
                line = rank[i].name + "," + rank[i].mode + "," + rank[i].spand_time;
                rd.WriteLine(line);
            }
            rd.Close();
        }
        public void Load() {
            if (File.Exists(filer)) {
                StreamReader rd = new StreamReader(filer);
                string line;
                string[] items;
                rank.Clear();
                while ((line = rd.ReadLine()) != null) {
                    items = line.Split(',');
                    Score a = new Score();
                    a.name = items[0];
                    a.mode = Convert.ToInt32(items[1]);
                    a.spand_time = Convert.ToSingle(items[2]);
                    rank.Add(a);
                }
                rd.Close();
            }
        }

        public void Clear() {
            Score a = new Score();
            a.name = Global.Name;
            a.mode = Global.Mode;
            a.spand_time = Global.TIME;
            rank.Add(a);
            rank.Sort((b, c) => { return b.spand_time.CompareTo(c.spand_time); });

            //寫檔並歸零
            Save();
            Global.TIME = 0;
        }

        public void ShowRank(int mode) {
            Load();
            //依據遊戲模式顯示排行榜
            switch (mode) { 
                case 0:
                    Load_Rank(0);
                    break;
                case 1:
                    Load_Rank(1);
                    break;
                case 2:
                    Load_Rank(2);
                    break;
            }
        }

        public void Load_Rank(int mode) {
            ResultForm dlg = new ResultForm(rank,mode);
            dlg.ShowDialog();
        }

    }
    class Score {
        public string name;
        public int mode;    // 0: Story Mode ; 1: Stage1 Mode; 2: Stage2 Mode
        public float spand_time;
    }
}
