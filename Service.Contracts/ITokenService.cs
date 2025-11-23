using Entities.Models;

namespace Service.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
