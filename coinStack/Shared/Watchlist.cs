using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class Watchlist
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<Coin> Coins { get; set; }
    }
}
