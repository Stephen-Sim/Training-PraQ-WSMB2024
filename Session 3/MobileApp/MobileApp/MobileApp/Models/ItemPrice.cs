using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class ItemPrice
    {
        public long ID { get; set; }
        public string DisplayStatus { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string DiplayPrice { get; set; }
        public decimal Price { get; set; }
        public string Rule { get; set; }
        public long RuleId { get; set; }
    }
}
