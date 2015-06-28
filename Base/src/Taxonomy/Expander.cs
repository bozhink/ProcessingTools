using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Base
{
    namespace Taxonomy
    {
        public class Expander : Base
        {
            public Expander()
                : base()
            {
            }

            public Expander(string xml)
                : base(xml)
            {
            }

            public Expander(Config config)
                : base(config)
            {
            }





            public void StableExpand()
            {
                xml = Format.Format.NormalizeNlmToSystemXml(config, xml);

                // In this method it is supposed that the subspecies name is not shortened
                Taxonomy.PrintMethodMessage("StableExpand");

                xml = Format.Format.NormalizeNlmToSystemXml(config, xml);
                ParseXmlStringToXmlDocument();

                List<string> shortTaxaListUnique = Taxonomy.ListOfShortenedTaxa(xmlDocument);
                List<string> nonShortTaxaListUnique = Taxonomy.ListOfNonShortenedTaxa(xmlDocument);

                List<Species> speciesList = new List<Species>();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    speciesList.Add(new Species(taxon));
                }

                foreach (string shortTaxon in shortTaxaListUnique)
                {
                    string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", "");
                    string replace = text;

                    Species sp = new Species(shortTaxon);
                    Taxonomy.PrintNextShortened(sp);

                    foreach (Species sp1 in speciesList)
                    {
                        if (String.Compare(sp.subspecies, sp1.subspecies) == 0)
                        {
                            Match matchGenus = Regex.Match(sp1.genus, sp.genusPattern);
                            Match matchSubgenus = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                            Match matchSpecies = Regex.Match(sp1.species, sp.speciesPattern);

                            // Check if the subgenus is empty
                            if (String.Compare(sp.subgenus, string.Empty) == 0)
                            {
                                if (matchGenus.Success && matchSpecies.Success)
                                {
                                    if (String.Compare(sp1.subgenus, string.Empty) == 0)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                                        replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
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
                                    replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                                    replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.subgenus);
                                    replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
                                }

                            }
                        }
                    }
                    xml = Regex.Replace(xml, Regex.Escape(text), replace);
                }

                // Return
                if (config.NlmStyle)
                {
                    xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
                }
            }


            public void UnstableExpand3()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");

                xml = Format.Format.NormalizeNlmToSystemXml(config, xml);
                ParseXmlStringToXmlDocument();

                

                // Loop over paragraphs containong shortened taxa
                foreach (XmlNode p in xmlDocument.SelectNodes("//p[count(.//tn-part[normalize-space(@full-name)='']) > 0]"))
                {
                    Alert.Message(p.InnerText);
                    Alert.Message("\n\n");

                    List<string> shortTaxaListUnique = Taxonomy.ListOfShortenedTaxa(p);
                    List<string> nonShortTaxaListUnique = Taxonomy.ListOfNonShortenedTaxa(p);

                    List<Species> speciesList = new List<Species>();
                    foreach (string taxon in nonShortTaxaListUnique)
                    {
                        speciesList.Add(new Species(taxon));
                    }

                    foreach(string taxon in shortTaxaListUnique)
                    {
                        Alert.Message(taxon);
                    }
                    Alert.Message();
                    foreach (string taxon in nonShortTaxaListUnique)
                    {
                        Alert.Message(taxon);
                    }



                    Alert.Message("\n\n");
                }










                //foreach (string shortTaxon in shortTaxaListUnique)
                //{
                //    string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", "");
                //    string replace = text;

                //    Species sp = new Species(shortTaxon);
                //    Taxonomy.PrintNextShortened(sp);

                //    foreach (Species sp1 in speciesList)
                //    {
                //        if (String.Compare(sp.subspecies, sp1.subspecies) == 0)
                //        {
                //            Match matchGenus = Regex.Match(sp1.genus, sp.genusPattern);
                //            Match matchSubgenus = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                //            Match matchSpecies = Regex.Match(sp1.species, sp.speciesPattern);

                //            // Check if the subgenus is empty
                //            if (String.Compare(sp.subgenus, string.Empty) == 0)
                //            {
                //                if (matchGenus.Success && matchSpecies.Success)
                //                {
                //                    if (String.Compare(sp1.subgenus, string.Empty) == 0)
                //                    {
                //                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                //                        replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                //                        replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
                //                    }
                //                    else
                //                    {
                //                        Taxonomy.PrintSubstitutionMessageFail(sp, sp1);
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                //                {
                //                    Taxonomy.PrintSubstitutionMessage(sp, sp1);
                //                    replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                //                    replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.subgenus);
                //                    replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
                //                }

                //            }
                //        }
                //    }
                //    xml = Regex.Replace(xml, Regex.Escape(text), replace);
                //}










                //ParseXmlStringToXmlDocument();

                //XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", namespaceManager);
                //foreach (XmlNode node in lowerTaxa)
                //{
                //    Species sp = new Species(node.InnerXml);
                //    if (Taxonomy.EmptyGenus(node.InnerXml, sp))
                //    {
                //        return;
                //    }
                //    // Select only shortened taxa with non-zero species name
                //    if (sp.isShortened && (sp.species.Length > 0))
                //    {
                //        Taxonomy.PrintNextShortened(sp);

                //        //TODO
                //        for (Match p = Regex.Match(xml, "<p>[\\s\\S]+?" + Regex.Escape(node.InnerXml)); p.Success; p = p.NextMatch())
                //        {
                //            Console.WriteLine("Paragraph content:\n\t{0}\n", Splitter.UnSplitTaxa(p.Value));

                //            Species last = new Species();
                //            bool isFound = false;
                //            for (Match taxon = Nlm.Expander.FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                //            {
                //                Species sp1 = new Species(taxon.Value);
                //                if (Regex.Match(sp1.genus, sp.genusSkipPattern).Success)
                //                {
                //                    last.SetGenus(sp1.genus);
                //                    isFound = true;
                //                }
                //                if (Regex.Match(sp1.subgenus, sp.subgenusSkipPattern).Success)
                //                {
                //                    last.SetSubgenus(sp1.subgenus);
                //                    isFound = true;
                //                }
                //                if (Regex.Match(sp1.species, sp.speciesSkipPattern).Success)
                //                {
                //                    last.SetSpecies(sp1.species);
                //                    isFound = true;
                //                }
                //                Taxonomy.PrintFoundMessage("paragraph", sp1);
                //            }
                //            if (isFound)
                //            {
                //                Taxonomy.PrintSubstitutionMessage(sp, last);
                //                string replace = node.InnerXml;
                //                if (last.genus.Length > 0)
                //                {
                //                    replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                //                }
                //                if (last.subgenus.Length > 0)
                //                {
                //                    replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                //                }
                //                if (last.species.Length > 0)
                //                {
                //                    replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                //                }
                //                xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(node.InnerXml), replace));
                //            }
                //            else
                //            {
                //                Console.WriteLine("\n\tNo suitable genus name has been found in the current paragraph.\n");
                //            }
                //        }
                //        Console.WriteLine("\n");
                //    }
                //}



                // Return
                xml = xmlDocument.OuterXml;
                if (config.NlmStyle)
                {
                    xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
                }
            }



            public void UnstableExpand8()
            {
                Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 8: WARNING: search in the whole article");

                xml = Format.Format.NormalizeNlmToSystemXml(config, xml);
                ParseXmlStringToXmlDocument();

                List<string> shortTaxaListUnique = Taxonomy.ListOfShortenedTaxa(xmlDocument);
                List<string> nonShortTaxaListUnique = Taxonomy.ListOfNonShortenedTaxa(xmlDocument);

                List<Species> speciesList = new List<Species>();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    speciesList.Add(new Species(taxon));
                }

                foreach (string shortTaxon in shortTaxaListUnique)
                {
                    string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", "");
                    string replace = text;

                    Species sp = new Species(shortTaxon);
                    Taxonomy.PrintNextShortened(sp);

                    foreach (Species sp1 in speciesList)
                    {
                        Match matchGenus = Regex.Match(sp1.genus, sp.genusPattern);
                        Match matchSubgenus = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                        Match matchSpecies = Regex.Match(sp1.species, sp.speciesPattern);

                        if (matchGenus.Success || matchSubgenus.Success || matchSpecies.Success)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                            replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                            replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.subgenus);
                            replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
                        }
                    }
                    xml = Regex.Replace(xml, Regex.Escape(text), replace);
                }

                // Return
                if (config.NlmStyle)
                {
                    xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
                }
            }
        }

        namespace Nlm
        {
            public class Expander : Base
            {
                public Expander() : base() { }
                public Expander(string xml) : base(xml) { }

                public static Regex findLowerTaxa = new Regex(@"<italic><tp:taxon-name[^>\-]*>(.*?)</tp:taxon-name></italic>");
                public static Regex FindLowerTaxa = new Regex(@"<italic><tp:taxon-name[^>\-]*>([\s\S]*?)</tp:taxon-name></italic>");

                public const string lGenus = "<tp:taxon-name-part taxon-name-part-type=\"genus\">";
                public const string rGenus = "</tp:taxon-name-part>";
                public const string lSubgenus = "<tp:taxon-name-part taxon-name-part-type=\"subgenus\">";
                public const string rSubgenus = "</tp:taxon-name-part>";
                public const string lSpecies = "<tp:taxon-name-part taxon-name-part-type=\"species\">";
                public const string rSpecies = "</tp:taxon-name-part>";
                public const string lSubspecies = "<tp:taxon-name-part taxon-name-part-type=\"subspecies\">";
                public const string rSubspecies = "</tp:taxon-name-part>";


                public void UnstableExpand1()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1\nTrying to expand all quasi-stable cases like\n[Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species");
                    ParseXmlStringToXmlDocument();

                    XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", namespaceManager);
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
                        if (sp.isShortened && sp.species.Length > 0)
                        {
                            Taxonomy.PrintNextShortened(sp);
                            foreach (Species sp1 in speciesList)
                            {
                                SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                                if (compare.subspecies) // We are interested only in coincident subspecies names
                                {
                                    if (compare.species) // First process all taxa with coincident species names
                                    {
                                        if (compare.subgenus) //... and coincident subgenus names, i.e. try to find suitable genus names
                                        {
                                            if (Regex.Match(sp1.genus, sp.genusSkipPattern).Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                                            }
                                        }
                                        else if (compare.genus) //... or coincident genus names, i.e. try to find suitable subgenus names
                                        {
                                            if (Regex.Match(sp1.subgenus, sp.subgenusSkipPattern).Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.subgenus);
                                            }
                                        }
                                    }
                                    if (compare.genus) // First process all taxa with coincident genus names
                                    {
                                        if (compare.subgenus) //... and coincident subgenus names, i.e. try to find suitable genus names
                                        {
                                            if (Regex.Match(sp1.species, sp.speciesSkipPattern).Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    xml = xmlDocument.OuterXml;
                }

                public void _UnstableExpand1()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1");
                    // On the first stage try to expand all quasi-stable cases like
                    // [Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species

                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        string replace = m.Value;
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && String.Compare(sp.species, string.Empty) != 0)
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            bool found = false;
                            string replace1 = m.Value;
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (String.Compare(sp.subspecies, sp1.subspecies) == 0)
                                {// We are interested only in coincident subspecies names
                                    if (String.Compare(sp.species, sp1.species) == 0)
                                    {// First process all taxa with coincident species names
                                        if (String.Compare(sp.subgenus, sp1.subgenus) == 0)
                                        {//... and coincident subgenus names, i.e. try to find suitable genus names
                                            Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern);
                                            if (mgen.Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.genusTagged), sp1.genusTagged);
                                                found = true;
                                            }
                                        }
                                        else if (String.Compare(sp.genus, sp1.genus) == 0)
                                        {//... or coincident genus names, i.e. try to find suitable subgenus names
                                            Match msgen = Regex.Match(sp1.subgenus, sp.subgenusSkipPattern);
                                            if (msgen.Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.subgenusTagged), sp1.subgenusTagged);
                                                found = true;
                                            }
                                        }
                                    }
                                    if (String.Compare(sp.genus, sp1.genus) == 0)
                                    {// First process all taxa with coincident genus names
                                        if (String.Compare(sp.subgenus, sp1.subgenus) == 0)
                                        {//... and coincident subgenus names, i.e. try to find suitable genus names
                                            Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern);
                                            if (msp.Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.speciesTagged), sp1.speciesTagged);
                                                found = true;
                                            }
                                        }
                                    }
                                }
                            }
                            if (found) replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                            //if (found) Console.WriteLine("\n{0}\n{1}\n", m.Value, replace);
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                    }
                }


                public void UnstableExpand2()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2\nTrying to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)");
                    ParseXmlStringToXmlDocument();

                    XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", namespaceManager);
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
                        if ((sp.genus.IndexOf('.') > -1) && ((sp.subgenus.IndexOf('.') < 0) && (sp.subgenus.Length > 0)) && (sp.species.Length > 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            foreach (Species sp1 in speciesList)
                            {
                                SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                                if (compare.subgenus && !compare.genus)
                                {
                                    if (Regex.Match(sp1.genus, sp.genusPattern).Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                        node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                                    }
                                }
                            }
                        }
                        /*
                         * Select only shortened taxa with non-zero species and genus name
                         */
                        if ((sp.subgenus.IndexOf('.') > -1) && ((sp.genus.IndexOf('.') < 0) && (sp.genus.Length > 0)) && (sp.species.Length > 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            foreach (Species sp1 in speciesList)
                            {
                                SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                                if (compare.genus && !compare.subgenus)
                                {
                                    if (Regex.Match(sp1.subgenus, sp.subgenusPattern).Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                        node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.subgenus);
                                    }
                                }
                            }
                        }
                    }
                    xml = xmlDocument.OuterXml;
                }

                public void _UnstableExpand2()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2");
                    // On the second stage try to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        string replace = m.Value;
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;

                        // Select only shortened taxa with non-zero species and subgenus name
                        if ((sp.genus.IndexOf('.') > -1) && (sp.subgenus.IndexOf('.') < 0) && (String.Compare(sp.subgenus, string.Empty) != 0) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            bool found = false;
                            string replace1 = m.Value;
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (String.Compare(sp.genus, sp1.genus) == 0 && String.Compare(sp.subgenus, sp1.subgenus) != 0)
                                {
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                                    if (msgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.subgenusTagged), sp1.subgenusTagged);
                                        found = true;
                                    }
                                }
                                if (String.Compare(sp.subgenus, sp1.subgenus) == 0 && String.Compare(sp.genus, sp1.genus) != 0)
                                {
                                    Match mgen = Regex.Match(sp1.genus, sp.genusPattern);
                                    if (mgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.genusTagged), sp1.genusTagged);
                                        found = true;
                                    }
                                }
                            }
                            if (found) replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                        //
                        //
                        // Select only shortened taxa with non-zero species and subgenus name
                        if ((sp.genus.IndexOf('.') < 0) && (sp.subgenus.IndexOf('.') > -1) && (String.Compare(sp.subgenus, string.Empty) != 0) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            bool found = false;
                            string replace1 = m.Value;
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (String.Compare(sp.genus, sp1.genus) == 0 && String.Compare(sp.subgenus, sp1.subgenus) != 0)
                                {
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                                    if (msgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.subgenusTagged), sp1.subgenusTagged);
                                        found = true;
                                    }
                                }
                                if (String.Compare(sp.subgenus, sp1.subgenus) == 0 && String.Compare(sp.genus, sp1.genus) != 0)
                                {
                                    Match mgen = Regex.Match(sp1.genus, sp.genusPattern);
                                    if (mgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage1(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.genusTagged), sp1.genusTagged);
                                        found = true;
                                    }
                                }
                            }
                            if (found) replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                    }
                }


                public void UnstableExpand3()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");
                    ParseXmlStringToXmlDocument();

                    XmlNodeList lowerTaxa = xmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", namespaceManager);
                    foreach (XmlNode node in lowerTaxa)
                    {
                        Species sp = new Species(node.InnerXml);
                        if (Taxonomy.EmptyGenus(node.InnerXml, sp))
                        {
                            return;
                        }
                        // Select only shortened taxa with non-zero species name
                        if (sp.isShortened && (sp.species.Length > 0))
                        {
                            Taxonomy.PrintNextShortened(sp);

                            //TODO
                            for (Match p = Regex.Match(xml, "<p>[\\s\\S]+?" + Regex.Escape(node.InnerXml)); p.Success; p = p.NextMatch())
                            {
                                Console.WriteLine("Paragraph content:\n\t{0}\n", Splitter.UnSplitTaxa(p.Value));

                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    if (Regex.Match(sp1.genus, sp.genusSkipPattern).Success)
                                    {
                                        last.SetGenus(sp1.genus);
                                        isFound = true;
                                    }
                                    if (Regex.Match(sp1.subgenus, sp.subgenusSkipPattern).Success)
                                    {
                                        last.SetSubgenus(sp1.subgenus);
                                        isFound = true;
                                    }
                                    if (Regex.Match(sp1.species, sp.speciesSkipPattern).Success)
                                    {
                                        last.SetSpecies(sp1.species);
                                        isFound = true;
                                    }
                                    Taxonomy.PrintFoundMessage("paragraph", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = node.InnerXml;
                                    if (last.genus.Length > 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (last.subgenus.Length > 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (last.species.Length > 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(node.InnerXml), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match p = Regex.Match(xml, "<p>.*?" + Regex.Escape(m.Value));
                            if (p.Success)
                            {
                                Console.WriteLine("Paragraph content:\n\t{0}\n", Splitter.UnSplitTaxa(p.Value));
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);

                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("paragraph", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match p = Regex.Match(xml, "<tp:treatment-sec[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (p.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("treatment section", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match p = Regex.Match(xml, "<tp:taxon-treatment>[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (p.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("treatment", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match p = Regex.Match(xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (p.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("section", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match p = Regex.Match(xml, "<?xml[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (p.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("preceding text", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match p = Regex.Match(xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (p.Success)
                            {
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("article", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                public Expander() : base() { }
                public Expander(string xml) : base(xml) { }

                private Regex findLowerTaxa = new Regex(@"<i><tn[^>\-]*>([\s\S]*?)</tn></i>");
                private Regex FindLowerTaxa = new Regex(@"<i><tn[^>\-]*>([\s\S]*?)</tn></i>");

                public const string lGenus = "<tn-part type=\"genus\">";
                public const string rGenus = "</tn-part>";
                public const string lSubgenus = "<tn-part type=\"subgenus\">";
                public const string rSubgenus = "</tn-part>";
                public const string lSpecies = "<tn-part type=\"species\">";
                public const string rSpecies = "</tn-part>";
                public const string lSubspecies = "<tn-part type=\"subspecies\">";
                public const string rSubspecies = "</tn-part>";

                public void StableExpand()
                {
                    // In this method it is supposed that the subspecies name is not shortened
                    Taxonomy.PrintMethodMessage("StableExpand");

                    ParseXmlStringToXmlDocument();

                    XmlNodeList shortTaxaList = xmlDocument.SelectNodes("//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']]][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']", namespaceManager);
                    XmlNodeList nonShortTaxaList = xmlDocument.SelectNodes("//tn[@type='lower'][not(tn-part[@full-name])][tn-part[@type='genus']]", namespaceManager);

                    List<string> shortTaxaListUnique = shortTaxaList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
                    List<string> nonShortTaxaListUnique = nonShortTaxaList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();

                    List<Species> speciesList = new List<Species>();
                    foreach (string taxon in nonShortTaxaListUnique)
                    {
                        speciesList.Add(new Species(taxon));
                    }

                    foreach (string shortTaxon in shortTaxaListUnique)
                    {
                        string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", "");
                        string replace = text;

                        Species sp = new Species(shortTaxon);
                        Taxonomy.PrintNextShortened(sp);

                        foreach (Species sp1 in speciesList)
                        {
                            if (String.Compare(sp.subspecies, sp1.subspecies) == 0)
                            {
                                Match matchGenus = Regex.Match(sp1.genus, sp.genusPattern);
                                Match matchSubgenus = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                                Match matchSpecies = Regex.Match(sp1.species, sp.speciesPattern);

                                // Check if the subgenus is empty
                                if (String.Compare(sp.subgenus, string.Empty) == 0)
                                {
                                    if (matchGenus.Success && matchSpecies.Success)
                                    {
                                        if (String.Compare(sp1.subgenus, string.Empty) == 0)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                                            replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
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
                                        replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.genus);
                                        replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.subgenus);
                                        replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.species);
                                    }

                                }
                            }
                        }
                        xml = Regex.Replace(xml, Regex.Escape(text), replace);
                    }
                }

                public void StableExpand1()
                {
                    // In this method it is supposed that the subspecies name is not shortened
                    Taxonomy.PrintMethodMessage("StableExpand");

                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        string replace = m.Value;
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;

                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && String.Compare(sp.species, string.Empty) != 0)
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                string replace1 = m.Value;

                                Species sp1 = new Species(taxon.Value);

                                // We are interested only on non-shortened lower taxa
                                if ((taxon.Value.IndexOf('.') < 0) && (String.Compare(sp.subspecies, sp1.subspecies) == 0))
                                {
                                    Match mgen = Regex.Match(sp1.genus, sp.genusPattern);
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                                    Match msp = Regex.Match(sp1.species, sp.speciesPattern);

                                    // Check if the subgenus is empty
                                    if (String.Compare(sp.subgenus, string.Empty) == 0)
                                    {
                                        if (mgen.Success && msp.Success)
                                        {
                                            if (String.Compare(sp1.subgenus, string.Empty) == 0)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn genus=\"" + sp1.genus + "\">");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\tThere is a genus-species coincidence but the subgenus does not match:");
                                                Console.WriteLine("\t\t{0}\t|\t{1}", sp.speciesName, sp1.speciesName);
                                                Console.WriteLine("\t\tSubstitution will not be done!");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (mgen.Success && msgen.Success && msp.Success)
                                        {
                                            Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                            replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn genus=\"" + sp1.genus + "\" subgenus=\"" + sp1.subgenus + "\">");
                                        }
                                    }
                                }
                            }
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                    }
                }

                public void UnstableExpand(int stage)
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand" + stage);

                    //string [] division = {"paragraph", "treatment section", "treatment", "section", "beginning", "all"};
                    //string [] pattern = {"<p>.*?", "<tp:treatment-sec[^\r]*?", "<tp:taxon-treatment>[^\r]*?", "<sec[^\r]*?", "<?xml[^\r]*?", "[^\r]*"};

                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;

                        string xgenus = (sp.genus.IndexOf('.') > -1) ? sp.genus.Substring(0, sp.genus.Length - 1) + "[a-z]+?" : "SKIP";
                        string xsubgenus = (sp.subgenus.IndexOf('.') > -1) ? sp.subgenus.Substring(0, sp.subgenus.Length - 1) + "[a-z]+?" : "SKIP";
                        string xspecies = (sp.species.IndexOf('.') > -1) ? sp.species.Substring(0, sp.species.Length - 1) + "[a-z]+?" : "SKIP";

                        // Select only shortened taxa with non-zero species name
                        if (sp.isShortened && !sp.isSpeciesNull)
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
                                for (Match taxon = FindLowerTaxa.Match(div.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, xgenus); if (mgen.Success) { spl.SetGenus(sp1); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, xsubgenus); if (msgen.Success) { spl.SetSubgenus(sp1); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, xspecies); if (msp.Success) { spl.SetSpecies(sp1); isFound = true; }
                                    Console.WriteLine("........ Found: genus {0} | subgenus {1} | species {2}", sp1.genus, sp1.subgenus, sp1.species);
                                }
                                if (isFound)
                                {
                                    Console.WriteLine("________ Substitution '{0}, ({1}), {2}'  by '{3}, ({4}), {5}'.", sp.genus, sp.subgenus, sp.species, spl.genus, spl.subgenus, spl.species);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (!spl.isGenusNull)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(lGenus + sp.genus + rGenus), lGenus + spl.genus + rGenus);
                                    }
                                    if (!spl.isSubgenusNull)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(lSubgenus + sp.subgenus + rSubgenus), lSubgenus + spl.subgenus + rSubgenus);
                                    }
                                    if (!spl.isSpeciesNull)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(lSpecies + sp.species + rSpecies), lSpecies + spl.species + rSpecies);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(div.Value), Regex.Replace(div.Value, Regex.Escape(m.Value), replace));
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
                    // On the first stage try to expand all quasi-stable cases like
                    // [Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species

                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        string replace = m.Value;
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && String.Compare(sp.species, string.Empty) != 0)
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            bool found = false;
                            string replace1 = m.Value;
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (String.Compare(sp.subspecies, sp1.subspecies) == 0)
                                {// We are interested only in coincident subspecies names
                                    if (String.Compare(sp.species, sp1.species) == 0)
                                    {// First process all taxa with coincident species names
                                        if (String.Compare(sp.subgenus, sp1.subgenus) == 0)
                                        {//... and coincident subgenus names, i.e. try to find suitable genus names
                                            Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern);
                                            if (mgen.Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.genusTagged), sp1.genusTagged);
                                                found = true;
                                            }
                                        }
                                        else if (String.Compare(sp.genus, sp1.genus) == 0)
                                        {//... or coincident genus names, i.e. try to find suitable subgenus names
                                            Match msgen = Regex.Match(sp1.subgenus, sp.subgenusSkipPattern);
                                            if (msgen.Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.subgenusTagged), sp1.subgenusTagged);
                                                found = true;
                                            }
                                        }
                                    }
                                    if (String.Compare(sp.genus, sp1.genus) == 0)
                                    {// First process all taxa with coincident genus names
                                        if (String.Compare(sp.subgenus, sp1.subgenus) == 0)
                                        {//... and coincident subgenus names, i.e. try to find suitable genus names
                                            Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern);
                                            if (msp.Success)
                                            {
                                                Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.speciesTagged), sp1.speciesTagged);
                                                found = true;
                                            }
                                        }
                                    }
                                }
                            }
                            if (found) replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            //if (found) Console.WriteLine("\n{0}\n{1}\n", m.Value, replace);
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                    }
                }

                public void UnstableExpand2()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2");
                    // On the second stage try to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        string replace = m.Value;
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;

                        // Select only shortened taxa with non-zero species and subgenus name
                        if ((sp.genus.IndexOf('.') > -1) && (sp.subgenus.IndexOf('.') < 0) && (String.Compare(sp.subgenus, string.Empty) != 0) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            bool found = false;
                            string replace1 = m.Value;
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (String.Compare(sp.genus, sp1.genus) == 0 && String.Compare(sp.subgenus, sp1.subgenus) != 0)
                                {
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                                    if (msgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.subgenusTagged), sp1.subgenusTagged);
                                        found = true;
                                    }
                                }
                                if (String.Compare(sp.subgenus, sp1.subgenus) == 0 && String.Compare(sp.genus, sp1.genus) != 0)
                                {
                                    Match mgen = Regex.Match(sp1.genus, sp.genusPattern);
                                    if (mgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.genusTagged), sp1.genusTagged);
                                        found = true;
                                    }
                                }
                            }
                            if (found) replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            //if (found) Console.WriteLine("\n{0}\n{1}\n", m.Value, replace);
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                        //
                        //
                        // Select only shortened taxa with non-zero species and subgenus name
                        if ((sp.genus.IndexOf('.') < 0) && (sp.subgenus.IndexOf('.') > -1) && (String.Compare(sp.subgenus, string.Empty) != 0) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            bool found = false;
                            string replace1 = m.Value;
                            for (Match taxon = FindLowerTaxa.Match(xml); taxon.Success; taxon = taxon.NextMatch())
                            {
                                Species sp1 = new Species(taxon.Value);
                                if (String.Compare(sp.genus, sp1.genus) == 0 && String.Compare(sp.subgenus, sp1.subgenus) != 0)
                                {
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusPattern);
                                    if (msgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.subgenusTagged), sp1.subgenusTagged);
                                        found = true;
                                    }
                                }
                                if (String.Compare(sp.subgenus, sp1.subgenus) == 0 && String.Compare(sp.genus, sp1.genus) != 0)
                                {
                                    Match mgen = Regex.Match(sp1.genus, sp.genusPattern);
                                    if (mgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.genusTagged), sp1.genusTagged);
                                        found = true;
                                    }
                                }
                            }
                            if (found) replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            //if (found) Console.WriteLine("\n{0}\n{1}\n", m.Value, replace);
                            xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                        }
                    }
                }

                public void UnstableExpand3()
                {
                    Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs");
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match paragraph = Regex.Match(xml, "<p>.*?" + Regex.Escape(m.Value));
                            if (paragraph.Success)
                            {
                                Console.WriteLine("Paragraph content:\n\t{0}\n", Splitter.UnSplitTaxa(paragraph.Value));
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);

                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subgenusSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("paragraph", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match paragraph = Regex.Match(xml, "<tp:treatment-sec[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (paragraph.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("treatment section", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match paragraph = Regex.Match(xml, "<tp:taxon-treatment>[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (paragraph.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("treatment", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
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
                    for (Match m = findLowerTaxa.Match(xml); m.Success; m = m.NextMatch())
                    {
                        Species sp = new Species(m.Value);
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match paragraph = Regex.Match(xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (paragraph.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("section", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
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
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match paragraph = Regex.Match(xml, "<?xml[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (paragraph.Success)
                            {
                                //Console.WriteLine("Paragraph content:\n\t{0}\n", paragraph.Value);
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("preceding text", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
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
                        if (Taxonomy.EmptyGenus(m.Value, sp)) return;
                        // Select only shortened taxa with non-zero species name
                        if ((m.Value.IndexOf('.') > -1) && (String.Compare(sp.species, string.Empty) != 0))
                        {
                            Taxonomy.PrintNextShortened(sp);
                            // Scan all lower-taxon names in the article
                            Match paragraph = Regex.Match(xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                            if (paragraph.Success)
                            {
                                Species last = new Species();
                                bool isFound = false;
                                for (Match taxon = FindLowerTaxa.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                                {
                                    Species sp1 = new Species(taxon.Value);
                                    Match mgen = Regex.Match(sp1.genus, sp.genusSkipPattern); if (mgen.Success) { last.SetGenus(sp1.genus); isFound = true; }
                                    Match msgen = Regex.Match(sp1.subgenus, sp.subspeciesSkipPattern); if (msgen.Success) { last.SetSubgenus(sp1.subgenus); isFound = true; }
                                    Match msp = Regex.Match(sp1.species, sp.speciesSkipPattern); if (msp.Success) { last.SetSpecies(sp1.species); isFound = true; }
                                    Taxonomy.PrintFoundMessage("article", sp1);
                                }
                                if (isFound)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, last);
                                    string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                                    if (String.Compare(last.genus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.genusTagged), last.genusTagged);
                                    }
                                    if (String.Compare(last.subgenus, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.subgenusTagged), last.subgenusTagged);
                                    }
                                    if (String.Compare(last.species, string.Empty) != 0)
                                    {
                                        replace = Regex.Replace(replace, Regex.Escape(sp.speciesTagged), last.speciesTagged);
                                    }
                                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
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
                    //
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
                                xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), replace);
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
    }
}
