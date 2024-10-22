﻿using Microsoft.AspNetCore.Mvc;
using Transformador.Domain.Dtos;
using Transformador.Domain.Interfaces.Services;

namespace Transformador.API.Controllers
{
    [Route("api/[controller]")]
    public class TransformerController : BaseController
    {
        private readonly ITransformerService _service;

        public TransformerController(ITransformerService service, INotificador notificador) : base(notificador)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service)); ;
        }

        /// <summary>
        /// Retorna todos os Transformadores cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarTodos()
        {
            return CustomResponse(_service.BuscarTodos());
        }

        /// <summary>
        /// Retorna todas informações de um transformador, cujo o Id é informado
        /// </summary>
        /// <param name="id">id hexadecimal</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuscarTransformadorAsync(string id)
        {
            if (!ValidarIdHexadecimal(id))
            {
                NotificarErro("Id inválido!");
                return CustomResponse();
            }
            var vm = await _service.BuscarTransformadorAsync(id);
            return CustomResponse(vm);
        }

        /// <summary>
        /// Cadastra um novo transformador no banco de dados
        /// </summary>
        /// <param name="dto">dados do transformador a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarTransformador(TransformerDto dto)
        {
            if (!ValidarIdHexadecimal(dto.UserId))
            {
                NotificarErro("Id de usuário inválido!");
                return CustomResponse();
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _service.CriarAsync(dto);
            return CustomResponse(result);
        }

        /// <summary>
        /// Atualiza as informações de um transformador, conforme o id informado
        /// </summary>
        /// <param name="id">id hexadecimal</param>
        /// <param name="dto">dados atualizados</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarTransformador(string id, TransformerDto dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!ValidarIdHexadecimal(id) || !ValidarIdHexadecimal(dto.UserId))
            {
                NotificarErro("Id de transformador ou de usuário inválido!");
                return CustomResponse();
            }

            var result = await _service.AtualizarAsync(id, dto);
            return CustomResponse(result);
        }
    }
}