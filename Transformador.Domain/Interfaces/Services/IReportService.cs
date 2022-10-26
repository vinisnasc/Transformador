using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;

namespace Transformador.Domain.Interfaces.Services
{
    public interface IReportService
    {
        IEnumerable<ReportVM> BuscarTodos();
        Task<ReportVMComplete> BuscarReportAsync(string id);
        Task<ReportVM> CriarAsync(ReportDto dto);
        Task<ReportVM> AtualizarAsync(string id, ReportDto dto);
        Task<ReportVM> DesativarReport(string id);
        IEnumerable<ReportVM> BuscarApenasAtivos();
    }
}