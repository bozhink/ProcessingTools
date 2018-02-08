namespace ProcessingTools.Users.Data.Entity
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProcessingTools.Users.Data.Entity.Models;

    public class UsersDbContext : IdentityDbContext<User>
    {
        public UsersDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static UsersDbContext Create()
        {
            return new UsersDbContext();
        }
    }
}
