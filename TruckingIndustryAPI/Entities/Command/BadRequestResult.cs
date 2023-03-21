namespace TruckingIndustryAPI.Entities.Command
{
    public class BadRequestResult : ICommandResult
    {
        public bool Success => false;
        public object Data => null;
        public IEnumerable<string> Errors { get; set; }
    }
}
