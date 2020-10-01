using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace temperatura_sensor
{
    
    class Program
    {
        static HttpClient client = new HttpClient();
        static string powerBiPostUrl = "url to report the powerBI";
        static Random randomTemp = new Random();
        static void Main(string[] args)
        {
            while (true)
            {
                var rValue = randomTemp.NextDouble() * (23.5 - 24.0) + 23.5;

                var tempDate = new Temp()
                {
                    ZoneName = "Zona A",
                    DateTime = DateTime.Now,
                    Temperature = (decimal)rValue
                };
                var jsonString = JsonConvert.SerializeObject(tempDate);

                var postToPowerBi = HttpPostAsync(powerBiPostUrl, "[" + jsonString + "]");

                Console.Write(jsonString);

                System.Threading.Thread.Sleep(1000);
            }
        }
    
        static async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
