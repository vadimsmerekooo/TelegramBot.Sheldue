using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WindowAppMain.Classes.WindowAuthClasses;
using WindowAppMain.Model.Controls;

namespace WindowAppMain
{
    /// <summary>
    /// Логика взаимодействия для WindowAppAuthReg.xaml
    /// </summary>
    /// 

    public partial class WindowAppAuthReg : Window
    {
        //InfoUser
        private string Name;
        public string NameUser { get { return Name; } set { Name = value; } }
        public string UserLogin { get { return UserLogin; } }


        //Parametr's
        //Brushes
        private SolidColorBrush DefaultBorder = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private SolidColorBrush Error = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        //Timer's
        private DispatcherTimer timerRegPanel = new DispatcherTimer();
        private DispatcherTimer timerAuthPanel = new DispatcherTimer();
        private DispatcherTimer timerLogin = new DispatcherTimer();
        //New UIElement
        private Label errorLabel = new Label();
        private ErrorLogin uiElementError = new ErrorLogin();
        private OkLogin uiElementOK = new OkLogin();
        private Label userLabel = new Label();
        //Bool params
        private bool toogleButtonCheck = false;
        private bool goCheckLogin = false;
        private bool goStopTimer = false;
        //Control Animate
        private int tenOpacity;

        public WindowAppAuthReg()
        {
            InitializeComponent();
            Keyboard.Focus(TextBoxLoginAuthPanel);
        }
        //ToogleEvent's
        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            toogleButtonCheck = toogleButtonCheck ? false : true;
        }

