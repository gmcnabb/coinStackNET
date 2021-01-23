using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class Coin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(100)]
        public string id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [Required]
        [MaxLength(100)]
        public string symbol { get; set; }
        [MaxLength(300)]
        public string image { get; set; }
        [MaxLength(100)]
        public string last_updated { get; set; }
    }
}
