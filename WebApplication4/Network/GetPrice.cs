using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using WebApplication4.models;

namespace WebApplication4
{
    public class GetPrice
    {
        public static CurrencyInfo USD(DateTime dateTime)
        {
            string date = dateTime.ToString("yyyy-MM-dd");
            string url = $@"https://ggroupp-getcurrencyrate.azurewebsites.net/api/currency/usd/rate?RequestDate={date}";
            string reqeustResult;

            WebClient client = new WebClient();

            using (Stream data = client.OpenRead(url))
            {
                using (StreamReader reader = new StreamReader(data))
                {
                    reqeustResult = reader.ReadToEnd();
                }
            }

            CurrencyInfo currencyInfo = JsonConvert.DeserializeObject<CurrencyInfo>(reqeustResult);

            return currencyInfo;
        }

    }
}
