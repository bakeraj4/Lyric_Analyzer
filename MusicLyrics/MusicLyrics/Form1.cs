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
using System.Net;

// WordCount = Dictionary<string, int>

namespace MusicLyrics {

    public partial class Music : Form {

        private string path = Directory.GetCurrentDirectory() + "\\ProcessedBands";
        private DataForForms data;

        public Music() {
            InitializeComponent();
            BandProcessor.init_API_Key();
            if (Directory.Exists(path) && isThereExistingData()) {
                data = new DataForForms(getExisitingBands());
                Exisiting_Band.Enabled = true;
                AddManyBandsToList();
            } else {
                data = new DataForForms();
            }
        }

        private void AddManyBandsToList() {
            string[] exisitngBands = data.getAllBands();
            foreach (string band in exisitngBands) {
                String[] itemAdded = {band};
                BandList.Items.Add(new ListViewItem(itemAdded));
            }
        }

        private bool isThereExistingData() {
            return Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly).Length > 0;
        }

        private ArrayList getExisitingBands() {
            string[] existingBands = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
            ArrayList ret = new ArrayList();
            foreach (string s in existingBands) {
                int indexOfPeriod = s.IndexOf(".");
                int indexOfLastSlash = s.LastIndexOf("\\");
                ret.Add(s.Substring(indexOfLastSlash + "\\".Length, (indexOfPeriod - indexOfLastSlash - 1)));
            }
            return ret;
        }

        private void New_Band_Click(object sender, EventArgs e) {
            New_Band.Enabled = false;
            New_Band.Visible = false;

            Exisiting_Band.Enabled = false;
            Exisiting_Band.Visible = false;

            Enter_Band_Choice.Enabled = true;
            Enter_Band_Choice.Visible = true;

            Enter_Band_Name.Enabled = true;
            Enter_Band_Name.Visible = true;

            pictureBox1.Visible = false;
            pictureBox1.Enabled = false;

            BandList.Visible = true;
            BandList.Enabled = true;

            Display_Button.Enabled = false;
            Display_Button.Visible = false;
        }

        private void Exisiting_Band_Click(object sender, EventArgs e) {
            Exisiting_Band.Enabled = false;
            Exisiting_Band.Visible = false;

            pictureBox1.Visible = false;
            pictureBox1.Enabled = false;

            BandList.Visible = true;
            BandList.Enabled = true;

            Display_Button.Enabled = true;
            Display_Button.Visible = true;
        }

        private void Enter_Band_Choice_Click(object sender, EventArgs e) {
            if (!Enter_Band_Name.Text.Equals("")) {
                // search
                try {
                    //test
                    string band = Enter_Band_Name.Text;
                    string[] albs = BandProcessor.getBandsAlbums(ref band);


                    Enter_Band_Choice.Enabled = false;
                    Enter_Band_Choice.Visible = false;

                    Enter_Band_Name.Enabled = false;
                    Enter_Band_Name.Text = "";
                    Enter_Band_Name.Visible = false;

                    Display_Button.Enabled = true;
                    Display_Button.Visible = true;

                    data.addBand(ref band);
                    string[] itemsAdded = {band};
                    Dictionary<string, int> tmp = BandProcessor.getOverallLyrics(ref band, ref albs);
                    BandList.Items.Add(new ListViewItem(itemsAdded));

                    // go to section as if they chose an existing one
                    // create pop up with tmp as a prameter
                } catch (Exception) {
                    // band doesn't exist
                }
            }
        }

