using System.Threading.Tasks;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IPresidentialService
    {
        [OperationContract]
        Task<string> PresidentName(int id);

        [OperationContract]
        Task<string> Presidents();
    }
}
