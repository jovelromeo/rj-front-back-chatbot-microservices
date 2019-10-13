using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using romeojovelchatbot.Services;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace romeojovelchatbot
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IStockCsvParser _stockCsvParser;

        public HomeController(IStockCsvParser stockCsvParser)
        {
            _stockCsvParser = stockCsvParser;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<string> Index([FromQuery] string code="aapl.us")
        {
             await _stockCsvParser.SendQuoteAsync(code);
            return "";
        }
    }
}
