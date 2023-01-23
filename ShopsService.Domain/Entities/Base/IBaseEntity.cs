using System;

namespace ShopsService.Domain.Entities.Base
{
    public interface IBaseEntity
    {
    }

    public interface IBaseEntity<out TKey> : IBaseEntity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }

        public bool IsDeleted { get; set; }
    }
}
