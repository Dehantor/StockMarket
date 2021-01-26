using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var stocks = _context.Stocks
                .Include(c => c.Company).OrderByDescending(x=>x.Date);
            
            return View(await stocks.ToListAsync());
        }
    }
}
