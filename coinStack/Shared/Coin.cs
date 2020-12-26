using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class Coin
    {
        [Key]
        public int CoinId { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string symbol { get; set; }
    }
}
