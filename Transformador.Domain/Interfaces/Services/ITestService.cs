using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface ITestService
    {
        Task<TestVM> CriarAsync(TestDto dto);
        IEnumerable<TestVM> BuscarTodos();
        Task<TestVMComplete> BuscarTestAsync(string id);
    }
}