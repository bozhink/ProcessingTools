using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using ProcessingTools.Constants.Schema;
using ProcessingTools.Contracts;
using ProcessingTools.Extensions;
using ProcessingTools.Processors.Contracts.Floats;
using ProcessingTools.Processors.Models.Floats;
using ProcessingTools.Processors.Types;

namespace ProcessingTools.Processors.Processors.Floats
{
    public class FloatsTagger : IFloatsTagger
    {
        private const int MaxNumberOfPunctuationSigns = 10;
        private const int MaxNumberOfSequentalFloats = 60;
        private const string SubfloatsPattern = "\\s*(([A-Za-z]\\d?|[ivx]+)[,\\s]*([,;‒–—−-]|and|\\&amp;)\\s*)*([A-Za-z]\\d?|[ivx]+)";

        private readonly Regex selectDash = new Regex("[‒–—−-]");
        private readonly Regex selectNaNChar = new Regex(@"\D");

        private ConcurrentDictionary<Type, IFloatObject> floatObjects;

        private Hashtable floatIdByLabel = null;
        private Hashtable floatLabelById = null;
        private IEnumerable floatIdByLabelKeys = null;
        private IEnumerable floatIdByLabelValues = null;
        private ILogger logger;

        public FloatsTagger(ILogger logger)
        {
            this.logger = logger;
            this.InitFloats();

            this.floatObjects = new ConcurrentDictionary<Type, IFloatObject>();
        }

        public Task<object> Tag(XmlNode context) => Task.Run(() => this.TagSync(context));

        private object TagSync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Force Fig. and Figs
            context.InnerXml = context.InnerXml
                .RegexReplace(@"(Fig)\s+", "$1. ")
                .RegexReplace(@"(Figs)\.", "$1");

            string defaultFloatObjectInterfaceName = typeof(IFloatObject).FullName;

