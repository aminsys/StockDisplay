using Microsoft.AspNetCore.Mvc;
using StockTracker.Models;
using System.Diagnostics;
using YahooFinanceApi;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<Stock> _stocks = new List<Stock>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private async Task<List<Stock>> GetStockData(string symbol, DateTime startDate, DateTime endDate)
        {
            try
            {
                var historic_data = await Yahoo.GetHistoricalAsync(symbol, startDate, endDate);
                /*var security = await Yahoo.Symbols(symbol).Fields(Field.LongName).QueryAsync();
                var ticker = security[symbol];
                var companyName = ticker[Field.LongName];*/

                foreach(var h in historic_data)
                {
                    // Console.WriteLine(h.DateTime + ". Close: " + h.Close + " Adjusted Close: " + h.AdjustedClose);
                    _stocks.Add(new Stock
                    {
                        Symbol = symbol,
                        Volume = h.Volume,
                        CloseDate = h.DateTime,
                        Id = Guid.NewGuid(),
                        Close = h.Close,
                        AdjustedClose = h.AdjustedClose
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong! - " + e.ToString());
            }
            return _stocks;         
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> Stock()
        {
            var model = await GetStockData("VOLV-B.ST", new DateTime(2022, 05, 01), DateTime.Now);
            ViewBag.Stock = model.ToList();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}