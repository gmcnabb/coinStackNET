using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class PortfolioTransaction
    {
        [Key]
        public int Id { get; set; }
        public int UserPortfolioId { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
