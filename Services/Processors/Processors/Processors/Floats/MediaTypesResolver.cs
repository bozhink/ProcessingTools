namespace ProcessingTools.Processors.Floats
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Floats;
    using Models.Floats;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.MediaType.Services.Data.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class MediaTypesResolver : IMediaTypesResolver
    {
        private readonly ILogger logger;
        private readonly IMediaTypeDataService service;

        public MediaTypesResolver(IMediaTypeDataService service, ILogger logger)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
            this.logger = logger;
        }

        public async Task<object> Parse(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            XmlNodeList mediaElements = context.SelectNodes(XPathConstants.MediaElementXPath);

            if (mediaElements.Count < 1)
            {
                return false;
            }

            var extensions = this.GetExtensions(mediaElements);
            var mediatypes = await this.ResolveMediaTypes(extensions);

            foreach (XmlNode mediaNode in mediaElements)
            {
                var fileName = this.GetFileName(mediaNode);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    this.logger?.Log(LogType.Warning, "File name is not provided.");
                    continue;
                }

                var mediatype = this.GetMediaTypeOfFileName(mediatypes, fileName);

                mediaNode
                    .SetOrUpdateAttribute(AttributeNames.MimeType, mediatype.MimeType)
                    .SetOrUpdateAttribute(AttributeNames.MimeSubtype, mediatype.MimeSubtype);
            }

            return true;
        }

        private IEnumerable<string> GetExtensions(XmlNodeList mediaNodes)
        {
            return mediaNodes.Cast<XmlNode>()
                .Select(this.GetFileName)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Select(this.GetFileExtension)
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Distinct()
                .ToArray();
        }

        private string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).TrimStart('.');
        }

        private string GetFileName(XmlNode mediaNode)
        {
            return mediaNode.Attributes[AttributeNames.XLinkHrefFullAttributeName]?.InnerText;
        }

        private IMediaType GetMediaTypeOfFileName(IEnumerable<IMediaType> mediatypes, string fileName)
        {
            IMediaType mediatype = new MediaTypeResponseModel
            {
                FileExtension = this.GetFileExtension(fileName)
            };

            if (!string.IsNullOrEmpty(mediatype.FileExtension))
            {
                var result = mediatypes.FirstOrDefault(t => t.FileExtension == mediatype.FileExtension);
                if (result != null)
                {
                    mediatype = result;
                }
            }

            return mediatype;
        }

        private async Task<IMediaType> ResolveFileExtensionToMediaType(string extension)
        {
            var result = new MediaTypeResponseModel
            {
                FileExtension = extension
            };

            var response = (await this.service.GetMediaType(extension))
                .FirstOrDefault();

            if (response != null)
            {
                result.FileExtension = response.FileExtension;
                result.MimeType = response.MimeType;
                result.MimeSubtype = response.MimeSubtype;
            }

            return result;
        }

        private async Task<IEnumerable<IMediaType>> ResolveMediaTypes(IEnumerable<string> extensions)
        {
            var mediatypes = new HashSet<IMediaType>();
            foreach (var extension in extensions)
            {
                var mediatype = await this.ResolveFileExtensionToMediaType(extension);
                mediatypes.Add(mediatype);
            }

            return mediatypes;
        }
    }
}
