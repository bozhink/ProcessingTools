// <copyright file="AccountController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

// See http://benfoster.io/blog/asp-net-identity-role-claims
// See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
// See https://stackoverflow.com/questions/39577906/add-claims-when-creating-a-new-user
namespace ProcessingTools.Web.Documents.Controllers
{
    using System;
    using System.Globalization;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Web.Services;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Models;
    using ProcessingTools.Web.Documents.Models.AccountViewModels;
    using ProcessingTools.Web.Documents.Services;

    /// <summary>
    /// Account controller.
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Account";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = ActionNames.Index;

        /// <summary>
        /// Login action name.
        /// </summary>
        public const string LoginActionName = nameof(Login);

        /// <summary>
        /// LoginWith2fa action name.
        /// </summary>
        public const string LoginWith2faActionName = nameof(LoginWith2fa);

        /// <summary>
        /// LoginWithRecoveryCode action name.
        /// </summary>
        public const string LoginWithRecoveryCodeActionName = nameof(LoginWithRecoveryCode);

        /// <summary>
        /// Lockout action name.
        /// </summary>
        public const string LockoutActionName = nameof(Lockout);

        /// <summary>
        /// Register action name.
        /// </summary>
        public const string RegisterActionName = nameof(Register);

        /// <summary>
        /// Logout action name.
        /// </summary>
        public const string LogoutActionName = nameof(Logout);

        /// <summary>
        /// ExternalLogin action name.
        /// </summary>
        public const string ExternalLoginActionName = nameof(ExternalLogin);

        /// <summary>
        /// ExternalLoginCallback action name.
        /// </summary>
        public const string ExternalLoginCallbackActionName = nameof(ExternalLoginCallback);

        /// <summary>
        /// ExternalLoginConfirmation action name.
        /// </summary>
        public const string ExternalLoginConfirmationActionName = nameof(ExternalLoginConfirmation);

        /// <summary>
        /// ConfirmEmail action name.
        /// </summary>
        public const string ConfirmEmailActionName = nameof(ConfirmEmail);

        /// <summary>
        /// ForgotPassword action name.
        /// </summary>
        public const string ForgotPasswordActionName = nameof(ForgotPassword);

        /// <summary>
        /// ForgotPasswordConfirmation action name.
        /// </summary>
        public const string ForgotPasswordConfirmationActionName = nameof(ForgotPasswordConfirmation);

        /// <summary>
        /// ResetPassword action name.
        /// </summary>
        public const string ResetPasswordActionName = nameof(ResetPassword);

        /// <summary>
        /// ResetPasswordConfirmation action name.
        /// </summary>
        public const string ResetPasswordConfirmationActionName = nameof(ResetPasswordConfirmation);

        /// <summary>
        /// AccessDenied action name.
        /// </summary>
        public const string AccessDeniedActionName = nameof(AccessDenied);

        /// <summary>
        /// ExternalLogin view name.
        /// </summary>
        public const string ExternalLoginViewName = nameof(ExternalLogin);

        /// <summary>
        /// ConfirmEmail view name.
        /// </summary>
        public const string ConfirmEmailViewName = nameof(ConfirmEmail);

        /// <summary>
        /// Error view name.
        /// </summary>
        public const string ErrorViewName = "Error";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign In manager.</param>
        /// <param name="emailSender">E-Mail sender.</param>
        /// <param name="logger">Logger.</param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ILogger<AccountController> logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// /Account.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [HttpPost]
        [ActionName(IndexActionName)]
        public IActionResult Index(Uri returnUrl = null)
        {
            if (returnUrl is null)
            {
                return this.RedirectToAction(HomeController.IndexActionName, HomeController.ControllerName, null);
            }

            return this.Redirect(returnUrl.ToString());
        }

        /// <summary>
        /// GET /Account/Login.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(LoginActionName)]
        public async Task<IActionResult> Login(Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme).ConfigureAwait(false);

