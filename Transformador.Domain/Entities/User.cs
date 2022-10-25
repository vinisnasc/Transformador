using Transformador.Domain.Entities.MongoExtension;

namespace Transformador.Domain.Entities
{
    [BsonCollection("Users")]
    public class User : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}