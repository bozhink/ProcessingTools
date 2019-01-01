namespace ProcessingTools.Web.Abstractions.Controllers
{
    using System.Web.Mvc;

    public abstract class BaseMvcController : Controller
    {
        protected string UserId => "fake-user-id";

        protected void AddErrors(params string[] errors)
        {
            foreach (var error in errors)
            {
                this.ModelState.AddModelError("Error", error);
            }
        }
    }
}
