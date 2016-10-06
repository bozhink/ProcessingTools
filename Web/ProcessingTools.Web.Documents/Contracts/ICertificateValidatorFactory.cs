namespace ProcessingTools.Web.Documents.Contracts
{
    using Microsoft.Owin.Security;

    public interface ICertificateValidatorFactory
    {
        ICertificateValidator Create();
    }
}
