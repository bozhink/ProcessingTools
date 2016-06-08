namespace ProcessingTools.Web.Documents.Areas.GeoData.ViewModels.Continents
{
    using System;

    public class ContinentSynonymViewModel : ContinentViewModel
    {
        public ContinentSynonymViewModel()
        {
            this.Id = (Guid.NewGuid().ToString() + DateTime.UtcNow.ToLongTimeString()).GetHashCode();
        }

        public int ContinentId { get; set; }
    }
}
