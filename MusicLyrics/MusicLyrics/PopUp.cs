using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace MusicLyrics{        
    public partial class PopUp : Form {

        private string bandName;
        private Dictionary<string, int> data;
        private bool drawn = false;


        public PopUp(Dictionary<string, int> dict, string name)  {
            InitializeComponent();
            data = dict;
            bandName = name;
        }

        private void drawChart() {
            if (!drawn) {
                Color[] topWordsColors = {Color.Crimson, Color.DarkBlue, Color.SaddleBrown, Color.SeaGreen, Color.Teal,
                    Color.DarkGreen, Color.DarkOrange, Color.Maroon, Color.Navy, Color.Sienna };
                Color restColor = Color.Black;
                Graphics graphics = chartArea.CreateGraphics();
                Rectangle r = new Rectangle(0, 0, 200, 200);
                Tuple<string[], int[], int> t = getTopWords(10);
                graphics.Clear(chartArea.BackColor);

                float fDegValue = 0.0f;
                float fDegSum = 0.0f;

                for (int i = 0; i < t.Item2.Length; ++i) {
                    fDegValue = (((float)t.Item2[i]) / t.Item3) * 360.0f;
                    Brush brush = new SolidBrush(topWordsColors[i]);
                    graphics.FillPie(brush, r, fDegSum, fDegValue);
                    fDegSum += fDegValue;
                }
                Brush rBrush = new SolidBrush(restColor);
                graphics.FillPie(rBrush, r, fDegSum, (360.0f - fDegSum));

                float startingX = 210.0f, startingY = 10.0f;

                // print the key
                for (int i = 0; i < t.Item1.Length; ++i, startingY += 15.0f) {
                    graphics.DrawRectangle(new Pen(topWordsColors[i], 10.0f), startingX, startingY, 10, 10);
                    graphics.DrawString(t.Item1[i], new Font(FontFamily.GenericSerif, 9.5f), Brushes.Black, new PointF(startingX + 15.0f, startingY));
                }
                graphics.DrawRectangle(new Pen(Color.Black, 10.0f), startingX, startingY, 10, 10);
                graphics.DrawString("Other Words", new Font(FontFamily.GenericSerif, 9.5f), Brushes.Black, new PointF(startingX + 15.0f, startingY));

                // draw the name
                graphics.DrawString(bandName,  new Font(FontFamily.GenericSerif, 18.0f), Brushes.Black, new PointF(80.0f, 225.0f));

                drawn = true;
            }          
        }

        private int smallestIndex(int[] counts) {
            int index = 0;
            int min = int.MaxValue;
            for (int i = 0; i < counts.Length; ++i) {
                if (counts[i] < min) {
                    min = counts[i];
                    index = i;
                }
            }
            return index;
        }

        private Tuple<string[], int[], int> getTopWords(int num) {
            string[] retStr = new string[num];
            int [] retCnt = new int[num];
            for (int i = 0; i < num; ++i) {
                retStr[i] = "";
                retCnt[i] = 0;
            }
            int total = 0;         
            foreach (KeyValuePair<string, int> p in data) {
                int index = smallestIndex(retCnt);
                if (retCnt[index] < p.Value) {
                    retCnt[index] = p.Value;
                    retStr[index] = p.Key;
                }
                total += p.Value;
            }
            return Tuple.Create(retStr, retCnt, total);
        }

        private void setTrue(object sender, EventArgs e) {
            drawn = false;
            drawChart();
        }

        private void chartArea_Hover(object sender, EventArgs e) {
            drawChart();
        }
    }
}
