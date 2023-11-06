namespace Talbat.Errors
{
    public class ApiExcaptionResponse :ResponsiApi
    {
        public string? Details { get; set; }
        public ApiExcaptionResponse
            (int statusCode, string? massege = null, string? details = null) :
            base(statusCode, massege)
        {
            Details = details;
        }
    }
}
