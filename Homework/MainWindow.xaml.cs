using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Homework {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class ItemOfList {
        public string Word { get; set; }
        public override string ToString() {
            return Word;
        }
    }

    public partial class MainWindow : Window {
        private string[] data;
        private List<ItemOfList> searchResult = new List<ItemOfList>();
        private List<ItemOfList> searchResultLev = new List<ItemOfList>();
        private List<string> words = new List<string>();
        private Stopwatch time;
        private char[] delims = new char[] { '\n', '\r', ' ', '.', ',', '!', '?' };

        public MainWindow() {
            InitializeComponent();
            time = new Stopwatch();
            resultListBox.ItemsSource = searchResult;
            resultListBox5.ItemsSource = searchResultLev;
        }

        private void onReadButton(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "Document";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true) {
                time.Restart();
                data = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8).Split(delims);
                words.Clear();
                foreach (string s in data) {
                    if (s.Trim() != "" && !words.Contains(s)) {
                        words.Add(s);
                    }
                }
                time.Stop();
                readTimeLabel.Content = "Read time: " + time.Elapsed.TotalMilliseconds + " ms";
            }
        }

        private void onSearchButton(object sender, RoutedEventArgs e) {
            string word = searchWord.Text;
            if (words.Count == 0) {
                MessageBox.Show("Read file first");
                return;
            }
            searchResult.Clear();
            time.Restart();
            foreach (string s in words) {
                if (s.ToUpper().Contains(word.ToUpper())) {
                    searchResult.Add(new ItemOfList() { Word = s });
                }
            }
            time.Stop();
            searchTimeLabel.Content = "Search time: " + time.Elapsed.TotalMilliseconds + " ms";
            resultListBox.Items.Refresh();
        }

        private void onSearchButtonLevenshtain(object sender, RoutedEventArgs e) {
            string word = searchWord.Text; int max, threadNumber;
            if (words.Count == 0 || !Int32.TryParse(levMaxValue.Text, out max) || !Int32.TryParse(threadCount.Text, out threadNumber)) {
                MessageBox.Show("Read file first or wrong MaxValue or wrong thread count");
                return;
            }
            time.Restart();
            List<ParallelSearchResult> result = new List<ParallelSearchResult>();
            List<MinMax> arrayDivList = SubArrays.DivideSubArrays(0, words.Count, threadNumber);
            int count = arrayDivList.Count;
            Task<List<ParallelSearchResult>>[] tasks = new Task<List<ParallelSearchResult>>[count];
            for (int i = 0; i < count; i++) {
                List<string> tmpTaskList = words.GetRange(arrayDivList[i].Min, arrayDivList[i].Max - arrayDivList[i].Min);
                tasks[i] = new Task<List<ParallelSearchResult>>(ArrayThreadTask, new ParallelSearchThreadParams()
                {
                    tmpList = tmpTaskList,
                    levMaxValue = max,
                    threadNum = i,
                    searchWord = word
                });
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
            time.Stop();
            for (int i = 0; i < count; i++)
                result.AddRange(tasks[i].Result);

            searchResultLev.Clear();
            foreach (var i in result) {
                searchResultLev.Add(new ItemOfList { Word = i.word });
            }
            searchTimeLabel5.Content = "Search time: " + time.Elapsed.TotalMilliseconds + " ms";
            resultListBox5.Items.Refresh();
        }

        static List<ParallelSearchResult> ArrayThreadTask(object paramObj) {
            ParallelSearchThreadParams param = (ParallelSearchThreadParams)paramObj;
            string tmpStr = param.searchWord.Trim().ToUpper();
            List<ParallelSearchResult> Result = new List<ParallelSearchResult>();

            foreach (string str in param.tmpList) {
                int distance = Lab5.LevDistance.Distance(str.ToUpper(), tmpStr);
                if (distance <= param.levMaxValue) {
                    ParallelSearchResult tmp = new ParallelSearchResult() {
                        word = str,
                        dist = distance,
                        threadCount = param.threadNum
                    };
                    Result.Add(tmp);
                }
            }
            return Result;
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e) {
            string reportFileNameTmp = "Report_" + DateTime.Now.ToString("dd_MM_yyyy_hhmmss");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = reportFileNameTmp;
            saveFileDialog.DefaultExt = ".html";
            saveFileDialog.Filter = "HTML reports (.html)|*.html";

            if (saveFileDialog.ShowDialog() == true) {
                string reportFileName = saveFileDialog.FileName;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("<html>");
                stringBuilder.AppendLine("\t<head>");
                stringBuilder.AppendLine("\t\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
                stringBuilder.AppendLine("\t\t<title>" + "Report: " + reportFileName + "</title>");
                stringBuilder.AppendLine("\t</head>");
                stringBuilder.AppendLine("\t<body>");
                stringBuilder.AppendLine("\t\t<h1>" + "Report: " + reportFileName + "</h1>");
                stringBuilder.AppendLine("\t\t<table border='1'>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Read time(from file)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + readTimeLabel.Content.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Search word</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + searchWord.Text + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Count of unique words in file</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + words.Count.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");


                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Max value for levDistance search</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + levMaxValue.Text + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Time(Levenshtain search)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + searchTimeLabel5.Content.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr>");
                stringBuilder.AppendLine("\t\t\t\t<td>Time(exact search)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>" + searchTimeLabel.Content.ToString() + "</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr valign='top'>");
                stringBuilder.AppendLine("\t\t\t\t<td>Search result(exact search)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>");
                stringBuilder.AppendLine("\t\t\t\t\t<ul>");

                foreach (var i in resultListBox.Items){
                    stringBuilder.AppendLine("\t\t\t\t\t\t<li>" + i.ToString() + "</li>");
                }

                stringBuilder.AppendLine("\t\t\t\t\t</ul>");
                stringBuilder.AppendLine("\t\t\t\t</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t\t<tr valign='top'>");
                stringBuilder.AppendLine("\t\t\t\t<td>Search result(Levenshtain)</td>");
                stringBuilder.AppendLine("\t\t\t\t<td>");
                stringBuilder.AppendLine("\t\t\t\t\t<ul>");

                foreach (var i in resultListBox5.Items)
                {
                    stringBuilder.AppendLine("\t\t\t\t\t\t<li>" + i.ToString() + "</li>");
                }

                stringBuilder.AppendLine("\t\t\t\t\t</ul>");
                stringBuilder.AppendLine("\t\t\t\t</td>");
                stringBuilder.AppendLine("\t\t\t</tr>");

                stringBuilder.AppendLine("\t\t</table>");
                stringBuilder.AppendLine("\t</body>");
                stringBuilder.AppendLine("</html>");

                File.AppendAllText(reportFileName, stringBuilder.ToString());
                MessageBox.Show("Report written to " + reportFileName);
            }
        }
    }
}
