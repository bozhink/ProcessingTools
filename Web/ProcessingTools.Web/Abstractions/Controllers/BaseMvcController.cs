namespace ProcessingTools.Web.Abstractions.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Web.Constants;
    using ProcessingTools.Web.Controllers;

    public abstract class BaseMvcController : Controller
    {
        protected object DefaultRouteValues => new
        {
            area = AreaNames.DefaultArea
        };

        protected string UserId => this.User?.Identity?.GetUserId();

        [HttpGet, ActionName(RouteValues.HelpActionName)]
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
