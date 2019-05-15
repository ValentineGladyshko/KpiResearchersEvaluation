using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KpiResearchersEvaluation.WebLibrary
{
    public static class WebSocket
    {
        private static readonly HttpClient client;

        static WebSocket()
        {
            ServicePointManager.DefaultConnectionLimit = 10;
            client = new HttpClient();
        }

        public static async Task<string> GetPage(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                return string.Empty;
            }

        }
    }
}
