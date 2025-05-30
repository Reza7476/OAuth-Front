using OAuth_Front.Application.Register.Contracts.Dtos;
using OAuth_Front.Dtos;
using OAuth_Front.Interfaces;
using OAuth_Presentation.Models.Auth;

namespace OAuth_Presentation.Services.Authentication;

public interface IAuthService : IService
{
    Task<ApiResultDto<LoginResponse>> LogIn(LogInDto dto);
}
