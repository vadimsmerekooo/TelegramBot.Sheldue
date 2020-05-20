using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Serialization;
using Telegram_Bot.BL.Classes.App;
using WindowAppMain.Classes.WindowAuthClasses;
using WindowAppMain.Model.Controls;
using IFCore;

namespace WindowAppMain.Model.Window
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthReg.xaml
    /// </summary>
    public partial class WindowAuthReg : System.Windows.Window
    {
        private LoadingAnimation loadedControl;
        private CryptAndDecryptPassword cryptPassword = new CryptAndDecryptPassword();
        private IFCore.Person userInfoList;

        private XmlSerializer serializer = new XmlSerializer(typeof(Person), new XmlRootAttribute() { ElementName = "UserInfo" });


        #region ListPerson
        private List<string> personList = new List<string>()
        {
            "Студент", "Преподаватель"
        };
        #endregion

        #region ListTeacher
        private SortedSet<string> teacherList = new SortedSet<string>()
        {
            "Толочко П.С.", "Киреня О.П.", "Воронко Л.А.",
            "Равбуть Л.А.", "Масько Е.Ч.", "Анисько Р.И.",
            "Котович Н.С.", "Назарчук Т.Н.", "Шмулькштене Е.И.",
            "Лис Н.Н.", "Петрякова Н.С.", "Воропай О.А.", "Алексейченко И.В.",
            "Новик А.И.", "Можейко Е.А.", "Карпович Т.Я.", "Васьков Н.М.",
            "Анищик Р.М.", "Карпович Т.Я.", "Дереченик М.А.", "Кривопуст Е.Е.",
            "Качан Е.И.", "Авдей И.И.", "Лебедь Т.М.", "Левицкая Л.В.",
            "Лавекль Ю.В.", "Романович А.В.", "Шиманович Т.С.", "Воропай О.А.",
            "Шкута О.Г.", "Лис Н.Н.", "Новицкая Ю.В.", "Сакович О.И.", "Ильющеня П.И.",
            "Аблажевич И.В.", "Гоманчук В.К.", "Юхневич Н.И.", "Трайгель В.В.",
            "Самойло Ж.В.", "Гоманчук В.К.", "Чистобаева Н.И.", "Снацкая И.И.", "Лупач О.И."
        };
        #endregion

        #region ListDepartment
        private List<string> departmentList = new List<string>()
        {
            "Информационное отделение"
            //"Швейное отделение", "Электромеханическое отделение", "Отделение машиностроения"
        };
        #endregion

        #region ListGroups
        private List<string> informationDepartmentGroupList = new List<string>()
        {
            "27 тп", "29 тп", "30 тп", "31 тп", "32 тп", "33 тп", "06 шо"
        };

        //private List<string> sewingDepartmentGroupList = new List<string>()
        //{
        //     "05 шо", "30 шо", "29 шо", "11 з", "05 мктт", "06 мктт"
        //};
        //private List<string> elektromechanicDepartmentGroupList = new List<string>()
        //{
        //     "03 эс", "04 эс", "19 опс", "20 опс", "18 опс", "10 эо", "11 эо"
        //};
        //private List<string> machinebuildingDepartmentGroupList = new List<string>()
        //{
        //     "01 эс", "02 эс", "26 тм", "2 м", "1 м", "3 м", "2 от", "03 от"
        //};
        #endregion

        public WindowAuthReg()
        {
            if (CheckCookieUsers())
            {
                try
                {
                    new MainWindow(userInfoList).Show();
                    this.Close();
                }
                catch
                {

                }
            }
            else
            {
                InitializeComponent();
                Storyboard sb = this.FindResource("OpenWindow") as Storyboard;
                sb.Begin();
                Keyboard.Focus(TextBoxLogin);
            }
        }

        public WindowAuthReg(MainWindow mainWindow)
        {
            InitializeComponent();
            Keyboard.Focus(TextBoxLogin);
            mainWindow.Close();
        }

        private bool CreateLoadAnimation(Grid gridName)
        {
            loadedControl = new LoadingAnimation();
            loadedControl.LoadAnimationElement.Height = 70;
            loadedControl.LoadAnimationElement.Width = 70;
            loadedControl.LoadAnimationElement.RingsThickness = 3;
            gridName.Children.Add(loadedControl);
            Grid.SetColumnSpan(loadedControl, 3);
            Grid.SetRowSpan(loadedControl, 2);
            return loadedControl.StartAnimation();
        }
        private void SeccessfulReg()
        {
            Storyboard sb = this.FindResource("InfoPanelAuthReg") as Storyboard;
            sb.Begin();
            KindIconInfoStackPanel.Kind = MaterialDesignThemes.Wpf.PackIconKind.Luck;
            TextBoxInfoStackPanel.FontSize = 14;
            TextBoxInfoStackPanel.Text = "Регистрация прошла успешно!";
            BorderInfoPanelAuthReg.Background = Brushes.Chartreuse;
            Storyboard sbauth = this.FindResource("ClickAuth") as Storyboard;
            sbauth.Begin();
            Keyboard.Focus(TextBoxLogin);
        }
        private void ErrorReg(string textMessagePanel)
        {
            Storyboard sb = this.FindResource("InfoPanelAuthReg") as Storyboard;
            sb.Begin();
            KindIconInfoStackPanel.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
            TextBoxInfoStackPanel.FontSize = 12;
            TextBoxInfoStackPanel.Text = textMessagePanel;
            BorderInfoPanelAuthReg.Background = Brushes.Red;
            BorderTexBoxUser.Background = Brushes.Red;
        }

        //Close Program
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("CloseWindow") as Storyboard;
            sb.Completed += WindowClosed;
            sb.Begin();
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        //Close Program

        //Change TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BorderLoginTextBox.Background = Brushes.White;
            PasswordBoxPassword.Clear();
        }
        //Change TextBox

        //Change PasswordBox
        private void PasswordBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            BorderPasswordPasswordBox.Background = Brushes.White;
        }
        private void PasswordBoxPasswordOrigin_PasswordChanged(object sender, RoutedEventArgs e)
        {
            BorderPasswordBoxUser.Background = Brushes.White;
        }
        private void TextBoxNameser_TextChanged(object sender, TextChangedEventArgs e)
        {
            BorderTexBoxUser.Background = Brushes.White;
            PasswordBoxPasswordOrigin.Password = string.Empty;
            BorderPasswordBoxUser.Background = Brushes.White;
        }
        //Change PasswordBox

        //MouseEventArgs
        //MouseClick Auth and Click Panel
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LineAuth.Opacity == 0)
            {
                Storyboard sb = this.FindResource("ClickAuth") as Storyboard;
                sb.Begin();
                Keyboard.Focus(TextBoxLogin);
            }


        }
        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (LineReg.Opacity == 0)
            {
                Storyboard sb = this.FindResource("ClickReg") as Storyboard;
                sb.Begin();
            }
            PersonComboBox.ItemsSource = null;
            TeacherNameorDepartment.ItemsSource = null;
            GroupListComboBox.ItemsSource = null;
            PersonComboBox.ItemsSource = personList;
            TeacherNameorDepartment.IsEnabled = false;
            GroupListComboBox.IsEnabled = false;
            PasswordBoxPasswordOrigin.IsEnabled = false;
            ButtonReg.IsEnabled = false;

            TextBoxLogin.Text = string.Empty;
            PasswordBoxPassword.Password = string.Empty;
            if (!ToggleButtonYesNo.StateClosed)
                ToggleButtonYesNo.Dot_MouseLeftButtonDown(sender, e);
        }
        //MouseClick Auth and Click Panel

        //Mouse Click Autorization
        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBoxLogin.Text) && !String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
            {
                if (CreateLoadAnimation(MainAuthRegGrid))
                {
                    try
                    {
                        ReferenseDALClass user = new ReferenseDALClass();
                        if (user.SetConnectionDBCheckUser(TextBoxLogin.Text, PasswordBoxPassword.Password))
                        {
                            userInfoList = user.userListInformantion;
                            if (!ToggleButtonYesNo.StateClosed)
                            {
                                SerializeToggleButtonCheck(userInfoList);
                            }
                            else
                            {
                                try { using (StreamWriter sw = new StreamWriter("SET_COOKIEUSER.xml")) { sw.WriteLine(string.Empty); } } catch { }
                            }
                            loadedControl.StopAnimation();
                            new MainWindow(userInfoList).Show();
                            this.Close();
                        }
                        else
                        {
                            loadedControl.StopAnimation();
                            ErrorReg("Неверный логин или пароль!");
                        }
                    }
                    catch
                    {
                        loadedControl.StopAnimation();
                        ErrorReg("Ошибка авторизации!");
                    }
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(TextBoxLogin.Text))
                    BorderLoginTextBox.Background = Brushes.Red;
                if (String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
                    BorderPasswordPasswordBox.Background = Brushes.Red;
            }
        }
        private void SerializeToggleButtonCheck(IFCore.Person userInfo)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("SET_COOKIEUSER.xml"))
                {
                    sw.WriteLine(string.Empty);
                }
                using (FileStream fs = new FileStream("SET_COOKIEUSER.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, userInfo);
                }
            }
            catch
            {

            }
        }
        private bool CheckCookieUsers()
        {
            try
            {
                string dateToday = DateTime.Today.ToShortDateString();
                string dateChangeFile = File.GetLastWriteTime("SET_COOKIEUSER.xml").ToShortDateString();
                if (Convert.ToDateTime(dateToday) > Convert.ToDateTime(dateChangeFile))
                {
                    return false;
                }
                else
                {
                    using (FileStream fs = new FileStream("SET_COOKIEUSER.xml", FileMode.Open))
                    {
                        userInfoList = new Person();
                        userInfoList = (Person)serializer.Deserialize(fs);
                    }
                    return new ReferenseDALClass().SetConnectionDBCheckCOOKIESUser(userInfoList.Login);
                }
            }
            catch
            {
                return false;
            }
        }
        private async void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            CheckValidateForm checkValidate = new CheckValidateForm();
            switch (PersonComboBox.SelectedIndex)
            {
                case 0:
                    if (!String.IsNullOrWhiteSpace(PasswordBoxPasswordOrigin.Password) && !String.IsNullOrWhiteSpace(TextBoxNameUser.Text))
                    {
                        if (checkValidate.IsValidateEmail(TextBoxNameUser.Text))
                        {
                            if (checkValidate.IsValidatePassword(PasswordBoxPasswordOrigin.Password))
                            {
                                CreateLoadAnimation(MainAuthRegGrid);
                                ReferenseDALClass refClassDAL = new ReferenseDALClass();
                                //await Task.Run((Action)delegate () { this.Dispatcher.BeginInvoke((ThreadStart)delegate () { var userExcl = methodsCheckUser.CheckExclusiveUser(TextBoxLogin.Text); }); }).ConfigureAwait(true);
                                bool userExcl = await refClassDAL.SetConnectionDBCheckExcluziveUser(TextBoxNameUser.Text);
                                if (userExcl)
                                {
                                    try
                                    {
                                        IFCore.User userLogin = new IFCore.User()
                                        {
                                            Email = TextBoxNameUser.Text,
                                            Password = cryptPassword.CalculateMD5Hash(PasswordBoxPasswordOrigin.Password).ToString()
                                        };
                                        IFCore.UserInfo userInfo = new IFCore.UserInfo()
                                        {
                                            UserStatus = PersonComboBox.SelectedValue.ToString(),
                                            UserDepartment = TeacherNameorDepartment.SelectedValue.ToString(),
                                            UserGroup = GroupListComboBox.SelectedValue.ToString()
                                        };
                                        await refClassDAL.SetConnectionDBRegUser(userLogin, userInfo);
                                        loadedControl.StopAnimation();
                                        SeccessfulReg();
                                    }
                                    catch (Exception ex)
                                    {
                                        string exs = ex.ToString();
                                        loadedControl.StopAnimation();
                                        ErrorReg("Ошибка регистрации!");
                                    }
                                }
                                else
                                {
                                    loadedControl.StopAnimation();
                                    ErrorReg("Учащийся с таким e-mail, зарегистрирован!");
                                }
                            }
                            else
                            {
                                BorderPasswordBoxUser.Background = Brushes.Red;
                                ErrorReg("Пароль не соотвествует требования!");
                            }
                        }
                        else
                        {
                            BorderTexBoxUser.Background = Brushes.Red;
                            ErrorReg("Логин не соотвествует требования!");
                        }
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(PasswordBoxPasswordOrigin.Password))
                            BorderPasswordBoxUser.Background = Brushes.Red;
                        if (String.IsNullOrWhiteSpace(TextBoxNameUser.Text))
                            BorderTexBoxUser.Background = Brushes.Red;
                    }
                    break;
                case 1:
                    if (!String.IsNullOrWhiteSpace(PasswordBoxPasswordOrigin.Password) && !String.IsNullOrWhiteSpace(TextBoxNameUser.Text))
                    {
                        if (checkValidate.IsValidateEmail(TextBoxNameUser.Text))
                        {
                            if (checkValidate.IsValidatePassword(PasswordBoxPasswordOrigin.Password))
                            {
                                CreateLoadAnimation(MainAuthRegGrid);
                                ReferenseDALClass refClassDAL = new ReferenseDALClass();
                                bool userExcl = await refClassDAL.SetConnectionDBCheckExcluziveUser(TextBoxNameUser.Text);
                                if (userExcl)
                                {
                                    try
                                    {
                                        await refClassDAL.SetConnectionDBRegUser(
                                            new User()
                                            {
                                                Email = TextBoxNameUser.Text,
                                                Password = cryptPassword.CalculateMD5Hash(PasswordBoxPasswordOrigin.Password).ToString()
                                            },
                                            new UserInfo()
                                            {
                                                UserName = TeacherNameorDepartment.SelectedValue.ToString(),
                                                UserStatus = PersonComboBox.SelectedValue.ToString()
                                            });
                                        loadedControl.StopAnimation();
                                        SeccessfulReg();
                                    }
                                    catch
                                    {
                                        loadedControl.StopAnimation();
                                        ErrorReg("Ошибка регистрации!");
                                    }
                                }
                                else
                                {
                                    loadedControl.StopAnimation();
                                    ErrorReg("Данный преподаватель, зарегистрирован!");
                                }
                            }
                            else
                            {
                                BorderPasswordBoxUser.Background = Brushes.Red;
                                ErrorReg("Пароль не соотвествует требования!");
                            }
                        }
                        else
                        {
                            BorderTexBoxUser.Background = Brushes.Red;
                            ErrorReg("Логин не соотвествует требования!");
                        }
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(PasswordBoxPasswordOrigin.Password))
                            BorderPasswordBoxUser.Background = Brushes.Red;
                        if (String.IsNullOrWhiteSpace(TextBoxNameUser.Text))
                            BorderTexBoxUser.Background = Brushes.Red;
                    }
                    break;
            }
        }
        //Mouse Clicl Registation

        //Mouse Click Reset Password
        private void ShowWindowResetPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard sb = this.FindResource("ShowWindowResetPassword") as Storyboard;
            sb.Begin();
            new WindowPasswordReset().ShowDialog();
            Storyboard sbclose = this.FindResource("CloseWindowResetPassword") as Storyboard;
            sbclose.Begin();
        }
        //Mouse Click Reset Password
        //MouseEventArgs

        //Combobox RegGrid Changed
        private void PersonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeacherNameorDepartment.IsEnabled = true;
            TeacherNameorDepartment.ItemsSource = null;

            GroupListComboBox.ItemsSource = null;
            GroupListComboBox.IsEnabled = false;

            PasswordBoxPasswordOrigin.IsEnabled = false;
            PasswordBoxPasswordOrigin.Password = string.Empty;

            TextBoxNameUser.Text = string.Empty;
            TextBoxNameUser.IsEnabled = false;

            ButtonReg.IsEnabled = false;

            switch (PersonComboBox.SelectedIndex)
            {
                case 0:
                    //Default row Password Panel
                    Grid.SetRow(PasswordBoxUser, 4);
                    Grid.SetRow(BorderPasswordBoxUser, 4);

                    //default row textBox Panel
                    Grid.SetRow(TextboxUser, 3);
                    Grid.SetRow(BorderTexBoxUser, 3);
                    TextboxUser.Visibility = Visibility.Visible;
                    BorderTexBoxUser.Visibility = Visibility.Visible;
                    StackPanelGroupList.Visibility = Visibility.Visible;
                    GroupListComboBox.Visibility = Visibility.Visible;
                    GroupListComboBox.IsEnabled = false;

                    StackPanelTeacherOrDep.ToolTip = "Выберите отделение";
                    TeacherNameorDepartment.ItemsSource = departmentList;
                    break;
                case 1:
                    Grid.SetRow(PasswordBoxUser, 3);
                    Grid.SetRow(BorderPasswordBoxUser, 3);
                    TextboxUser.Visibility = Visibility.Visible;
                    BorderTexBoxUser.Visibility = Visibility.Visible;
                    TextboxUser.IsEnabled = true;
                    TextBoxNameUser.IsEnabled = true;
                    Grid.SetRow(TextBoxNameUser, 2);
                    Grid.SetRow(TextboxUser, 2);
                    Grid.SetRow(BorderTexBoxUser, 2);
                    StackPanelGroupList.Visibility = Visibility.Hidden;
                    GroupListComboBox.Visibility = Visibility.Hidden;
                    PasswordBoxPasswordOrigin.IsEnabled = true;
                    ButtonReg.IsEnabled = true;
                    StackPanelTeacherOrDep.ToolTip = "Ваше ФИО";
                    TeacherNameorDepartment.ItemsSource = teacherList;
                    break;
            }
        }
        private void TeacherNameorDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupListComboBox.IsEnabled = true;
            TextBoxNameUser.IsEnabled = false;
            TextBoxNameUser.Text = string.Empty;
            PasswordBoxPasswordOrigin.IsEnabled = false;
            PasswordBoxPasswordOrigin.Password = string.Empty;
            ButtonReg.IsEnabled = false;
            switch (PersonComboBox.SelectedIndex)
            {
                case 0:
                    switch (TeacherNameorDepartment.SelectedIndex)
                    {
                        case 0:
                            GroupListComboBox.ItemsSource = informationDepartmentGroupList;
                            break;
                            //case 1:
                            //    GroupListComboBox.ItemsSource = sewingDepartmentGroupList;
                            //    break;
                            //case 2:
                            //    GroupListComboBox.ItemsSource = elektromechanicDepartmentGroupList;
                            //    break;
                            //case 3:
                            //    GroupListComboBox.ItemsSource = machinebuildingDepartmentGroupList;
                            //    break;
                    }
                    break;
                case 1:

                    TextboxUser.IsEnabled = true;
                    TextBoxNameUser.IsEnabled = true;
                    PasswordBoxPasswordOrigin.IsEnabled = true;
                    ButtonReg.IsEnabled = true;
                    break;
            }
        }
        private void GroupListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxNameUser.IsEnabled = true;
            PasswordBoxPasswordOrigin.IsEnabled = true;
            ButtonReg.IsEnabled = true;
        }
        //Combobox RegGrid Changed
    }
}
