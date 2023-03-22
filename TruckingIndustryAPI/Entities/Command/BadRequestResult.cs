namespace TruckingIndustryAPI.Entities.Command
{
    public class BadRequestResult : ICommandResult
    {
        public bool Success => false;
        public object Data => null;
        public string Error { get; set; }
    }
}
