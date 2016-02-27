namespace ProcessingTools.Services.Validation.Models
{
    using Contracts;

    public class UrlModel : IUrl
    {
        public string Address { get; set; }

        public string BaseAddress { get; set; }

        public string FullAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.BaseAddress))
                {
                    return this.Address;
                }
                else
                {
                    return this.BaseAddress.TrimEnd(' ', '/') + "/" + this.Address.TrimStart(' ', '/');
                }
            }
        }
    }
}