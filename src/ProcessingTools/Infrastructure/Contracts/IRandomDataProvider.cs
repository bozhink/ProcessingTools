namespace ProcessingTools.Contracts
{
    public interface IRandomDataProvider
    {
        int GetRandomNumber(int min, int max);

        string GetRandomString(int length);

        string GetRandomString(int minLength, int maxLength);

        string GetRandomText(params string[] phrases);
    }
}
