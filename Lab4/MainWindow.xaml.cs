using System;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using Lab5;
using System.Windows.Controls;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] data;
        private List<ItemOfList> searchResult = new List<ItemOfList>();
        private List<ItemOfList> searchResultLev = new List<ItemOfList>();
        private List<string> words = new List<string>();
        private Stopwatch time;
        private char[] delims = new char[] { '\n', '\r', ' ', '.', ',', '!', '?' };
        private bool distanceFlag = true;
        public MainWindow()
        {
            time = new Stopwatch();
            InitializeComponent();
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
                    if (s.Trim() != "" && !words.Contains(s) ) {
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
                if (s.ToUpper().Contains( word.ToUpper() ) ) {
                    searchResult.Add(new ItemOfList() { Word = s });
                }
            }
            time.Stop();
            searchTimeLabel.Content = "Search time: " + time.Elapsed.TotalMilliseconds + " ms";
            resultListBox.Items.Refresh();
        }

        private void onSearchButtonLevenshtain(object sender, RoutedEventArgs e) {
            string word = searchWord.Text; int max;
            if (words.Count == 0 || !Int32.TryParse(levMaxValue.Text, out max)) {
                MessageBox.Show("Read file first or wrong MaxValue");
                return;
            }
            searchResultLev.Clear();
            time.Restart();
            if (distanceFlag)
            {
                foreach (string s in words)
                {
                    if (LevDistance.Distance(s, word) <= max)
                        searchResultLev.Add(new ItemOfList() { Word = s });
                }
            }
            else {
                foreach (string s in words)
                {
                    if (LevDistance.DistanceDameray(s, word) <= max)
                        searchResultLev.Add(new ItemOfList() { Word = s });
                }
            }
           
            time.Stop();
            searchTimeLabel5.Content = "Search time: " + time.Elapsed.TotalMilliseconds + " ms";
            resultListBox5.Items.Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Equals(DamerawDistance))
            {
                distanceFlag = false;
            }
            else
                distanceFlag = true;
        }
    }

    public class ItemOfList {
        public string Word { get; set; }
    }
}
