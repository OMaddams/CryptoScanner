using CryptoScanner.Data.Models;

namespace CryptoScanner.Data.Repositories.Interfaces
{
    public interface ICoinsRepository : IDisposable
    {
        Task<IEnumerable<CoinModel>> GetAll();
        Task<CoinModel?> Get(int Id);
        Task<CoinModel?> Get(string name);
        Task AddCoin(CoinModel coin);
        Task UpdateCoin(CoinModel coin);
        Task DeleteCoin(int Id);

    }
}
