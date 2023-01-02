using System.Reflection;

namespace Test.Domain.Seed;

public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromId<T>(int id) where T : Enumeration
        {
            return GetAll<T>().First(x => x.Id == id);
        }

        public static T FromName<T>(string name) where T : Enumeration
        {
            return GetAll<T>().First(x => x.Name.Equals(name));
        }

        public static bool IsValid<T>(string name) where T : Enumeration
        {
            try
            {
                FromName<T>(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public static bool operator ==(Enumeration? lhs, Enumeration? rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Enumeration lhs, Enumeration rhs) => !(lhs == rhs);

        public int CompareTo(object? other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Id.CompareTo(((Enumeration) other).Id);
        }

        public override int GetHashCode()
        {
            int hashCode = 1460282102;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }
    }