namespace ProcessingTools.Web.Controllers
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
    using ProcessingTools.Web.Common.Enumerations;
    using ViewModels.Manage;

    [RequireHttps]
    [Authorize]
    public class ManageController : BaseMvcController
    {
        public const string ControllerName = "Manage";
        public const string RemoveLoginActionName = "RemoveLogin";
        public const string ManageLoginsActionName = "ManageLogins";
        public const string AddPhoneNumberActionName = "AddPhoneNumber";
        public const string VerifyPhoneNumberActionName = "VerifyPhoneNumber";
        public const string EnableTwoFactorAuthenticationActionName = "EnableTwoFactorAuthentication";
        public const string DisableTwoFactorAuthenticationActionName = "DisableTwoFactorAuthentication";
        public const string RemovePhoneNumberActionName = "RemovePhoneNumber";
        public const string ChangePasswordActionName = "ChangePassword";
        public const string SetPasswordActionName = "SetPassword";
        public const string LinkLoginActionName = "LinkLogin";
        public const string LinkLoginCallbackActionName = "LinkLoginCallback";

        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: /Manage/Index
        [HttpGet, ActionName(ManageController.IndexActionName)]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : string.Empty;

            var userId = this.UserId;
            var viewModel = new IndexViewModel
            {
                HasPassword = this.HasPassword(),
                PhoneNumber = await this.UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await this.UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await this.UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            return this.View(viewModel);
        }

        // POST: /Manage/RemoveLogin
        [HttpPost, ActionName(ManageController.RemoveLoginActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await this.UserManager.RemoveLoginAsync(this.UserId, new UserLoginInfo(loginProvider, providerKey));

            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.UserId);
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }

            return this.RedirectToAction(ManageController.ManageLoginsActionName, new { Message = message });
        }

        // GET: /Manage/AddPhoneNumber
        [HttpGet, ActionName(ManageController.AddPhoneNumberActionName)]
        public ActionResult AddPhoneNumber()
        {
            return this.View();
        }

        // POST: /Manage/AddPhoneNumber
        [HttpPost, ActionName(ManageController.AddPhoneNumberActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // Generate the token and send it
            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(this.UserId, model.Number);

            if (this.UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };

                await this.UserManager.SmsService.SendAsync(message);
            }

            return this.RedirectToAction(ManageController.VerifyPhoneNumberActionName, new { PhoneNumber = model.Number });
        }

        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost, ActionName(ManageController.EnableTwoFactorAuthenticationActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(this.UserId, true);
            var user = await this.UserManager.FindByIdAsync(this.UserId);
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return this.RedirectToAction(ManageController.IndexActionName);
        }

        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost, ActionName(ManageController.DisableTwoFactorAuthenticationActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(this.UserId, false);
            var user = await this.UserManager.FindByIdAsync(this.UserId);
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return this.RedirectToAction(ManageController.IndexActionName);
        }

        // GET: /Manage/VerifyPhoneNumber
        [HttpGet, ActionName(ManageController.VerifyPhoneNumberActionName)]
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(this.UserId, phoneNumber);

            // Send an SMS through the SMS provider to verify the phone number
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return this.View(ViewNames.Error);
            }

            return this.View(new VerifyPhoneNumberViewModel
            {
                PhoneNumber = phoneNumber
            });
        }

        // POST: /Manage/VerifyPhoneNumber
        [HttpPost, ActionName(ManageController.VerifyPhoneNumberActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.ChangePhoneNumberAsync(this.UserId, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.UserId);
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return this.RedirectToAction(ManageController.IndexActionName, new { Message = ManageMessageId.AddPhoneSuccess });
                }

                this.AddErrors("Failed to verify phone");
            }

            return this.View(model);
        }

        // POST: /Manage/RemovePhoneNumber
        [HttpPost, ActionName(ManageController.RemovePhoneNumberActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await this.UserManager.SetPhoneNumberAsync(this.UserId, null);
            if (!result.Succeeded)
            {
                return this.RedirectToAction(ManageController.IndexActionName, new { Message = ManageMessageId.Error });
            }

            var user = await this.UserManager.FindByIdAsync(this.UserId);
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return this.RedirectToAction(ManageController.IndexActionName, new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        // GET: /Manage/ChangePassword
        [HttpGet, ActionName(ManageController.ChangePasswordActionName)]
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost, ActionName(ManageController.ChangePasswordActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.ChangePasswordAsync(this.UserId, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.UserId);
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return this.RedirectToAction(ManageController.IndexActionName, new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                this.AddErrors(result);
            }

            return this.View(model);
        }

        // GET: /Manage/SetPassword
        [HttpGet, ActionName(ManageController.SetPasswordActionName)]
        public ActionResult SetPassword()
        {
            return this.View();
        }

        // POST: /Manage/SetPassword
        [HttpPost, ActionName(ManageController.SetPasswordActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.AddPasswordAsync(this.UserId, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.UserId);
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return this.RedirectToAction(ManageController.IndexActionName, new { Message = ManageMessageId.SetPasswordSuccess });
                }

                this.AddErrors(result);
            }

            return this.View(model);
        }

        // GET: /Manage/ManageLogins
        [HttpGet, ActionName(ManageController.ManageLoginsActionName)]
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;

            var user = await this.UserManager.FindByIdAsync(this.UserId);
            if (user == null)
            {
                return this.View(ViewNames.Error);
            }

            var userLogins = await this.UserManager.GetLoginsAsync(this.UserId);
            var otherLogins = this.AuthenticationManager.GetExternalAuthenticationTypes()
                .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider))
                .ToList();

            this.ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        // POST: /Manage/LinkLogin
        [HttpPost, ActionName(ManageController.LinkLoginActionName)]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, this.Url.Action(LinkLoginCallbackActionName, ControllerName), this.UserId);
        }

        // GET: /Manage/LinkLoginCallback
        [HttpGet, ActionName(ManageController.LinkLoginCallbackActionName)]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(AccountController.ChallengeResult.XsrfKey, this.UserId);
            if (loginInfo == null)
            {
                return this.RedirectToAction(ManageController.ManageLoginsActionName, new { Message = ManageMessageId.Error });
            }

            var result = await this.UserManager.AddLoginAsync(this.UserId, loginInfo.Login);
            if (result.Succeeded)
            {
                return this.RedirectToAction(ManageController.ManageLoginsActionName);
            }

            return this.RedirectToAction(ManageController.ManageLoginsActionName, new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(this.UserId);
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = this.UserManager.FindById(this.UserId);
            if (user != null)
            {
                return user.PhoneNumber != null;
            }

            return false;
        }
    }
}
