using AutoMapper;
using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;
using Transformador.Domain.Interfaces.Services;
using Transformador.Domain.Validacoes;

namespace Transformador.Services
{
    public class TransformerService : BaseService, ITransformerService
    {
        private readonly ITransformerRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TransformerService(ITransformerRepository repository, IMapper mapper, INotificador notificador, IUserRepository userRepository) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public IEnumerable<TransformerVM> BuscarTodos()
        {
            var entities = _repository.SelecionarTudo();
            return _mapper.Map<IEnumerable<TransformerVM>>(entities);
        }

        public async Task<TransformerVMComplete> BuscarTransformadorasync(string id)
        {
            var entity = await _repository.SelecionarPorId(id);
            if (entity == null)
            {
                Notificar("Não foi encontrado um transformador com este id!");
                return null;
            }
            var vm = _mapper.Map<TransformerVMComplete>(entity);
            vm.User = _mapper.Map<UserVM>(await _userRepository.SelecionarPorId(entity.UserId));
            return vm;
        }

        public async Task<TransformerVM> CriarAsync(TransformerDto dto)
        {
            if(await _userRepository.SelecionarPorId(dto.UserId) == null)
            {
                Notificar("Id de usuário não existe!");
                return null;
            }

            var entity = _mapper.Map<Transformer>(dto);
            if (!ExecutarValidacao(new TransformerValidation(), entity)) return null;
            await _repository.Incluir(entity);
            return _mapper.Map<TransformerVM>(entity);
        }
    }
}