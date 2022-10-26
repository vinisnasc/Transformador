using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface IReportService
    {
        IEnumerable<ReportVM> BuscarTodos();
        Task<ReportVM> CriarAsync(ReportDto dto);
    }
}