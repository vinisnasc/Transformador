using AutoMapper;
using Transformador.Domain.Interfaces.Data.Repository;
using Transformador.Domain.Interfaces.Services;

namespace Transformador.Services
{
    public class TransformerService : BaseService, ITransformerService
    {
        private readonly ITransformerRepository _repository;
        private readonly IMapper _mapper;

        public TransformerService(ITransformerRepository repository, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}