        //OpenRegPanel
        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            if (PanelAuth.Visibility == Visibility.Visible)
            {
                LineReg.Y1 = 40;
                LineReg.Y2 = 40;
                LineReg.Opacity = 0;
                LineReg.Visibility = Visibility.Visible;
                LineAuth.Visibility = Visibility.Hidden;
                ButtonAuth.Opacity = 0.5;
                ButtonReg.Opacity = 1;
                PanelReg.Opacity = 0;
                PanelReg.Visibility = Visibility.Visible;
                timerRegPanel.Tick += new EventHandler(TimerRegPanelMethod);
                timerRegPanel.Interval = new TimeSpan(0, 0, 0, 0, 30);
                tenOpacity = 0;
                timerRegPanel.Start();
                //ClearTextAligment
                foreach (UIElement item in PanelAuth.Children)
                {
                    if (item is TextBox)
                        (item as TextBox).Text = string.Empty;
                    if (item is PasswordBox)
                        (item as PasswordBox).Password = string.Empty;
                }
                ToggleButtonYesNo.Dot.Margin = new Thickness(-39, 0, 0, 0);
            }
        }
        private void TimerRegPanelMethod(object sender, EventArgs e)
        {
            PanelReg.Opacity += 0.1;
            PanelAuth.Opacity -= 0.1;
            LineReg.Y1 += 1;
            LineReg.Y2 += 1;
            LineReg.Opacity += 0.1;
            tenOpacity++;
            if (tenOpacity == 9)
            {
                PanelAuth.Visibility = Visibility.Hidden;
                timerRegPanel.Stop();
            }
        }
        
        //OpenAuthPanel
        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            if (PanelReg.Visibility == Visibility.Visible)
            {
                LineAuth.Y1 = 40;
                LineAuth.Y2 = 40;
                LineAuth.Opacity = 0;
                LineAuth.Visibility = Visibility.Visible;
                LineReg.Visibility = Visibility.Hidden;
                ButtonReg.Opacity = 0.5;
                ButtonAuth.Opacity = 1;
                PanelAuth.Opacity = 0;
                PanelAuth.Visibility = Visibility.Visible;
                timerAuthPanel.Tick += new EventHandler(TimerAuthPanelMethod);
                timerAuthPanel.Interval = new TimeSpan(0, 0, 0, 0, 30);
                tenOpacity = 0;
                timerAuthPanel.Start();
                //ClearTextAlig
                foreach (UIElement item in PanelReg.Children)
                {
                    if (item is TextBox)
                        (item as TextBox).Text = string.Empty;
                    if (item is PasswordBox)
                        (item as PasswordBox).Password = string.Empty;
                }
                ToggleButtonYesNo.Dot.Margin = new Thickness(-39, 0, 0, 0);

            }
        }
        private void TimerAuthPanelMethod(object sender, EventArgs e)
        {
            PanelAuth.Opacity += 0.1;
            PanelReg.Opacity -= 0.1;
            LineAuth.Y1 += 1;
            LineAuth.Y2 += 1;
            LineAuth.Opacity += 0.1;
            tenOpacity++;
            if (tenOpacity == 9)
            {
                PanelReg.Visibility = Visibility.Hidden;
                timerAuthPanel.Stop();
            }
        }

        //AuthEvent's
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //CheckEmptyText
            if (!String.IsNullOrWhiteSpace(TextBoxLoginAuthPanel.Text) && !String.IsNullOrWhiteSpace(PasswordBoxAuthPanel.Password))
            {
                //StartAnimationLoad
                PanelAnimate.Visibility = Visibility.Visible;
                FillRectangle.Visibility = Visibility.Visible;
                Ellepses.Visibility = Visibility.Visible;
                DispatcherTimer timerSleep = new DispatcherTimer();
                timerSleep.Tick += new EventHandler(timerSleepMethod);
                timerSleep.Interval = new TimeSpan(0, 0, 2);
                timerSleep.Start();
                //DefaultBorderColor
                foreach (UIElement item in PanelAuth.Children)
                {
                    if (item is TextBox)
                        (item as TextBox).BorderThickness = new Thickness(0, 0, 0, 0);
                    if (item is PasswordBox)
                        (item as PasswordBox).BorderThickness = new Thickness(0, 0, 0, 0);
                }
                goStopTimer = true;
                //SQLWork
                CryptAndDecryptPassword cryptPassword = new CryptAndDecryptPassword();
                string query = $"SELECT * FROM UserInfo WHERE Email = '{TextBoxLoginAuthPanel.Text.Trim()}' AND Password = '{cryptPassword.CalculateMD5Hash(PasswordBoxAuthPanel.Password.Trim())}'"; //SQL Query
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionToDBUser"].ConnectionString; //DB Query
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, connectionString);
                DataTable dataTableUser = new DataTable(); //Tabel With User('s)
                sqlAdapter.Fill(dataTableUser);
                if (dataTableUser.Rows.Count == 1)
                {
                    //ClearBox's
                    foreach (UIElement item in PanelAuth.Children)
                    {
                        if (item is TextBox)
                            (item as TextBox).Text = string.Empty;
                        if (item is PasswordBox)
                            (item as PasswordBox).Password = string.Empty;
                    }
                    DataRow[] foundRows;
                    foundRows = dataTableUser.Select();
                    this.Name = foundRows[0][1].ToString(); // Name User
                    //Start Animation Seccessfull Auth
                    goCheckLogin = true;
                }
                else
                {
                    //Start Animation Error Auth
                    goCheckLogin = false;
                }
            }
            //Empty Box's
            else
            {
                //Select Red Border Clear Box
                foreach (var item in PanelAuth.Children)
                {
                    if (item is TextBox)
                    {
                        if (String.IsNullOrWhiteSpace(((TextBox)item).Text))
                        {
                            ((TextBox)item).BorderThickness = new Thickness(1, 1, 1, 1);
                            ((TextBox)item).BorderBrush = Error;
                        }
                        else
                            ((TextBox)item).BorderThickness = new Thickness(0, 0, 0, 0);

                    }
                    if (item is PasswordBox)
                    {
                        if (String.IsNullOrWhiteSpace(((PasswordBox)item).Password))
                        {
                            ((PasswordBox)item).BorderThickness = new Thickness(1, 1, 1, 1);
                            ((PasswordBox)item).BorderBrush = Error;
                        }
                        else
                            ((PasswordBox)item).BorderThickness = new Thickness(0, 0, 0, 0);
                    }
                }
            }
        }
        private void timerSleepMethod(object sender, EventArgs e)
        {
            if (goStopTimer)
            {
                //Seccessfull Auth
                if (goCheckLogin)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                //Error Auth
                else
                {
                    ErrorEntryMethod("Неверный логин\n    или пароль!", "Закрыть");
                }
                goStopTimer = false;
            }
        }
        private void ButtonClose_Click (object sender, RoutedEventArgs e)
        {
            //Close FillRectangle With Animation Auth
            PanelAnimate.Visibility = Visibility.Hidden;
            Ellepses.Visibility = Visibility.Hidden;
            Grid1.Children.Remove(errorLabel);
            errorLabel.Visibility = Visibility.Hidden;
            Grid1.Children.Remove(userLabel);
            userLabel.Visibility = Visibility.Hidden;
            Grid1.Children.Remove(uiElementOK);
            uiElementOK.Visibility = Visibility.Hidden;
            Grid1.Children.Remove(uiElementError);
            uiElementError.Visibility = Visibility.Hidden;
            FillRectangle.Visibility = Visibility.Hidden;
            ButtonClose.Visibility = Visibility.Hidden;
            goStopTimer = false;
            goCheckLogin = false;
            timerLogin.Stop();

        }
        //Method's With Params For Animate
        private void SeccessfullEntryMethod(string textOutput, string contentButton)
        {
            PanelAnimate.Visibility = Visibility.Visible;
            FillRectangle.Visibility = Visibility.Visible;
            Ellepses.Visibility = Visibility.Hidden;
            uiElementOK.Height = 50;
            uiElementOK.Width = 50;
            uiElementOK.Margin = new Thickness(0, -100, 0, 0);
            Grid.SetColumn(uiElementOK, 1);
            Grid.SetRowSpan(uiElementOK, 2);
            Grid1.Children.Add(uiElementOK);
            userLabel.Content = $@"{textOutput}";
            userLabel.FontFamily = new FontFamily("Bahnschrift");
            userLabel.FontSize = 20;
            userLabel.Foreground = Brushes.Chartreuse;
            userLabel.VerticalAlignment = VerticalAlignment.Center;
            userLabel.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(userLabel, 1);
            Grid.SetRowSpan(userLabel, 2);
            Grid1.Children.Add(userLabel);
            ButtonClose.Visibility = Visibility.Visible;
            ButtonClose.Content = contentButton;
        }
        private void ErrorEntryMethod(string textOutput, string contentButton)
        {
            Ellepses.Visibility = Visibility.Hidden;
            uiElementError.Height = 50;
            uiElementError.Width = 50;
            uiElementError.Margin = new Thickness(0, -100, 0,0);
            Grid.SetColumn(uiElementError, 1);
            Grid.SetRowSpan(uiElementError, 2);
            uiElementError.Visibility = Visibility.Visible;
            Grid1.Children.Add(uiElementError);
            errorLabel.Content = textOutput; 
            errorLabel.FontFamily = new FontFamily("Bahnschrift");
            errorLabel.FontSize = 20;
            errorLabel.Foreground = Brushes.Chartreuse;
            errorLabel.VerticalAlignment = VerticalAlignment.Center;
            errorLabel.HorizontalAlignment = HorizontalAlignment.Center;
            errorLabel.Visibility = Visibility.Visible;
            Grid.SetColumn(errorLabel, 1);
            Grid.SetRowSpan(errorLabel, 2);
            Grid1.Children.Add(errorLabel);
            ButtonClose.Visibility = Visibility.Visible;
            ButtonClose.Content = contentButton;
        }
        
        //AuthEventClearBorderTextAndPasswordBoxe's
        private void PasswordBoxAuthPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = e.Source;
            if (item is TextBox)
                ((TextBox)item).BorderBrush = DefaultBorder;
            if (item is PasswordBox)
                ((PasswordBox)item).BorderBrush = DefaultBorder;
        }
        //RegEvent's
        private void ButtonRegPanel_Click(object sender, RoutedEventArgs e)
        {
            //Check Empty Box's
            if (!String.IsNullOrWhiteSpace(TextBoxNameRegPanel.Text) && !String.IsNullOrWhiteSpace(TextBoxEmailRegPanel.Text) && !String.IsNullOrWhiteSpace(PasswordBoxRegPanelOrigin.Password))
            {
                //Call Class IsValidate
                CheckValidateForm checkValidate = new CheckValidateForm();
                if (checkValidate.IsValidateEmail(TextBoxEmailRegPanel.Text))
                {
                    if (checkValidate.IsValidatePassword(PasswordBoxRegPanelOrigin.Password, PasswordBoxRegPanelCopy.Password))
                    {
                        try
                        {
                            //Seccessfull IsValidate Box's
                            //Add User In DB UserInfo
                            FillRectangle.Visibility = Visibility.Visible;
                            Ellepses.Visibility = Visibility.Visible;
                            DispatcherTimer timerAnimate = new DispatcherTimer();
                            timerAnimate.Tick += new EventHandler(timerAnimateMethod);
                            timerAnimate.Interval = new TimeSpan(0, 0, 2);
                            timerAnimate.Start();
                            goStopTimer = true;
                            CryptAndDecryptPassword cryptPassword = new CryptAndDecryptPassword();
                            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionToDBUser"].ConnectionString; //SQL Query DB
                            using (SqlConnection sqlCon = new SqlConnection(connectionString))
                            {
                                sqlCon.Open();
                                SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlCon); //In DB Setting's
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.AddWithValue("@Name", TextBoxNameRegPanel.Text);
                                sqlCmd.Parameters.AddWithValue("@Email", TextBoxEmailRegPanel.Text);
                                sqlCmd.Parameters.AddWithValue("@Password", cryptPassword.CalculateMD5Hash(PasswordBoxRegPanelOrigin.Password));
                                sqlCmd.ExecuteNonQuery();
                            }
                            goCheckLogin = true;
                        }
                        catch 
                        {
                            goCheckLogin = false;
                        }
                    }
                    //Error IsValidate Box's
                    else
                    {
                        if (PasswordBoxRegPanelCopy.Password != PasswordBoxRegPanelOrigin.Password)
                        {
                            PasswordBoxRegPanelCopy.BorderThickness = new Thickness(1, 1, 1, 1);
                            PasswordBoxRegPanelCopy.BorderBrush = Error;
                            ErrorPasswordNotEqual.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            PasswordBoxRegPanelOrigin.BorderThickness = new Thickness(1, 1, 1, 1);
                            PasswordBoxRegPanelOrigin.BorderBrush = Error;
                            ErrorValidPassword.Visibility = Visibility.Visible;
                        }

                    }
                }
                //Error IsValidate E-mail
                else
                {
                    TextBoxEmailRegPanel.BorderThickness = new Thickness(1, 1, 1, 1);
                    TextBoxEmailRegPanel.BorderBrush = Error;
                }
            }
            //Error IsValidate All Box's
            else
            {
                foreach (var item in PanelReg.Children)
                {
                    if (item is TextBox)
                    {
                        if (String.IsNullOrWhiteSpace(((TextBox)item).Text))
                        {
                            ((TextBox)item).BorderThickness = new Thickness(1, 1, 1, 1);
                            ((TextBox)item).BorderBrush = Error;
                        }
                        else
                            ((TextBox)item).BorderThickness = new Thickness(0, 0, 0, 0);

                    }
                    if (item is PasswordBox)
                    {
                        if (String.IsNullOrWhiteSpace(((PasswordBox)item).Password))
                        {
                            ((PasswordBox)item).BorderThickness = new Thickness(1, 1, 1, 1);
                            ((PasswordBox)item).BorderBrush = Error;
                        }
                        else
                            ((PasswordBox)item).BorderThickness = new Thickness(0, 0, 0, 0);
                    }
                }
            }
        }
        private void timerAnimateMethod(object sender, EventArgs e)
        {
            if (goStopTimer)
            {
                //Seccessfull Reg
                if (goCheckLogin)
                {
                    SeccessfullEntryMethod("Вы зарегистрированы!", "Закрыть");
                    ButtonAuth.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                //Error Reg
                else
                {
                    ErrorEntryMethod("Oop's, что-то\n пошло не так:(", "Закрыть");
                }
                goStopTimer = false;
            }
        }
        private void PasswordBoxRegPanel_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ErrorValidPassword.Visibility = Visibility.Hidden;
            ErrorPasswordNotEqual.Visibility = Visibility.Hidden;
        }

        //KeyAuthPanelEvents
        private void TextBoxLoginAuthPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Keyboard.Focus(PasswordBoxAuthPanel);
        }
        private void PasswordBoxAuthPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonGoAuthPanel.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

    }
}
