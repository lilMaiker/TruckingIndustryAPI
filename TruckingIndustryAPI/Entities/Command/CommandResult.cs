namespace TruckingIndustryAPI.Entities.Command
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Errors { get; set; }
    }
}
