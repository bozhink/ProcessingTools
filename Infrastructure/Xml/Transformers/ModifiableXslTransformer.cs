namespace ProcessingTools.Xml.Transformers
{
    using Contracts.Providers;
    using Contracts.Transformers;

    public class ModifiableXslTransformer : XslTransformer<IModifiableXslTransformProvider>, IModifiableXslTransformer
    {
        public ModifiableXslTransformer(IModifiableXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }

        public string XslFilePath
        {
            get
            {
                return this.XslTransformProvider.XslFilePath;
            }

            set
            {
                this.XslTransformProvider.XslFilePath = value;
            }
        }
    }
}
