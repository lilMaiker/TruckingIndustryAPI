using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingIndustryAPI.Entities.Models
{
    public class Bid : Base.BaseModelLong
    {
        [ForeignKey("Cars")]
        public long CarsId { get; set; }
        public Car? Cars { get; set; }

        [ForeignKey("Foundation")]
        public long FoundationId { get; set; }
        public Foundation? Foundation { get; set; }

        public double FreightAMount { get; set; }

        [ForeignKey("Currency")]
        public long CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public DateTime? DateToLoad { get; set; }
        public DateTime? DateToUnload { get; set; }

        public string ActAccNumber { get; set; }

        [ForeignKey("Status")]
        public long StatusId { get; set; }
        public Status? Status { get; set; }

        public DateTime? PayDate { get; set; }

        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
