namespace TruckingIndustryAPI.Entities.Command
{
    public class NotFoundResult : ICommandResult
    {
        public bool Success => false;
        public object Data { get; set; }
        public string Error { get; set; } = "Not Found";
    }
}
