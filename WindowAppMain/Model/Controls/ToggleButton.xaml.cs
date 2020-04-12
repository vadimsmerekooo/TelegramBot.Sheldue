using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WindowAppMain
{
    /// <summary>
    /// Логика взаимодействия для ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        public bool StateClosed = true;
        public ToggleButton()
        {
            InitializeComponent();
            
        }
        public void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("Yes") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("No") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("Yes") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("No") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }
    }
}
