// <copyright file="ManageController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Models;
    using ProcessingTools.Web.Documents.Models.ManageViewModels;
    using ProcessingTools.Web.Documents.Services;
    using ProcessingTools.Web.Services.Contracts;

    /// <summary>
    /// Manage
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Manage";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// SendVerificationEmail action name.
        /// </summary>
        public const string SendVerificationEmailActionName = nameof(SendVerificationEmail);

        /// <summary>
        /// ChangePassword action name.
        /// </summary>
        public const string ChangePasswordActionName = nameof(ChangePassword);

        /// <summary>
        /// SetPassword action name.
        /// </summary>
        public const string SetPasswordActionName = nameof(SetPassword);

        /// <summary>
        /// ExternalLogins action name.
        /// </summary>
        public const string ExternalLoginsActionName = nameof(ExternalLogins);

        /// <summary>
        /// LinkLogin action name.
        /// </summary>
        public const string LinkLoginActionName = nameof(LinkLogin);

        /// <summary>
        /// LinkLoginCallback action name.
        /// </summary>
        public const string LinkLoginCallbackActionName = nameof(LinkLoginCallback);

        /// <summary>
        /// RemoveLogin action name.
        /// </summary>
        public const string RemoveLoginActionName = nameof(RemoveLogin);

        /// <summary>
        /// TwoFactorAuthentication action name.
        /// </summary>
        public const string TwoFactorAuthenticationActionName = nameof(TwoFactorAuthentication);

        /// <summary>
        /// Disable2faWarning action name.
        /// </summary>
        public const string Disable2faWarningActionName = nameof(Disable2faWarning);

        /// <summary>
        /// Disable2fa action name.
        /// </summary>
        public const string Disable2faActionName = nameof(Disable2fa);

        /// <summary>
        /// EnableAuthenticator action name.
        /// </summary>
        public const string EnableAuthenticatorActionName = nameof(EnableAuthenticator);

        /// <summary>
        /// ResetAuthenticator action name.
        /// </summary>
        public const string ResetAuthenticatorActionName = nameof(ResetAuthenticator);

        /// <summary>
        /// ResetAuthenticatorWarning action name.
        /// </summary>
        public const string ResetAuthenticatorWarningActionName = nameof(ResetAuthenticatorWarning);

        /// <summary>
        /// GenerateRecoveryCodes action name.
        /// </summary>
        public const string GenerateRecoveryCodesActionName = nameof(GenerateRecoveryCodes);

#pragma warning disable S1075 // URIs should not be hardcoded
        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
