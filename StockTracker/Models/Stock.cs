using System.ComponentModel.DataAnnotations;

namespace StockTracker.Models
{
    public class Stock
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Display(Name = "Company name")]
        public string Symbol { get; set; }

        [Display(Name = "Close date")]
        public DateTime CloseDate { get; set; }

        [Display(Name = "Price at close")]
        public decimal Close { get; set; }

        [Display(Name = "Price at close - adjusted")]
        public decimal AdjustedClose { get; set; }

        [Display(Name = "Volume of traded stocks")]
        public long Volume { get; set; }

        public Stock()
        {
            // Placeholder
        }
    }
}
