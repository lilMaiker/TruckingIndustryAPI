using Microsoft.ML.Data;

namespace TruckingIndustryAPI.Entities.Models
{
    public class SalesPrediction
    {
        [ColumnName("Score")]
        public float Prediction { get; set; }
    }
}
