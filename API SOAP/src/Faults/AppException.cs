namespace API_SOAP.Src.Exceptions
{
    internal class AppException : Exception
    {
        public bool Success = false;
        public int HttpCode;
        public string? ErrorCode;
        public AppException(string? message , int httpCode , string errorCode) : base(message)
        { 
            HttpCode = httpCode;
            ErrorCode = errorCode; 
        }
    }
}