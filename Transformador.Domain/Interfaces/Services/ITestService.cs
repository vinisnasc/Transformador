using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface ITestService
    {
        IEnumerable<TestVM> BuscarTodos();
        IEnumerable<TestVM> BuscarApenasAtivos();
        Task<TestVMComplete> BuscarTestAsync(string id);
        Task<TestVM> CriarAsync(TestDto dto);
        Task<TestVM> AtualizarAsync(string id, TestDto dto);
        Task<TestVM> DesativarTest(string id);
    }
}