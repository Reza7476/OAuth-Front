using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Exceptions;
using OAuth_Front.Interfaces;

namespace OAuth_Front.Application.Entities.Users.Contracts;

public interface IUserService : IService
{
    Task<(List<GetAllUserDto> Users, CustomException Error)> GetAll();
}
