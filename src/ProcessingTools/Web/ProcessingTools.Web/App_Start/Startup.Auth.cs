namespace ProcessingTools.Web
{
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.Google;
    using Microsoft.Owin.Security.MicrosoftAccount;
    using Microsoft.Owin.Security.Twitter;
    using Owin;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Users.Data.Entity;
    using ProcessingTools.Users.Data.Entity.Models;
    using ProcessingTools.Web.Services;

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(UsersDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            try
            {
                // See https://apps.dev.microsoft.com
                string microsoftClientId = AppSettings.MicrosoftClientId;
                string microsoftClientSecret = AppSettings.MicrosoftClientSecret;
                if (!string.IsNullOrWhiteSpace(microsoftClientId) && !string.IsNullOrWhiteSpace(microsoftClientSecret))
                {
                    app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions
                    {
                        ClientId = microsoftClientId,
                        ClientSecret = microsoftClientSecret,
                        CallbackPath = new PathString("/signin-microsoft")
                    });
                }
            }
            catch
            {
                // Ignore if Microsoft api fails
            }

            try
            {
                string twitterConsumerKey = AppSettings.TwitterConsumerKey;
                string twitterConsumerSecret = AppSettings.TwitterConsumerSecret;
                if (!string.IsNullOrWhiteSpace(twitterConsumerKey) && !string.IsNullOrWhiteSpace(twitterConsumerSecret))
                {
                    app.UseTwitterAuthentication(new TwitterAuthenticationOptions
                    {
                        ConsumerKey = twitterConsumerKey,
                        ConsumerSecret = twitterConsumerSecret,
                        CallbackPath = new PathString("/signin-twitter"),
                        BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(CertificateValidationConstants.ValidSubjectKeyIdentifiers)
                    });
                }
            }
            catch
            {
                // Ignore if Twitter api fails
            }

            try
            {
                string facebookAppId = AppSettings.FacebookAppId;
                string facebookAppSecret = AppSettings.FacebookAppSecret;
                if (!string.IsNullOrWhiteSpace(facebookAppId) && !string.IsNullOrWhiteSpace(facebookAppSecret))
                {
                    app.UseFacebookAuthentication(new FacebookAuthenticationOptions
                    {
                        AppId = facebookAppId,
                        AppSecret = facebookAppSecret,
                        CallbackPath = new PathString("/signin-facebook")
                    });
                }
            }
            catch
            {
                // Ignore if Facebook api fails
            }

            try
            {
                string googleClientId = AppSettings.GoogleClientId;
                string googleClientSecret = AppSettings.GoogleClientSecret;
                if (!string.IsNullOrWhiteSpace(googleClientId) && !string.IsNullOrWhiteSpace(googleClientSecret))
                {
                    app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
                    {
                        ClientId = googleClientId,
                        ClientSecret = googleClientSecret,
                        CallbackPath = new PathString("/signin-google")
                    });
                }
            }
            catch
            {
                // Ignore if Google api fails
            }
        }
    }
}
