namespace TruckingIndustryAPI.Entities.Command
{
    public class NotFoundResult : ICommandResult
    {
        public bool Success => false;
        public object Data => null;
        public IEnumerable<string> Errors => new List<string> { "Not found" };
    }
}
