using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] data;
        private List<ItemOfList> searchResult;
        private List<string> words;
        private Stopwatch time;

        public MainWindow()
        {
            time = new Stopwatch();
            InitializeComponent();
        }

        private void onReadButton(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "Document";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true) {
                time.Restart();
                data = File.ReadAllText(openFileDialog.FileName).Split(new char[] { '\n', '\r' });
                words = new List<string>();
                foreach (string s in data) {
                    if (s.Trim() != "" && !words.Contains(s))
                    {
                        words.Add(s);
                    }
                }
                time.Stop();
                readTimeLabel.Content = "Read time: " + time.Elapsed.TotalMilliseconds + " ms";
            }
        }

        private void onSearchButton(object sender, RoutedEventArgs e) {
            string word = searchWord.Text;
            if (words == null) {
                MessageBox.Show("Read file first");
                return;
            }
            searchResult = new List<ItemOfList>();
            time.Restart();
            foreach (string s in words) {
                if (s.Contains(word)) {
                    searchResult.Add(new ItemOfList() { Word = s });
                }
            }
            time.Stop();
            searchTimeLabel.Content = "Search time: " + time.Elapsed.TotalMilliseconds + " ms";
            resultListBox.ItemsSource = searchResult;
        }
    }

    public class ItemOfList {
        public string Word { get; set; }
    }
}
