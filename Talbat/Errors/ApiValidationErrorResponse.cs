namespace Talbat.Errors
{
    public class ApiValidationErrorResponse : ResponsiApi
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        
        }
    }
}
