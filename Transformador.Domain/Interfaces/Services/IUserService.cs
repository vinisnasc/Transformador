using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserVM> BuscarTodos();
        Task<UserVM> BuscarUsuarioasync(string id);
        Task<UserVM> CriarAsync(UserDto user);
        Task<UserVM> AtualizarAsync(string id, UserDto user);
    }
}