using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WindowAppMain.Model.Controls
{
    public partial class LoadingAnimation : UserControl
    {
        public LoadingAnimation()
        {
            InitializeComponent();
        }
        public void StartAnimation()
        {
            Storyboard sb = this.FindResource("LoadAnimate") as Storyboard;
            sb.Begin(); 
        }
        public void StopAnimation()
        {
            Storyboard sb = this.FindResource("StopAnimate") as Storyboard;
            sb.Begin();
        }
    }
}
