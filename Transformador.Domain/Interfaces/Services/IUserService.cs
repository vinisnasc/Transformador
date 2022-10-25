using Transformador.Domain.Dtos;
using Transformador.Domain.Entities;

namespace Transformador.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> CriarAsync(UserDto user);
    }
}