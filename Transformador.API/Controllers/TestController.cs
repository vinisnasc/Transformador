using Microsoft.AspNetCore.Mvc;
using Transformador.Domain.Dtos;
using Transformador.Domain.Interfaces.Services;

namespace Transformador.API.Controllers
{
    [Route("api/[controller]")]
    public class TestController : BaseController
    {
        private readonly ITestService _service;

        public TestController(ITestService service, INotificador notificador) : base(notificador)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service)); ;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodos()
        {
            return CustomResponse(_service.BuscarTodos());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuscarTestePorId(string id)
        {
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id inválido!");
                return CustomResponse();
            }
            var vm = await _service.BuscarTestAsync(id);
            return CustomResponse(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarTeste(TestDto dto)
        {
            if (!ValidarIdHexadecimal(dto.TransformerId))
            {
                NotificarErro("Id de transformador inválido!");
                return CustomResponse();
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _service.CriarAsync(dto);
            return CustomResponse(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarTest(string id, TestDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!ValidarIdHexadecimal(id) || !ValidarIdHexadecimal(dto.TransformerId))
            {
                NotificarErro("Id de transformador ou de teste inválido!");
                return CustomResponse();
            }

            var result = await _service.AtualizarAsync(id, dto);
            return CustomResponse(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DesativarTest(string id)
        {
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id de teste inválido!");
                return CustomResponse();
            }

            var result = await _service.DesativarTest(id);
            return CustomResponse(result);
        }
    }
}