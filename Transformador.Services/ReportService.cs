using AutoMapper;
using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;
using Transformador.Domain.Interfaces.Services;
using Transformador.Domain.Validacoes;

namespace Transformador.Services
{
    public class ReportService : BaseService, IReportService
    {
        private readonly IReportRepository _repository;
        private readonly ITestRepository _testRepository;
        private readonly ITransformerRepository _transformerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository repository, IMapper mapper, INotificador notificador,
                             ITestRepository testRepository, ITransformerRepository transformerRepository,
                             IUserRepository userRepository) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _testRepository = testRepository;
            _transformerRepository = transformerRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<ReportVM> BuscarTodos()
        {
            var entities = _repository.SelecionarTudo();
            return _mapper.Map<IEnumerable<ReportVM>>(entities);
        }

        public async Task<ReportVMComplete> BuscarReportAsync(string id)
        {
            var entity = await _repository.SelecionarPorId(id);
            if (entity == null)
            {
                Notificar("Não foi encontrado um relatorio com este id!");
                return null;
            }
            var vm = _mapper.Map<ReportVMComplete>(entity);

            var teste = await _testRepository.SelecionarPorId(entity.TestId);
            vm.Test = _mapper.Map<TestVMComplete>(teste);

            var transformer = await _transformerRepository.SelecionarPorId(teste.TransformerId);
            vm.Test.Transformer = _mapper.Map<TransformerVMComplete>(transformer);
            vm.Test.Transformer.User = _mapper.Map<UserVM>(await _userRepository.SelecionarPorId(transformer.UserId.ToString()));
            return vm;
        }

        public async Task<ReportVM> CriarAsync(ReportDto dto)
        {
            if (!await TestExiste(dto.TestId))
                return null;

            if (_repository.Buscar(x => x.TestId == dto.TestId).Count() != 0)
            {
                Notificar("Já existe um relatório para o teste informado!");
                return null;
            }

            var entity = _mapper.Map<Report>(dto);
            if (!ExecutarValidacao(new ReportValidation(), entity)) return null;
            await _repository.Incluir(entity);
            return _mapper.Map<ReportVM>(entity);
        }

        public async Task<ReportVM> AtualizarAsync(string id, ReportDto dto)
        {
            var original = await _repository.SelecionarPorId(id);

            if (original == null)
            {
                Notificar("Id inválido!");
                return null;
            }

            if (!await TestExiste(dto.TestId))
                return null;

            if(_repository.Buscar(x => x.TestId == dto.TestId).Count() > 0 && _repository.Buscar(x => x.TestId == dto.TestId).Any(x => x == original))
            {
                Notificar("Já existe um relatório para o teste informado!");
                return null;
            }

            var entity = _mapper.Map<Report>(dto);
            entity.Id = new MongoDB.Bson.ObjectId(id);
            if (!ExecutarValidacao(new ReportValidation(), entity)) return null;
            await _repository.Alterar(entity);
            return _mapper.Map<ReportVM>(entity);
        }

        public async Task<ReportVM> DesativarReport(string id)
        {
            var entity = await _repository.SelecionarPorId(id);
            entity.Status = false;
            await _repository.Alterar(entity);
            return _mapper.Map<ReportVM>(entity);
        }

        private async Task<bool> TestExiste(string id)
        {
            if (await _testRepository.SelecionarPorId(id) == null)
            {
                Notificar("Id de Teste não existe!");
                return false;
            }

            return true;
        }
    }
}