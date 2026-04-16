using System.ServiceModel;
using CoreWCF;

// Definir el contrato es importante
// ya que define el WSDL automaticamente
namespace API_SOAP.Controllers.Interfaces
{

    [ServiceContract]
    public interface IHomeController
    {
        [OperationContract]
        string Index(string name);
    }
}