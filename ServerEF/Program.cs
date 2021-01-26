using Microsoft.EntityFrameworkCore;
using System;

namespace ServerEF
{
    class Program
    {

        static void Main(string[] args)
        {
            InitialServerEF initialServerEF = new InitialServerEF();
            initialServerEF.Initial();
            using (Context db = new Context())
            {
                var c = db.Stocks
               .Include(c => c.Company);
                foreach (var item in c)
                {
                    Console.WriteLine($"{item.Price}  {item.Company.Ticker}");
                }
            }
            Console.WriteLine("Сервер по обновлению данных в БД запущен");
            Console.ReadKey();
        }
    }
}
