using System.Net;
using System.Net.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading.Tasks;
using System;

namespace WebLibrary
{
    public static class WebSocket
    {
        public static IWebDriver Driver { get; set; }
        public static IWebDriver GoogleDriver { get; set; }
        public static IWebDriver PublonsDriver { get; set; }
        public static IWebDriver ScopusDriver { get; set; }


        static readonly HttpClient client;

        static WebSocket()
        {
            ServicePointManager.DefaultConnectionLimit = 10;
            client = new HttpClient();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.Proxy = null;
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--silent");
            //options.AddArgument("--proxy-server='direct://'");
            //options.AddArgument("--proxy-bypass-list=*");
            Driver = new ChromeDriver(service, options);

            GoogleDriver = new ChromeDriver(service, options);
            PublonsDriver = new ChromeDriver(service, options);
            ScopusDriver = new ChromeDriver(service, options);
        }

        public static string GetPageAsync(string url)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException)
            {
                return string.Empty;
            }
        }
    }
}
