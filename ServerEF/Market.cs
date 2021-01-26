using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ServerEF.Services;
using System.Threading;
using ServerEF.Models;
using System.Linq;
namespace ServerEF
{
    /// <summary>
    /// Добавление котировок  в БД
    /// </summary>
    class Market
    {
        Setting _setting;
        public Market(Setting setting)
        {
            _setting = setting;
        }
        /// <summary>
        /// асинхронный Запуск 
        /// </summary>
        public void getStockAsync()
        {
            Task.Run(new Action(getStock));
        }
        /// <summary>
        /// синхронный запуск 
        /// </summary>
        void getStock(){
            while (true)
            {
                try
                {
                    //получаем список компаний
                    List<string> companies;
                    using (Context db = new Context())
                    {
                        companies = (from c in db.Companies
                                     select c.Ticker).ToList();
                    }
                    IStockPrice stockPrice = null;
                    if (_setting.Source == 1)
                    {
                        stockPrice = new Finnhub();
                    }
                    else if (_setting.Source == 2)
                    {
                        stockPrice = new Moex();
                    }
                    else
                    {
                        stockPrice = new Finnhub();
                    }
                    Dictionary<string, double> prices = stockPrice.GetStock(companies);
                    //добавляем полученный данные в бд
                    using (Context db = new Context())
                    {
                        List<Stock> stocks = new List<Stock>();
                        foreach (var item in prices)
                        {
                            int id = (from c in db.Companies
                                      where c.Ticker == item.Key
                                      select c.Id).FirstOrDefault();
                            DateTime date = DateTime.UtcNow;
                            

                            stocks.Add(new Stock()
                            {
                                SourceId = _setting.Source,
                                Price = item.Value,
                                Date = date,
                                CompanyId = id
                            }); ;

                        }
                        db.Stocks.AddRange(stocks);
                        db.SaveChanges();
                    }
                    //ждем положенное время
                    Thread.Sleep(_setting.Time);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        } 
    }
}
