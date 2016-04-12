using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace FirstTryInno.Helpers
{
    public class WebApiHelper
    {
        public static async Task<List<ProviderMenuItem>> GetAllProducts()
        {
            var client = new HttpClient();
            {
                using (var response = await client.GetAsync(WebApiLinks.ItemsFromToday))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ProviderMenuItem[]>(productJsonString).ToList();
                    }
                }
            }
            return null;
        }

        public static async Task<ProviderMenuItem> UpdateProduct(long id, bool action)
        {
            var client = new HttpClient();
            string URI = string.Format(WebApiLinks.LikeItemById, id);
            if (!action)
            {
                URI = string.Format(WebApiLinks.DisLikeItemById, id);
            }
            var result = await client.PutAsync(URI, null);
            if (result.IsSuccessStatusCode)
            {
                var response = await client.GetAsync(string.Format(WebApiLinks.ItemFromTodayById, id));
                if (response.IsSuccessStatusCode)
                {
                    var productJsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ProviderMenuItem>(productJsonString);
                }
            }
            return null;
        }


        //private async void DeleteProduct()
        //{
        //    //using (var client = new HttpClient())
        //    //{
        //    //    var result = await client.DeleteAsync(String.Format("{0}/{1}", URI, 3));
        //    //}
        //    //GetAllProducts();
        //}
    }
}
