namespace Shoppify.Market.App.Domain.Entites
{
    public abstract class BaseEntity<TKey> : IRootEntity<TKey>, IEquatable<BaseEntity<TKey>> where TKey : struct
    {
        public BaseEntity()
        {

        }

        public TKey Id { get; set; }
        public bool IsDeleted { get; protected set; } = false;
        public bool IsDisabled { get; protected set; } = false;
        public DateTimeOffset CreatedDateUtc { get; protected set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ModifiedDateUtc { get; set; }
        TKey IRootEntity<TKey>.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Equals(BaseEntity<TKey>? other)
        {
            return base.Equals(other);
        }

        protected void Disable()
        {
            if (IsDisabled)
                return;
            IsDisabled = true;
            ModifiedDateUtc = DateTimeOffset.UtcNow;
        }
        protected void Enable()
        {
            if (!IsDisabled)
                return;
            IsDisabled = false;
            ModifiedDateUtc = DateTimeOffset.UtcNow;
        }

        protected void Delete()
        {
            if (IsDeleted)
                return;
            IsDeleted = true;
            ModifiedDateUtc = DateTimeOffset.UtcNow;
        }
        protected void UnDelete()
        {
            if (!IsDeleted)
                return;
            IsDeleted = false;
            ModifiedDateUtc = DateTimeOffset.UtcNow;
        }
    }

    public interface IRootEntity
    {

    }

    public interface IRootEntity<TKey> : IRootEntity
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
