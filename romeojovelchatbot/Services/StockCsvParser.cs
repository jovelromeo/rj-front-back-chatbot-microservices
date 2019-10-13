using romeojovelchatbot.Producers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace romeojovelchatbot.Services
{
    public class StockCsvParser : IStockCsvParser
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly RabbitMQClient _rabbitMQClient;

        public StockCsvParser(IHttpClientFactory httpClientFactory, RabbitMQClient rabbitMQClient)
        {
            _clientFactory = httpClientFactory;
            _rabbitMQClient = rabbitMQClient;
        }
        private async Task<string> GetFileStringAsync(string stockCode)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get,
               $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv");

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    StreamReader sr = new StreamReader(await response.Content.ReadAsStreamAsync());
                    string results = sr.ReadToEnd();
                    sr.Close();

                    return results;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return "Error at bot. Could not get the csv file.";
            }
        }

        public async Task SendQuoteAsync(string stockCode)
        {
            var message = string.Empty;
            try
            {

                string fileList = await GetFileStringAsync(stockCode);
                string[] tempStr;
                tempStr = fileList.Split("\r\n");
                if (tempStr.Length < 2)
                {
                    message = "Error at bot. Parsing the CSV file was not posible.";
                }
                var headers = tempStr[0].ToLower().Split(',');
                var openIndex = Array.IndexOf(headers, "open");
                var values = tempStr[1].Split(',');
                if (values.Length <= openIndex)
                {
                    message = "Error at bot. Parsing the CSV file was not posible";
                }
                message = $"{stockCode.ToUpper()} quote is ${values[openIndex]} per share";
            }
            catch (Exception)
            {

                message = "Error at bot. Parsing the CSV file was not posible.";
            }
            _rabbitMQClient.PushMessage(message);
        }

        
    }
}
