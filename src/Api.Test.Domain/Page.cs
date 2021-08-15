using System.Collections.Generic;
using Api.Test.Domain.Models;

namespace Api.Test.Domain
{
    public class Page<T> where T : Entity
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }

        public Page(
            int take,
            int skip,
            int total,
            IEnumerable<T> items
        )
        {
            Take = take;
            Skip = skip;
            Total = total;
            Items = items;
        }
    }
}