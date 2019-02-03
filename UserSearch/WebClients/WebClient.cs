using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace UserSearch.WebClients
{
    public class WebClient : IWebClient
    {
        public T Query<T>(string userUrl)
        {
            using (var client = new HttpClient())
            {
                var methodUri = new Uri(userUrl);
                client.DefaultRequestHeaders.Add("User-Agent", "*");
                var response = client.GetAsync(methodUri).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            return default(T);
        }
    }
}
