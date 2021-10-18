using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class Shared
    {
        public static string Token;

        public static async Task<string> GetCommand(string route, string token)
        {
            var returnData = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(route);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync(route);
                if (response.IsSuccessStatusCode)
                {
                    returnData = await response.Content.ReadAsStringAsync();
                }
            }

            return returnData;
        }

        public static async Task<string> POSTCommand(string route, object data)
        {
            var returnData = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(route);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string strPayload = JsonConvert.SerializeObject(data);
                HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(route, c);
                if (response.IsSuccessStatusCode)
                {
                    returnData = await response.Content.ReadAsStringAsync();
                }
            }

            return returnData;
        }
    }
}
