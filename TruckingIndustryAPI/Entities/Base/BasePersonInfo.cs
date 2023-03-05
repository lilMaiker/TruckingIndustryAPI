namespace TruckingIndustryAPI.Entities.Base
{
    public class BasePersonInfo : BaseModelLong
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string SerialNumber { get; set; }
        public int PassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
