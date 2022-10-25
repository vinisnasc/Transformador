using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserVM> CriarAsync(UserDto user);
        IEnumerable<UserVM> BuscarTodosAsync();
    }
}