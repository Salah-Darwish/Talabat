namespace Talbat.Errors
{
    public class ResponsiApi
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ResponsiApi(int statuscode,string? message=null)
        {
       StatusCode = statuscode ;
          Message = message ?? ForDefaultMessageForStatusCode(statuscode); 
        }

        private string? ForDefaultMessageForStatusCode(int statuscode)
        {
          return statuscode switch
          {
              400 => "A bad request, you have made", 
              401 =>"Authorized, you are not", 
              404=>"Resource was not found", 
              500 =>"Errors are the path to the dark side .Errors lead to anger.Anger leads to hate.Hate leads to career change ", 
              _=>null 
               
          };
        }
    }
}
