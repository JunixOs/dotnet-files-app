namespace Mobile_App___dotNET_MAUI.Models
{
    internal class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}