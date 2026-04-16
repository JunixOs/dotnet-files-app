namespace Construccion_II___App_API_Rest.Src
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        // Lo que se ve en la funcion Ok() es similar a esto:
        //  public static Result<T> Ok(T data)
        //  {
        //      return new Result<T>
        //      {
        //          Success = true , 
        //          Data = data
        //      };
        //  }
        public static Result<T> Ok(T data) => new Result<T> { Success = true , Data = data};
        public static Result<T> Fail(string message) => new Result<T> { Success = false , Message = message};
    }
}