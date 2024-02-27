using Core.Errors;

namespace Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> ValidationErrors { get; }

        public ValidationException(string message, IEnumerable<ValidationError> validationErrors) : base(message)
        {
            this.ValidationErrors = validationErrors;
        }      
    }
}
