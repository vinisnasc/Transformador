using AutoMapper;
using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;
using Transformador.Domain.Interfaces.Services;
using Transformador.Domain.Validacoes;

namespace Transformador.Services
{
    public class TestService : BaseService, ITestService
    {
        private readonly ITestRepository _repository;
        private readonly ITransformerRepository _transformerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TestService(ITestRepository repository, IMapper mapper, INotificador notificador,
                           ITransformerRepository transformerRepository, IUserRepository userRepository) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _transformerRepository = transformerRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<TestVM> BuscarTodos()
        {
            var entities = _repository.SelecionarTudo();
            return _mapper.Map<IEnumerable<TestVM>>(entities);
        }

        public async Task<TestVMComplete> BuscarTestAsync(string id)
        {
            var entity = await _repository.SelecionarPorId(id);
            if (entity == null)
            {
                Notificar("Não foi encontrado um teste com este id!");
                return null;
            }
            var vm = _mapper.Map<TestVMComplete>(entity);

            var transf = await _transformerRepository.SelecionarPorId(entity.TransformerId);

            vm.Transformer = _mapper.Map<TransformerVMComplete>(transf);
            vm.Transformer.User = _mapper.Map<UserVM>(await _userRepository.SelecionarPorId(transf.UserId.ToString()));
            return vm;
        }

        public async Task<TestVM> CriarAsync(TestDto dto)
        {
            if (await TransformadorExiste(dto.TransformerId))
                return null;

            var entity = _mapper.Map<Test>(dto);
            if (!ExecutarValidacao(new TestValidation(), entity)) return null;
            await _repository.Incluir(entity);
            return _mapper.Map<TestVM>(entity);
        }

        public async Task<TestVM> AtualizarAsync(string id, TestDto dto)
        {
            if (await BuscarTestAsync(id) == null)
            {
                Notificar("Id inválido!");
                return null;
            }

            if (await TransformadorExiste(dto.TransformerId))
                return null;

            var entity = _mapper.Map<Test>(dto);
            entity.Id = new MongoDB.Bson.ObjectId(id);
            if (!ExecutarValidacao(new TestValidation(), entity)) return null;
            await _repository.Alterar(entity);
            return _mapper.Map<TestVM>(entity);
        }

        private async Task<bool> TransformadorExiste(string id)
        {
            if (await _transformerRepository.SelecionarPorId(id) == null)
            {
                Notificar("Id de Transformador não existe!");
                return false;
            }

            return true;
        }
    }
}