namespace ProcessingTools.Users.Data.Entity
{
    using ProcessingTools.Users.Data.Entity.Contracts;

    public class UsersDbContextFactory : IUsersDbContextFactory
    {
        public UsersDbContext Create() => UsersDbContext.Create();
    }
}
