namespace ProcessingTools.Web.Abstractions.Controllers
{
    using Microsoft.AspNet.Identity;
    using System.Net;
    using System.Web.Mvc;
    using Constants;
    using ProcessingTools.Web.Controllers;

    public abstract class BaseMvcController : Controller
    {
        public const string IndexActionName = "Index";
        public const string HelpActionName = "Help";

        protected object DefaultRouteValues => new { area = AreaNames.DefaultArea };

        protected string UserId => this.User?.Identity?.GetUserId();

        [HttpGet, ActionName(HelpActionName)]
        public virtual ActionResult Help()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        protected void AddErrors(params string[] errors)
        {
            foreach (var error in errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(
                HomeController.IndexActionName,
                HomeController.ControllerName,
                routeValues: this.DefaultRouteValues);
        }
    }
}
