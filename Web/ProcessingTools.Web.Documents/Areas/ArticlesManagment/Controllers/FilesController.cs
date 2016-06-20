namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Xsl;

    using ProcessingTools.Web.Common.Constants;

    using ViewModels.Files;

    public class FilesController : Controller
    {
        public static string ControllerName => "Files";

        private string DataDirectory => Server.MapPath("~/App_Data/");

        private string XslTansformFile => Path.Combine(Server.MapPath("~/App_Code/Xsl"), "main.xsl");

        // GET: File/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: File/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (Request?.Files == null || Request.Files.Count < 1)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            try
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid().ToString()}.xml";
                    var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                    file.SaveAs(path);
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: File/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                string fileName = this.GetFileNameById(id);
                System.IO.File.Delete(fileName);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: File/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: File/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                string fileName = this.GetFileNameById(id);

                string documentContent = await this.GetDocumentContent(fileName);

                var model = new FileDetailsViewModel
                {
                    Id = id,
                    Document = documentContent
                };

                return this.View(model);
            }
            catch (Exception e)
            {
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Details));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: File/Edit/5
        public ActionResult Edit(int id)
        {
            // TODO
            var error = new HandleErrorInfo(new NotImplementedException(), ControllerName, nameof(this.Edit));
            return this.View(ViewConstants.DefaultErrorViewName, error);
        }

        // POST: File/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            // TODO
            var error = new HandleErrorInfo(new NotImplementedException(), ControllerName, nameof(this.Edit));
            return this.View(ViewConstants.DefaultErrorViewName, error);
        }

        // GET: File
        public ActionResult Index()
        {
            try
            {
                var files = this.GetFiles();
                return this.View(files);
            }
            catch (Exception e)
            {
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Index));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        private async Task<string> GetDocumentContent(string fileName)
        {
            string content = string.Empty;

            var settings = new XmlReaderSettings
            {
                Async = true,
                CloseInput = true,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = false,
                CheckCharacters = false
            };

            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {
                var transform = new XslCompiledTransform();
                transform.Load(this.XslTansformFile);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    transform.Transform(reader, null, memoryStream);
                    memoryStream.Position = 0;
                    var streamReader = new StreamReader(memoryStream);

                    content = await streamReader.ReadToEndAsync();
                }
            }

            return content;
        }

        private string GetFileNameById(int id)
        {
            string file = Directory.GetFiles(this.DataDirectory)
                .FirstOrDefault(f => Path.GetFileName(f).GetHashCode() == id);

            return file;
        }

        private IEnumerable<FileMetadataViewModel> GetFiles()
        {
            return Directory.GetFiles(this.DataDirectory)
                .Select(f =>
                {
                    var fileInfo = new FileInfo(f);
                    return new FileMetadataViewModel
                    {
                        FileName = Path.GetFileName(f),
                        DateCreated = fileInfo.CreationTimeUtc,
                        DateModified = fileInfo.LastWriteTime
                    };
                })
                .OrderByDescending(i => i.DateModified)
                .ToList();
        }
    }
}
