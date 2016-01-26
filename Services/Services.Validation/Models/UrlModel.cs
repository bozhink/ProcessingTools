namespace ProcessingTools.Services.Validation.Models
{
    using Contracts;

    public class UrlModel : IUrl
    {
        public string Address { get; set; }

        public string BaseAddress { get; set; }
    }
}
