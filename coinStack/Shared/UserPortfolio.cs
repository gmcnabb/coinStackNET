﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class UserPortfolio
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool CurrentlySelected { get; set; }
        public List<PortfolioTransaction> PortfolioTransactions { get; set; }
        public List<PortfolioCoin> PortfolioCoins { get; set; }
    }
}
