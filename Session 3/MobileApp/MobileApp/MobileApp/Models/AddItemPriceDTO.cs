using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class AddItemPriceDTO
    {
        public long ItemID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal WeekendPrice { get; set; }
        public long WeekendRuleId { get; set; }
        public decimal HolidayPrice { get; set; }
        public long HolidayRuleId { get; set; }
        public decimal OtherDayPrice { get; set; }
        public long OtherDayRuleId { get; set; }
    }
}
