using Core.Errors;

namespace Core.Features.Abums.GetAllAlbums
{
    public static class GetAllAlbumsValidator
    {
        public static IEnumerable<ValidationError> Validate(this GetAllAlbumsQuery query)
        {
            var validationErrors = new List<ValidationError>();
            if (query.PageSize > Constants.DefaultPagination.MaxPageSize || query.PageSize <= 0)
            {
                validationErrors.Add(new ValidationError("Invalid page size."));
            }
            if (query.Page <= 0)
            {
                validationErrors.Add(new ValidationError("Invalid page."));
            }

            return validationErrors;
        }

    }
}
