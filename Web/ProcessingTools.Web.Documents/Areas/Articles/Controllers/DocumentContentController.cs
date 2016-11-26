using Strings = Resources.Strings;

namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Xml;

    using Microsoft.AspNet.Identity;

    using ProcessingTools.Common;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Net.Constants;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Areas.Articles.Models.DocumentContent;

    using Resources;

    [Authorize]
    public class DocumentContentController : Controller
    {
        private const int MaximalJsonLength = int.MaxValue;

        private readonly IXmlPresenter presenter;

        // TODO: To be removed
        private readonly int fakeArticleId = 0;

        public DocumentContentController(IXmlPresenter presenter)
        {
            if (presenter == null)
            {
                throw new ArgumentNullException(nameof(presenter));
            }

            this.presenter = presenter;
        }

        // GET: Articles/DocumentContent
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(
                actionName: nameof(FilesController.Index),
                controllerName: ControllerNames.FilesControllerName);
        }

        [HttpPut]
        public async Task<JsonResult> SaveHtml(string id, string content)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.InvalidDocumentIdJsonResult();
            }

            var userId = User.Identity.GetUserId();
            var document = new DocumentServiceModel
            {
                Id = id,
                ContentType = ContentTypeConstants.XmlContentType
            };

            await this.presenter.SaveHtml(userId, this.fakeArticleId, document, content);

            return this.DocumentSavedSuccessfullyJsonResult();
        }

        [HttpPut]
        public async Task<JsonResult> SaveXml(string id, string content)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.InvalidDocumentIdJsonResult();
            }

            var userId = User.Identity.GetUserId();
            var document = new DocumentServiceModel
            {
                Id = id,
                ContentType = ContentTypeConstants.XmlContentType
            };

            await this.presenter.SaveXml(userId, this.fakeArticleId, document, content);

            return this.DocumentSavedSuccessfullyJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> GetHtml(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.InvalidDocumentIdJsonResult();
            }

            var userId = User.Identity.GetUserId();
            var content = await this.presenter.GetHtml(userId, this.fakeArticleId, id);
            return this.ContentJsonResult(id, content);
        }

        [HttpPost]
        public async Task<JsonResult> GetXml(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.InvalidDocumentIdJsonResult();
            }

            var userId = User.Identity.GetUserId();
            var content = await this.presenter.GetXml(userId, this.fakeArticleId, id);
            return this.ContentJsonResult(id, content);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.NotFoundJsonResult(filterContext.Exception);
            }
            else if (filterContext.Exception is XmlException)
            {
                filterContext.Result = this.InvalidXmlJsonResult(filterContext.Exception);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestJsonResult(filterContext.Exception);
            }
            else
            {
                filterContext.Result = this.InternalServerErrorJsonResult(filterContext.Exception);
            }

            filterContext.ExceptionHandled = true;
        }

        private JsonResult DocumentSavedSuccessfullyJsonResult()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new MessageResponseModel
                {
                    Status = MessageResponseStatus.OK,
                    Message = Strings.DocumentSavedSuccessfullyResponseMessage
                }
            };
        }

        private JsonResult ContentJsonResult(string id, string content)
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new ContentResponseModel
                {
                    ArticleId = this.fakeArticleId.ToString(),
                    DocumentId = id,
                    Content = content
                },
                MaxJsonLength = MaximalJsonLength
            };
        }

        private JsonResult InternalServerErrorJsonResult(Exception e)
        {
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new MessageResponseModel
                {
                    Status = MessageResponseStatus.Error,
                    Message = e.Message
                }
            };
        }

        private JsonResult InvalidDocumentIdJsonResult()
        {
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new MessageResponseModel
                {
                    Status = MessageResponseStatus.Error,
                    Message = "Invalid document ID"
                }
            };
        }

        private JsonResult BadRequestJsonResult(Exception e)
        {
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new MessageResponseModel
                {
                    Status = MessageResponseStatus.Error,
                    Message = "Bad Request: " + e?.Message
                }
            };
        }

        private JsonResult InvalidXmlJsonResult(Exception e)
        {
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new MessageResponseModel
                {
                    Status = MessageResponseStatus.Error,
                    Message = "Invalid XML: " + e?.Message
                }
            };
        }

        private JsonResult NotFoundJsonResult(Exception e)
        {
            this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                Data = new MessageResponseModel
                {
                    Status = MessageResponseStatus.Error,
                    Message = "Not Found: " + e?.Message
                }
            };
        }
    }
}
