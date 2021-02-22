using System.Threading.Tasks;
using UserMicroservice.Domain.Models;

namespace UserMicroservice.Domain.Services
{
    public interface IUserService : IDataService<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByName(string name);
    }
}