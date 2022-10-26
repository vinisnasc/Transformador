using Transformador.Data.MongoConfiguration;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;

namespace Transformador.Data.Repository
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}