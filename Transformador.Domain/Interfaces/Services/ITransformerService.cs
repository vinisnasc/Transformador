using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface ITransformerService
    {
        Task<TransformerVM> CriarAsync(TransformerDto dto);
    }
}