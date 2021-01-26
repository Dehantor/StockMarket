using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockMarket.Models
{
    public class Stock
    {
        /// <summary>
        /// Котировки
        /// </summary>
        public int Id { get; set; }
        public double Price { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
