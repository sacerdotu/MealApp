using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MealApp.Models;
using Newtonsoft.Json;

namespace MealApp.Helpers
{
    public class WebApiHelper
    {
        public static async Task<List<ProviderMenuItem>> GetAllProducts()
        {
            try
            {

                var client = new HttpClient();
                {
                    var response =
                        await client.GetAsync(string.Format(WebApiLinks.ItemsFromToday, WebApiLinks.BaseIPAddress));

                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var productJsonString = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<ProviderMenuItem[]>(productJsonString).ToList();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
            }
            return null;
        }

        public static async Task<ProviderMenuItem> UpdateProduct(long id, bool action)
        {
            var client = new HttpClient();
            string URI = string.Format(WebApiLinks.LikeItemById, WebApiLinks.BaseIPAddress, id);
            if (!action)
            {
                URI = string.Format(WebApiLinks.DisLikeItemById, WebApiLinks.BaseIPAddress, id);
            }

            try
            {
                var result = await client.PutAsync(URI, null);
                if (result.IsSuccessStatusCode)
                {
                    var response = await client.GetAsync(string.Format(WebApiLinks.ItemFromTodayById, WebApiLinks.BaseIPAddress, id));
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ProviderMenuItem>(productJsonString);
                    }
                }
            }
            catch
            { }
            return null;
        }
    }
}
