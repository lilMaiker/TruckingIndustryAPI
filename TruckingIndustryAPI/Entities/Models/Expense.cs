using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Expense : Base.BaseModelLong
    {
        public string NameExpense { get; set; }
        public double Amount { get; set; }

        [ForeignKey("Currency")]
        public long CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public DateTime? DateTransfer { get; set; }

        public string Commnet { get; set; }

        [ForeignKey("Bids")]
        public long BidsId { get; set; }
        public Bid Bids { get; set; }
    }
}
