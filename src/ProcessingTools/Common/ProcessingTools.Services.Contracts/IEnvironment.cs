namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    public interface IEnvironment : IService
    {
        IEnvironmentUser User { get; }

        IDateTimeProvider DateTime { get; }
    }
}
