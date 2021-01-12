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
        public int Id { get; set; }
        [Required]
        public string GeckoId { get; set; }
        [Required]
        public string GeckoName { get; set; }
        [Required]
        public string GeckoSymbol { get; set; }
    }
}
