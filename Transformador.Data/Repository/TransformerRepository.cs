using Transformador.Data.MongoConfiguration;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;

namespace Transformador.Data.Repository
{
    public class TransformerRepository : BaseRepository<Transformer>, ITransformerRepository
    {
        public TransformerRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}