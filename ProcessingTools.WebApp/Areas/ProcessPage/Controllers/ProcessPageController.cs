namespace ProcessingTools.WebApp.Areas.ProcessPage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ProcessPageController : Controller
    {
        // GET: ProcessPage/ProcessPage
        public ActionResult Index()
        {
            return this.View();
        }
    }
}