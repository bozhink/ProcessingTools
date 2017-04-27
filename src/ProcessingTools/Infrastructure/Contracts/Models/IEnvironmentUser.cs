namespace ProcessingTools.Contracts.Models
{
    using ProcessingTools.Enumerations;

    public interface IEnvironmentUser : IStringIdentifiable
    {
        string UserName { get; }

        string Email { get; }

        UserRole Role { get; }
    }
}
