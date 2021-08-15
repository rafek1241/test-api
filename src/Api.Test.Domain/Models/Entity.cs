using System;

namespace Api.Test.Domain.Models
{
    public abstract class Entity
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid Id { get; protected set; }
    }
}