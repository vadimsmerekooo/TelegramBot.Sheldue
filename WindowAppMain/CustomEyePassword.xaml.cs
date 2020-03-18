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

namespace WindowAppMain
{
    /// <summary>
    /// Логика взаимодействия для CustomEyePassword.xaml
    /// </summary>
    public partial class CustomEyePassword : UserControl
    {
        public CustomEyePassword()
        {
            InitializeComponent();
            Line.Visibility = Visibility.Visible;
        }
        private void LineOffOn(object sender, MouseButtonEventArgs e)
        {
            if(Line.Visibility == Visibility.Visible)
            {
                Line.Visibility = Visibility.Hidden;
            }
            else
            {
                Line.Visibility = Visibility.Visible;
            }
        }
    }
}
