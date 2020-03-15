using MNG.API.Models;
using MNG.Infrastructure.Models;

namespace MNG.API.Code.Contracts
{
    public interface ITokenManager
    {
        ModelResponse<JWTResult> GetJWT(User userModel);
    }
}