namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Files.IO;

    public class TextToXmlFileReader : ITextFileReader
    {
        public Task<string> ReadAllText(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return Task.Run(() =>
            {
                var document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                var body = document.CreateElement("body");
                foreach (var line in File.ReadLines(fullName))
                {
                    var paragraph = document.CreateElement("p");
                    paragraph.InnerText = line;
                    body.AppendChild(paragraph);
                }

                var documentElement = document.CreateElement("article");
                documentElement.AppendChild(body);

                document.AppendChild(documentElement);

                return document.OuterXml;
            });
        }
    }
}
