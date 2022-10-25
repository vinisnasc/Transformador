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
        private readonly IMapper _mapper;

        public TestService(ITestRepository repository, IMapper mapper, INotificador notificador, 
                           ITransformerRepository transformerRepository) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _transformerRepository = transformerRepository;
        }

        public IEnumerable<TestVM> BuscarTodos()
        {
            var entities = _repository.SelecionarTudo();
            return _mapper.Map<IEnumerable<TestVM>>(entities);
        }

        public async Task<TestVM> CriarAsync(TestDto dto)
        {
            if (await _transformerRepository.SelecionarPorId(dto.TransformerId) == null)
            {
                Notificar("Id de Transformador não existe!");
                return null;
            }

            var entity = _mapper.Map<Test>(dto);
            if (!ExecutarValidacao(new TestValidation(), entity)) return null;
            await _repository.Incluir(entity);
            return _mapper.Map<TestVM>(entity);
        }
    }
}