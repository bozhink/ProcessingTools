using System.Text.RegularExpressions;

namespace Base.Format.Nlm
{
	public class Html : Base
	{

		public Html()
			: base()
		{
		}

		public Html(string xml)
			: base(xml)
		{
		}

		/// <summary>
		/// Reformat Nlm-Html file.
		/// </summary>
		public void Format()
		{
			xml = Regex.Replace(xml, "\r", "");
			xml = Regex.Replace(xml, "(class=\")\\s*(.*)\\s*(\")", "$1$2$3");
			xml = Regex.Replace(xml, "<div class=\"uris\">\\s*</div>", "");

			for (Match m = Regex.Match(xml, "<div class=\"mainTitle\">[\\s\\S]*?</div>"); m.Success; m = m.NextMatch())
			{
				string replace = Regex.Replace(m.Value, "\\s+", " ");
				replace = Regex.Replace(replace, @"\(\s*(.*)\s*\)", "($1)");
				xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
			}
			for (Match m = Regex.Match(xml, "<div class=\"affiliationRow\">[\\s\\S]*?</div>"); m.Success; m = m.NextMatch())
			{
				string replace = Regex.Replace(m.Value, "\\s+", " ");
				xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
			}

			xml = Regex.Replace(xml, "\\s+(</div>|</p>|</th>|</td>)", "$1");
			xml = Regex.Replace(xml, "(<(p|div|td|th)(>|\\s+[^>]*>))\\s+", "$1");

			// Remove useless anchors in the nomenclature
			xml = Regex.Replace(xml, "(\\s*<a [^>]*taxonNameLink[^>]*%20%20%20[^>]*></a>)", "");
			// Make taxonomic names on one row
			xml = Regex.Replace(xml, "(<a [^>]*class=\"taxonNameLink\"[^>]*>)\\s*(.*?)\\s*(</a>)", "$1$2$3");


			// Change italics around lower taxa
			for (Match m = Regex.Match(xml, "<i><span [^>]*class=\"node_taxon-name\"><span>.*?</span></span></i>"); m.Success; m = m.NextMatch())
			{
				string replace = Regex.Replace(m.Value, "</?i>", "");
				replace = Regex.Replace(replace, "(<a [^>]*>)(.*?)(</a>)", "$1<i>$2</i>$3");
				xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
			}

			// Nomenclature with species or subspecies
			for (Match m = Regex.Match(xml, "<span [^>]*class=\"node_taxon-name\"><span><a [^>]*>[^<>]*?</a>\\s+<a [^>]*>.*?</span></span>"); m.Success; m = m.NextMatch())
			{
				string replace = Regex.Replace(m.Value, "(<a [^>]*>)(.*?)(</a>)", "$1<i>$2</i>$3");
				xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
			}

			// Remove MARK123
			xml = Regex.Replace(xml, "MARK123\\s*(<span [^>]*class=\"node_taxon-name\"><span>)(<a [^>]*?><i>[\\w\\(\\)\\s]*?</i></a>\\s*)+(<a [^>]*?><i>\\w*?</i></a></span></span>)", "$1$3");

			// Remove italics from subgenus brackets
			xml = Regex.Replace(xml, @"(<a [^>]*>)<i>\(([^<>]*)\)</i>(</a>)", "$1(<i>$2</i>)$3");


			xml = Regex.Replace(xml, @"\s+(</span>)", " $1");
			xml = Regex.Replace(xml, @"(<span[^>]*>)\s+", "$1 ");

			// Remove relicts of <xref ref-type="tn"...
			xml = Regex.Replace(xml, "<span class=\"xrefLink node_fn\"></span>", "");
			// Breaks
			xml = Regex.Replace(xml, "(&lt;br\\s*/&gt;|<br>)", "<br/>");
			// Figures in xref section in nomencltures
			xml = Regex.Replace(xml, "(</span>–)\\s*(<span)", "$1$2");
			xml = Regex.Replace(xml, "(</span>)\\s+(<span class=\\WxrefLink node_fig\\W)", "$1$2");
			// Other reorganization
			xml = Regex.Replace(xml, "(<div class=\"section\">)\n(<a\\s)", "$1$2");
			xml = Regex.Replace(xml, "(<div class=\"reference[^>]*>.*?)\\s*?(<div class=\"unfloat\"></div>)\\s*?(</div>)", "$1$2$3");
			xml = Regex.Replace(xml, "><table", ">\n<table");
			xml = Regex.Replace(xml, "\n\\s+?(</script>)", "\n$1");

			// male and female
			xml = Regex.Replace(xml, "<i>([♂♀\\s]+)</i>", "$1");

			// Format vertical spaces
			xml = Regex.Replace(xml, "(\\s+)(<span [^>]*class=\"node_title\")(>.*?</span>)\\s*(<div class=\"section\">)",
				"$1$2 style=\"padding-bottom:0px\"$3$1$4");
			for (int i = 0; i < 2; i++)
			{
				xml = Regex.Replace(xml, "(\\s+)(<span [^>]*class=\"node_title1 sec\")(>.*?</span>)\\s*(<span [^>]*class=\"node_title1 sec\")",
					"$1$2 style=\"padding-bottom:0px\"$3$1$4");
			}
			xml = Regex.Replace(xml, "(\\s+)(<span [^>]*class=\"node_title(1 sec)?\")(>.*?</span>)\\s+(<div class=\"taxonTreatmentsHolder secHolder\">)",
				"$1$2 style=\"padding-bottom:0px\"$4$1$5");

			// The slowest part
			xml = Regex.Replace(xml, @"(\s*?\n)+", "\n");
			xml = Regex.Replace(xml, "\\s*<p", "\n<p");
			xml = Regex.Replace(xml, "\\s+<div", "\n<div");
			xml = Regex.Replace(xml, Regex.Escape("    "), "\t");
			//xml = Regex.Replace(xml, @"\n\s+", "\n");

			// Final formatting
			xml = Regex.Replace(xml, "</script><div", "</script>\n<div");
		}
	}
}