#pragma warning restore S1075 // URIs should not be hardcoded

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ILogger logger;
        private readonly UrlEncoder urlEncoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageController"/> class.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign-in manager.</param>
        /// <param name="emailSender">Email sender.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="urlEncoder">URL encoder.</param>
        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.urlEncoder = urlEncoder ?? throw new ArgumentNullException(nameof(urlEncoder));
        }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        private string UserId => this.userManager.GetUserId(this.User);

        /// <summary>
        /// GET Manage
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = this.StatusMessage
            };

            return this.View(model);
        }

        /// <summary>
        /// POST Manage
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(IndexActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await this.userManager.SetEmailAsync(user, model.Email).ConfigureAwait(false);
                if (!setEmailResult.Succeeded)
                {
                    throw new OperationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, model.PhoneNumber).ConfigureAwait(false);
                if (!setPhoneResult.Succeeded)
                {
                    throw new OperationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToAction(IndexActionName);
        }

        /// <summary>
        /// POST Manage/SendVerificationEmail
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(SendVerificationEmailActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            var callbackUrl = this.Url.EmailConfirmationLink(user.Id, code, this.Request.Scheme);
            var email = user.Email;
            await this.emailSender.SendEmailConfirmationAsync(email, callbackUrl).ConfigureAwait(false);

            this.StatusMessage = "Verification email sent. Please check your email.";
            return this.RedirectToAction(IndexActionName);
        }

        /// <summary>
        /// GET Manage/ChangePassword
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(ChangePasswordActionName)]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var hasPassword = await this.userManager.HasPasswordAsync(user).ConfigureAwait(false);
            if (!hasPassword)
            {
                return this.RedirectToAction(SetPasswordActionName);
            }

            var model = new ChangePasswordViewModel { StatusMessage = this.StatusMessage };
            return this.View(model);
        }

        /// <summary>
        /// POST Manage/ChangePassword
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(ChangePasswordActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var changePasswordResult = await this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAwait(false);
            if (!changePasswordResult.Succeeded)
            {
                this.AddErrors(changePasswordResult);
                return this.View(model);
            }

            await this.signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
            this.logger.LogInformation("User changed their password successfully.");
            this.StatusMessage = "Your password has been changed.";

            return this.RedirectToAction(ChangePasswordActionName);
        }

        /// <summary>
        /// GET Manage/SetPassword
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(SetPasswordActionName)]
        public async Task<IActionResult> SetPassword()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var hasPassword = await this.userManager.HasPasswordAsync(user).ConfigureAwait(false);

            if (hasPassword)
            {
                return this.RedirectToAction(ChangePasswordActionName);
            }

            var model = new SetPasswordViewModel { StatusMessage = this.StatusMessage };
            return this.View(model);
        }

        /// <summary>
        /// POST Manage/SetPassword
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(SetPasswordActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var addPasswordResult = await this.userManager.AddPasswordAsync(user, model.NewPassword).ConfigureAwait(false);
            if (!addPasswordResult.Succeeded)
            {
                this.AddErrors(addPasswordResult);
                return this.View(model);
            }

            await this.signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
            this.StatusMessage = "Your password has been set.";

            return this.RedirectToAction(SetPasswordActionName);
        }

        /// <summary>
        /// GET Manage/ExternalLogins
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(ExternalLoginsActionName)]
        public async Task<IActionResult> ExternalLogins()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var model = new ExternalLoginsViewModel { CurrentLogins = await this.userManager.GetLoginsAsync(user).ConfigureAwait(false) };
            model.OtherLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait(false))
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            model.ShowRemoveButton = await this.userManager.HasPasswordAsync(user).ConfigureAwait(false) || model.CurrentLogins.Count > 1;
            model.StatusMessage = this.StatusMessage;

            return this.View(model);
        }

        /// <summary>
        /// POST Manage/LinkLogin
        /// </summary>
        /// <param name="provider">Login provider</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(LinkLoginActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme).ConfigureAwait(false);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = this.Url.Action(LinkLoginCallbackActionName);
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, this.UserId);
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// GET Manage/LinkLoginCallback
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(LinkLoginCallbackActionName)]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var info = await this.signInManager.GetExternalLoginInfoAsync(user.Id).ConfigureAwait(false);
            if (info == null)
            {
                throw new InformationNotFoundException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await this.userManager.AddLoginAsync(user, info).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new OperationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme).ConfigureAwait(false);

            this.StatusMessage = "The external login was added.";
            return this.RedirectToAction(ExternalLoginsActionName);
        }

        /// <summary>
        /// POST Manage/RemoveLogin
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(RemoveLoginActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var result = await this.userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new OperationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await this.signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
            this.StatusMessage = "The external login was removed.";
            return this.RedirectToAction(ExternalLoginsActionName);
        }

        /// <summary>
        /// GET Manage/TwoFactorAuthentication
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(TwoFactorAuthenticationActionName)]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var model = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await this.userManager.GetAuthenticatorKeyAsync(user).ConfigureAwait(false) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await this.userManager.CountRecoveryCodesAsync(user).ConfigureAwait(false),
            };

            return this.View(model);
        }

        /// <summary>
        /// GET Manage/Disable2faWarning
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Disable2faWarningActionName)]
        public async Task<IActionResult> Disable2faWarning()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for user with ID '{user.Id}'.");
            }

            return this.View(Disable2faActionName);
        }

        /// <summary>
        /// POST Manage/Disable2fa
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(Disable2faActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2fa()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var disable2faResult = await this.userManager.SetTwoFactorEnabledAsync(user, false).ConfigureAwait(false);
            if (!disable2faResult.Succeeded)
            {
                throw new OperationException($"Unexpected error occurred disabling 2FA for user with ID '{user.Id}'.");
            }

            this.logger.LogInformation("User with ID {UserId} has disabled 2fa.", user.Id);
            return this.RedirectToAction(TwoFactorAuthenticationActionName);
        }

        /// <summary>
        /// GET Manage/EnableAuthenticator
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(EnableAuthenticatorActionName)]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            var unformattedKey = await this.userManager.GetAuthenticatorKeyAsync(user).ConfigureAwait(false);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await this.userManager.ResetAuthenticatorKeyAsync(user).ConfigureAwait(false);
                unformattedKey = await this.userManager.GetAuthenticatorKeyAsync(user).ConfigureAwait(false);
            }

            var model = new EnableAuthenticatorViewModel
            {
                SharedKey = this.FormatKey(unformattedKey),
                AuthenticatorUri = this.GenerateQrCodeUri(user.Email, unformattedKey)
            };

            return this.View(model);
        }

        /// <summary>
        /// POST Manage/EnableAuthenticator
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(EnableAuthenticatorActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            // Strip spaces and hypens
            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await this.userManager.VerifyTwoFactorTokenAsync(user, this.userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode).ConfigureAwait(false);

            if (!is2faTokenValid)
            {
                this.ModelState.AddModelError("model.Code", "Verification code is invalid.");
                return this.View(model);
            }

            await this.userManager.SetTwoFactorEnabledAsync(user, true).ConfigureAwait(false);
            this.logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", user.Id);
            return this.RedirectToAction(GenerateRecoveryCodesActionName);
        }

        /// <summary>
        /// GET Manage/ResetAuthenticatorWarning
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(ResetAuthenticatorWarningActionName)]
        public IActionResult ResetAuthenticatorWarning()
        {
            return this.View(ResetAuthenticatorActionName);
        }

        /// <summary>
        /// POST Manage/ResetAuthenticator
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(ResetAuthenticatorActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticator()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            await this.userManager.SetTwoFactorEnabledAsync(user, false).ConfigureAwait(false);
            await this.userManager.ResetAuthenticatorKeyAsync(user).ConfigureAwait(false);
            this.logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", user.Id);

            return this.RedirectToAction(EnableAuthenticatorActionName);
        }

        /// <summary>
        /// GET Manage/GenerateRecoveryCodes
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(GenerateRecoveryCodesActionName)]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID '{this.UserId}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled.");
            }

            var recoveryCodes = await this.userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10).ConfigureAwait(false);
            var model = new GenerateRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() };

            this.logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", user.Id);

            return this.View(model);
        }

        /// <summary>
        /// Help
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
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

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenicatorUriFormat,
                this.urlEncoder.Encode("ProcessingTools.Web.Documents"),
                this.urlEncoder.Encode(email),
                unformattedKey);
        }
    }
}
