using Microsoft.AspNetCore.Mvc;
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
    }
}