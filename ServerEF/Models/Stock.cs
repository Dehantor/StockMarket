using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerEF.Models
{
    public class Stock
    {
        /// <summary>
        /// Котировки
        /// </summary>
        public int Id { get; set; }
        public double Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
