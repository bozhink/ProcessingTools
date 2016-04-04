namespace ProcessingTools.Documents.Services.Data.Models
{
    using System;

    public class CountryServiceModel
    {
        public CountryServiceModel()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int Id { get; set; }

        public string ModifiedByUserId { get; set; }

        public string Name { get; set; }
    }
}
