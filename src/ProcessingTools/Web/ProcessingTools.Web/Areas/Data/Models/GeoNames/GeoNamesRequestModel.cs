namespace ProcessingTools.Web.Areas.Data.Models.GeoNames
{
    using System;
    using System.Linq;
    using ProcessingTools.Contracts.Models.Geo;

    public class GeoNamesRequestModel
    {
        public string Names { get; set; }

        public IGeoName[] ToArray()
        {
            if (string.IsNullOrWhiteSpace(this.Names))
            {
                return new IGeoName[] { };
            }

            return this.Names.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new GeoNameRequestModel
                {
                    Name = x.Trim(new[] { '\r' })
                })
                .ToArray();
        }
    }
}
