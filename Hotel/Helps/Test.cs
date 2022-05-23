using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Helps
{
    class Test
    {
        public static async Task<string> f()
        {
            string baseUrl = @"https://swapi.dev/api/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.met
                HttpResponseMessage res = await client.GetAsync(@"people/1");

                if (res.IsSuccessStatusCode)
                {
                    var strRes = res.Content.ReadAsStringAsync().Result;
                    return strRes;
                }
                return "";
            }
        }
    }
}
