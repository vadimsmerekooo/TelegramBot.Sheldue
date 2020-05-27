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
                    weatherStringText += $"Будет {osadki}, возьми зонтик☔";
                }
                else
                {
                    if (osadki.ToLower().Contains("ясно"))
                    {
                        weatherStringText += $"Будет {osadki}, захвати очки👓";
                    }
                    else
                    {
                        weatherStringText += $"Будет {osadki}";
                    }
                }
            }
            catch
            {

            }
            return weatherStringText;
        }
    }
}
