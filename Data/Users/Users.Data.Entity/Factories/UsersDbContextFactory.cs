namespace ProcessingTools.Users.Data.Entity.Factories
{
    using Contracts;

    public class UsersDbContextFactory : IUsersDbContextFactory
    {
        public UsersDbContext Create() => UsersDbContext.Create();
    }
}
