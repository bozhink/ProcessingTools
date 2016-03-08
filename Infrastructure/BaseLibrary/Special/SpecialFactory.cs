namespace ProcessingTools.BaseLibrary.Special
{
    using ProcessingTools.Configurator;

    public abstract class SpecialFactory : ConfigurableDocument
    {
        public SpecialFactory(Config config, string xml)
            : base(config, xml)
        {
        }
    }
}