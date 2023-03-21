namespace TruckingIndustryAPI.Entities.Command
{
    public class NotFoundResult : ICommandResult
    {
        public bool Success => false;
        public object Data => null;
        public string Errors { get; set; } = "Not Found";
    }
}
