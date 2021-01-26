using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerEF.Services
{
    /// <summary>
    /// Получение информации с сервера о котровках
    /// </summary>
    interface IStockPrice
    {
        Dictionary<string, double> GetStock(List<string> companies);
    }
}
