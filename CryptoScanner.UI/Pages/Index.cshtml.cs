using CryptoScanner.App;
using CryptoScanner.App.Services;
using CryptoScanner.Data.Models;
using CryptoScanner.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoScanner.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICoinsRepository repository;
        [BindProperty]
        public string SelectedCoin { get; set; }
        public List<CoinModel> Coins { get; set; } = new();
        public string ErrorMessage { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICoinsRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public async Task OnGetAsync()
        {
            //var coin = await new ApiCaller(repository).MakeCall("avalanche");
            Coins = (List<CoinModel>)await repository.GetAll();





        }
        /*  public async Task<IActionResult> OnPost()
          {
              return Pagge();
          }*/
        public async Task<IActionResult> OnPostRefresh(string coinName)
        {
            try
            {
                await new CoinsManager(repository).RefreshCoinAsync(coinName);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return RedirectToPage("index");
        }
        public async Task<IActionResult> OnPostAdd(string coinName)
        {
            var coin = await new ApiCaller(repository).MakeCall(coinName);
            Coins = (List<CoinModel>)await repository.GetAll();
            return RedirectToPage("index");
        }

        public async Task<IActionResult> OnPostDelete(string coinName)
        {
            await new CoinsManager(repository).DeleteCoin(coinName);
            return RedirectToPage("index");
        }
    }
}
