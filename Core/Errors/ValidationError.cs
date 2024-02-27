namespace Core.Errors
{
    public class ValidationError
    {
        /// <summary>
        /// Validation error
        /// </summary>
        public string Error { get; set; }

        public ValidationError(string error)
        {
            Error = error;
        }
    }
}
