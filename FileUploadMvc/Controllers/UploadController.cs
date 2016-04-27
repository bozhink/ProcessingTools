namespace FileUploadMvc.Controllers
{
    using System;
    using System.IO;
    using System.Net.Mail;
    using System.Web.Mvc;

    using Models;

    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count < 1)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid().ToString()}.xml";
                var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file.SaveAs(path);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public virtual ActionResult ContactUs(ContactUsModel Model)
        {
            if (Model.attachment != null && Model.attachment.ContentLength > 0)
            {
                //save the file

                //Send it as an attachment
                Attachment messageAttachment = new Attachment(Model.attachment.InputStream, Model.attachment.FileName);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public ActionResult Error()
        {
            return this.View();
        }
    }
}
