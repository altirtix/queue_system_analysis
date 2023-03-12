using QS.Models.Algorithms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ScottPlot;

namespace QS.Controllers.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowController : Window
    {
        public MainWindowController()
        {
            InitializeComponent();
        }

        QSModel qs;
        DispatcherTimer timer1;

        private void Calculate(object sender, RoutedEventArgs e)
        {
            qs = new QSModel(Convert.ToInt32(NCTextBox.Text),
                Convert.ToDouble(ISHTextBox.Text),
                Convert.ToDouble(ISTTextBox.Text),
                Convert.ToDouble(IHHTextBox.Text),
                Convert.ToDouble(IHTTextBox.Text));
            qs.Calc();
            OutputTextBox.Text = qs.ToString();
            GC.Collect();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timer_Tick1);
            timer1.Interval = new TimeSpan(0, 0, 1);
            timer1.Start();

            DateMenuItem.Header = Controllers.UI.NavBar.Info.GetDate();
            OSMenuItem.Header = Controllers.UI.NavBar.Info.GetOS();
            LocationMenuItem.Header = Controllers.UI.NavBar.Info.GetLocation();
            WANIPMenuItem.Header = Controllers.UI.NavBar.Info.GetWANIP();
            LANIPMenuItem.Header = Controllers.UI.NavBar.Info.GetLANIP();
        }

        private void timer_Tick1(object sender, EventArgs e)
        {
            TimeMenuItem.Header = Controllers.UI.NavBar.Info.GetTime();
            StopwatchMenuItem.Header = Controllers.UI.NavBar.Info.GetStopwatch();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.File.OpenFile();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.File.SaveFile();
        }

        private void UnblockMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Edit.Unblock(this);
        }

        private void BlockMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Edit.Block(this);
        }

        private void ClearMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Edit.Clear(this);
        }

        private void RestartMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Tools.Restart();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Tools.Exit();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, Controllers.UI.NavBar.Help.About(), "Message");
        }
    }
}
