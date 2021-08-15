using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Test.Domain.Models;

namespace Api.Test.Domain
{
    public interface IRepository<T> where T : Entity
    {
        Task<IList<T>> Get(CancellationToken token = default);
        Task<T> Get(Guid id, CancellationToken token = default);
        Task<Guid> Post(T entity, CancellationToken token = default);
    }
}