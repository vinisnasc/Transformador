using Microsoft.AspNetCore.Mvc;
using Transformador.Domain.Dtos;
using Transformador.Domain.Interfaces.Services;

namespace Transformador.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service, INotificador notificador) : base(notificador)
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
        public async Task<IActionResult> BuscarUsuarioAsync(string id)
        {
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id inválido!");
                return CustomResponse();
            }
            var vm = await _service.BuscarUsuarioasync(id);
            return CustomResponse(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarUsuario(UserDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _service.CriarAsync(dto);
            return CustomResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarUsuario(string id, UserDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id inválido!");
                return CustomResponse();
            }

            var result = await _service.AtualizarAsync(id, dto);
            return CustomResponse(result);
        }
    }
}