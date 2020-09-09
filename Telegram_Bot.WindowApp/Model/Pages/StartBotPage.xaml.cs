using IFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Telegram.Bot;
using Telegram_Bot.View;
using Telegram_Bot.View.Classes.Menu;

namespace Telegram_Bot.WindowApp.Model.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartBotPage.xaml
    /// </summary>
    public partial class StartBotPage : Page
    {

        bool selected_TelegramBot;
        bool selected_VkBot_Working;
        bool selected_ViberBot_Working;

        public StartBotPage(string bot)
        {
            InitializeComponent();
            switch (bot)
            {
                case nameof(MainWindow.TelegramBot_Working):
                    SetTokensBot(Path.GetFullPath("Configure_Files/TelegramBot_Files/api_key_bot.dat"));
                    selected_TelegramBot = true;
                    break;
                case nameof(MainWindow.VkBot_Working):
                    SetTokensBot("Configure_Files/VkBot_Files/api_key_bot.dat");
                    selected_VkBot_Working = true;
                    break;
                case nameof(MainWindow.ViberBot_Working):
                    SetTokensBot("Configure_Files/ViberBot_Files/api_key_bot.dat");
                    selected_ViberBot_Working = true;
                    break;
            }
        }

        private void ButtonStartBot_Click(object sender, RoutedEventArgs e)
        {
            StartupBot();
        }
        private void StartupBot()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
            {
                if (selected_TelegramBot)
                {
                    if (TokensBotComboBox.SelectedIndex == -1)
                        return;

                    MainWindow._mWindow.ShowSuccessfulMessage("Загрузка расписания.");

                    if (!File.Exists("SheldueList.xml"))
                    {
                        MainWindow._mWindow.ShowErrorMessage("Файл с расписанием, не найден!!!");
                        return;
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(List<IFCore.GetSheldueDic>), new XmlRootAttribute() { ElementName = "DictionarySerSheldueTelegram" });

                    using (FileStream fs = new FileStream("SheldueList.xml", FileMode.Open))
                    {
                        MainWindow.allSheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
                        var deserlist = (List<IFCore.GetSheldueDic>)serializer.Deserialize(fs);
                        foreach (var item in deserlist)
                        {
                            MainWindow.allSheldue.Add(item.Name, item.Sheldue);
                        }
                    }
                    if (MainWindow.allSheldue is null)
                    {
                        MainWindow._mWindow.ShowErrorMessage("Файл с расписанием пуст!!!");
                        return;
                    }
                    MainWindow.allSheldueCopy = MainWindow.allSheldue;
                    if (MainWindow.allSheldue != null)
                    {
                        MainWindow._mWindow.ShowSuccessfulMessage("Расписание загружено.");
                        MainWindow._mWindow.ShowSuccessfulMessage("Загрузка файла, изменение к расписанию, с сайта.");
                        MainWindow.changeSheldue = new View.Classes.GetShelduePL().GetChangesSheldue(out MainWindow.weekCheck);
                        if (MainWindow.changeSheldue != null)
                        {
                            MainWindow.allSheldue = ChangeMainSheldueWithNewSheldue(MainWindow.allSheldue, MainWindow.changeSheldue);
                            MainWindow._mWindow.ShowSuccessfulMessage("Изменение к расписанию загружено.");
                        }
                        else
                        {
                            MainWindow._mWindow.ShowErrorMessage("Изменение к расписанию не загружено.");
                        }
                        TelegramBotStartup();

                    }
                    else
                    {
                        MainWindow._mWindow.ShowErrorMessage("Расписание не загружено. Бот не будет запущен!");
                    }
                }
            });
        }


        private void SetTokensBot(string path_File)
        {
            TokensBotComboBox.ItemsSource = File.ReadAllLines(path_File);
        }
        private static MainMenu menuLibriary = null;
        private async void TelegramBotStartup()
        {
            if (TokensBotComboBox.SelectedIndex == -1)
                return;

            string apiToken = TokensBotComboBox.SelectedValue.ToString().Trim();
            TelegramBotClient BotRoma = new TelegramBotClient(apiToken);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            menuLibriary = new MainMenu(BotRoma, apiToken, ref MainWindow.allSheldue);
            menuLibriary.SetWeek = MainWindow.weekCheck;
            if (menuLibriary.bw.IsBusy != true)
            {
                try
                {
                    BotRoma = new TelegramBotClient(apiToken);
                    await BotRoma.SetWebhookAsync("");
                    IFCore.IFCore.loggerMain.Debug("Bot: Status - Run");
                    MainWindow.timerChangesSheldue.Tick += new EventHandler(TimerIntervalParseFile);
                    MainWindow.timerChangesSheldue.Interval = new TimeSpan(0, 0, 20);
                    MainWindow.timerChangesSheldue.Start();
                    MainWindow.SetParam(nameof(MainWindow.TelegramBot_Working), true);
                    MainWindow._mWindow.ShowSuccessfulMessage("Бот запущен!!!");
                    MainWindow._mWindow.MainWindowPage.NavigationService.Navigate(new StartupBotPage());
                    menuLibriary.bw.DoWork += menuLibriary.StartedMenu;
                    menuLibriary.bw.RunWorkerAsync(apiToken);
                }
                catch
                {
                    MainWindow._mWindow.ShowErrorMessage("Бот не запущен!!! Не верный токен!!!");
                    IFCore.IFCore.loggerMain.Error("Bot: Status - Stop. Exception;");
                }
            }
            else
            {
                IFCore.IFCore.loggerMain.Debug("Bot: Status - Stop");
            }
        }
        private static void TimerIntervalParseFile(object sender, EventArgs e)
        {
            if (menuLibriary.bw == null && menuLibriary.bw.IsBusy == true)
            {
                MainWindow.timerChangesSheldue.Tick -= TimerIntervalParseFile;
                MainWindow.timerChangesSheldue.Stop();
            }
            try
            {
                var newSheldueAtTimer = new View.Classes.GetShelduePL().GetChangesSheldue(out MainWindow.weekCheck);
                if (newSheldueAtTimer == null || newSheldueAtTimer.Keys == null)
                    return;
                ICollection<string> keys = newSheldueAtTimer.Keys;
                if (keys != null && !keys.Contains(MainWindow.dayOldSheldue.ToLower()))
                {
                    MainWindow._mWindow.ShowSuccessfulMessage("Присутствуют замены к расписанию!");
                    MainWindow.allSheldue = MainWindow.allSheldueCopy;
                    if (newSheldueAtTimer != null)
                    {
                        MainWindow.allSheldue = ChangeMainSheldueWithNewSheldue(MainWindow.allSheldue, newSheldueAtTimer);
                        MainMenu.SetSheldue = MainWindow.allSheldue;
                        MainWindow.dayOldSheldue = keys.ToArray()[0];
                        using (FileStream fs = new FileStream("ListIdMessageChatClients.xml", FileMode.Open))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<int>), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
                            var idMessageClients = new List<int>();
                            idMessageClients = (List<int>)serializer.Deserialize(fs);
                            new SendAlertAllUsers(MainMenu.GetBot, MainMenu.GetApi, idMessageClients, MainWindow.allSheldue).AlertMessage("⚠️🚨 На сайте появились замены к расписанию 🌐 Узнай свое новое расписание на завтра ⚡");

                        }
                        MainWindow._mWindow.ShowSuccessfulMessage("Опопвещение о новом расписании выполняется!");
                    }
                }
            }
            catch
            {

            }
        }
        public static Dictionary<string, List<SheldueAllDaysTelegram>> ChangeMainSheldueWithNewSheldue(
                      Dictionary<string, List<SheldueAllDaysTelegram>> mainSheldue,
                      Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> shangeSheldues)
        {
            try
            {
                foreach (var changeSheldueItem in shangeSheldues)
                {
                    foreach (var changeSheldueItemValue in changeSheldueItem.Value)
                    {
                        foreach (var itemMain in mainSheldue)
                        {
                            if (changeSheldueItemValue.Key.ToLower() == itemMain.Key.Split(' ')[1].ToLower())
                            {
                                foreach (var itemChangeValue in changeSheldueItemValue.Value)
                                {
                                    foreach (var itemMainValue in itemMain.Value)
                                    {
                                        if (itemChangeValue.DayName.ToLower() == itemMainValue.DayName.ToLower())
                                        {
                                            try
                                            {
                                                itemMainValue.Day[0].ChangeSheldue = itemChangeValue.Day[0]?.ChangeSheldue;
                                                if (itemMainValue.Day[0].Para1 != null)
                                                    itemMainValue.Day[0].Para1[0] = null;
                                                if (itemMainValue.Day[0].Para2 != null)
                                                    itemMainValue.Day[0].Para2[0] = null;
                                                if (itemMainValue.Day[0].Para3 != null)
                                                    itemMainValue.Day[0].Para3[0] = null;
                                                if (itemMainValue.Day[0].Para4 != null)
                                                    itemMainValue.Day[0].Para4[0] = null;
                                                if (itemMainValue.Day[0].Para5 != null)
                                                    itemMainValue.Day[0].Para5[0] = null;
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return mainSheldue;
        }
    }
}
