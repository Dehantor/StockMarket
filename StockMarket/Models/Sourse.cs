using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Models
{
    /// <summary>
    /// Источник
    /// </summary>
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BaseAPIUrl { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
