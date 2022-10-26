using Transformador.Data.MongoConfiguration;
using Transformador.Domain.Entities;
using Transformador.Domain.Interfaces.Data.Repository;

namespace Transformador.Data.Repository
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}