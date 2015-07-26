using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessingTools.Base.Taxonomy
{
    namespace Nlm
    {
        public class Expander : Base
        {
            private const string GenusLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"genus\">";
            private const string GenusRightTag = "</tp:taxon-name-part>";
            private const string SubgenusLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"subgenus\">";
            private const string SubgenusRightTag = "</tp:taxon-name-part>";
            private const string SpeciesLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"species\">";
            private const string SpeciesRightTag = "</tp:taxon-name-part>";
            private const string SubspeciesLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"subspecies\">";
            private const string SubspeciesRightTag = "</tp:taxon-name-part>";

            private static Regex findLowerTaxa = new Regex(@"<italic><tp:taxon-name[^>\-]*>(.*?)</tp:taxon-name></italic>");
            private static Regex findLowerTaxaMultiLine = new Regex(@"<italic><tp:taxon-name[^>\-]*>([\s\S]*?)</tp:taxon-name></italic>");

            public Expander(string xml)
                : base(xml)
            {
            }

            public Expander(Config config, string xml)
                : base(config, xml)
            {
            }

            public Expander(Base baseObject)
                : base(baseObject)
            {
            }

            public void UnstableExpand1()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1\nTrying to expand all quasi-stable cases like\n[Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species");
                this.ParseXmlStringToXmlDocument();

                XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", NamespaceManager);
                List<Species> speciesList = new List<Species>();
                foreach (XmlNode node in lowerTaxa)
                {
                    speciesList.Add(new Species(node.InnerXml));
                }

                for (int i = 0; i < lowerTaxa.Count; i++)
                {
                    XmlNode node = lowerTaxa[i];
                    Species sp = speciesList[i];
                    if (Taxonomy.EmptyGenus(node.InnerXml, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if (sp.IsShortened && sp.SpeciesName.Length > 0)
                    {
                        Taxonomy.PrintNextShortened(sp);
                        foreach (Species sp1 in speciesList)
                        {
                            SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                            if (compare.EqualSubspecies)
                            {
                                // We are interested only in coincident subspecies names
                                if (compare.EqaulSpecies)
                                {
                                    // First process all taxa with coincident species names
                                    if (compare.EqualSubgenera)
                                    {
                                        // ... and coincident subgenus names, i.e. try to find suitable genus names
                                        if (Regex.Match(sp1.GenusName, sp.GenusSkipPattern).Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                        }
                                    }
                                    else if (compare.EqualGenera)
                                    {
                                        // ... or coincident genus names, i.e. try to find suitable subgenus names
                                        if (Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern).Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                        }
                                    }
                                }

                                if (compare.EqualGenera)
                                {
                                    // First process all taxa with coincident genus names
                                    if (compare.EqualSubgenera)
                                    {
                                        // ... and coincident subgenus names, i.e. try to find suitable genus names
                                        if (Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern).Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                this.xml = this.xmlDocument.OuterXml;
            }

            public void _UnstableExpand1()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1");
                //// On the first stage try to expand all quasi-stable cases like
                //// [Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species

                for (Match m = findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    string replace = m.Value;
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && string.Compare(sp.SpeciesName, string.Empty) != 0)
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        bool found = false;
                        string replace1 = m.Value;
                        for (Match taxon = findLowerTaxaMultiLine.Match(this.xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0)
                            {
                                // We are interested only in coincident subspecies names
                                if (string.Compare(sp.SpeciesName, sp1.SpeciesName) == 0)
                                {
                                    // First process all taxa with coincident species names
                                    if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0)
                                    {
                                        // ... and coincident subgenus names, i.e. try to find suitable genus names
                                        Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                        if (mgen.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                            found = true;
                                        }
                                    }
                                    else if (string.Compare(sp.GenusName, sp1.GenusName) == 0)
                                    {
                                        // ... or coincident genus names, i.e. try to find suitable subgenus names
                                        Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern);
                                        if (msgen.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                            found = true;
                                        }
                                    }
                                }

                                if (string.Compare(sp.GenusName, sp1.GenusName) == 0)
                                {
                                    // First process all taxa with coincident genus names
                                    if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0)
                                    {
                                        // ... and coincident subgenus names, i.e. try to find suitable genus names
                                        Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                        if (msp.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace1 = Regex.Replace(replace1, Regex.Escape(sp.SpeciesTagged), sp1.SpeciesTagged);
                                            found = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (found)
                        {
                            replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            public void UnstableExpand2()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2\nTrying to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)");
                this.ParseXmlStringToXmlDocument();

                XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", NamespaceManager);
                List<Species> speciesList = new List<Species>();
                foreach (XmlNode node in lowerTaxa)
                {
                    speciesList.Add(new Species(node.InnerXml));
                }

                for (int i = 0; i < lowerTaxa.Count; i++)
                {
                    XmlNode node = lowerTaxa[i];
                    Species sp = speciesList[i];
                    if (Taxonomy.EmptyGenus(node.InnerXml, sp))
                    {
                        return;
                    }

                    /*
                     * Select only shortened taxa with non-zero species and subgenus name
                     */
                    if ((sp.GenusName.IndexOf('.') > -1) && ((sp.SubgenusName.IndexOf('.') < 0) && (sp.SubgenusName.Length > 0)) && (sp.SpeciesName.Length > 0))
                    {
                        Taxonomy.PrintNextShortened(sp);
                        foreach (Species sp1 in speciesList)
                        {
                            SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                            if (compare.EqualSubgenera && !compare.EqualGenera)
                            {
                                if (Regex.Match(sp1.GenusName, sp.GenusPattern).Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                    node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                }
                            }
                        }
                    }

                    /*
                     * Select only shortened taxa with non-zero species and genus name
                     */
                    if ((sp.SubgenusName.IndexOf('.') > -1) && ((sp.GenusName.IndexOf('.') < 0) && (sp.GenusName.Length > 0)) && (sp.SpeciesName.Length > 0))
                    {
                        Taxonomy.PrintNextShortened(sp);
                        foreach (Species sp1 in speciesList)
                        {
                            SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                            if (compare.EqualGenera && !compare.EqualSubgenera)
                            {
                                if (Regex.Match(sp1.SubgenusName, sp.SubgenusPattern).Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                    node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                }
                            }
                        }
                    }
                }

                this.xml = this.xmlDocument.OuterXml;
            }

            public void _UnstableExpand2()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2");

                // On the second stage try to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)
                for (Match m = findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    string replace = m.Value;
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species and subgenus name
                    if ((sp.GenusName.IndexOf('.') > -1) && (sp.SubgenusName.IndexOf('.') < 0) && (string.Compare(sp.SubgenusName, string.Empty) != 0) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        bool found = false;
                        string replace1 = m.Value;
                        for (Match taxon = findLowerTaxaMultiLine.Match(this.xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (string.Compare(sp.GenusName, sp1.GenusName) == 0 && string.Compare(sp.SubgenusName, sp1.SubgenusName) != 0)
                            {
                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                                if (msgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                    found = true;
                                }
                            }

                            if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                            {
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                                if (mgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                    found = true;
                                }
                            }
                        }

                        if (found)
                        {
                            replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }

                    // Select only shortened taxa with non-zero species and subgenus name
                    if ((sp.GenusName.IndexOf('.') < 0) && (sp.SubgenusName.IndexOf('.') > -1) && (string.Compare(sp.SubgenusName, string.Empty) != 0) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        bool found = false;
                        string replace1 = m.Value;
                        for (Match taxon = findLowerTaxaMultiLine.Match(this.xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (string.Compare(sp.GenusName, sp1.GenusName) == 0 && string.Compare(sp.SubgenusName, sp1.SubgenusName) != 0)
                            {
                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                                if (msgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                    found = true;
                                }
                            }

                            if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                            {
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                                if (mgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                    found = true;
                                }
                            }
                        }

                        if (found)
                        {
                            replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            public void UnstableExpand3()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");
                this.ParseXmlStringToXmlDocument();

                XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", NamespaceManager);
                foreach (XmlNode node in lowerTaxa)
                {
                    Species sp = new Species(node.InnerXml);
                    if (Taxonomy.EmptyGenus(node.InnerXml, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if (sp.IsShortened && (sp.SpeciesName.Length > 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // TODO
                        for (Match p = Regex.Match(this.xml, "<p>[\\s\\S]+?" + Regex.Escape(node.InnerXml)); p.Success; p = p.NextMatch())
                        {
                            Console.WriteLine("Paragraph content:\n\t{0}\n", TaxaParser.UnSplitTaxa(p.Value));

                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (Regex.Match(sp1.GenusName, sp.GenusSkipPattern).Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                if (Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern).Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                if (Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern).Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("paragraph", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = node.InnerXml;
                                if (last.GenusName.Length > 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (last.SubgenusName.Length > 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (last.SpeciesName.Length > 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(node.InnerXml), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current paragraph.\n");
                            }
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void _UnstableExpand3()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");
                for (Match m = findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match p = Regex.Match(this.xml, "<p>.*?" + Regex.Escape(m.Value));
                        if (p.Success)
                        {
                            Console.WriteLine("Paragraph content:\n\t{0}\n", TaxaParser.UnSplitTaxa(p.Value));
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);

                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("paragraph", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current paragraph.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a paragraph or is already expanded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand4()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 4: look in treatment sections");
                for (Match m = findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match p = Regex.Match(this.xml, "<tp:treatment-sec[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (p.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("treatment section", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current treatment section.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a treatment section or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand5()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 5: look in treatments");
                for (Match m = findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match p = Regex.Match(this.xml, "<tp:taxon-treatment>[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (p.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("treatment", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current treatment.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a treatment or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand6()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 6: look in sections");
                for (Match m = findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match p = Regex.Match(this.xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (p.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("section", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current section.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a section or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand7()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 7: WARNING: search from the beginnig");
                for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match p = Regex.Match(xml, "<?xml[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (p.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("preceding text", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the preceding text.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in the preceding text");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand8()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 8: WARNING: search in the whole article");
                for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match p = Regex.Match(xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (p.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("article", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the article.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in the article or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }
        }
    }

    namespace NlmSystem
    {
        public class Expander : Base
        {
            public const string GenusLeftTag = "<tn-part type=\"genus\">";
            public const string GenusRightTag = "</tn-part>";
            public const string SubgenusLeftTag = "<tn-part type=\"subgenus\">";
            public const string SubgenusRgithTag = "</tn-part>";
            public const string SpeciesLeftTag = "<tn-part type=\"species\">";
            public const string SpeciesRightTag = "</tn-part>";
            public const string SubspeciesLeftTag = "<tn-part type=\"subspecies\">";
            public const string SubspeciesRightTag = "</tn-part>";

            private Regex findLowerTaxa = new Regex(@"<i><tn[^>\-]*>([\s\S]*?)</tn></i>");
            private Regex findLowerTaxaMultiLine = new Regex(@"<i><tn[^>\-]*>([\s\S]*?)</tn></i>");

            public Expander(string xml)
                : base(xml)
            {
            }

            public Expander(Config config, string xml)
                : base(config, xml)
            {
            }

            public Expander(Base baseObject)
                : base(baseObject)
            {
            }

            public void StableExpand()
            {
                // In this method it is supposed that the subspecies name is not shortened
                Taxonomy.PrintMethodMessage("StableExpand");

                this.ParseXmlStringToXmlDocument();

                XmlNodeList shortTaxaList = xmlDocument.SelectNodes("//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']]][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']", NamespaceManager);
                XmlNodeList nonShortTaxaList = xmlDocument.SelectNodes("//tn[@type='lower'][not(tn-part[@full-name])][tn-part[@type='genus']]", NamespaceManager);

                List<string> shortTaxaListUnique = shortTaxaList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
                List<string> nonShortTaxaListUnique = nonShortTaxaList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();

                List<Species> speciesList = new List<Species>();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    speciesList.Add(new Species(taxon));
                }

                foreach (string shortTaxon in shortTaxaListUnique)
                {
                    string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                    string replace = text;

                    Species sp = new Species(shortTaxon);
                    Taxonomy.PrintNextShortened(sp);

                    foreach (Species sp1 in speciesList)
                    {
                        if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0)
                        {
                            Match matchGenus = Regex.Match(sp1.GenusName, sp.GenusPattern);
                            Match matchSubgenus = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                            Match matchSpecies = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                            // Check if the subgenus is empty
                            if (string.Compare(sp.SubgenusName, string.Empty) == 0)
                            {
                                if (matchGenus.Success && matchSpecies.Success)
                                {
                                    if (string.Compare(sp1.SubgenusName, string.Empty) == 0)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                        replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                    }
                                    else
                                    {
                                        Taxonomy.PrintSubstitutionMessageFail(sp, sp1);
                                    }
                                }
                            }
                            else
                            {
                                if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                    replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                    replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                    replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                }
                            }
                        }
                    }

                    this.xml = Regex.Replace(this.xml, Regex.Escape(text), replace);
                }
            }

            public void StableExpand1()
            {
                // In this method it is supposed that the subspecies name is not shortened
                Taxonomy.PrintMethodMessage("StableExpand");

                for (Match m = this.findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                {
                    string replace = m.Value;
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && string.Compare(sp.SpeciesName, string.Empty) != 0)
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            string replace1 = m.Value;

                            Species sp1 = new Species(taxon.Value);

                            // We are interested only on non-shortened lower taxa
                            if ((taxon.Value.IndexOf('.') < 0) && (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0))
                            {
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                                // Check if the subgenus is empty
                                if (string.Compare(sp.SubgenusName, string.Empty) == 0)
                                {
                                    if (mgen.Success && msp.Success)
                                    {
                                        if (string.Compare(sp1.SubgenusName, string.Empty) == 0)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn genus=\"" + sp1.GenusName + "\">");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\tThere is a genus-species coincidence but the subgenus does not match:");
                                            Console.WriteLine("\t\t{0}\t|\t{1}", sp.SpeciesNameAsString, sp1.SpeciesNameAsString);
                                            Console.WriteLine("\t\tSubstitution will not be done!");
                                        }
                                    }
                                }
                                else
                                {
                                    if (mgen.Success && msgen.Success && msp.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn genus=\"" + sp1.GenusName + "\" subgenus=\"" + sp1.SubgenusName + "\">");
                                    }
                                }
                            }
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            public void UnstableExpand(int stage)
            {
                Taxonomy.PrintMethodMessage("UnstableExpand" + stage);

                for (Match m = this.findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    string xgenus = (sp.GenusName.IndexOf('.') > -1) ? sp.GenusName.Substring(0, sp.GenusName.Length - 1) + "[a-z]+?" : "SKIP";
                    string xsubgenus = (sp.SubgenusName.IndexOf('.') > -1) ? sp.SubgenusName.Substring(0, sp.SubgenusName.Length - 1) + "[a-z]+?" : "SKIP";
                    string xspecies = (sp.SpeciesName.IndexOf('.') > -1) ? sp.SpeciesName.Substring(0, sp.SpeciesName.Length - 1) + "[a-z]+?" : "SKIP";

                    // Select only shortened taxa with non-zero species name
                    if (sp.IsShortened && !sp.IsSpeciesNull)
                    {
                        Console.WriteLine("\nNext shortened taxon:\t{0}", sp.AsString());

                        // Scan all lower-taxon names in the article
                        // TODO
                        Match div = Regex.Match(xml, "<p>.*?" + Regex.Escape(m.Value));
                        if (div.Success)
                        {
                            // TODO
                            Console.WriteLine("Paragraph content:\n\t{0}\n", div.Value);
                            bool isFound = false;
                            Species spl = new Species();
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(div.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, xgenus);
                                if (mgen.Success)
                                {
                                    spl.SetGenus(sp1);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, xsubgenus);
                                if (msgen.Success)
                                {
                                    spl.SetSubgenus(sp1);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, xspecies);
                                if (msp.Success)
                                {
                                    spl.SetSpecies(sp1);
                                    isFound = true;
                                }

                                Console.WriteLine("........ Found: genus {0} | subgenus {1} | species {2}", sp1.GenusName, sp1.SubgenusName, sp1.SpeciesName);
                            }

                            if (isFound)
                            {
                                Console.WriteLine("________ Substitution '{0}, ({1}), {2}'  by '{3}, ({4}), {5}'.", sp.GenusName, sp.SubgenusName, sp.SpeciesName, spl.GenusName, spl.SubgenusName, spl.SpeciesName);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (!spl.IsGenusNull)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(GenusLeftTag + sp.GenusName + GenusRightTag), GenusLeftTag + spl.GenusName + GenusRightTag);
                                }

                                if (!spl.IsSubgenusNull)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(SubgenusLeftTag + sp.SubgenusName + SubgenusRgithTag), SubgenusLeftTag + spl.SubgenusName + SubgenusRgithTag);
                                }

                                if (!spl.IsSpeciesNull)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(SpeciesLeftTag + sp.SpeciesName + SpeciesRightTag), SpeciesLeftTag + spl.SpeciesName + SpeciesRightTag);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(div.Value), Regex.Replace(div.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("________ No suitable genus name has been found in the current division.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in such a division or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand1()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1");
                //// On the first stage try to expand all quasi-stable cases like
                //// [Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species

                for (Match m = this.findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                {
                    string replace = m.Value;
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && string.Compare(sp.SpeciesName, string.Empty) != 0)
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        bool found = false;
                        string replace1 = m.Value;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(this.xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0)
                            {
                                // We are interested only in coincident subspecies names
                                if (string.Compare(sp.SpeciesName, sp1.SpeciesName) == 0)
                                {
                                    // First process all taxa with coincident species names
                                    if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0)
                                    {
                                        // ... and coincident subgenus names, i.e. try to find suitable genus names
                                        Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                        if (mgen.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                            found = true;
                                        }
                                    }
                                    else if (string.Compare(sp.GenusName, sp1.GenusName) == 0)
                                    {
                                        // ... or coincident genus names, i.e. try to find suitable subgenus names
                                        Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern);
                                        if (msgen.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                            found = true;
                                        }
                                    }
                                }

                                if (string.Compare(sp.GenusName, sp1.GenusName) == 0)
                                {
                                    // First process all taxa with coincident genus names
                                    if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0)
                                    {
                                        // ... and coincident subgenus names, i.e. try to find suitable genus names
                                        Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                        if (msp.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace1 = Regex.Replace(replace1, Regex.Escape(sp.SpeciesTagged), sp1.SpeciesTagged);
                                            found = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (found)
                        {
                            replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            public void UnstableExpand2()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2");

                // On the second stage try to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    string replace = m.Value;
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species and subgenus name
                    if ((sp.GenusName.IndexOf('.') > -1) && (sp.SubgenusName.IndexOf('.') < 0) && (string.Compare(sp.SubgenusName, string.Empty) != 0) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        bool found = false;
                        string replace1 = m.Value;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(this.xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (string.Compare(sp.GenusName, sp1.GenusName) == 0 && string.Compare(sp.SubgenusName, sp1.SubgenusName) != 0)
                            {
                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                                if (msgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                    found = true;
                                }
                            }

                            if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                            {
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                                if (mgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                    found = true;
                                }
                            }
                        }

                        if (found)
                        {
                            replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }

                    // Select only shortened taxa with non-zero species and subgenus name
                    if ((sp.GenusName.IndexOf('.') < 0) && (sp.SubgenusName.IndexOf('.') > -1) && (string.Compare(sp.SubgenusName, string.Empty) != 0) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        bool found = false;
                        string replace1 = m.Value;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(this.xml); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (string.Compare(sp.GenusName, sp1.GenusName) == 0 && string.Compare(sp.SubgenusName, sp1.SubgenusName) != 0)
                            {
                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                                if (msgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                    found = true;
                                }
                            }

                            if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                            {
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                                if (mgen.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                    replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                    found = true;
                                }
                            }
                        }

                        if (found)
                        {
                            replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                        }

                        this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                    }
                }
            }

            public void UnstableExpand3()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match paragraph = Regex.Match(this.xml, "<p>.*?" + Regex.Escape(m.Value));
                        if (paragraph.Success)
                        {
                            Console.WriteLine("Paragraph content:\n\t{0}\n", TaxaParser.UnSplitTaxa(paragraph.Value));
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);

                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("paragraph", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current paragraph.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a paragraph or is already expanded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand4()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 4: look in treatment sections");
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match paragraph = Regex.Match(this.xml, "<tp:treatment-sec[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (paragraph.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("treatment section", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current treatment section.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a treatment section or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand5()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 5: look in treatments");
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match paragraph = Regex.Match(xml, "<tp:taxon-treatment>[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (paragraph.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("treatment", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current treatment.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a treatment or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand6()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 6: look in sections");
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match paragraph = Regex.Match(this.xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (paragraph.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("section", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the current section.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in a section or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand7()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 7: WARNING: search from the beginnig");
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match paragraph = Regex.Match(this.xml, "<?xml[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (paragraph.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("preceding text", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the preceding text.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in the preceding text");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableExpand8()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 8: WARNING: search in the whole article");
                for (Match m = this.findLowerTaxa.Match(this.xml); m.Success; m = m.NextMatch())
                {
                    Species sp = new Species(m.Value);
                    if (Taxonomy.EmptyGenus(m.Value, sp))
                    {
                        return;
                    }

                    // Select only shortened taxa with non-zero species name
                    if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                    {
                        Taxonomy.PrintNextShortened(sp);

                        // Scan all lower-taxon names in the article
                        Match paragraph = Regex.Match(this.xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                        if (paragraph.Success)
                        {
                            Species last = new Species();
                            bool isFound = false;
                            for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                if (mgen.Success)
                                {
                                    last.SetGenus(sp1.GenusName);
                                    isFound = true;
                                }

                                Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                                if (msgen.Success)
                                {
                                    last.SetSubgenus(sp1.SubgenusName);
                                    isFound = true;
                                }

                                Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                if (msp.Success)
                                {
                                    last.SetSpecies(sp1.SpeciesName);
                                    isFound = true;
                                }

                                Taxonomy.PrintFoundMessage("article", sp1);
                            }

                            if (isFound)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, last);
                                string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                if (string.Compare(last.GenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                                }

                                if (string.Compare(last.SubgenusName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                                }

                                if (string.Compare(last.SpeciesName, string.Empty) != 0)
                                {
                                    replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                                }

                                this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                            }
                            else
                            {
                                Console.WriteLine("\n\tNo suitable genus name has been found in the article.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This species is not in the article or is already unfolded");
                        }

                        Console.WriteLine("\n");
                    }
                }
            }

            public void UnstableUnfold()
            {
                string genusPrefix = "<tn-part type=\"genus\">";
                string genusSuffix = "</tn-part>";
                string genusNPrefix = "(?<=" + genusPrefix + ")";
                string genusNSuffix = "(?=" + genusSuffix + ")";
                string genusSpeciesPattern = "<tn[^>]*?><tn-part type=\"genus\">[A-Z][a-z]*\\.</tn-part>\\s*<tn-part type=\"species\">.*?</tn-part></tn>";

                // Print all recognized genera in the article
                Console.WriteLine();
                Match genus = Regex.Match(xml, genusNPrefix + "[A-Z][a-z\\.]+?" + genusNSuffix);
                while (genus.Success)
                {
                    Console.WriteLine("Found genus: {0}", genus.Value);
                    genus = genus.NextMatch();
                }

                Console.Write("\n\n\n\n\n");

                // Show only genera in the current paragraph
                Match genSp = Regex.Match(xml, genusSpeciesPattern);
                while (genSp.Success)
                {
                    Match genusShort = Regex.Match(genSp.Value, genusNPrefix + "[A-Z][a-z]*?(?=\\.)");
                    Match species = Regex.Match(genSp.Value, "(?<=<tn-part type=\"species\">).*?(?=<)");
                    Console.WriteLine("Shortened species found:\t{0}. {1}\n", genusShort.Value, species.Value);
                    bool isFound = false;
                    Console.WriteLine("Scanning containing paragraph to find suitable genus...");
                    Match paragraph = Regex.Match(xml, "<p>.*?" + Regex.Escape(genSp.Value));
                    if (paragraph.Success)
                    {
                        Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                        string lastGenusFound = string.Empty;
                        isFound = false;
                        Match genusPar = Regex.Match(paragraph.Value, genusNPrefix + Regex.Escape(genusShort.Value) + "[a-z]+?" + genusNSuffix);
                        while (genusPar.Success)
                        {
                            isFound = true;
                            lastGenusFound = genusPar.Value;
                            Console.WriteLine("........ Found Genus in paragraph: {0}\n", lastGenusFound);
                            genusPar = genusPar.NextMatch();
                        }

                        if (isFound)
                        {
                            Console.WriteLine("\n\tSpecies name '{0}. {1}' will be replaced by '{2} {1}' in the current paragraph.\n", genusShort.Value, species.Value, lastGenusFound);
                            string replace = Regex.Replace(paragraph.Value, ">" + genusPrefix + genusShort.Value + "\\." + genusSuffix, " unfold=\"true\"><tn-part type=\"genus\">" + lastGenusFound + "</tn-part>");
                            this.xml = Regex.Replace(this.xml, Regex.Escape(paragraph.Value), replace);
                        }
                        else
                        {
                            Console.WriteLine("\n\tNo suitable genus name has been found in the current paragraph.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("This species is not in a paragraph or is already unfolded");
                    }

                    Console.WriteLine("\n");
                    genSp = genSp.NextMatch();
                }
            }
        }
    }

    public class Expander : Base
    {
        public Expander(string xml)
            : base(xml)
        {
        }

        public Expander(Config config, string xml)
            : base(config, xml)
        {
        }

        public Expander(Base baseObject)
            : base(baseObject)
        {
        }

        public void StableExpand()
        {
            this.xml = Base.NormalizeNlmToSystemXml(this.Config, this.xml);

            // In this method it is supposed that the subspecies name is not shortened
            Taxonomy.PrintMethodMessage("StableExpand");

            this.xml = Base.NormalizeNlmToSystemXml(this.Config, this.xml);
            this.ParseXmlStringToXmlDocument();

            List<string> shortTaxaListUnique = Taxonomy.ListOfShortenedTaxa(this.xmlDocument);
            List<string> nonShortTaxaListUnique = Taxonomy.ListOfNonShortenedTaxa(this.xmlDocument);

            List<Species> speciesList = new List<Species>();
            foreach (string taxon in nonShortTaxaListUnique)
            {
                speciesList.Add(new Species(taxon));
            }

            foreach (string shortTaxon in shortTaxaListUnique)
            {
                string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                string replace = text;

                Species sp = new Species(shortTaxon);
                Taxonomy.PrintNextShortened(sp);

                foreach (Species sp1 in speciesList)
                {
                    if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0)
                    {
                        Match matchGenus = Regex.Match(sp1.GenusName, sp.GenusPattern);
                        Match matchSubgenus = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                        Match matchSpecies = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                        // Check if the subgenus is empty
                        if (string.Compare(sp.SubgenusName, string.Empty) == 0)
                        {
                            if (matchGenus.Success && matchSpecies.Success)
                            {
                                if (string.Compare(sp1.SubgenusName, string.Empty) == 0)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                    replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                    replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                }
                                else
                                {
                                    Taxonomy.PrintSubstitutionMessageFail(sp, sp1);
                                }
                            }
                        }
                        else
                        {
                            if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                            }
                        }
                    }
                }

                this.xml = Regex.Replace(this.xml, Regex.Escape(text), replace);
            }

            if (this.Config.NlmStyle)
            {
                this.xml = Base.NormalizeSystemToNlmXml(this.Config, this.xml);
            }
        }

        public void UnstableExpand3()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");

            this.xml = Base.NormalizeNlmToSystemXml(this.Config, this.xml);
            this.ParseXmlStringToXmlDocument();

            // Loop over paragraphs containong shortened taxa
            foreach (XmlNode p in xmlDocument.SelectNodes("//p[count(.//tn-part[normalize-space(@full-name)='']) > 0]"))
            {
                Alert.Log(p.InnerText);
                Alert.Log("\n\n");

                List<string> shortTaxaListUnique = Taxonomy.ListOfShortenedTaxa(p);
                List<string> nonShortTaxaListUnique = Taxonomy.ListOfNonShortenedTaxa(p);

                List<Species> speciesList = new List<Species>();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    speciesList.Add(new Species(taxon));
                }

                foreach (string taxon in shortTaxaListUnique)
                {
                    Alert.Log(taxon);
                }

                Alert.Log();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    Alert.Log(taxon);
                }

                Alert.Log("\n\n");
            }

            this.xml = this.xmlDocument.OuterXml;
            if (this.Config.NlmStyle)
            {
                this.xml = Base.NormalizeSystemToNlmXml(this.Config, this.xml);
            }
        }

        public void UnstableExpand8()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 8: WARNING: search in the whole article");

            this.xml = Base.NormalizeNlmToSystemXml(this.Config, this.xml);
            this.ParseXmlStringToXmlDocument();

            List<string> shortTaxaListUnique = Taxonomy.ListOfShortenedTaxa(xmlDocument);
            List<string> nonShortTaxaListUnique = Taxonomy.ListOfNonShortenedTaxa(xmlDocument);

            List<Species> speciesList = new List<Species>();
            foreach (string taxon in nonShortTaxaListUnique)
            {
                speciesList.Add(new Species(taxon));
            }

            foreach (string shortTaxon in shortTaxaListUnique)
            {
                string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                string replace = text;

                Species sp = new Species(shortTaxon);
                Taxonomy.PrintNextShortened(sp);

                foreach (Species sp1 in speciesList)
                {
                    Match matchGenus = Regex.Match(sp1.GenusName, sp.GenusPattern);
                    Match matchSubgenus = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                    Match matchSpecies = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                    if (matchGenus.Success || matchSubgenus.Success || matchSpecies.Success)
                    {
                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                        replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                        replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                        replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                    }
                }

                this.xml = Regex.Replace(this.xml, Regex.Escape(text), replace);
            }

            // Return
            if (this.Config.NlmStyle)
            {
                this.xml = Base.NormalizeSystemToNlmXml(this.Config, this.xml);
            }
        }
    }
}
