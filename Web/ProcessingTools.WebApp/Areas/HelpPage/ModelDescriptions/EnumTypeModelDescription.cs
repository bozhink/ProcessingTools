namespace ProcessingTools.WebApp.Areas.HelpPage.ModelDescriptions
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            this.Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}