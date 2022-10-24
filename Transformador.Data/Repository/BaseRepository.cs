using MongoDB.Bson;
using MongoDB.Driver;
using Transformador.Data.MongoConfiguration;
using Transformador.Domain.Entities;
using Transformador.Domain.Entities.MongoExtension;
using Transformador.Domain.Interfaces.Data.Repository;

namespace Transformador.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected IMongoCollection<T> _dbCollection;

        public BaseRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _dbCollection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public async Task<T> SelecionarPorId(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            return await _dbCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task Alterar(T entity)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, entity.Id);
            await _dbCollection.FindOneAndReplaceAsync(filter, entity);
        }

        public async Task Incluir(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).Name + " object is null");
            }

            await _dbCollection.InsertOneAsync(entity);
        }

        public IQueryable<T> SelecionarTudo()
        {
            return _dbCollection.AsQueryable();
        }
    }
}