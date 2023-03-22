namespace TruckingIndustryAPI.Entities.Models
{
    public class CarWithFreeWeight : Car
    {
        public double FreeWeight => MaxWeight - LoadedWeight;
        public double LoadedWeight { get; set; }
    }
}
