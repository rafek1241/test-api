using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Test.Domain;
using Api.Test.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Test.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly CustomerContext _context;
        private DbSet<T> entities;

        public Repository(CustomerContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public async Task<Page<T>> Get(
            int skip,
            int take,
            CancellationToken token = default
        )
        {
            using (var tran = await _context.Database.BeginTransactionAsync(token))
            {
                var total = entities.Count();
                var items = await entities
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                var page = new Page<T>(take, skip, total, items);

                await tran.CommitAsync();
                return page;
            }
        }

        public Task<T> Get(Guid id, CancellationToken token = default) => entities.SingleOrDefaultAsync(x => x.Id == id, token);

        public async Task<Guid> Post(T entity, CancellationToken token = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(Entity).Name);
            }

            await entities.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
            return entity.Id;
        }
    }
}