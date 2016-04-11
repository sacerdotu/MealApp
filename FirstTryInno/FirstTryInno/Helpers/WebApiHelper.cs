using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Helpers
{
    public class WebApiHelper
    {
        const string URI = "http://192.168.0.12:8765/api/providermenuitem/today";

        public static async Task<List<ProviderMenuItem>> GetAllProducts()
        {
            //return new List<ProviderMenuItem>()
            //    {
            //        new ProviderMenuItem() { Name="Shaorma cu de'toate", ProviderMenuItemID=1, Date = DateTime.Now },
            //        new ProviderMenuItem() { Name="Kebab si shaorma la pachet", ProviderMenuItemID=2, Date = DateTime.Now },
            //        new ProviderMenuItem() { Name="Nici pizza nici cola", ProviderMenuItemID=3, Date = DateTime.Now }
            //    };

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
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

        private async void AddProduct()
        {
            ProviderMenuItem p = new ProviderMenuItem();
            p.ProviderMenuItemID = 3;
            p.Name = "Sarmale";
            p.LikeCount = 5;
            p.DislikeCount = 5;
            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
            }
            GetAllProducts();
        }



        private async void UpdateProduct()
        {
            ProviderMenuItem p = new ProviderMenuItem();
            p.ProviderMenuItemID = 3;
            p.Name = "Ciorba";
            p.LikeCount = 5;
            p.DislikeCount = 5;

            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(String.Format("{0}/{1}", URI, p.ProviderMenuItemID), content);
            }
            GetAllProducts();
        }


        private async void DeleteProduct()
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(String.Format("{0}/{1}", URI, 3));
            }
            GetAllProducts();
        }
    }
}
