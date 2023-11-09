using System.Reflection;
using System.Text;

namespace Craft.Blent.Utilities;

/// <summary>
/// Base class for all complex enums.
/// </summary>
/// <typeparam name="T">Type of the complex enum.</typeparam>
public record Enumeration<T> where T : Enumeration<T>
{
    string name;
    string cachedName;

    public Enumeration(string name)
    {
        Name = name;
        ParentEnumeration = default;
    }

    protected Enumeration(T parentEnumeration, string name)
    {
        Name = name;
        ParentEnumeration = parentEnumeration;
    }

    private string BuildName()
    {
        var sb = new StringBuilder();

        if (ParentEnumeration != null)
            sb.Append(ParentEnumeration.Name).Append(' ');

        sb.Append(name);

        return sb.ToString();
    }

    public string Name
    {
        get
        {
            cachedName ??= BuildName();

            return cachedName;
        }
        private set
        {
            if (name == value)
                return;

            name = value;
            cachedName = null;
        }
    }

    public override string ToString()
        => Name;

    public T ParentEnumeration { get; private set; }

    public override int GetHashCode()
        => Name.GetHashCode();

    public static IEnumerable<T> GetAll() =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();
}
