namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using Contracts;

    public static class Taxonomy
    {
        public static bool EmptyGenus(string source, Species sp, ILogger logger)
        {
            if (string.Compare(sp.GenusName, string.Empty) == 0)
            {
                logger?.Log("\nERROR: Empty genus name found!:\n{0}\n", source);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void PrintFoundMessage(string where, Species sp, ILogger logger)
        {
            logger?.Log("....... Found in {0}: {1}", where, sp.SpeciesNameAsString);
        }

        public static void PrintMethodMessage(string name, ILogger logger)
        {
            logger?.Log("\n\n#\n##\n### {0} will be executed...\n##\n#\n", name);
        }

        public static void PrintNextShortened(Species sp, ILogger logger)
        {
            logger?.Log("\nNext shortened taxon:\t{0}", sp.SpeciesNameAsString);
        }

        public static void PrintSubstitutionMessage(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tSubstitution:\t{0}\t-->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameAsString);
        }

        public static void PrintSubstitutionMessage1(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tSubstitution:\t{0}\t-->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameGenusSubgenusAsString);
        }

        public static void PrintSubstitutionMessageFail(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tFailed Subst:\t{0}\t<->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameAsString);
        }
    }
}