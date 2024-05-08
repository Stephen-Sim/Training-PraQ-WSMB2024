using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        public WSC2022SE_Session2Entities ent { get; set; }

        public ValuesController()
        {
            ent = new WSC2022SE_Session2Entities();
        }

        [HttpGet]
        public object Login(string username, string password)
        {
            var user = ent.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user.ID);
        }

        [HttpGet]
        public object GetListings(long userID)
        {
            var items = ent.Items.ToList().Where(x => x.UserID == userID).OrderBy(x => x.ItemPrices.Count() == 0).ThenBy(x => x.ItemPrices.Count).Select(x => new
            {
                x.ID,
                Name = x.Title,
                LastDateInfo = x.ItemPrices.Count() == 0 ?  "There is no item prices" : $"Last date of pricing: {x.ItemPrices.OrderByDescending(y => y.Date).First().Date.ToString("yyyy/MM/dd")}",
                Color = new Func<string>(() =>
                {
                    if (x.ItemPrices.Count == 0)
                    {
                        return "red";
                    }

                    if (x.ItemPrices.Any(y => y.Date >= DateTime.Today && y.Date <= DateTime.Today.AddDays(5)))
                    {
                        return "default";
                    }

                    return "red";
                })()
            });

            return Ok(items);
        }

        [HttpGet]
        public object GetItemPrices(long ItemID)
        {
            var itemPrices = ent.ItemPrices.ToList().Where(x => x.ItemID == ItemID).OrderBy(x => x.Date).Select(x => new
            {
                x.ID,
                DisplayStatus = new Func<string>(() =>
                {
                    if (x.BookingDetails.Count != 0)
                    {
                        return "🔒";
                    }

                    if (ent.DimDates.Any(y => y.Date == x.Date && y.isHoliday))
                    {
                        return "🍺";
                    }

                    return "";
                })(),
                Status = new Func<string>(() =>
                {
                    if (x.BookingDetails.Count != 0)
                    {
                        return "Booked";
                    }

                    if (ent.DimDates.Any(y => y.Date == x.Date && y.isHoliday))
                    {
                        return "Holiday";
                    }

                    return "";
                })(),
                Date = x.Date.ToString("yyyy/MM/dd"),
                DisplayPrice = $"$ {x.Price}",
                x.Price,
                Rule = $"({x.CancellationPolicy.Name})",
                RuleId = x.CancellationPolicyID
            });

            return Ok(itemPrices);
        }

        [HttpGet]
        public object DeleteItemPrice(long ItemPriceId)
        {
            var ip = ent.ItemPrices.FirstOrDefault(x => x.ID == ItemPriceId);
            ent.ItemPrices.Remove(ip);
            ent.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public object GetRules()
        {
            var rules = ent.CancellationPolicies.Select(x => new
            {
                x.ID,
                x.Name
            });

            return Ok(rules);
        }

        [HttpGet]
        public object EditItemPrice(long ItemPriceID, decimal price, long ruleId)
        {
            var ip = ent.ItemPrices.FirstOrDefault(x => x.ID == ItemPriceID);
            ip.Price = price;
            ip.CancellationPolicyID = ruleId;
            ent.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public object AddItemPrice(AddItemPriceDTO addItemPriceDTO)
        {
            for (var i = addItemPriceDTO.StartDate; i <= addItemPriceDTO.EndDate; i = i.AddDays(1))
            {
                if (ent.ItemPrices.Any(x => x.ItemID == addItemPriceDTO.ItemID && x.Date == i))
                {
                    var ip = ent.ItemPrices.First(x => x.ItemID == addItemPriceDTO.ItemID && x.Date == i);

                    if (ip.BookingDetails.Count != 0)
                    {
                        continue;
                    }

                    ent.ItemPrices.Remove(ip);
                }

                var itemPrice = new ItemPrice()
                {
                    ItemID = addItemPriceDTO.ItemID,
                    GUID = Guid.NewGuid(),
                    Date = i,
                };

                if (ent.DimDates.Any(x => x.Date == i && x.isHoliday == true))
                {
                    itemPrice.Price = addItemPriceDTO.HolidayPrice;
                    itemPrice.CancellationPolicyID = addItemPriceDTO.HolidayRuleId;
                }
                else if (ent.DimDates.Any(x => x.Date == i && (x.DayName == "Saturday" || (x.DayName == "Sunday"))))
                {
                    itemPrice.Price = addItemPriceDTO.WeekendPrice;
                    itemPrice.CancellationPolicyID = addItemPriceDTO.WeekendRuleId;
                }
                else
                {
                    itemPrice.Price = addItemPriceDTO.OtherDayPrice;
                    itemPrice.CancellationPolicyID = addItemPriceDTO.OtherDayRuleId;
                }

                ent.ItemPrices.Add(itemPrice);  
            }

            ent.SaveChanges();
            return Ok();
        }

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
}
