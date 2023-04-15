using Microsoft.ML.Data;

namespace TruckingIndustryAPI.Entities.Models
{
    public class SalesData
    {
        public long CarsId { get; set; }
        public long FoundationId { get; set; }
        public float FreightAmount { get; set; }
        public long CurrencyId { get; set; }
        public long StatusId { get; set; }
        public long EmployeeId { get; set; }
        public float Sales { get; set; }
        public float Label { get; set; } // новый столбец Label
    }
}
