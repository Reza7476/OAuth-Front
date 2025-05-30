using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Dtos;
using OAuth_Front.Interfaces;

namespace OAuth_Presentation.Services.Users;

public interface IUserService : IService
{
    Task<ApiResultDto<List<GetAllUserDto>>> GetAll(string? token);
}
