using System;

namespace Api.Test.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}