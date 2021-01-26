using System;
using System.Collections.Generic;
using System.Text;
using ServerEF.Models;
namespace ServerEF
{
    class InitialServerEF
    {
        static Setting setting = new Setting();
        Market market = new Market(setting);
        MessageClient messageClient = new MessageClient(setting);
        /// <summary>
        /// Запуск сервера загрузки в бд котировок
        /// </summary>
        public  void Initial()
        {
            //StartSourse();
            //StartCompany();
            market.getStockAsync();
            messageClient.receiveMessageAsync();
        }
        /// <summary>
        /// Первичная инициализация Источников
        /// </summary>
        static void StartSourse()
        {
            using (Context db = new Context())
            {
                db.Sources.Add(new Source() { Name = "Finnhub", BaseAPIUrl = "https://finnhub.io/api/v1/quote?symbol=" });
                db.Sources.Add(new Source() { Name = "Moex", BaseAPIUrl = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=securities&securities.columns=SECID,PREVADMITTEDQUOTE" });
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Первичная инициализация Компаний
        /// </summary>
        static void StartCompany()
        {
            using (Context db = new Context())
            {

                List<Company> companies = new List<Company>() {
                    new Company() { Name = "Сбербанк", Ticker = "SBER" },
                    new Company() { Name = "ЛУКОЙЛ", Ticker = "LKOH" },
                    new Company() { Name = "ГАЗПРОМ", Ticker = "GAZP" },
                    new Company() { Name = "Роснефть", Ticker = "ROSN" },
                    new Company() { Name = "Polymetal", Ticker = "POLY" },
                    new Company() { Name = "Yandex", Ticker = "YNDX" },
                    };
                db.Companies.AddRange(companies);
                db.SaveChanges();
            }
        }

    }
}
