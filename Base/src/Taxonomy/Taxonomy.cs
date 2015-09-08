namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;

    public enum TaxaType
    {
        Lower,
        Higher,
        Any
    }

    public static class Taxonomy
    {
        /*
         * Messages
         */

        public static bool EmptyGenus(string source, Species sp)
        {
            if (string.Compare(sp.GenusName, string.Empty) == 0)
            {
                Console.WriteLine("\nERROR: Empty genus name found!:\n{0}\n", source);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void PrintFoundMessage(string where, Species sp)
        {
            Console.WriteLine("....... Found in {0}: {1}", where, sp.SpeciesNameAsString);
        }

        public static void PrintMethodMessage(string name)
        {
            Console.WriteLine("\n\n#\n##\n### {0} will be executed...\n##\n#\n", name);
        }

        public static void PrintNextShortened(Species sp)
        {
            Console.WriteLine("\nNext shortened taxon:\t{0}", sp.SpeciesNameAsString);
        }

        public static void PrintSubstitutionMessage(Species original, Species substitution)
        {
            Console.WriteLine("\tSubstitution:\t{0}\t-->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameAsString);
        }

        public static void PrintSubstitutionMessage1(Species original, Species substitution)
        {
            Console.WriteLine("\tSubstitution:\t{0}\t-->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameGenusSubgenusAsString);
        }

        public static void PrintSubstitutionMessageFail(Species original, Species substitution)
        {
            Console.WriteLine("\tFailed Subst:\t{0}\t<->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameAsString);
        }
    }
}