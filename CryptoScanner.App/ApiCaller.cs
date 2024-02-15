using CryptoScanner.Data.Repositories.Interfaces;

namespace CryptoScanner.App
{
    public class ApiCaller
    {
        private readonly ICoinsRepository repo;

        public ApiCaller(ICoinsRepository repo)
        {
            this.repo = repo;
        }
    }
}
