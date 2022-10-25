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
            return CustomResponse(_service.BuscarTodosAsync());
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
    }
}