// <copyright file="AdminController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Models;

    /// <summary>
    /// Admin controller.
    /// </summary>
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class AdminController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Admin";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Register first user action name.
        /// </summary>
        public const string RegisterFirstUserActionName = nameof(RegisterFirstUser);

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="roleManager">Role manager.</param>
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        /// <summary>
        /// /Admin/Index
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// /Admin/RegisterFirstUser
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [AllowAnonymous]
        public async Task<IActionResult> RegisterFirstUser()
        {
            if (this.userManager.Users.Any())
            {
                return this.RedirectToActionPermanent(IndexActionName);
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@admin.com",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                AccessFailedCount = 0,
                LockoutEnabled = true,
                EmailConfirmed = true,
                PasswordHash = null
            };

            var identityResult = await this.userManager.CreateAsync(user).ConfigureAwait(false);

            if (identityResult.Succeeded)
            {
                await this.userManager.ChangePasswordAsync(user, user.PasswordHash, "123").ConfigureAwait(false);
            }

            // Adding Admin Role
            var roleCheck = await this.roleManager.RoleExistsAsync(nameof(UserRole.Admin));
            if (!roleCheck)
            {
                // Create the roles and seed them to the database
                identityResult = await this.roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = nameof(UserRole.Admin) });
            }

            if (identityResult.Succeeded)
            {
                // Assign Admin role to the main User here we have given our newly registered login id for Admin management
                user = await this.userManager.FindByEmailAsync(user.Email);
                await this.userManager.AddToRoleAsync(user, nameof(UserRole.Admin));
            }

            return this.RedirectToActionPermanent(IndexActionName);
        }
    }
}
