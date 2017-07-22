namespace ProcessingTools.Web.Areas.Data.Models.GeoEpithets
{
    using System;
    using System.Linq;
    using ProcessingTools.Contracts.Models.Geo;

    public class GeoEpithetsRequestModel
    {
        public string Names { get; set; }

        public IGeoEpithet[] ToArray()
        {
            if (string.IsNullOrWhiteSpace(this.Names))
            {
                return new IGeoEpithet[] { };
            }

            return this.Names.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new GeoEpithetRequestModel
                {
                    Name = x.Trim(new[] { '\r' })
                })
                .ToArray();
        }
    }
}
