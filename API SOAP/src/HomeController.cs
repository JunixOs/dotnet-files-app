using API_SOAP.Controllers.Interfaces;

namespace API_SOAP.Controllers
{
    public class HomeController : IHomeController
    {
        public string Index(string name)
        {
            return $"Hello {name}";
        }
    }
}