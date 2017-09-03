// <copyright file="ApplicationSignInManager.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using ProcessingTools.Users.Data.Entity.Models;

    /// <summary>
    /// Default application sign-in manager
    /// </summary>
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSignInManager"/> class.
        /// </summary>
        /// <param name="userManager"><see cref="ApplicationUserManager"/> implementation</param>
        /// <param name="authenticationManager"><see cref="IAuthenticationManager"/> implementation</param>
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        /// Creates instance of <see cref="ApplicationSignInManager"/> class with default settings.
        /// </summary>
        /// <param name="options">Configuration for Identity options</param>
        /// <param name="context">Owin context to obtain db context</param>
        /// <returns>Instance of <see cref="ApplicationSignInManager"/> class with default settings</returns>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        /// <summary>
        /// Called to generate the <see cref="ClaimsIdentity"/> for the user. Adds additional claims before SignIn.
        /// </summary>
        /// <param name="user"><see cref="User"/> object</param>
        /// <returns><see cref="ClaimsIdentity"/> for the user</returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)this.UserManager);
        }
    }
}
