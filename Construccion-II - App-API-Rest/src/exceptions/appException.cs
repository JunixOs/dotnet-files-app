namespace Construccion_II___App_API_Rest.Src.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string? _message , string? errorCode , int httpCode) : base(_message) {
            Success = false;
            ErrorCode = errorCode;
            HttpCode = httpCode;
        }

        public bool Success { get; set; }
        public string? ErrorCode { get; set; }
        public int HttpCode { get; set; }
    }
}