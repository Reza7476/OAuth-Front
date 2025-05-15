using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;

namespace OAuth_Front.Application.Entities.Users;

public class UserAppService : IUserService
{
    private readonly IUserRepository _repository;

    public Task<List<GetAllUserDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}
