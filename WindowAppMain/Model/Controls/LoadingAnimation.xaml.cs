using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WindowAppMain.Model.Controls
{
    /// <summary>
    /// Логика взаимодействия для LoadingAnimation.xaml
    /// </summary>
    public partial class LoadingAnimation : UserControl
    {
        DispatcherTimer ellipseLeftTimerUp = new DispatcherTimer();
        DispatcherTimer ellipseRightTimerUp = new DispatcherTimer();
        DispatcherTimer ellipseRightTimerDown = new DispatcherTimer();
        DispatcherTimer ellipseLeftTimerDown = new DispatcherTimer();
        public LoadingAnimation()
        {
            InitializeComponent();
            ellipseRightTimerUp.Tick += new EventHandler(RightAnimationEllipseUp);
            ellipseRightTimerUp.Interval = new TimeSpan(0, 0, 0, 0, 2);
            ellipseRightTimerDown.Tick += new EventHandler(RightAnimationEllipseDown);
            ellipseRightTimerDown.Interval = new TimeSpan(0, 0, 0, 0, 2);
            ellipseLeftTimerUp.Tick += new EventHandler(LeftAnimationEllipseUp);
            ellipseLeftTimerUp.Interval = new TimeSpan(0, 0, 0, 0, 2);
            ellipseLeftTimerDown.Tick += new EventHandler(LeftAnimationEllipseDown);
            ellipseLeftTimerDown.Interval = new TimeSpan(0, 0, 0, 0, 2);
            ControlAnimate();
        }
        private int checkAnimateInt;
        public bool startAnimate = true;
        private void ControlAnimate()
        {
            if (startAnimate)
            {
                ellipseRightTimerUp.Start();
                
            }
        }

        private void LeftAnimationEllipseDown(object sender, EventArgs e)
        {
            if (checkAnimateInt == 20)
            {
                checkAnimateInt = 0;
                EllipseLeft.Fill = System.Windows.Media.Brushes.Chartreuse;
                ellipseRightTimerUp.Start();
                ellipseLeftTimerDown.Stop();
            }
            else
            {
                Thickness th = EllipseLeft.Margin;
                th.Right--;
                th.Left++;
                th.Top++;
                th.Bottom--;
                EllipseLeft.Margin = th;
                checkAnimateInt++;
            }

        }

        private void LeftAnimationEllipseUp(object sender, EventArgs e)
        {
            if (checkAnimateInt == 20)
            {
                checkAnimateInt = 0;
                ellipseLeftTimerDown.Start();
                ellipseLeftTimerUp.Stop();
            }
            else
            {
                EllipseLeft.Fill = System.Windows.Media.Brushes.Lime;
                Thickness th = EllipseLeft.Margin;
                th.Right++;
                th.Left--;
                th.Top--;
                th.Bottom++;
                EllipseLeft.Margin = th;
                checkAnimateInt++;
            }
        }

        private void RightAnimationEllipseDown(object sender, EventArgs e)
        {
            if (checkAnimateInt == 20)
            {
                checkAnimateInt = 0;
                EllipseRight.Fill = System.Windows.Media.Brushes.Chartreuse;
                ellipseLeftTimerUp.Start();
                ellipseRightTimerDown.Stop();
            }
            else
            {
                Thickness th = EllipseRight.Margin;
                th.Right++;
                th.Left--;
                th.Top++;
                th.Bottom--;
                EllipseRight.Margin = th;
                checkAnimateInt++;
            }
        }

        private void RightAnimationEllipseUp(object sender, EventArgs e)
        {
            if (checkAnimateInt == 20)
            {
                checkAnimateInt = 0;
                ellipseRightTimerDown.Start();
                ellipseRightTimerUp.Stop();
            }
            else
            {
                EllipseRight.Fill = System.Windows.Media.Brushes.Lime;
                Thickness th = EllipseRight.Margin;
                th.Right--;
                th.Left++;
                th.Top--;
                th.Bottom++;
                EllipseRight.Margin = th;
                checkAnimateInt++;
            }
        }
    }
}
