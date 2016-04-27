namespace FileUploadMvc.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Xsl;
    using Models;

    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            string directory = Server.MapPath("~/App_Data/");
            var files = Directory.GetFiles(directory)
                .Select(f =>
                {
                    var fileInfo = new FileInfo(f);
                    return new ListModel
                    {
                        FileName = Path.GetFileName(f),
                        DateCreated = fileInfo.CreationTimeUtc,
                        DateModified = fileInfo.LastWriteTime
                    };
                });

            return this.View(files);
        }

        // GET: File/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string fileName = this.GetFileNameById(id);

            string document = string.Empty;

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
                transform.Load(Path.Combine(Server.MapPath("~/Xsl/"), "main.xsl"));

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    transform.Transform(reader, null, memoryStream);
                    memoryStream.Position = 0;
                    var streamReader = new StreamReader(memoryStream);

                    document = await streamReader.ReadToEndAsync();
                }
            }

            var model = new DetailsModel
            {
                Id = id,
                Document = document
            };

            return this.View(model);
        }

        // GET: File/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: File/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: File/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View("Error");
        }

        // POST: File/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View("Error");
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

        private string GetFileNameById(int id)
        {
            string directory = Server.MapPath("~/App_Data/");
            string file = Directory.GetFiles(directory)
                .FirstOrDefault(f => Path.GetFileName(f).GetHashCode() == id);

            return file;
        }
    }
}
