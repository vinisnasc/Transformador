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
        private readonly IMapper _mapper;

        public ReportService(IReportRepository repository, IMapper mapper, INotificador notificador,
                             ITestRepository testRepository) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task<ReportVM> CriarAsync(ReportDto dto)
        {
            if (!await TestExiste(dto.TestId))
                return null;

            if(_repository.Buscar(x => x.TestId == dto.TestId) != null)
            {
                Notificar("Já existe um relatório para o teste informado!");
                return null;
            }

            var entity = _mapper.Map<Report>(dto);
            if (!ExecutarValidacao(new ReportValidation(), entity)) return null;
            await _repository.Incluir(entity);
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