using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationTransactionsComparison.Models;
using WebApplicationTransactionsComparison.Interfaces;
using CsvHelper;
using WebApplicationTransactionsComparison.PaginatedList;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationTransactionsComparison.Controllers
{
	public class HomeController : Controller
	{

        private readonly ILogger<HomeController> _logger;

		private readonly IComparisonService _comparisonService;

        private IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger, IComparisonService comparisonService, IMemoryCache cache)
		{
			_logger = logger;
			_comparisonService = comparisonService;
            _cache = cache;
		}

        public IActionResult Index()
		{
			return View();
		}

        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            if (formFile != null)
            {
                try
                {
                    List<TransactionModel> transactions;
                    using (var reader = new StreamReader(formFile.OpenReadStream()))
                    using (var csvr = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        transactions = csvr.GetRecords<TransactionModel>().ToList();
                    }

                    var timer = new Stopwatch();
                    timer.Start();

                    var result = await _comparisonService.CompareTransactions(transactions);

                    timer.Stop();

                    TimeSpan timeTaken = timer.Elapsed;

                    _logger.LogInformation("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set("ident", result.Item1, cacheEntryOptions);
                    _cache.Set("csv", result.Item2, cacheEntryOptions);
                    _cache.Set("dbonly", result.Item3, cacheEntryOptions);

                    return RedirectToAction("PaginationData");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View();
        }

        [DisableRequestSizeLimit]
        public ActionResult PaginationData(int pageNumber = 1, int pageSize = 5, string from = "ident")
        {
            var items = _cache.Get<IEnumerable<TransactionModel>>(from);

            List<string> fileFrom = new List<string>()
            {
                "ident",
                "csv",
                "dbonly",
            };

            ViewBag.FileFrom = new SelectList(fileFrom, from);

            return View(PaginatedList<TransactionModel>.CreateAsync(items.ToList(), pageNumber, pageSize));
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}