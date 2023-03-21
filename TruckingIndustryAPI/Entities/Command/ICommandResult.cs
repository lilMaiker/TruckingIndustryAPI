namespace TruckingIndustryAPI.Entities.Command
{
    public interface ICommandResult
    {
        bool Success { get; }
        object Data { get; }
        IEnumerable<string> Errors { get; }
    }
}
