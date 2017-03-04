namespace ProcessingTools.Services.Web.Contracts.Factories
{
    using Microsoft.Owin.Security;

    public interface ICertificateValidatorFactory
    {
        ICertificateValidator Create();
    }
}
