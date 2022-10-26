using Transformador.Domain.Entities.MongoExtension;

namespace Transformador.Domain.Entities
{
    [BsonCollection("Tests")]
    public class Test : BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int DurationInSeconds { get; set; }
        public string TransformerId { get; set; }
    }
}