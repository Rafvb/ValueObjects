namespace ValueObjects.Common
{
    /// <summary>
    /// Variant of https://enterprisecraftsmanship.com/posts/value-object-better-implementation/.
    /// Can also be found in the https://github.com/vkhorikov/CSharpFunctionalExtensions package.
    /// </summary>
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return GetType() == obj.GetType() && GetEqualityMembers().SequenceEqual(((ValueObject)obj).GetEqualityMembers());
        }

        public override int GetHashCode()
        {
            return GetEqualityMembers().Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return (current * 23) + (obj?.GetHashCode() ?? 0);
                }
            });
        }

        protected abstract IEnumerable<object> GetEqualityMembers();
    }
}