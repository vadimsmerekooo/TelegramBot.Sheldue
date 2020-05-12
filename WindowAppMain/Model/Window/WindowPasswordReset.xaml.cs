using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Telegram_Bot.BL.Classes.App;
using WindowAppMain.Classes.WindowAuthClasses;
using WindowAppMain.Model.Controls;

namespace WindowAppMain.Model.Window
{
    /// <summary>
    /// Логика взаимодействия для WindowPasswordReset.xaml
    /// </summary>
    public partial class WindowPasswordReset : System.Windows.Window
    {
        #region Params
        private LoadingAnimation loadedControl;
        private ReferenseDALClass refDALClass = new ReferenseDALClass();
        private int stepNumber = 1;
        private List<string> personList = new List<string>()
        {
            "Студент", "Преподаватель"
        };
        private List<string> teacherList = new List<string>()
        {
            "Толочко П.С.", "Киреня О.П.", "Воронко Л.А."
        };
        private List<string> departmentList = new List<string>()
        {
            "Информационное отделение", "Швейное отделение", "Электромеханическое отделение", "Отделение машиностроения"
        };
        private CheckValidateForm checkValidate = new CheckValidateForm();
        #endregion

        public WindowPasswordReset()
        {
            InitializeComponent();
            ComboBoxPerson.ItemsSource = personList;
            Storyboard sb = this.FindResource("OpenWindow") as Storyboard;
            sb.Begin();
        }
        public void CloseWindowResetPass(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("CloseWindow") as Storyboard;
            sb.Completed += WindowClosed;
            sb.Begin();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            this.Close();
        }


        private void CreateLoadAnimation(Grid gridName)
        {
            loadedControl = new LoadingAnimation();
            loadedControl.LoadAnimationElement.Height = 70;
            loadedControl.LoadAnimationElement.Width = 70;
            loadedControl.LoadAnimationElement.RingsThickness = 3;
            gridName.Children.Add(loadedControl);
            Grid.SetColumnSpan(loadedControl, 3);
            Grid.SetRowSpan(loadedControl, 2);
            loadedControl.StartAnimation();
        }


        public void ClickLeftArrow(object sender, RoutedEventArgs e)
        {
            switch (stepNumber)
            {
                case 2:
                    ButtonArrowLeft.Visibility = Visibility.Hidden;
                    ButtonArrowRight.Visibility = Visibility.Visible;
                    ButtonresetPassword.Visibility = Visibility.Hidden;
                    Storyboard sb2 = this.FindResource("Step1") as Storyboard;
                    sb2.Begin();
                    KindStep1.Foreground = Brushes.Chartreuse;
                    KindStep2.Foreground = Brushes.White;
                    KindStep3.Foreground = Brushes.White;
                    stepNumber--;
                    break;
                case 3:
                    ButtonArrowRight.Visibility = Visibility.Visible;
                    ButtonresetPassword.Visibility = Visibility.Hidden;
                    Storyboard sb3 = this.FindResource("Step2") as Storyboard;
                    sb3.Begin();
                    KindStep1.Foreground = Brushes.White;
                    KindStep2.Foreground = Brushes.Chartreuse;
                    KindStep3.Foreground = Brushes.White;
                    stepNumber--;
                    break;
            }
        }
        public async void ClickRightArrow(object sender, RoutedEventArgs e)
        {
            switch (stepNumber)
            {
                case 1:
                    if (checkValidate.IsValidateEmail(TextBoxLogin.Text))
                    {
                        CreateLoadAnimation(MainGridResetPassword);
                        bool userExcl = await refDALClass.SetConnectionDBCheckExcluziveUser(TextBoxLogin.Text);
                        if (!userExcl)
                        {
                            refDALClass.SetConnectionDBCollectionInformationUser(TextBoxLogin.Text);
                            Storyboard sb1 = this.FindResource("Step2") as Storyboard;
                            sb1.Begin();
                            KindStep1.Foreground = Brushes.White;
                            KindStep2.Foreground = Brushes.Chartreuse;
                            KindStep3.Foreground = Brushes.White;
                            ButtonArrowLeft.Visibility = Visibility.Visible;
                            ButtonArrowRight.IsEnabled = false;
                            stepNumber++;
                            loadedControl.StopAnimation();
                        }
                        else
                        {
                            BorderLoginTextBox.Background = Brushes.Red;
                            loadedControl.StopAnimation();
                        }
                    }
                    else
                    {
                        BorderLoginTextBox.Background = Brushes.Red;
                    }
                    break;
                case 2:
                    switch (ComboBoxPerson.SelectedIndex)
                    {
                        case 0:
                            if (ComboBoxPerson.Text == refDALClass.userListInformantion.Status && ComboBoxNameOrGroup.Text == refDALClass.userListInformantion.Department)
                            {
                                Storyboard sb2 = this.FindResource("Step3") as Storyboard;
                                sb2.Begin();
                                KindStep1.Foreground = Brushes.White;
                                KindStep2.Foreground = Brushes.White;
                                KindStep3.Foreground = Brushes.Chartreuse;
                                stepNumber++;
                                ButtonArrowRight.Visibility = Visibility.Hidden;
                                ButtonresetPassword.Visibility = Visibility.Visible;
                                ButtonresetPassword.IsEnabled = false;
                            }
                            else
                            {
                                ComboBoxPerson.Foreground = Brushes.Red;
                                ComboBoxNameOrGroup.Foreground = Brushes.Red;
                            }
                            break;

                        case 1:
                            if (ComboBoxPerson.Text == refDALClass.userListInformantion.Status && ComboBoxNameOrGroup.Text == refDALClass.userListInformantion.Name)
                            {
                                Storyboard sb2 = this.FindResource("Step3") as Storyboard;
                                sb2.Begin();
                                KindStep1.Foreground = Brushes.White;
                                KindStep2.Foreground = Brushes.White;
                                KindStep3.Foreground = Brushes.Chartreuse;
                                stepNumber++;
                                ButtonArrowRight.Visibility = Visibility.Hidden;
                                ButtonresetPassword.Visibility = Visibility.Visible;
                                ButtonresetPassword.IsEnabled = false;
                            }
                            else
                            {
                                ComboBoxPerson.Foreground = Brushes.Red;
                                ComboBoxNameOrGroup.Foreground = Brushes.Red;
                            }
                            break;
                    }
                    break;
            }
        }

