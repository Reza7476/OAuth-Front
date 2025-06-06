using OAuth_Presentation.Configurations.Interfaces;
using OAuth_Presentation.Models.Auth;
using OAuth_Presentation.Models.Commons;

namespace OAuth_Presentation.Services.Authentication;

public interface IAuthService : IService
{
    Task<ApiResultDto<LoginResponse>> LogIn(LogInDto dto);
    Task<ApiResultDto<LoginResponse>> LogInWithGoogle(LoginWithGoogleDto dto);
}
