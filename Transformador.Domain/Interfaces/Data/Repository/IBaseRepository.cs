using System.Linq.Expressions;
using Transformador.Domain.Entities;

namespace Transformador.Domain.Interfaces.Data.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Incluir(T entity);
        Task Alterar(T entity);
        Task<T> SelecionarPorId(string id);
        IQueryable<T> SelecionarTudo();
        List<T> Buscar(Expression<Func<T, bool>> predicate);
    }
}