using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class PortfolioCoin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserPortfolioId { get; set; }
        [Required]
        public int CoinId { get; set; }
        public Coin Coin { get; set; }
    }
}
