using CryptoScanner.Data.Models;
using CryptoScanner.Data.Repositories.Interfaces;

namespace CryptoScanner.App.Services
{
    public class CoinsManager
    {
        private readonly ICoinsRepository repo;

        public CoinsManager(ICoinsRepository repo)
        {
            this.repo = repo;
        }

        public List<CoinModel> GetDesc()
        {
            return repo.GetAll().Result.OrderByDescending(c => c.CurrentPriceDollar).ToList();
        }
        public async Task<List<CoinModel>> GetAsc()
        {
            return repo.GetAll().Result.OrderBy(c => c.CurrentPriceDollar).ToList();
        }

        public async Task RefreshCoinAsync(string name)
        {
            var coinToRefresh = await repo.Get(name);
            var updatedCoin = await new ApiCaller(repo).GetById(coinToRefresh.ApiId);
            await repo.UpdateCoin(updatedCoin);
        }
        public async Task DeleteCoin(String name)
        {
            var coinToDelete = await repo.Get(name);
            if (coinToDelete != null)
            {
                await repo.DeleteCoin(coinToDelete.Id);
            }

        }
    }
}
