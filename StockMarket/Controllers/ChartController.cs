using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Controllers
{
    public class ChartController : Controller
    {
        private readonly Context _context;
        public ChartController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Chart> stocks = (from st in _context.Stocks.Include(x => x.Company)
                         where st.Company.Ticker == "YNDX"
                         select new Chart (){ DateTime=st.Date,Price=st.Price }).ToList();
            stocks = (from st in stocks
                     orderby st.DateTime
                     select st).ToList();
            return View(stocks);
        }
    }
}
