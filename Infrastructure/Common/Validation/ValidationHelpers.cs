namespace ProcessingTools.Common.Validation
{
    using Constants;
    using Exceptions;

    public static class ValidationHelpers
    {
        public static void ValidateId(object id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }
        }

        public static void ValidatePageNumber(int pageNumber)
        {
            if (pageNumber < 0)
            {
                throw new InvalidPageNumberException();
            }
        }

        public static void ValidateNumberOfItemsPerPage(int numberOfItemsPerPAge)
        {
            if (1 > numberOfItemsPerPAge || numberOfItemsPerPAge > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }
        }
    }
}
