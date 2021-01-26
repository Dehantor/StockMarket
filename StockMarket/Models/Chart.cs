using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockMarket.Models
{
    public class Chart
    {
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
    }

}
