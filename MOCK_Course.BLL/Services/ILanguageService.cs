using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface ILanguageService
    {
        Task<Responses<LanguageResponse>> GetAll();
    }
}