            var floatOfjectTypes = this.GetType().Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.FullName == defaultFloatObjectInterfaceName))
                .Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract)
                .ToArray();

            // Tag citations in text.
            foreach (var floatObjectType in floatOfjectTypes)
            {
                var floatObject = this.floatObjects.GetOrAdd(floatObjectType, t => Activator.CreateInstance(t) as IFloatObject);

                this.TagFloatObjects(context, floatObject);
            }

            // Set correct values of xref/@ref-type.
            foreach (var floatObject in this.floatObjects.Values)
            {
                if (floatObject.InternalReferenceType != floatObject.ResultantReferenceType)
                {
                    string xpath = $".//xref[@ref-type='{floatObject.InternalReferenceType}']";

                    context.SelectNodes(xpath)
                        .Cast<XmlNode>()
                        .AsParallel()
                        .ForAll(xref =>
                        {
                            xref.Attributes[AttributeNames.RefType].InnerText = floatObject.ResultantReferenceType;
                        });
                }
            }

            return true;
        }

        private string FloatsFirstOccurencePattern(string labelPattern)
        {
            return @"(?<!<[^>]*)\b(" + labelPattern + @")\s*(([A-Z]?\d+)(?:" + SubfloatsPattern + @")?)(?=\W)";
        }

        private string FloatsFirstOccurenceReplace(string refType)
        {
            return "$1 <xref ref-type=\"" + refType + "\" rid=\"$3\">$2</xref>";
        }

        private string FloatsNextOccurencePattern(string refType)
        {
            return "(<xref ref-type=\"" + refType + "\" [^>]*>[^<]*</xref>[,\\s]*([,;‒–—−-]|and|\\&amp;)\\s*)(([A-Z]?\\d+)(" + SubfloatsPattern + ")?)(?=\\W)";
        }

        private string FloatsNextOccurenceReplace(string refType)
        {
            return "$1<xref ref-type=\"" + refType + "\" rid=\"$4\">$3</xref>";
        }

        private void FormatXref(XmlNode context)
        {
            string xml = context.InnerXml;

            // Format content between </xref> and <xref
            xml = Regex.Replace(xml, @"(?<=</xref>)\s*[‒–—−-]\s*(?=<xref)", "–");
            xml = Regex.Replace(xml, @"(?<=</xref>)\s*([,;])\s*(?=<xref)", "$1 ");
            xml = Regex.Replace(xml, @"(?<=</xref>)\s*(and|\\&amp;)\s*(?=<xref)", " $1 ");

            xml = Regex.Replace(xml, @"(<xref [^>]*>)\s*[‒–—−-]\s*(?=[A-Za-z0-9][^<>]*</xref>)", "–$1");

            // Remove xref from attributes
            for (int i = 0; i < 2 * MaxNumberOfSequentalFloats; i++)
            {
                xml = Regex.Replace(xml, "(<[^<>]+=\"[^<>\"]*)<[^<>]*>", "$1");
            }

            context.InnerXml = xml;
        }

        private void FormatXrefGroup(XmlNode context, string refType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                string xrefGroupNodeXPath = $".//xref-group[xref[@ref-type='{refType}']]";
                foreach (XmlNode xrefGroupNode in context.SelectNodes(xrefGroupNodeXPath))
                {
                    // Format content in xref-group
                    string xrefGroup = xrefGroupNode.InnerXml;

                    // <xref-group>Figures 109–112
                    Regex matchDashed = new Regex("<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>[–—−-]<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>");
                    for (Match dashed = matchDashed.Match(xrefGroup); dashed.Success; dashed = dashed.NextMatch())
                    {
                        string xrefReplace = dashed.Value;

                        string prefixId = Regex.Replace(xrefReplace, @"<xref .*?rid=\W(.*?)\d+.*?>.*", "$1");
                        string firstId = Regex.Replace(xrefReplace, @"\A<xref .*?(\d+).*?>.*", "$1");
                        string lastId = Regex.Replace(xrefReplace, @".*[‒–—−-]<xref .*?(\d+).*?>.*", "$1");

                        int first = int.Parse(firstId);
                        int last = int.Parse(lastId);

                        // Parse the dash
                        // Convert the dash to a sequence of xref
                        {
                            stringBuilder.Clear();
                            for (int i = first + 1; i < last; i++)
                            {
                                string rid = prefixId + i;
                                stringBuilder.Append(", <xref ref-type=\"" + refType + "\" rid=\"" + rid + "\">" + this.floatLabelById[rid] + "</xref>");
                            }

                            xrefReplace = Regex.Replace(xrefReplace, "(</xref>)[‒–—−-](<xref [^>]*>)", "$1" + stringBuilder.ToString() + ", $2");
                        }

                        // <xref-group>Figs <xref ref-type="fig" rid="F1">1</xref>, <xref ref-type="F2">5–11</xref>, <xref ref-type="F3">12–18</xref>
                        // , <xref ref-type="F4">19–22</xref>, <xref ref-type="F5">23–31</xref>, <xref ref-type="F6">32–37</xref>, <xref ref-type="fig" rid="F7">40</xref>

                        // Parse left xref
                        {
                            Match matchLeftXref = Regex.Match(xrefReplace, @"\A<xref [^>]*>[^<>]+</xref>, <xref [^>]*>[A-Z]?\d+");
                            string leftXref = matchLeftXref.Value;
                            if (matchLeftXref.Success)
                            {
                                string thisXrefLastItem = Regex.Replace(matchLeftXref.Value, @"\A<xref [^>]*>[^<>]*?([A-Z]?\d+)</xref>, <xref [^>]*>[A-Z]?\d+", "$1");
                                string thisXrefRid = Regex.Replace(matchLeftXref.Value, @"\A<xref [^>]*rid=""([^<>""]+)""[^>]*>[^<>]+</xref>, <xref [^>]*>[A-Z]?\d+", "$1");
                                string thisLabelLastItem = Regex.Replace(this.floatLabelById[thisXrefRid].ToString(), @"\A.*?([A-Z]?\d+)\W*?\Z", "$1");

                                if (string.Compare(thisXrefLastItem, thisLabelLastItem) != 0)
                                {
                                    leftXref = Regex.Replace(matchLeftXref.Value, @"(?=</xref>, )", "–" + thisLabelLastItem);
                                }
                            }

                            xrefReplace = Regex.Replace(xrefReplace, Regex.Escape(matchLeftXref.Value), leftXref);
                        }

                        // Parse the right xref
                        {
                            Match matchRightXref = Regex.Match(xrefReplace, @"[A-Z]?\d+</xref>, <xref [^>]*>[^<>]+</xref>\Z");
                            string rightXref = matchRightXref.Value;
                            if (matchRightXref.Success)
                            {
                                string thisXrefFirstItem = Regex.Replace(matchRightXref.Value, @"[A-Z]?\d+</xref>, <xref [^>]*>([A-Z]?\d+)[^<>]*?</xref>\Z", "$1");
                                string thisXrefRid = Regex.Replace(matchRightXref.Value, @"[A-Z]?\d+</xref>, <xref [^>]*rid=""([^<>""]+)""[^>]*>[^<>]+</xref>\Z", "$1");
                                string thisLabelFirstItem = Regex.Replace(this.floatLabelById[thisXrefRid].ToString(), @"\A([A-Z]?\d+).*?\Z", "$1");

                                if (string.Compare(thisXrefFirstItem, thisLabelFirstItem) != 0)
                                {
                                    rightXref = Regex.Replace(matchRightXref.Value, @"(?<=, <xref [^>]*>)", thisLabelFirstItem + "–");
                                }
                            }

                            xrefReplace = Regex.Replace(xrefReplace, Regex.Escape(matchRightXref.Value), rightXref);
                        }

                        xrefGroup = Regex.Replace(xrefGroup, Regex.Escape(dashed.Value), xrefReplace);
                    }

                    xrefGroupNode.InnerXml = xrefGroup;
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private string GetFloatId(FloatsReferenceType floatReferenceType, XmlNode node)
        {
            string id = string.Empty;
            if (node.Attributes[AttributeNames.Id] != null)
            {
                id = node.Attributes[AttributeNames.Id].InnerText;
            }
            else
            {
                switch (floatReferenceType)
                {
                    case FloatsReferenceType.Table:
                        try
                        {
                            id = node[ElementNames.Table].Attributes[AttributeNames.Id].InnerText;
                        }
                        catch (Exception e)
                        {
                            this.logger?.Log(e, "There is no 'table-wrap/@id' or 'table-wrap/table' or 'table-wrap/table/@id'");
                        }

                        break;
                }
            }

            return id;
        }

        private string GetFloatLabelText(XmlNode node)
        {
            string labelText = string.Empty;
            if (node[ElementNames.Label] != null)
            {
                labelText = node[ElementNames.Label].InnerText;
            }
            else if (node[ElementNames.Title] != null)
            {
                labelText = node[ElementNames.Title].InnerText;
            }

            return labelText;
        }

        /// <summary>
        /// Gets the number of floating objects of a given type and populates label-and-id-related hash tables.
        /// This method generates the "dictionary" to correctly post-process xref/@rid references.
        /// </summary>
        /// <param name="context">XmlNode to be harvested.</param>
        /// <param name="floatObject">IFloatObject model to provide information for the floating object.</param>
        /// <returns>Number of floating objects of type refType with label containing "floatType".</returns>
        private int GetFloatsOfType(XmlNode context, IFloatObject floatObject)
        {
            this.floatIdByLabel = new Hashtable();
            this.floatLabelById = new Hashtable();

            int numberOfFloatsOfType = 0;
            try
            {
                XmlNodeList floatsNodeList = context.SelectNodes(floatObject.FloatObjectXPath);
                foreach (XmlNode floatNode in floatsNodeList)
                {
                    string id = this.GetFloatId(floatObject.FloatReferenceType, floatNode);
                    string labelText = this.GetFloatLabelText(floatNode);

                    this.UpdateFloatLabelByIdList(id, labelText);
                    this.UpdateFloatIdByLabelList(id, labelText);
                }

                numberOfFloatsOfType = floatsNodeList.Count;
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }

            this.floatIdByLabelKeys = this.floatIdByLabel.Keys;
            this.floatIdByLabelValues = this.floatIdByLabel.Values;

            this.PrintFloatsDistributionById(floatObject);

            return numberOfFloatsOfType;
        }

        private void InitFloats()
        {
            if (this.floatIdByLabel != null)
            {
                this.floatIdByLabel.Clear();
                this.floatIdByLabel = null;
            }

            if (this.floatLabelById != null)
            {
                this.floatLabelById.Clear();
            }

            this.floatIdByLabelKeys = null;
            this.floatIdByLabelValues = null;
        }

        private void ParseFloatIndexRangesInLabel(string id, string labelText, Match floatIndexInLabel, string currentFloatIndex)
        {
            Match dash = this.selectDash.Match(floatIndexInLabel.Value);
            if (dash.Success)
            {
                int currentFloatIndexNumber = int.Parse(this.selectNaNChar.Replace(currentFloatIndex, string.Empty));

                string nextFloatIndex = floatIndexInLabel.NextMatch().Success ? this.selectDash.Replace(floatIndexInLabel.NextMatch().Value, string.Empty) : string.Empty;
                int nextFloatIndexNumber = int.Parse(this.selectNaNChar.Replace(nextFloatIndex, string.Empty));

                if (currentFloatIndexNumber < nextFloatIndexNumber)
                {
                    string prefix = Regex.Replace(currentFloatIndex, @"([A-Z]?)\d+", "$1");
                    for (int i = currentFloatIndexNumber + 1; i < nextFloatIndexNumber; ++i)
                    {
                        this.floatIdByLabel.Add(prefix + i, id);
                    }
                }
                else
                {
                    throw new Exception($"Error in multiple-float's label '{labelText}': Label numbers must be strictly increasing.");
                }
            }
        }

        private void PrintFloatsDistributionById(IFloatObject floatObject)
        {
            try
            {
                Regex matchNumber = new Regex(@"\d+");

                this.logger?.Log();
                this.floatIdByLabelKeys
                    .Cast<string>()
                    .OrderBy(s => int.Parse(matchNumber.Match(s).Value ?? "1"))
                    .ToList()
                    .ForEach(id =>
                    {
                        this.logger?.Log(
                            "{2}\t#{0}\tis in float\t#{1}",
                            id,
                            this.floatIdByLabel[id],
                            floatObject.Description);
                    });
            }
            catch (Exception e)
            {
                this.logger?.Log(e, "Cannot print the table of floats.");
            }
        }

        private void ProcessFloatsRid(XmlNode context, int floatsNumber, string refType)
        {
            string xml = context.InnerXml;

            foreach (string rid in this.floatIdByLabelKeys)
            {
                xml = Regex.Replace(xml, "<xref ref-type=\"" + refType + "\" rid=\"" + rid + "\">", "<xref ref-type=\"" + refType + "\" rid=\"" + this.floatIdByLabel[rid] + "\">");
            }

            foreach (string rid in this.floatIdByLabelValues.Cast<string>().Select(c => c).Distinct())
            {
                for (int j = 0; j < MaxNumberOfSequentalFloats; j++)
                {
                    xml = Regex.Replace(xml, "((<xref ref-type=\"" + refType + "\" rid=\"" + rid + "\">)[^<>]*)</xref>\\s*[‒–—−-]\\s*\\2", "$1–");
                }
            }

            context.InnerXml = xml;
        }

        private void TagFloatObjects(XmlNode context, IFloatObject floatObject)
        {
            this.InitFloats();
            int numberOfFloatsOfType = this.GetFloatsOfType(context, floatObject);

            if (numberOfFloatsOfType > 0)
            {
                this.TagFloatsOfType(context, floatObject.InternalReferenceType, floatObject.MatchCitationPattern);
                this.FormatXref(context);
                this.ProcessFloatsRid(context, numberOfFloatsOfType, floatObject.InternalReferenceType);
                this.FormatXrefGroup(context, floatObject.InternalReferenceType);
            }
        }

        /// <summary>
        /// Find and put in xref citations of a floating object of given type.
        /// </summary>
        /// <param name="context">XmlNode to be processed.</param>
        /// <param name="refType">Logical type of the floating object. This string will be put as current value of the attribute xref/@ref-type.</param>
        /// <param name="matchCitationPattern">Regex pattern to find citations of floating objects of the given type.</param>
        private void TagFloatsOfType(XmlNode context, string refType, string matchCitationPattern)
        {
            string pattern = this.FloatsFirstOccurencePattern(matchCitationPattern);
            string replace = this.FloatsFirstOccurenceReplace(refType);

            string xml = context.InnerXml;
            xml = Regex.Replace(xml, pattern, replace);

            pattern = this.FloatsNextOccurencePattern(refType);
            replace = this.FloatsNextOccurenceReplace(refType);
            for (int i = 0; i < MaxNumberOfSequentalFloats; i++)
            {
                xml = Regex.Replace(xml, pattern, replace);
            }

            context.InnerXml = xml;
        }

        private void UpdateFloatIdByLabelList(string id, string labelText)
        {
            for (Match floatIndexInLabel = Regex.Match(labelText, @"[A-Z]?\d+([‒–—−-](?=[A-Z]?\d+))?"); floatIndexInLabel.Success; floatIndexInLabel = floatIndexInLabel.NextMatch())
            {
                string currentFloatIndex = this.selectDash.Replace(floatIndexInLabel.Value, string.Empty);
                this.floatIdByLabel.Add(currentFloatIndex, id);

                this.ParseFloatIndexRangesInLabel(id, labelText, floatIndexInLabel, currentFloatIndex);
            }
        }

        private void UpdateFloatLabelByIdList(string id, string labelText)
        {
            if (Regex.IsMatch(labelText, @"\A\w+\s+([A-Za-z]?\d+\W*)+\Z"))
            {
                this.floatLabelById.Add(id, Regex.Replace(labelText, @"\A\w+\s+(([A-Za-z]?\d+\W*?)+)[\.;,:‒–—−-]*\s*\Z", "$1"));
            }
        }
    }
}