        private void Display_Button_Click(object sender, EventArgs e) {
            if (BandList.SelectedIndices.Count > 0) {
                string chosenBand = data.getAllBands()[BandList.SelectedIndices[0]];
                Dictionary<string, int>[] rawBandsData = BandProcessor.readInBandsData(chosenBand);
                Dictionary<string, int> bandsData = BandProcessor.joinDictionaires(ref rawBandsData);
                PopUp p = new PopUp(bandsData, chosenBand);
                p.ShowDialog();
                // it will close when user hits the close button
            }
        }

    }

    class BandProcessor {

        static WebClient webClient = new WebClient();
        const string breaktag = "<br />";
        const string requestPart1 = "http://ws.audioscrobbler.com/2.0/?method=";
        const string artistRequest = "artist.gettopalbums&artist=";
        const string albumRequestPart1 = "album.getinfo&artist=";
        const string albumRequestPart2 = "&album=";
        static string apiKeyPart;
        private static object LockingVar = new object();

        public static void init_API_Key() {
            apiKeyPart = "&api_key=" + File.ReadAllText(
                Directory.GetCurrentDirectory() + "\\API_KEY.txt");
        }

        public static Dictionary<string, int> joinDictionaires(ref Dictionary<string, int>[] albs) {
            Dictionary<string, int> bandsLyrics = new Dictionary<string, int>();
            foreach (Dictionary<string, int> dict in albs) {
                foreach (KeyValuePair<string, int> p in dict) {
                    try {
                        bandsLyrics[p.Key] += p.Value;
                    } catch (Exception) {
                        bandsLyrics.Add(p.Key, p.Value);
                    }
                }
            }
            return bandsLyrics;
        }

        public static string[] getBandsAlbums(ref string band) {
            return getAlbums(BandProcessor.getRawBandData(ref band));
        }

        private static string[] getAlbumsTracks(ref string band, ref string album) {
            return getTracks(BandProcessor.getRawAlbumData(ref band, ref album));
        }

        private static string[] getAlbums(string response) {
            ArrayList ret = new ArrayList();
            string rest = response;
            const string tag = "<album rank";
            const string nextTag = "<name>";
            const string tagAfter = "</name>";
            int startPt, endPt, index = rest.IndexOf(tag);
            while (index != -1) {
                startPt = rest.IndexOf(nextTag, index) + nextTag.Length;
                endPt = rest.IndexOf(tagAfter, startPt);
                ret.Add(rest.Substring(startPt, (endPt - startPt)));
                rest = rest.Substring(endPt + 1);
                index = rest.IndexOf(tag);
            }
            string[] albums = new string[ret.Count];
            for (int i = 0; i < albums.Length; ++i) {
                albums[i] = (string)ret.ToArray()[i];
            }
            return albums;
        }

        private static string[] getTracks(string response) {
            ArrayList ret = new ArrayList();
            string rest = response;
            const string tag = "<track rank=";
            const string nextTag = "<name>";
            const string tagAfter = "</name>";
            int startPt, endPt, index = rest.IndexOf(tag);
            while (index != -1) {
                startPt = rest.IndexOf(nextTag, index) + nextTag.Length;
                endPt = rest.IndexOf(tagAfter, startPt);
                ret.Add(rest.Substring(startPt, (endPt - startPt)));
                rest = rest.Substring(endPt + 1);
                index = rest.IndexOf(tag);
            }
            string[] tracks = new string[ret.Count];
            for (int i = 0; i < tracks.Length; ++i) {
                tracks[i] = (string)ret.ToArray()[i];
            }
            return tracks;
        }

        private static string getRawBandData(ref string band) {
            lock (LockingVar) {
                return webClient.DownloadString(requestPart1 + artistRequest
                    + band + apiKeyPart);
            }
        }

        private static string getRawAlbumData(ref string band, ref string albumNam) {
            lock (LockingVar) {
                return webClient.DownloadString(requestPart1
                    + albumRequestPart1 + band + albumRequestPart2 + albumNam
                    + apiKeyPart);
            }
        }

        private static void replaceNewLinesWithSpaces(ref string s) {
            s = String.Join(" ", s.Split(
                (new[] { "\n", " ", ".", "!", "?", ",", "\"" }),
                StringSplitOptions.RemoveEmptyEntries));
        }

        private static void removeBreakTags(ref string s) {
            int index = s.IndexOf(breaktag), lower = 0;
            string tmp = "";
            if (index != -1) {
                while (index != -1) {
                    int end = Math.Min(index + breaktag.Length, s.Length - 1);
                    tmp = s.Substring(lower, index) + s.Substring(end);
                    s = tmp;
                    index = s.IndexOf(breaktag);
                }
            }
        }

        static private Dictionary<string, int> mapSongLyics(ref string lyrics) {
            Dictionary<string, int> ret = new Dictionary<string, int>();
            string[] arr = lyrics.Split(" ".ToArray());
            foreach (string ss in arr) {
                if (!ss.Equals("") && !ss.Equals("\n")) {
                    try {
                        ret[ss]++;
                    }
                    catch (Exception) {
                        ret.Add(ss, 1);
                    }
                }
            }
            return ret;
        }

        static private Dictionary<string, int> downloadLyrics(string band, string song) {
            song = song.Replace(" ", "-");
            band = band.Replace(" ", "-");
            string html;
            lock (LockingVar) {
                html = webClient.DownloadString("http://www.lyrics.com/" + song + "-lyrics-" + band + ".html");
            }
            try {
                if (!html.Contains("The URI you submitted has disallowed characters.")) {
                    int start = html.IndexOf("div id=\"lyrics\" class=\"SCREENONLY\" itemprop=\"description\">") + "div id=\"lyrics\" class=\"SCREENONLY\" itemprop=\"description\">".Length;    
                    int end = html.IndexOf("<br />---", start);
                    string content = html.Substring(start, end - start);
                    replaceNewLinesWithSpaces(ref content);
                    removeBreakTags(ref content);
                    content = content.ToLower();
                    return mapSongLyics(ref content);
                } else {
                    return new Dictionary<string,int>();
                }
            } catch (Exception) {
                return new Dictionary<string, int>();
            }
        }

        private static void saveBand(ref string bandName, ref string[] albumNames, ref Dictionary<string, int>[] lyrics) {
            string path = Directory.GetCurrentDirectory() + "\\ProcessedBands"; // \\bandName.txt
            using (var writer = new StreamWriter(path + "\\" + bandName + ".txt")) {
                for (int i = 0; i < albumNames.Length; ++i) {
                    writer.WriteLine(albumNames[i]);
                    foreach (KeyValuePair<string, int> p in lyrics[i]) {
                        writer.WriteLine("\t" + p.ToString());
                    }
                    writer.WriteLine();
                }
            }
        }
        

        private static Dictionary<string, int>[] getAlbLyrics(string band, string alb) {
            ArrayList albThreadPool = new ArrayList();
            string[] tracks = BandProcessor.getAlbumsTracks(ref band, ref alb);
            foreach (string track in tracks) {
                albThreadPool.Add(Task.Factory.StartNew<Dictionary<string, int>>(() => BandProcessor.downloadLyrics(band, track)));
            }
            Dictionary<string, int>[] albsLyrics = new Dictionary<string,int>[tracks.Length];
            int i = 0;
            foreach (Task<Dictionary<string, int>> t in albThreadPool) {
                albsLyrics[i] = t.Result;
                ++i;
            }
            return albsLyrics;
        }

        public static Dictionary<string, int> getOverallLyrics(ref string band, ref string[] albNames) {
            ArrayList bandThreadPool = new ArrayList();
            foreach (string alb in albNames) {
                string lambdaParam = band;
                bandThreadPool.Add(Task.Factory.StartNew<Dictionary<string, int>[]>(() => getAlbLyrics(lambdaParam, alb)));
            }
            Dictionary<string, int>[][] allAlbLyrics = new Dictionary<string,int>[albNames.Length][];
            // join bandThreadPool's threads
            for (int i = 0; i < albNames.Length; ++i) {
                allAlbLyrics[i] = ((Task<Dictionary<string, int>[]>)bandThreadPool[i]).Result;
            }
            Dictionary<string, int>[] ret = new Dictionary<string,int>[albNames.Length];
            for (int i = 0; i < albNames.Length; ++i) {
                // Dict<>[] = {join(alb_0), .... join(abl_n)
                ret[i] = joinDictionaires(ref allAlbLyrics[i]);
            }
            saveBand(ref band, ref albNames, ref ret);
            return joinDictionaires(ref ret);
        }



        public static Dictionary<string, int>[] readInBandsData(string bandName) {
            string path = Directory.GetCurrentDirectory() + "\\ProcessedBands"; // \\bandName.txt
            ArrayList ret = new ArrayList();
            using (var reader = new StreamReader(path + "\\" + bandName + ".txt")) {
                Dictionary<string, int> currDict = new Dictionary<string,int>();
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    if (line.Contains("\t")) { // adds the key value pair
                        int leftBracketIndex = line.IndexOf("[");
                        int commaIndex = line.IndexOf(",");
                        int rightBracketIndex = line.LastIndexOf("]");
                        currDict.Add(
                            line.Substring(
                                leftBracketIndex + 1,
                                (commaIndex - leftBracketIndex - 1)
                            ),
                            int.Parse(
                                line.Substring(commaIndex + 2,
                                (rightBracketIndex - commaIndex - 2))
                            )
                        );
                    } else if (!line.Equals("")) {
                        // next album
                        ret.Add(new Dictionary<string, int>(currDict));
                        currDict = new Dictionary<string,int>();
                    }
                }
            }
            Dictionary<string, int>[] allAlbumsData = new Dictionary<string,int>[ret.Count];
            int i = 0;
            foreach (Object dict in ret) {
                allAlbumsData[i] = (Dictionary<string, int>)dict;
                ++i;
            }
            return allAlbumsData;
        }
    }

    class DataForForms {

        private ArrayList bands = new ArrayList();

        public DataForForms() { }

        public DataForForms(ArrayList givenBands) {
            foreach (Object band in givenBands) {
                bands.Add((string) band);
            }
        }

        public void addBand(ref string bandToAdd) {
            bands.Add(bandToAdd);
        }

        public string[] getAllBands() {
            string[] ret = new string[bands.Count];
            int i = 0;
            foreach (Object band in bands) {
                ret[i] = (string)band;
                ++i;
            }
            return ret;
        }
    }

}
