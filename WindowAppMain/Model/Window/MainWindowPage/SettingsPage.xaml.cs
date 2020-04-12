using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WindowAppMain.Model.Controls;

namespace WindowAppMain.Model.Window.MainWindowPage
{
    public partial class SettingsPage : Page
    {
        private DispatcherTimer StopAnimation = new DispatcherTimer();
        private LoadingAnimation loadedControl;
        public SettingsPage()
        {
            InitializeComponent();
            loadedControl = new LoadingAnimation();
            GridSettings.Children.Add(loadedControl);
            loadedControl.StartAnimation();
            StopAnimation.Tick += new EventHandler(StopAnimate);
            StopAnimation.Interval = new TimeSpan(0, 0, 1);
            StopAnimation.Start();
        }

        private void StopAnimate(object sender, EventArgs e)
        {
            loadedControl.StopAnimation();
            StopAnimation.Stop();
            GridSettings2.Opacity = 1;
        }
    }
}