        private void TextBoxLogin_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            BorderLoginTextBox.Background = Brushes.White;
            ButtonArrowRight.IsEnabled = true;
            PasswordBoxNewPassword.Password = string.Empty;
            ComboBoxPerson.SelectedValue = string.Empty;
            TextBlockNameOrGroup.Opacity = 0;
            ComboBoxNameOrGroup.SelectedValue = string.Empty;
            ComboBoxNameOrGroup.Visibility = Visibility.Hidden;
            ComboBoxPerson.Foreground = Brushes.White;
            ComboBoxNameOrGroup.Foreground = Brushes.White;
            if (TextBoxLogin.Text == string.Empty)
                ButtonArrowRight.IsEnabled = false;
        }

        private void ButtonresetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (checkValidate.IsValidatePassword(PasswordBoxNewPassword.Password))
            {
                refDALClass.SetConnectionDBChangePassword(TextBoxLogin.Text, PasswordBoxNewPassword.Password);
                this.Close();
            }
            else
            {
            }
        }

        private void ComboBoxPerson_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TextBlockNameOrGroup.Opacity = 1;
            ComboBoxNameOrGroup.SelectedValue = string.Empty;
            ComboBoxNameOrGroup.Visibility = Visibility.Visible;
            switch (ComboBoxPerson.SelectedIndex)
            {
                case 0:
                    TextBlockNameOrGroup.Text = "Выберите отделение";
                    ComboBoxNameOrGroup.ItemsSource = departmentList;
                    break;
                case 1:
                    TextBlockNameOrGroup.Text = "Ваше имя";
                    ComboBoxNameOrGroup.ItemsSource = teacherList;
                    break;
            }
        }

        private void ComboBoxNameOrGroup_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ButtonArrowRight.IsEnabled = true;
            ComboBoxPerson.Foreground = Brushes.White;
            ComboBoxNameOrGroup.Foreground = Brushes.White;
        }

        private void PasswordBoxNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ButtonresetPassword.IsEnabled = true;
            ComboBoxPerson.Foreground = Brushes.White;
            ComboBoxNameOrGroup.Foreground = Brushes.White;
        }
    }
}
