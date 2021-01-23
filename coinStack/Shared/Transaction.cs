using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Type { get; set; }
        [Required]
        public string Coinid { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int USDValue { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public Coin Coin { get; set; }
    }
}
