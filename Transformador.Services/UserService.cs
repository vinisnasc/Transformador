using AutoMapper;
using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;
using Transformador.Domain.Interfaces.Services;
using Transformador.Domain.Validacoes;

namespace Transformador.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<UserVM> BuscarTodos()
        {
            var entities = _repository.SelecionarTudo();
            return _mapper.Map<IEnumerable<UserVM>>(entities);
        }

        public async Task<UserVM> BuscarUsuarioasync(string id)
        {
            var entity = await _repository.SelecionarPorId(id);
            if (entity == null)
            {
                Notificar("Não foi encontrado um usuário com este id!");
                return null;
            }
            return _mapper.Map<UserVM>(entity);
        }

        public async Task<UserVM> CriarAsync(UserDto user)
        {
            var entity = _mapper.Map<User>(user);
            if (!ExecutarValidacao(new UserValidation(), entity)) return null;
            await _repository.Incluir(entity);
            return _mapper.Map<UserVM>(entity);
        }

        public async Task<UserVM> AtualizarAsync(string id, UserDto user)
        {
            var entity = _mapper.Map<User>(user);
            entity.Id = new MongoDB.Bson.ObjectId(id);
            if (!ExecutarValidacao(new UserValidation(), entity)) return null;
            await _repository.Alterar(entity);
            return _mapper.Map<UserVM>(entity);
        }
    }
}