namespace ProcessingTools.Users.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IUsersDbContextFactory : IDbContextFactory<UsersDbContext>
    {
    }
}
