using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CryptoScanner.Data.Models
{
    public class CoinModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ApiId { get; set; } = null!;
        [Precision(18, 2)]
        public decimal CurrentPriceSek { get; set; }
        [Precision(18, 2)]
        public decimal CurrentPriceEuro { get; set; }
        [Precision(18, 2)]
        public decimal CurrentPriceDollar { get; set; }
        [Precision(18, 2)]
        public decimal PriceChange24hPercent { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
