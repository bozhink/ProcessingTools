namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    public interface IEnvironment
    {
        IEnvironmentUser User { get; }

        IDateTimeProvider DateTime { get; }
    }
}
