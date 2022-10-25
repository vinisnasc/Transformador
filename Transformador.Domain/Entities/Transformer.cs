using Transformador.Domain.Entities.MongoExtension;

namespace Transformador.Domain.Entities
{
    [BsonCollection("Transformers")]
    public class Transformer : BaseEntity
    {
        public string Name { get; set; }
        public int InternalNumber { get; set; }
        public double TensionClass { get; set; }
        public double Potency { get; set; }
        public double Current { get; set; }
        public string UserId { get; set; }
    }
}