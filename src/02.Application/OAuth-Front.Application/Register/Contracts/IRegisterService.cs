using OAuth_Front.Application.Register.Contracts.Dtos;
using OAuth_Front.Interfaces;

namespace OAuth_Front.Application.Register.Contracts;

public interface IRegisterService : IService
{
    Task<string> LogIn(LogInDto dto);
}
