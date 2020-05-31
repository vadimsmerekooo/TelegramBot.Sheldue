using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Telegram_Bot.View.Classes.Menu
{
    public class Weather
    {
        private string osadki;
        private string temp;
        public string GetOsadki { get { return osadki; } }
        public string GetTemperature { get { return temp; } }

        public Weather()
        {
            try
            {
                WebRequest request;
                request = WebRequest.Create(@"https://legacy.meteoservice.ru/weather/now/grodno");
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string data = reader.ReadToEnd();
                        temp = (new Regex(@"<span class=""temperature"">(?<temp>[^<]+)</span>").Match(data).Groups["temp"].Value).Replace("&deg;C", "");
                        osadki = (new Regex(@"<td class=""title"">Облачность:</td>[^<]*?<td>(?<osadki>[^<]+)</td>").Match(data).Groups["osadki"].Value).Replace("&deg;C", "");
                    }
                }
            }
            catch (Exception ex)
            {
                int lineEx = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                IFCore.IFCore.loggerMain.Error("Weather class " + ex.ToString() + lineEx);
                new IFCore.IFCoreSendErrorMessage(MainMenu.GetBot, MainMenu.GetApi, "Ошибка при подключении или парсинге сайта: " + ex.ToString() + "Строка: "+ lineEx);
            }
        }
        public string GetInfoAboutWeather()
        {
            string weatherStringText = string.Empty;
            try
            {
                if (Convert.ToInt32(temp.Replace("+", "").Replace("-", "")) < 0)
                {
                    weatherStringText = $"На улице, {temp}, холодно⛄, надень шапку🥶";
                }
                else
                {
                    if (Convert.ToInt32(temp.Replace("+", "").Replace("-", "")) >= 0 && Convert.ToInt32(temp.Replace("+", "").Replace("-", "")) < 10)
                    {
                        weatherStringText = $"На улице, {temp}, прохладно⛄, оденься теплее🥶";
                    }
                    else
                    {
                        if (Convert.ToInt32(temp.Replace("+", "").Replace("-", "")) >= 10)
                        {
                            weatherStringText = $"На улице, {temp}, жакро☀, одень рубашку🥵";
                        }
                    }
                }
                if (osadki.ToLower().Contains("дождь"))
                {
                    weatherStringText += $" Будет {osadki}, возьми зонтик☔";
                }
                else
                {
                    if (osadki.ToLower().Contains("ясно"))
                    {
                        weatherStringText += $" Будет {osadki}, захвати очки👓";
                    }
                    else
                    {
                        weatherStringText += $" Будет {osadki}";
                    }
                }
            }
            catch(Exception ex)
            {
                int lineEx = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                IFCore.IFCore.loggerMain.Error("Weather class "+ ex.ToString());
                new IFCore.IFCoreSendErrorMessage(MainMenu.GetBot, MainMenu.GetApi, "Ошибка при получении погоды: " + ex.ToString() + "Строка: " + lineEx);
            }
            return weatherStringText;
        }
    }
}
