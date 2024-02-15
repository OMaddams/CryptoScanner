using CryptoScanner.Data.Database;
using CryptoScanner.Data.Models;
using CryptoScanner.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoScanner.Data.Repositories.Implementation
{
    public class CoinsRepository : ICoinsRepository
    {
        private readonly CoinDbContext context;

        public CoinsRepository(CoinDbContext context)
        {
            this.context = context;
        }
        public async Task AddCoin(CoinModel coin)
        {
            await context.Coins.AddAsync(coin);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCoin(int Id)
        {
            var coinToRemove = await Get(Id);
            if (coinToRemove != null)
            {
                context.Coins.Remove(coinToRemove);
                await context.SaveChangesAsync();
            }

        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public async Task<CoinModel?> Get(int Id)
        {
            return await context.Coins.FirstOrDefaultAsync(c => c.Id == Id);
        }
        public async Task<CoinModel?> Get(string name)
        {
            return await context.Coins.FirstOrDefaultAsync(c => c.Name == name)!;
        }
        public async Task<IEnumerable<CoinModel>> GetAll()
        {
            return await context.Coins.ToListAsync();
        }

        public async Task UpdateCoin(CoinModel coin)
        {
            CoinModel? coinToUpdate = await Get(coin.Name);
            if (coinToUpdate != null)
            {
                coinToUpdate.CurrentPriceEuro = coin.CurrentPriceEuro;
                coinToUpdate.CurrentPriceDollar = coin.CurrentPriceDollar;
                coinToUpdate.CurrentPriceSek = coin.CurrentPriceSek;
                coinToUpdate.PriceChange24hPercent = coin.PriceChange24hPercent;
                coinToUpdate.LastUpdated = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }
    }
}
