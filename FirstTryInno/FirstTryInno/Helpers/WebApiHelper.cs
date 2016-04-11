using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FirstTryInno.Models;
using Newtonsoft.Json;

namespace FirstTryInno.Helpers
{
    public class WebApiHelper
    {
        string URI = "http://localhost:1863/api/product";

        private async void GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();

                        //dataGridView1.DataSource = JsonConvert.DeserializeObject<Product[]>(productJsonString).ToList();

                    }
                }
            }
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
