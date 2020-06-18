using IFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WindowAppMain.Model.Controls;

namespace WindowAppMain.Model.Window.MainWindowPage
{
    public partial class SettingsPage : Page
    {
        string week = string.Empty;
        MainWindow _mWindow;
        private DispatcherTimer StopAnimation = new DispatcherTimer();
        private LoadingAnimation loadedControl;
        Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> allSheldueList;
        public SettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mWindow = mainWindow;
            loadedControl = new LoadingAnimation();
            MainSheldueGrid.Children.Add(loadedControl);
            loadedControl.StartAnimation();
            _mWindow.KindThrowMessage.Foreground = FindResource("WarningForegroundColorUIElements") as SolidColorBrush;
            _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.WarningOutline;
            _mWindow.TextBlockMessageThrow.Text = "Изменение к расписанию загружается! Не закрывайте программу!";
            Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
            sb.Begin();
            Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart)delegate () { LoadSheldueAsyncMethod(); }));
        }
        private async void LoadSheldueAsyncMethod()
        {
            await Task.Run(() => GetSheldueStudent());
            await Task.Run(() => SetSheldueList());
            StopAnimation.Tick += new EventHandler(StopMethodAnimation);
            StopAnimation.Interval = new TimeSpan(0, 0, 1);
            StopAnimation.Start();
        }
        private void SetSheldueList()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate
            {
                ChangeSheldueDay.Text = allSheldueList.Keys.ToList()[0];
                foreach (var item in allSheldueList.Values)
                {
                    foreach (var items in item.Keys)
                    {
                        if (items.Contains(_mWindow._userInfo.Group.Split(' ').ToList()[0]))
                            foreach (var itemss in item[items])
                            {
                                ChangeSheldue.Text = itemss.Day[0].ChangeSheldue;
                            }
                    }
                }
            });
        }
        private void GetSheldueStudent()
        {
            allSheldueList = new Telegram_Bot.BL.Classes.GetSheldueBL().GetChangesSheldue(out week);
        }
        private void StopMethodAnimation(object sender, EventArgs e)
        {
            Storyboard sb = this.FindResource("ShowPage") as Storyboard;
            sb.Begin();
            loadedControl.StopAnimation();
            BlockSheldueGrid.Opacity = 1;
            StopAnimation.Stop();
        }
    }
}
