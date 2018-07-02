// <copyright file="ApplicationUserManager.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services
{
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using ProcessingTools.Users.Data.Entity;
    using ProcessingTools.Users.Data.Entity.Models;

    /// <summary>
    /// Default <see cref="UserManager{TUser}"/> implementation
    /// </summary>
    public class ApplicationUserManager : UserManager<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="store">IUserStore instance</param>
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        /// <summary>
        /// Creates instance of <see cref="ApplicationUserManager"/> class with default settings.
        /// </summary>
        /// <param name="options">Configuration for Identity options</param>
        /// <param name="context">Owin context to obtain db context</param>
        /// <returns>Instance of <see cref="ApplicationUserManager"/> class with default settings</returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var db = context.Get<UsersDbContext>();
            var manager = new ApplicationUserManager(new UserStore<User>(db));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            var phoneNumberTokenProvider = new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Your security code is {0}"
            };

            manager.RegisterTwoFactorProvider("Phone Code", phoneNumberTokenProvider);
            manager.SmsService = new SmsService();

            var emailTokenProvider = new EmailTokenProvider<User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            };

            manager.RegisterTwoFactorProvider("Email Code", emailTokenProvider);
            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                var dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
                manager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtector);
            }

            return manager;
        }
    }
}
