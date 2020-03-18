using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WindowAppMain
{
    /// <summary>
    /// Логика взаимодействия для ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush Off = Brushes.Black;
        SolidColorBrush On = Brushes.Black;
        private bool Toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        public bool Toggled1 { get { return Toggled; } set { Toggled = value; } }

        DispatcherTimer timerOn = new DispatcherTimer();
        DispatcherTimer timerOff = new DispatcherTimer();
        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                timerOn.Tick += new EventHandler(LeftSiteToggle);
                timerOn.Interval = new TimeSpan(0, 0, 0, 0, 3);
                timerOn.Start();
                Dot.Fill = Brushes.Chartreuse;
            }
            else
            {
                Back.Fill = Off;
                Toggled = false;

                timerOff.Tick += new EventHandler(RightSiteToggle);
                timerOff.Interval = new TimeSpan(0, 0, 0, 0, 3);
                timerOff.Start();
                Dot.Fill = Brushes.White;
            }
        }

        private void RightSiteToggle(object sender, EventArgs e)
        {
            Thickness th = Dot.Margin;
            if (th.Left != -39)
            {
                th.Right += 1;
                th.Left -= 1;
                Dot.Margin = th;
            }
            else
                timerOff.Stop();
        }

        private void LeftSiteToggle(object sender, EventArgs e)
        {
            Thickness th = Dot.Margin;
            if (th.Right != -39)
            {
                th.Left += 1;
                th.Right -= 1;
                Dot.Margin = th;
            }
            else
                timerOn.Stop();
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                timerOn.Tick += new EventHandler(LeftSiteToggle);
                timerOn.Interval = new TimeSpan(0, 0, 0, 0, 30);
                timerOn.Start();
                Dot.Fill = Brushes.Chartreuse;
            }
            else
            {
                Back.Fill = Off;
                Toggled = false;

                timerOff.Tick += new EventHandler(RightSiteToggle);
                timerOff.Interval = new TimeSpan(0, 0, 0, 0, 30);
                timerOff.Start();
                Dot.Fill = Brushes.White;
            }

        }
    }
}
