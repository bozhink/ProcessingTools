﻿namespace ProcessingTools.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Abstractions.Controllers;
    using Constants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using ProcessingTools.Services.Web.Managers;
    using ProcessingTools.Users.Data.Entity.Models;
    using ViewModels.Account;
    using Strings = Resources.Controllers.Account.Strings;

    [RequireHttps]
    [Authorize]
    public class AccountController : BaseMvcController
    {
        public const string ControllerName = "Account";
        public const string ConfirmEmailActionName = "ConfirmEmail";
        public const string ExternalLoginActionName = "ExternalLogin";
        public const string ExternalLoginCallbackActionName = "ExternalLoginCallback";
        public const string ExternalLoginConfirmationActionName = "ExternalLoginConfirmation";
        public const string ExternalLoginFailureActionName = "ExternalLoginFailure";
        public const string ForgotPasswordActionName = "ForgotPassword";
        public const string ForgotPasswordConfirmationActionName = "ForgotPasswordConfirmation";
        public const string LoginActionName = "Login";
        public const string LogOffActionName = "LogOff";
        public const string RegisterActionName = "Register";
        public const string ResetPasswordActionName = "ResetPassword";
        public const string ResetPasswordConfirmationActionName = "ResetPasswordConfirmation";
        public const string SendCodeActionName = "SendCode";
        public const string VerifyCodeActionName = "VerifyCode";

        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager => this.HttpContext.GetOwinContext().Authentication;

        // GET: /Account/Login
        [HttpGet, ActionName(AccountController.LoginActionName)]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        // POST: /Account/Login
        [HttpPost, ActionName(AccountController.LoginActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await this.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return this.View(ViewNames.Lockout);

                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction(
                        SendCodeActionName,
                        new
                        {
                            ReturnUrl = returnUrl,
                            RememberMe = model.RememberMe
                        });

                case SignInStatus.Failure:
                default:
                    this.AddErrors(Strings.InvalidLoginAttemptModelError);
                    return this.View(model);
            }
        }

        // GET: /Account/VerifyCode
        [HttpGet, ActionName(AccountController.VerifyCodeActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await this.SignInManager.HasBeenVerifiedAsync())
            {
                return this.View(ViewNames.Error);
            }

            return this.View(new VerifyCodeViewModel
            {
                Provider = provider,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        // POST: /Account/VerifyCode
        [HttpPost, ActionName(AccountController.VerifyCodeActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await this.SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);

            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return this.View(ViewNames.Lockout);

                case SignInStatus.Failure:
                default:
                    this.AddErrors(Strings.InvalidCodeModelError);
                    return this.View(model);
            }
        }

        // GET: /Account/Register
        [HttpGet, ActionName(AccountController.RegisterActionName)]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        // POST: /Account/Register
        [HttpPost, ActionName(AccountController.RegisterActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await this.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //// For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    //// Send an email with this link
                    ////string code = await this.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    ////var callbackUrl = this.Url.Action(
                    ////    ConfirmEmailActionName,
                    ////    ControllerName,
                    ////    new
                    ////    {
                    ////        userId = user.Id,
                    ////        code = code,
                    ////        area = AreaNames.DefaultArea
                    ////    },
                    ////    protocol: Request.Url.Scheme);

                    ////await this.UserManager.SendEmailAsync(
                    ////    userId: user.Id,
                    ////    subject: Strings.RegistrationConfirmationSubject,
                    ////    body: string.Format(Strings.RegistrationConfirmationBody, callbackUrl));

                    return this.RedirectToAction(HomeController.IndexActionName, HomeController.ControllerName);
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // GET: /Account/ConfirmEmail
        [HttpGet, ActionName(AccountController.ConfirmEmailActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(code))
            {
                var result = await this.UserManager.ConfirmEmailAsync(userId, code);
                if (result.Succeeded)
                {
                    return this.View();
                }
            }

            return this.View(ViewNames.Error);
        }

        // GET: /Account/ForgotPassword
        [HttpGet, ActionName(AccountController.ForgotPasswordActionName)]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return this.View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost, ActionName(AccountController.ForgotPasswordActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await this.UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.View(ForgotPasswordConfirmationActionName);
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await this.UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = this.Url.Action(
                    ResetPasswordConfirmationActionName,
                    ControllerName,
                    routeValues: new
                    {
                        userId = user.Id,
                        code = code,
                        area = AreaNames.DefaultArea
                    },
                    protocol: Request.Url.Scheme);

                await this.UserManager.SendEmailAsync(
                    userId: user.Id,
                    subject: Strings.ResetPasswordEmailSubject,
                    body: string.Format(Strings.ResetPasswordEmailBody, callbackUrl));

                return this.RedirectToAction(ForgotPasswordConfirmationActionName, ControllerName);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet, ActionName(AccountController.ForgotPasswordConfirmationActionName)]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        // GET: /Account/ResetPassword
        [HttpGet, ActionName(AccountController.ResetPasswordActionName)]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? this.View(ViewNames.Error) : this.View();
        }

        // POST: /Account/ResetPassword
        [HttpPost, ActionName(AccountController.ResetPasswordActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction(ResetPasswordConfirmationActionName, ControllerName);
            }

            var result = await this.UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction(ResetPasswordConfirmationActionName, ControllerName);
            }

            this.AddErrors(result);
            return this.View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [HttpGet, ActionName(AccountController.ResetPasswordConfirmationActionName)]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }

        // POST: /Account/ExternalLogin
        [HttpPost, ActionName(AccountController.ExternalLoginActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(
                provider,
                this.Url.Action(
                    ExternalLoginCallbackActionName,
                    ControllerName,
                    new
                    {
                        ReturnUrl = returnUrl,
                        area = AreaNames.DefaultArea
                    }));
        }

        // GET: /Account/SendCode
        [HttpGet, ActionName(AccountController.SendCodeActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await this.SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return this.View(ViewNames.Error);
            }

            var userFactors = await this.UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem
            {
                Text = purpose,
                Value = purpose
            }).ToList();

            return this.View(new SendCodeViewModel
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        // POST: /Account/SendCode
        [HttpPost, ActionName(AccountController.SendCodeActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // Generate the token and send it
            if (!await this.SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return this.View(ViewNames.Error);
            }

            return this.RedirectToAction(
                VerifyCodeActionName,
                new
                {
                    Provider = model.SelectedProvider,
                    ReturnUrl = model.ReturnUrl,
                    RememberMe = model.RememberMe
                });
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet, ActionName(AccountController.ExternalLoginCallbackActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return this.RedirectToAction(LoginActionName);
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await this.SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return this.View(ViewNames.Lockout);

                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction(
                        SendCodeActionName,
                        new
                        {
                            ReturnUrl = returnUrl,
                            RememberMe = false
                        });

                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    this.ViewBag.ReturnUrl = returnUrl;
                    this.ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return this.View(
                        ExternalLoginConfirmationActionName,
                        new ExternalLoginConfirmationViewModel
                        {
                            Email = loginInfo.Email
                        });
            }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost, ActionName(AccountController.ExternalLoginConfirmationActionName)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction(
                    ManageController.IndexActionName,
                    ManageController.ControllerName,
                    routeValues: this.DefaultRouteValues);
            }

            if (this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this.AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return this.View(ExternalLoginFailureActionName);
                }

                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await this.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return this.RedirectToLocal(returnUrl);
                    }
                }

                this.AddErrors(result);
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }

        // POST: /Account/LogOff
        [HttpPost, ActionName(AccountController.LogOffActionName)]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToAction(
                HomeController.IndexActionName,
                HomeController.ControllerName,
                routeValues: this.DefaultRouteValues);
        }

        // GET: /Account/ExternalLoginFailure
        [HttpGet, ActionName(AccountController.ExternalLoginFailureActionName)]
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return this.View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            // Used for XSRF protection when adding external logins
            public const string XsrfKey = "XsrfId";

            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = this.RedirectUri
                };

                if (this.UserId != null)
                {
                    properties.Dictionary[XsrfKey] = this.UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
            }
        }
    }
}
