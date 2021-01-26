using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerEF.Services
{
    public class Finnhub:IStockPrice
    {
        /// <summary>
        /// Получение информации о котировках
        /// </summary>
        /// <param name="company">Список компаний</param>
        /// <returns></returns>
        public Dictionary<string, double> GetStock(List<string> company)
        {
            Finn value;
            Dictionary<string, double> prices = new Dictionary<string, double>();
            foreach (var item in company)
            {
                try
                {
                    //скачиваем данные
                    using (var webClient = new System.Net.WebClient())
                    {
                        webClient.Encoding = Encoding.UTF8;
                        webClient.Headers["User-Agent"] = "Mozilla/5.0";
                        string json = webClient.DownloadString("https://finnhub.io/api/v1/quote?symbol=" + item + "&token=c059f7748v6ta8gbo670");
                        value = JsonSerializer.Deserialize<Finn>(json);
                    }
                    try
                    {
                        prices.Add(item, value.c);
                    }
                    catch
                    {
                        prices.Add(item, 0);
                    }
                    System.Threading.Thread.Sleep(200);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return prices;
        }
       /// <summary>
       /// Для десерилизации json файла
       /// </summary>
        class Finn
        {
            public double c { get; set; }
        }
    }
}
