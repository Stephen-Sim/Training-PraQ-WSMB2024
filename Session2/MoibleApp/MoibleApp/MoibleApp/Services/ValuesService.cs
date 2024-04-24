using MoibleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoibleApp.Services
{
    public class ValuesService
    {
        public HttpClient client { get; set; }
        public string url { get; set; } = "http://10.105.13.186:45456/api/values/";

        public ValuesService()
        {
            client = new HttpClient();
        }

        public async Task<bool> Login(string username, string password)
        {
            var url = this.url + $"login?username={username}&password={password}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                App.User = JsonConvert.DeserializeObject<User>(result);
                return true;
            }

            return false;
        }

        public async Task<bool> StoreUser(User user)
        {
            var url = this.url + $"StoreUser";

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }

        public async Task<List<User>> GetUsers()
        {
            var url = this.url + "getusers";
            var response = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<List<User>>(response);
            return result;
        }

        public async Task<bool> DeleteUser(long Id)
        {
            var url = this.url + $"DeleteUser?Id={Id}";

            var res = await client.DeleteAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Role>> GetRoles()
        {
            var url = this.url + "GetRoles";
            var response = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<List<Role>>(response);
            return result;
        }
    }
}