            return this.View();
        }

        /// <summary>
        /// POST /Account/Login.
        /// </summary>
        /// <param name="model">Login request model.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(LoginActionName)]
        public async Task<IActionResult> Login(LoginViewModel model, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            if (model != null && this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    this.logger.LogInformation($"User {model.Email} logged in.");
                    return this.RedirectToLocal(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return this.RedirectToAction(LoginWith2faActionName, new { returnUrl, model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning($"User account {model.Email} locked out.");
                    return this.RedirectToAction(LockoutActionName);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        /// <summary>
        /// GET /Account/LoginWith2fa.
        /// </summary>
        /// <param name="rememberMe">Remember login.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(LoginWith2faActionName)]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            // Ensure the user has gone through the username & password screen first
            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };

            return this.View(model);
        }

        /// <summary>
        /// POST /Account/LoginWith2fa.
        /// </summary>
        /// <param name="model">Login with 2FA request model.</param>
        /// <param name="rememberMe">Remember login.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(LoginWith2faActionName)]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            if (model is null || !this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode
                .Replace(" ", string.Empty, false, CultureInfo.InvariantCulture)
                .Replace("-", string.Empty, false, CultureInfo.InvariantCulture);

            var result = await this.signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine).ConfigureAwait(false);

            if (result.Succeeded)
            {
                this.logger.LogInformation($"User with ID {user.Id} logged in with 2fa.");
                return this.RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                this.logger.LogWarning($"User with ID {user.Id} account locked out.");
                return this.RedirectToAction(LockoutActionName);
            }
            else
            {
                this.logger.LogWarning($"Invalid authenticator code entered for user with ID {user.Id}.");
                this.ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return this.View();
            }
        }

        /// <summary>
        /// GET /Account/LoginWithRecoveryCode.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(LoginWithRecoveryCodeActionName)]
        public async Task<IActionResult> LoginWithRecoveryCode(Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            // Ensure the user has gone through the username & password screen first
            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException($"Unable to load two-factor authentication user.");
            }

            return this.View();
        }

        /// <summary>
        /// POST /Account/LoginWithRecoveryCode.
        /// </summary>
        /// <param name="model">Login with recovery code request model.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(LoginWithRecoveryCodeActionName)]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            if (model is null || !this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty, false, CultureInfo.InvariantCulture);

            var result = await this.signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode).ConfigureAwait(false);

            if (result.Succeeded)
            {
                this.logger.LogInformation($"User with ID {user.Id} logged in with a recovery code.");
                return this.RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                this.logger.LogWarning($"User with ID {user.Id} account locked out.");
                return this.RedirectToAction(LockoutActionName);
            }
            else
            {
                this.logger.LogWarning($"Invalid recovery code entered for user with ID {user.Id}.");
                this.ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return this.View();
            }
        }

        /// <summary>
        /// GET /Account/Lockout.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(LockoutActionName)]
        public IActionResult Lockout()
        {
            return this.View();
        }

        /// <summary>
        /// GET /Account/Register.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(RegisterActionName)]
        public IActionResult Register(Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;
            return this.View();
        }

        /// <summary>
        /// POST /Account/Register.
        /// </summary>
        /// <param name="model">Register request model.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(RegisterActionName)]
        public async Task<IActionResult> Register(RegisterViewModel model, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            if (model != null && this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await this.userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    this.logger.LogInformation($"User {user.Email} created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                    var callbackUrl = this.Url.EmailConfirmationLink(user.Id, code, this.Request.Scheme);
                    await this.emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl).ConfigureAwait(false);

                    await this.signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
                    return this.RedirectToLocal(returnUrl);
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        /// <summary>
        /// POST /Account/Logout.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(LogoutActionName)]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync().ConfigureAwait(false);
            this.logger.LogInformation("User logged out.");
            return this.RedirectToAction(HomeController.IndexActionName, HomeController.ControllerName);
        }

        /// <summary>
        /// POST /Account/ExternalLogin.
        /// </summary>
        /// <param name="provider">Provider string.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(ExternalLoginActionName)]
        public IActionResult ExternalLogin(string provider, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            // Request a redirect to the external login provider.
            var redirectUrl = this.Url.Action(ExternalLoginCallbackActionName, ControllerName, new { returnUrl });
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return this.Challenge(properties, provider);
        }

        /// <summary>
        /// GET /Account/ExternalLoginCallback.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <param name="remoteError">Remote error.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(ExternalLoginCallbackActionName)]
        public async Task<IActionResult> ExternalLoginCallback(Uri returnUrl = null, string remoteError = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            if (remoteError != null)
            {
                this.ErrorMessage = $"Error from external provider: {remoteError}";
                return this.RedirectToAction(LoginActionName);
            }

            var info = await this.signInManager.GetExternalLoginInfoAsync().ConfigureAwait(false);
            if (info is null)
            {
                return this.RedirectToAction(LoginActionName);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await this.signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true).ConfigureAwait(false);
            if (result.Succeeded)
            {
                this.logger.LogInformation($"User logged in with {info.LoginProvider} provider.");
                return this.RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return this.RedirectToAction(LockoutActionName);
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                this.ViewData[ContextKeys.LoginProvider] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return this.View(ExternalLoginViewName, new ExternalLoginViewModel { Email = email });
            }
        }

        /// <summary>
        /// POST /Account/ExternalLoginConfirmation.
        /// </summary>
        /// <param name="model">External model request model.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(ExternalLoginConfirmationActionName)]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, Uri returnUrl = null)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            if (model != null && this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this.signInManager.GetExternalLoginInfoAsync().ConfigureAwait(false);
                if (info is null)
                {
                    throw new InformationNotFoundException("Error loading external login information during confirmation.");
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await this.userManager.CreateAsync(user).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    result = await this.userManager.AddLoginAsync(user, info).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
                        this.logger.LogInformation($"User created an account using {info.LoginProvider} provider.");

                        return this.RedirectToLocal(returnUrl);
                    }
                }

                this.AddErrors(result);
            }

            return this.View(ExternalLoginViewName, model);
        }

        /// <summary>
        /// GET /Account/ConfirmEmail.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="code">Code.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(ConfirmEmailActionName)]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId is null || code is null)
            {
                return this.RedirectToAction(HomeController.IndexActionName, HomeController.ControllerName);
            }

            var user = await this.userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{userId}'.");
            }

            var result = await this.userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);
            return this.View(result.Succeeded ? ConfirmEmailViewName : ErrorViewName);
        }

        /// <summary>
        /// GET /Account/ForgotPassword.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(ForgotPasswordActionName)]
        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        /// <summary>
        /// POST /Account/ForgotPassword.
        /// </summary>
        /// <param name="model">Forgot password request model.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(ForgotPasswordActionName)]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user is null || !(await this.userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.RedirectToAction(ForgotPasswordConfirmationActionName);
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await this.userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
                var callbackUrl = this.Url.ResetPasswordCallbackLink(user.Id, code, this.Request.Scheme);

                await this.emailSender.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>").ConfigureAwait(false);

                return this.RedirectToAction(ForgotPasswordConfirmationActionName);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        /// <summary>
        /// GET /Account/ForgotPasswordConfirmation.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(ForgotPasswordConfirmationActionName)]
        public IActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        /// <summary>
        /// GET /Account/ResetPassword.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(ResetPasswordActionName)]
        public IActionResult ResetPassword(string code = null)
        {
            if (code is null)
            {
                throw new InvalidCodeException("A code must be supplied for password reset.");
            }

            var model = new ResetPasswordViewModel { Code = code };
            return this.View(model);
        }

        /// <summary>
        /// POST /Account/ResetPassword.
        /// </summary>
        /// <param name="model">Reset password request model.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(ResetPasswordActionName)]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (model is null || !this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user is null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction(ResetPasswordConfirmationActionName);
            }

            var result = await this.userManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return this.RedirectToAction(ResetPasswordConfirmationActionName);
            }

            this.AddErrors(result);
            return this.View();
        }

        /// <summary>
        /// GET /Account/ResetPasswordConfirmation.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName(ResetPasswordConfirmationActionName)]
        public IActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }

        /// <summary>
        /// GET /Account/AccessDenied.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(AccessDeniedActionName)]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        /// <summary>
        /// Help.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [HttpPost]
        [ActionName(ActionNames.Help)]
        public IActionResult Help()
        {
            return this.View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(Uri uri)
        {
            string returnUrl = $"{uri}";

            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction(HomeController.IndexActionName, HomeController.ControllerName);
            }
        }
    }
}
