using Core.Errors;

namespace Core.Features.Abums.CreateAlbum
{
    public static class CreateAlbumCommandValidator
    {

        public static IEnumerable<ValidationError> Validate(this CreateAlbumCommand command)
        {
            var validationErrors = new List<ValidationError>();
            if(String.IsNullOrWhiteSpace(command.ArtistName))
            {
                validationErrors.Add(new ValidationError("Invalid ArtistName."));
            }
            if (String.IsNullOrWhiteSpace(command.Title))
            {
                validationErrors.Add(new ValidationError("Invalid Title."));
            }
            if (command.Stock < 0)
            {
                validationErrors.Add(new ValidationError("Invalid Stock value."));
            }

            return validationErrors;
        }
    }
}
