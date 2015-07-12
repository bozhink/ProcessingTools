using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Base
{
	public class Floats : Base
	{
		private const int maxNumberOfSequentalFloats = 30;
		private const int maxNumberOfPunctuationSigns = 10;

		private string[] floatNumericLabel;

		private Hashtable floatIdByLabel = null;
		private Hashtable floatLabelById = null;
		private IEnumerable floatIdByLabelKeys = null;
		private IEnumerable floatIdByLabelValues = null;

		private void InitFloats()
		{
			if (floatIdByLabel != null)
			{
				floatIdByLabel.Clear();
				floatIdByLabel = null;
			}
			if (floatLabelById != null)
			{
				floatLabelById.Clear();
			}
			floatIdByLabelKeys = null;
			floatIdByLabelValues = null;
		}

		public Floats()
			: base()
		{
			InitFloats();
		}

		public Floats(string xml)
			: base(xml)
		{
			InitFloats();
		}

		/// <summary>
		/// Gets the number of floating objects of a given type and populates label-and-id-related hash tables.
		/// This method generates the "dictionary" to correctly post-process xref/@rid references.
		/// </summary>
		/// <param name="refType">"Physical" type of the floating object: &lt;fig /&gt;, &lt;table-wrap /&gt;, &lt;boxed-text /&gt;, etc.</param>
		/// <param name="floatType">"Logical" type of the floating object: This string is supposed to be contained in the &lt;label /&gt; of the object.</param>
		/// <returns>Number of floating objects of type refType with label containing "floatType"</returns>
		public int GetFloats(ReferenceType refType = ReferenceType.Figure, string floatType = "Figure")
		{
			int numberOfFloatsOfType = 0;
			ParseXmlStringToXmlDocument();

			string xpath = string.Empty;
			switch (refType)
			{
				case ReferenceType.Figure:
					xpath = "//fig[contains(string(label),'" + floatType + "')]";
					break;
				case ReferenceType.Table:
					xpath = "//table-wrap[contains(string(label),'" + floatType + "')]";
					break;
				case ReferenceType.Textbox:
					xpath = "//box[contains(string(title),'" + floatType + "')]|//boxed-text[contains(string(label),'" + floatType + "')]";
					break;
				case ReferenceType.SupplementaryMaterial:
					xpath = "//supplementary-material[contains(string(label),'" + floatType + "')]";
					break;
				default:
					xpath = string.Empty;
					break;
			}

			floatIdByLabel = new Hashtable();
			floatLabelById = new Hashtable();

			try
			{
				XmlNodeList nodeList = xmlDocument.SelectNodes(xpath, namespaceManager);
				numberOfFloatsOfType = nodeList.Count;
				floatNumericLabel = new string[numberOfFloatsOfType + 1];
				for (int i = 0; i < numberOfFloatsOfType + 1; i++)
				{
					floatNumericLabel[i] = string.Empty;
				}

				int currentFloat = 0;
				foreach (XmlNode node in nodeList)
				{
					currentFloat++;
					string idAttribute = string.Empty;
					if (node.Attributes["id"] != null)
					{
						idAttribute = node.Attributes["id"].InnerText;
					}
					else
					{
						switch (refType)
						{
							case ReferenceType.Table:
								try
								{
									idAttribute = node["table"].Attributes["id"].InnerText;
								}
								catch (Exception e)
								{
									Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0,
										"There is no 'table-wrap/@id' or 'table-wrap/table' or 'table-wrap/table/@id'");
								}
								break;
						}
					}

					// Get the text of the current float
					string labelText = string.Empty;
					if (node["label"] != null)
					{
						labelText = node["label"].InnerXml;
					}
					else if (node["title"] != null)
					{
						labelText = node["title"].InnerXml;
					}

					if (Regex.Match(labelText, @"\A\w+\s+([A-Za-z]?\d+\W*)+\Z").Success)
					{
						floatNumericLabel[currentFloat] = Regex.Replace(labelText, @"\A\w+\s+(([A-Za-z]?\d+\W*?)+)[\.;,:–—−-]*\s*\Z", "$1");
						floatLabelById.Add(idAttribute, floatNumericLabel[currentFloat]);
					}

					for (Match m = Regex.Match(labelText, @"[A-Z]?\d+([–—−-](?=[A-Z]?\d+))?"); m.Success; m = m.NextMatch())
					{
						string curr = Regex.Replace(m.Value, "[–—−-]", "");
						string next = m.NextMatch().Success ? Regex.Replace(m.NextMatch().Value, "[–—−-]", "") : string.Empty;

						floatIdByLabel.Add(curr, idAttribute);

						Match dash = Regex.Match(m.Value, "[–—−-]");
						if (dash.Success)
						{
							try
							{
								int icurr = int.Parse(Regex.Replace(curr, @"\D", ""));
								int inext = int.Parse(Regex.Replace(next, @"\D", ""));
								string prefix = Regex.Replace(curr, @"([A-Z]?)\d+", "$1");
								if (icurr < inext)
								{
									for (int i = icurr + 1; i < inext; i++)
									{
										floatIdByLabel.Add(prefix + i, idAttribute);
									}
								}
								else
								{
									throw new Exception("Error in multiple-float's label '" + labelText + "': Label numbers must be strictly increasing.");
								}
							}
							catch (Exception e)
							{
								Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			floatIdByLabelKeys = floatIdByLabel.Keys;
			floatIdByLabelValues = floatIdByLabel.Values;

			Console.WriteLine();
			foreach (string s in floatIdByLabelKeys.Cast<string>().ToArray().OrderBy(s => s))
			{
				Console.WriteLine("{2}\t#{0}\tis in float\t#{1}", s, floatIdByLabel[s], refType.ToString());
			}

			return numberOfFloatsOfType;
		}

		public void TagAllFloats()
		{
			// Force Fig. and Figs
			xml = Regex.Replace(xml, @"(Fig)\s", "$1. ");
			xml = Regex.Replace(xml, @"(Figs)\.", "$1");

			{
				/*
				 * Tag Figures
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Figure, "Figure");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("fig", "Fig\\.|Figs|Figures?");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "fig");
					FormatXrefGroup("fig");
				}
			}
			{
				/*
				 * Tag Maps
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Figure, "Map");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("map", "Maps?");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "map");
					FormatXrefGroup("map");
				}
			}
			{
				/*
				 * Tag Plates
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Figure, "Plate");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("plate", "Plates?");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "plate");
					FormatXrefGroup("plate");
				}
			}
			{
				/*
				 * Tag Habitus
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Figure, "Habitus");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("habitus", "Habitus");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "habitus");
					FormatXrefGroup("habitus");
				}
			}
			{
				/*
				 * Tag Tables
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Table, "Table");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("table", "Tab\\.|Tabs|Tables?");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "table");
					FormatXrefGroup("table");
				}
			}
			{
				/*
				 * Tag Boxes of type table
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Table, "Box");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("table", "Box|Boxes");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "table");
					FormatXrefGroup("table");
				}
			}
			{
				/*
				 * Tag Boxes of type boxed-text
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.Textbox, "Box");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("boxed-text", "Box|Boxes");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "boxed-text");
					FormatXrefGroup("boxed-text");
				}
			}
			{
				/*
				 * Tag Supplementary materials
				 */
				InitFloats();
				int numberOfFloatsOfType = GetFloats(ReferenceType.SupplementaryMaterial, "Supplementary material");
				if (numberOfFloatsOfType > 0)
				{
					TagFloatsOfType("supplementary-material", @"Suppl\.\s*materials?");
					FormatXref();
					ProcessFloatsRid(numberOfFloatsOfType, "supplementary-material");
					FormatXrefGroup("supplementary-material");
				}
			}

			RemoveXrefInTitles();

			xml = Regex.Replace(xml, "\\s+ref-type=\"(map|plate|habitus)\"", " ref-type=\"fig\"");
		}

		//private const string subfloatsPattern = "\\s*([A-Za-z][\\.A-Za-z0-9]{0,2}?[,\\s]*([,;–−-]|and|\\&amp;)\\s*)*[A-Za-z][\\.A-Za-z0-9]{0,2}?";
		private const string subfloatsPattern = "\\s*(([A-Za-z]\\d?|[ivx]+)[,\\s]*([,;–−-]|and|\\&amp;)\\s*)*([A-Za-z]\\d?|[ivx]+)";
		private string FloatsFirstOccurencePattern(string labelPattern)
		{
			return "\\b(" + labelPattern + ")\\s*(([A-Z]?\\d+)(" + subfloatsPattern + ")?)(?=\\W)";
		}
		private string FloatsFirstOccurenceReplace(string floatType)
		{
			return "$1 <xref ref-type=\"" + floatType + "\" rid=\"$3\">$2</xref>";
		}
		private string FloatsNextOccurencePattern(string floatType)
		{
			return "(<xref ref-type=\"" + floatType + "\" [^>]*>[^<]*</xref>[,\\s]*([,;–−-]|and|\\&amp;)\\s*)(([A-Z]?\\d+)(" + subfloatsPattern + ")?)(?=\\W)";
		}
		private string FloatsNextOccurenceReplace(string floatType)
		{
			return "$1<xref ref-type=\"" + floatType + "\" rid=\"$4\">$3</xref>";
		}

		/// <summary>
		/// Find and put in xref citations of a floating object of given type.
		/// </summary>
		/// <param name="floatType">Logical type of the floating object. This string will be put as current value of the attribute xref/@ref-type.</param>
		/// <param name="labelPattern">Regex pattern to find citations of floating objects of the given type.</param>
		private void TagFloatsOfType(string floatType, string labelPattern)
		{
			string pattern = FloatsFirstOccurencePattern(labelPattern);
			string replace = FloatsFirstOccurenceReplace(floatType);
			xml = Regex.Replace(xml, pattern, replace);

			pattern = FloatsNextOccurencePattern(floatType);
			replace = FloatsNextOccurenceReplace(floatType);
			for (int i = 0; i < maxNumberOfSequentalFloats; i++)
			{
				xml = Regex.Replace(xml, pattern, replace);
			}
		}

		private void ProcessFloatsRid(int floatsNumber, string refType)
		{
			string pattern = string.Empty, replace = string.Empty;

			foreach (string s in floatIdByLabelKeys)
			{
				xml = Regex.Replace(xml, "<xref ref-type=\"" + refType + "\" rid=\"" + s + "\">", "<xref ref-type=\"" + refType + "\" rid=\"" + floatIdByLabel[s] + "\">");
			}
			foreach (string s in floatIdByLabelValues.Cast<string>().Select(c => c).Distinct().ToList())
			{
				for (int j = 0; j < maxNumberOfSequentalFloats; j++)
				{
					xml = Regex.Replace(xml, "((<xref ref-type=\"" + refType + "\" rid=\"" + s + "\">)[^<>]*)</xref>\\s*[–—−-]\\s*\\2", "$1–");
				}
			}
		}

		private void RemoveXrefInTitles()
		{
			ParseXmlStringToXmlDocument();
			string xpath = "//fig//label[xref]|//fig//title[xref]|//table-wrap//label[xref]|//table-wrap//title[xref]";
			try
			{
				foreach (XmlNode node in xmlDocument.SelectNodes(xpath, namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "<xref [^>]*>|</?xref>", "");
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			xml = xmlDocument.OuterXml;
		}

		private void FormatXref()
		{
			// Format content between </xref> and <xref
			xml = Regex.Replace(xml, @"(?<=</xref>)\s*[–—−-]\s*(?=<xref)", "–");
			xml = Regex.Replace(xml, @"(?<=</xref>)\s*([,;])\s*(?=<xref)", "$1 ");
			xml = Regex.Replace(xml, @"(?<=</xref>)\s*(and|\\&amp;)\s*(?=<xref)", " $1 ");

			xml = Regex.Replace(xml, @"(<xref [^>]*>)\s*[–—−-]\s*(?=[A-Za-z0-9][^<>]*</xref>)", "–$1");

			// Remove xref from attributes
			for (int i = 0; i < 2 * maxNumberOfSequentalFloats; i++)
			{
				xml = Regex.Replace(xml, "(<[^<>]+=\"[^<>\"]*)<[^<>]*>", "$1");
			}
		}

		private void FormatXrefGroup(string refType)
		{
			StringBuilder sb = new StringBuilder();
			ParseXmlStringToXmlDocument();

			try
			{
				foreach (XmlNode node in xmlDocument.SelectNodes("//xref-group[xref[@ref-type='" + refType + "']]", namespaceManager))
				{
					// Format content in xref-group
					string xref_group = node.InnerXml;

					//Alert.Message("\n" + Regex.Replace(xref_group, "</?[^>]+>", ""));
					//Alert.Message("\n" + xref_group + "\n");

					// <xref-group>Figures 109–112
					for (Match dashed = Regex.Match(xref_group, "<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>[–—−-]<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>"); dashed.Success; dashed = dashed.NextMatch())
					{
						string xref_replace = dashed.Value;
						//Alert.Message(xref_replace);

						string idPrefix = Regex.Replace(xref_replace, @"<xref .*?rid=\W(.*?)\d+.*?>.*", "$1");
						string first_id = Regex.Replace(xref_replace, @"\A<xref .*?(\d+).*?>.*", "$1");
						string last_id = Regex.Replace(xref_replace, @".*[–—−-]<xref .*?(\d+).*?>.*", "$1");

						int first = int.Parse(first_id);
						int last = int.Parse(last_id);

						//Alert.Message("MARK 1");
						// Parse the dash
						// Convert the dash to a sequence of xref
						{
							sb.Clear();
							for (int i = first + 1; i < last; i++)
							{
								string rid = idPrefix + i;
								sb.Append(", <xref ref-type=\"" + refType + "\" rid=\"" + rid + "\">" + floatLabelById[rid] + "</xref>");
							}
							xref_replace = Regex.Replace(xref_replace, "(</xref>)[–—−-](<xref [^>]*>)", "$1" + sb.ToString() + ", $2");
						}
						//Alert.Message(xref_replace);

						//<xref-group>Figs <xref ref-type="fig" rid="F1">1</xref>, <xref ref-type="F2">5–11</xref>, <xref ref-type="F3">12–18</xref>
						//, <xref ref-type="F4">19–22</xref>, <xref ref-type="F5">23–31</xref>, <xref ref-type="F6">32–37</xref>, <xref ref-type="fig" rid="F7">40</xref>

						//Alert.Message("MARK 2");
						// Parse left xref
						{
							Match mLeftXref = Regex.Match(xref_replace, @"\A<xref [^>]*>[^<>]+</xref>, <xref [^>]*>[A-Z]?\d+");
							string leftXref = mLeftXref.Value;
							if (mLeftXref.Success)
							{
								string thisXrefLastItem = Regex.Replace(mLeftXref.Value, @"\A<xref [^>]*>[^<>]*?([A-Z]?\d+)</xref>, <xref [^>]*>[A-Z]?\d+", "$1");
								string thisXrefRid = Regex.Replace(mLeftXref.Value, @"\A<xref [^>]*rid=""([^<>""]+)""[^>]*>[^<>]+</xref>, <xref [^>]*>[A-Z]?\d+", "$1");
								string thisLabelLastItem = Regex.Replace(floatLabelById[thisXrefRid].ToString(), @"\A.*?([A-Z]?\d+)\W*?\Z", "$1");

								if (String.Compare(thisXrefLastItem, thisLabelLastItem) != 0)
								{
									leftXref = Regex.Replace(mLeftXref.Value, @"(?=</xref>, )", "–" + thisLabelLastItem);
								}
							}
							xref_replace = Regex.Replace(xref_replace, Regex.Escape(mLeftXref.Value), leftXref);
						}

						//Alert.Message("MARK 3");
						// Parse the right xref
						{
							Match mRightXref = Regex.Match(xref_replace, @"[A-Z]?\d+</xref>, <xref [^>]*>[^<>]+</xref>\Z");
							string rightXref = mRightXref.Value;
							if (mRightXref.Success)
							{
								string thisXrefFirstItem = Regex.Replace(mRightXref.Value, @"[A-Z]?\d+</xref>, <xref [^>]*>([A-Z]?\d+)[^<>]*?</xref>\Z", "$1");
								string thisXrefRid = Regex.Replace(mRightXref.Value, @"[A-Z]?\d+</xref>, <xref [^>]*rid=""([^<>""]+)""[^>]*>[^<>]+</xref>\Z", "$1");
								string thisLabelFirstItem = Regex.Replace(floatLabelById[thisXrefRid].ToString(), @"\A([A-Z]?\d+).*?\Z", "$1");

								if (String.Compare(thisXrefFirstItem, thisLabelFirstItem) != 0)
								{
									rightXref = Regex.Replace(mRightXref.Value, @"(?<=, <xref [^>]*>)", thisLabelFirstItem + "–");
								}
							}
							xref_replace = Regex.Replace(xref_replace, Regex.Escape(mRightXref.Value), rightXref);
						}

						xref_group = Regex.Replace(xref_group, Regex.Escape(dashed.Value), xref_replace);
					}
					node.InnerXml = xref_group;
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			xml = xmlDocument.OuterXml;
		}

		public void TagTableFootNotes()
		{
			ParseXmlStringToXmlDocument();

			// Get list of table-wrap with correctly formatted foot-notes
			XmlNodeList tableWrapList = xmlDocument.SelectNodes("//table-wrap[table-wrap-foot[fn[label][@id]]]", namespaceManager);
			if (tableWrapList.Count < 1)
			{
				Alert.Message("There is no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");
			}
			else
			{
				Console.WriteLine("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);
				foreach (XmlNode tableWrap in tableWrapList)
				{
					Hashtable tableFootNotes = new Hashtable();
					// Get foot-note's label and corresponding @id-s
					foreach (XmlNode fn in tableWrap.SelectNodes("//fn[label][@id]", namespaceManager))
					{
						tableFootNotes.Add(fn["label"].InnerText.Trim(), fn.Attributes["id"].Value.Trim());
					}

					foreach (string x in tableFootNotes.Keys)
					{
						foreach (XmlNode footnoteSup in tableWrap.SelectNodes("//table//sup[normalize-space()='" + x + "']", namespaceManager))
						{
							//<xref ref-type="table-fn" rid="TN1"></xref>
							XmlNode xrefTableFootNote = xmlDocument.CreateElement("xref");

							XmlAttribute refType = xmlDocument.CreateAttribute("ref-type");
							refType.InnerXml = "table-fn";
							xrefTableFootNote.Attributes.Append(refType);

							XmlAttribute rid = xmlDocument.CreateAttribute("rid");
							rid.InnerXml = tableFootNotes[x].ToString();
							xrefTableFootNote.Attributes.Append(rid);

							xrefTableFootNote.InnerXml = footnoteSup.OuterXml;

							footnoteSup.InnerXml = xrefTableFootNote.OuterXml;
						}
					}
				}
			}

			xml = xmlDocument.OuterXml;
			xml = Regex.Replace(xml, @"<sup>(<xref ref-type=""table-fn"" [^>]*><sup>[^<>]*?</sup></xref>)</sup>", "$1");
		}
	}

	public enum ReferenceType
	{
		Affiliation, //"aff"
		Appendix, //"app"
		AuthorNotes, //"author-notes"
		BibliographiReference, //"bibr"
		ChemicalStructure, //"chem"
		Contributor, //"contrib"
		CorrespondingAuthor, //"corresp"
		DisplayFormula, //"disp-formula"
		Figure, //"fig"
		Footnote, //"fn"
		Keyword, //"kwd"
		List, //"list"
		Other, //"other"
		Plate, //"plate"
		Scheme, //"scheme"
		Section, //"sec"
		Statement, //"statement"
		SupplementaryMaterial, //"supplementary-material"
		Table, //"table"
		TableFootnote, //"table-fn"
		Textbox //"boxed-text"
	}
}
