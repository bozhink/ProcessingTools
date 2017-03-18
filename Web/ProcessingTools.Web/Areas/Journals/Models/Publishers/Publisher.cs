namespace ProcessingTools.Web.Areas.Journals.Models.Publishers
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;

    public class Publisher : IPublisher, IServiceModel
    {
        public string AbbreviatedName { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
