using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Test.Domain.Models;

namespace Api.Test.Domain
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Get(Guid id, CancellationToken token = default);
        Task<Guid> Post(T entity, CancellationToken token = default);

        Task<Page<T>> Get(
            int skip,
            int take,
            CancellationToken token = default
        );
    }
}