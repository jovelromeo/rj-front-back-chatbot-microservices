using System.Threading.Tasks;

namespace romeojovelchatbot.Services
{
    public interface IStockCsvParser
    {
        Task SendQuoteAsync(string stockCode);
    }
}