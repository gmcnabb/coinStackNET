using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class WatchlistCoin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserWatchlistId { get; set; }
        [Required]
        public int CoinId { get; set; }
        public Coin Coin { get; set; }
    }
}
