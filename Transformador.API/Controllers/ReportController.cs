using Microsoft.AspNetCore.Mvc;
using Transformador.Domain.Dtos;
using Transformador.Domain.Interfaces.Services;

namespace Transformador.API.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : BaseController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service, INotificador notificador) : base(notificador)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service)); ;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodos()
        {
            return CustomResponse(_service.BuscarTodos());
        }

        [HttpGet("buscar-ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodosAtivos()
        {
            return CustomResponse(_service.BuscarApenasAtivos());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuscarReportPorId(string id)
        {
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id inválido!");
                return CustomResponse();
            }
            var vm = await _service.BuscarReportAsync(id);
            return CustomResponse(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarReport(ReportDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _service.CriarAsync(dto);
            return CustomResponse(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarReport(string id, ReportDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!ValidarIdHexadecimal(id) || !ValidarIdHexadecimal(dto.TestId))
            {
                NotificarErro("Id de relatório ou de teste inválido!");
                return CustomResponse();
            }

            var result = await _service.AtualizarAsync(id, dto);
            return CustomResponse(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DesativarReport(string id)
        {
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id de relatório ou de teste inválido!");
                return CustomResponse();
            }

            var result = await _service.DesativarReport(id);
            return CustomResponse(result);
        }
    }
}