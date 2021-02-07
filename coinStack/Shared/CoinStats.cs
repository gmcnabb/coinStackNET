using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class CoinStats
    {
        public CoinStats(string id, double price)
        {
            coinId = id;
            quantityBought = 0;
            quantitySold = 0;
            sumOfCosts = 0;
            sumOfProceeds = 0;
            totalHoldings = 0;
            netCost = 0;
            avgNetCost = 0;
            marketValueHoldings = 0;
            PnL = 0;
            percentChange = 0;
            current_price = price;
        }
        public string coinId { get; set; }
        public double quantityBought { get; set; }
        public double quantitySold { get; set; }
        public double sumOfCosts { get; set; }
        public double sumOfProceeds { get; set; }
        public double totalHoldings { get; set; }
        public double netCost { get; set; }
        public double avgNetCost { get; set; }
        public double marketValueHoldings { get; set; }
        public double PnL { get; set; }
        public double percentChange { get; set; }
        public double current_price { get; set; }
    }
}
