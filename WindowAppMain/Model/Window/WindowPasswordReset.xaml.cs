using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WindowAppMain.Classes.WindowAuthClasses;
using WindowAppMain.Model.Controls;
using WindowAppMain.Model.DataBaseEF;

namespace WindowAppMain.Model.Window
{
    /// <summary>
    /// Логика взаимодействия для WindowPasswordReset.xaml
    /// </summary>
    public partial class WindowPasswordReset : System.Windows.Window
    {

        private LoadingAnimation loadedControl;
        private CheckUser methodsCheckUser = new CheckUser();
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
        private void SeccessfulReg()
        {
            //Storyboard sb = this.FindResource("InfoPanelAuthReg") as Storyboard;
            //sb.Begin();
            //KindIconInfoStackPanel.Kind = MaterialDesignThemes.Wpf.PackIconKind.Luck;
            //TextBoxInfoStackPanel.FontSize = 14;
            //TextBoxInfoStackPanel.Text = "Регистрация прошла успешно!";
            //BorderInfoPanelAuthReg.Background = Brushes.Chartreuse;
            //Storyboard sbauth = this.FindResource("ClickAuth") as Storyboard;
            //sbauth.Begin();
            //Keyboard.Focus(TextBoxLogin);
        }
        private void ErrorReg(string textMessagePanel)
        {
            //Storyboard sb = this.FindResource("InfoPanelAuthReg") as Storyboard;
            //sb.Begin();
            //KindIconInfoStackPanel.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
            //TextBoxInfoStackPanel.FontSize = 12;
            //TextBoxInfoStackPanel.Text = textMessagePanel;
            //BorderInfoPanelAuthReg.Background = Brushes.Red;
            //BorderTexBoxUser.Background = Brushes.Red;
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
                        bool userExcl = await methodsCheckUser.CheckExclusiveUser(TextBoxLogin.Text);
                        if (!userExcl)
                        {
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
                    if (ComboBoxPerson.Text == methodsCheckUser.checkUsers[1] && ComboBoxNameOrGroup.Text == methodsCheckUser.checkUsers[2])
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
                        ComboBoxPerson.Background = Brushes.Red;
                        ComboBoxNameOrGroup.Background = Brushes.Red;
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
            if (TextBoxLogin.Text == string.Empty)
                ButtonArrowRight.IsEnabled = false;
        }

        private void ButtonresetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (checkValidate.IsValidatePassword(PasswordBoxNewPassword.Password))
            {
                methodsCheckUser.ChangePasswordUser(TextBoxLogin.Text, PasswordBoxNewPassword.Password);
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
        }

        private void PasswordBoxNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ButtonresetPassword.IsEnabled = true;
        }
    }
}
