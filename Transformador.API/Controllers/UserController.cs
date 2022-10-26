using Microsoft.AspNetCore.Mvc;
using Transformador.Domain.Dtos;
using Transformador.Domain.Interfaces.Services;

namespace Transformador.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service, INotificador notificador) : base(notificador)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service)); ;
        }

        /// <summary>
        /// Retorna todos usuários cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodos()
        {
            return CustomResponse(_service.BuscarTodos());
        }

        /// <summary>
        /// Retorna informações completas do usuário cujo id foi informado
        /// </summary>
        /// <param name="id">id hexadecimal</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adiciona um novo usuário no banco de dados
        /// </summary>
        /// <param name="dto">Dados do usuário a ser informado</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarUsuario(UserDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _service.CriarAsync(dto);
            return CustomResponse(result);
        }

        /// <summary>
        /// Atualiza os dados de um usuário já existente, com base no id e dados informados
        /// </summary>
        /// <param name="id">id hexadecimal</param>
        /// <param name="dto">dados atualizados</param>
        /// <returns></returns>
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