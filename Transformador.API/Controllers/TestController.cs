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

        /// <summary>
        /// Retorna todos testes do banco de dados, inclusive os inativos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodos()
        {
            return CustomResponse(_service.BuscarTodos());
        }

        /// <summary>
        /// Retorna todos os testes que estão atualmente ativos no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodosAtivos()
        {
            return CustomResponse(_service.BuscarApenasAtivos());
        }

        /// <summary>
        /// Retorna um teste de acordo com seu id, mostrando seus dados completos, inclusive relatorio, usuario e transformador
        /// </summary>
        /// <param name="id">id hexadecimal com 12 digitos</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Cria um novo teste
        /// </summary>
        /// <param name="dto">dados do teste a ser criado</param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza os dados do teste que está sendo passado conforme seu id
        /// </summary>
        /// <param name="id">id hexadecimal com 12 digitos</param>
        /// <param name="dto">dados a serem sobrescritos</param>
        /// <returns></returns>
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

        /// <summary>
        /// Desativa o teste conforme seu id, desativa também os relatorios ligados a ele
        /// </summary>
        /// <param name="id">id hexadecimal com 12 digitos</param>
        /// <returns></returns>
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