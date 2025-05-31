using OAuth_Presentation.Configurations.Interfaces;
using OAuth_Presentation.Models.Commons;
using OAuth_Presentation.Models.Users;

namespace OAuth_Presentation.Services.Users;

public interface IUserService : IService
{
    Task<ApiResultDto<List<GetUserDto>>> GetAll(string? token);
}
