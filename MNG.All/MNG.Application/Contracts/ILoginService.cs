using MNG.Application.DTOs;
using MNG.Infrastructure.Models;

namespace MNG.Application.Contracts
{
    public interface ILoginService
    {
        ModelResponse<ClientDTO> LoginUser(string name);
    }
}
