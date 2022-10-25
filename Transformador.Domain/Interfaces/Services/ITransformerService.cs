using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface ITransformerService
    {
        IEnumerable<TransformerVM> BuscarTodos();
        Task<TransformerVMComplete> BuscarTransformadorasync(string id);
        Task<TransformerVM> CriarAsync(TransformerDto dto);
        Task<TransformerVM> AtualizarAsync(string id, TransformerDto dto);
    }
}