namespace ProcessingTools.Common.Providers.Contracts
{
    public interface IRandomProvider
    {
        int GetRandomNumber(int min, int max);

        string GetRandomString(int length);

        string GetRandomString(int minLength, int maxLength);

        string GetRandomText(params string[] phrases);
    }
}