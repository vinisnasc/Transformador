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
        private readonly ITestRepository _testRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public TransformerService(ITransformerRepository repository, IMapper mapper, INotificador notificador,
                                  IUserRepository userRepository, ITestRepository testRepository, 
                                  IReportRepository reportRepository) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _testRepository = testRepository;
            _reportRepository = reportRepository;
        }

        public IEnumerable<TransformerVM> BuscarTodos()
        {
            var entities = _repository.SelecionarTudo();
            return _mapper.Map<IEnumerable<TransformerVM>>(entities);
        }

        public async Task<TransformerVMComplete> BuscarTransformadorAsync(string id)
        {
            var entity = await _repository.SelecionarPorId(id);
            if (entity == null)
            {
                Notificar("Não foi encontrado um transformador com este id!");
                return null;
            }
            var vm = _mapper.Map<TransformerVMComplete>(entity);
            vm.User = _mapper.Map<UserVM>(await _userRepository.SelecionarPorId(entity.UserId));

            vm.Testes = _mapper.Map<List<TestVMComplete>>(_testRepository.Buscar(x => x.TransformerId == id && x.Status == true).ToList());

            foreach (var item in vm.Testes)
            {
                var teste = _reportRepository.Buscar(x => x.TestId == item.Id && x.Status == true);
                if (teste.FirstOrDefault() != null)
                    item.Report = _mapper.Map<ReportVM>(teste.FirstOrDefault());
            }

            return vm;
        }

        public async Task<TransformerVM> CriarAsync(TransformerDto dto)
        {
            if (await _userRepository.SelecionarPorId(dto.UserId) == null)
            {
                Notificar("Id de usuário não existe!");
                return null;
            }

            var entity = _mapper.Map<Transformer>(dto);
            if (!ExecutarValidacao(new TransformerValidation(), entity)) return null;
            await _repository.Incluir(entity);
            return _mapper.Map<TransformerVM>(entity);
        }

        public async Task<TransformerVM> AtualizarAsync(string id, TransformerDto dto)
        {
            var original = await _repository.SelecionarPorId(id);
            if (original == null)
            {
                Notificar("Id inválido!");
                return null;
            }

            if (!await UserExiste(dto.UserId))
                return null;

            var entity = _mapper.Map<Transformer>(dto);
            entity.Id = new MongoDB.Bson.ObjectId(id);
            if (!ExecutarValidacao(new TransformerValidation(), entity)) return null;
            await _repository.Alterar(entity);
            return _mapper.Map<TransformerVM>(entity);
        }

        private async Task<bool> UserExiste(string id)
        {
            if (await _userRepository.SelecionarPorId(id) == null)
            {
                Notificar("Id de Usuario não existe!");
                return false;
            }

            return true;
        }
    }
}