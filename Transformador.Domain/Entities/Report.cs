using Transformador.Domain.Entities.MongoExtension;

namespace Transformador.Domain.Entities
{
    [BsonCollection("Reports")]
    public class Report : BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public string TestId { get; set; }
    }
}