using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class ApiService
    {
        public string URL { get; set; } = "http://10.105.13.186:45455/api/values/";
        public HttpClient client { get; set; }

        public ApiService()
        {
            client = new HttpClient();
        }

        public async Task<bool> Login(string username, string password)
        {
            var url = this.URL + $"login?username={username}&password={password}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                App.UserId = long.Parse(result);
                return true;
            }

            return false;
        }

        public async Task<List<Listing>> GetListings()
        {
            var url = this.URL + $"GetListings?userID={App.UserId}";
            var res = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Listing>>(res);
        }

        public async Task<List<ItemPrice>> GetItemPrices(long ItemID)
        {
            var url = this.URL + $"GetItemPrices?ItemID={ItemID}";
            var res = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<ItemPrice>>(res);
        }

        public async Task<bool> DeleteItemPrice(long ItemPriceId)
        {
            var url = this.URL + $"DeleteItemPrice?ItemPriceId={ItemPriceId}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Rule>> GetRules()
        {
            var url = this.URL + $"GetRules";
            var res = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Rule>>(res);
        }

        public async Task<bool> EditItemPrice(long ItemPriceID, decimal price, long ruleId)
        {
            var url = this.URL + $"EditItemPrice?ItemPriceID={ItemPriceID}&price={price}&ruleId={ruleId}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddItemPrice(AddItemPriceDTO addItemPriceDTO)
        {
            var url = this.URL + $"AddItemPrice";

            var json = JsonConvert.SerializeObject(addItemPriceDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
