using Microsoft.AspNetCore.Mvc;

namespace Construccion_II___App_API_Rest.Src
{
    [ApiController]
    [Route("/")]
    public class HomeController
    {
        [HttpGet(Name = "WelcomeEndpoint")]
        public string Welcome()
        {
            DateTime NowDate = System.DateTime.Now;
            return $"Request Time: {NowDate.ToString()}";
        }
    }
}
