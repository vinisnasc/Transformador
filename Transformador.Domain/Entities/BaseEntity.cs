using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Transformador.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;

        public DateTime UpdatedAt => DateTime.Now;
    }
}