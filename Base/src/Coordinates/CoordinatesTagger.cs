namespace ProcessingTools.Base.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class CoordinatesTagger : Base, ITagger
    {
        public CoordinatesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public CoordinatesTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        internal enum CoordinateType
        {
            Latitude,
            Longitude,
            Null
        }

        public void Tag()
        {
            string xml = this.Xml;

            // Format deg symbol
            xml = Regex.Replace(xml, "(\\d)([º°˚]|<sup>o</sup>)", "$1°");

            // Tag coordinates
            string replace = @"<locality-coordinates latitude="""" longitude="""">$1</locality-coordinates>";

            xml = Regex.Replace(
                xml,
                @"(([NSEW])((\d+\.)?\d+°\d+(\.\d+)?\'(\d+(\.\d+)?\&quot;)?)\s*?\W?\s*([NSEW])((\d+\.)?\d+°\d+(\.\d+)?\'(\d+(\.\d+)?\&quot;)?))",
                replace);
            xml = Regex.Replace(xml, @"(([NSEW])((\d+\.)?\d+°)\s*?\W?\s*([NSEW])((\d+\.)?\d+°))", replace);

            xml = Regex.Replace(xml, @"((\d+\.\d+)\s*([NSEW])\s*\,?\s*(\d+\.\d+)\s*([NSEW]))", replace);
            xml = Regex.Replace(xml, @"(((\d+\.)?\d+°[^<>]{0,20}?[SWNE])\s*?\W?\s*((\d+\.)?\d+°[^</>]{0,20}?[SWNE]))", replace);
            xml = Regex.Replace(xml, @"((\-?\d{1,3}\.\d{3,6})\s*(;|,)\s*(\-?\d{1,3}\.\d{3,6}))", replace);

            this.Xml = xml;
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }
    }
}