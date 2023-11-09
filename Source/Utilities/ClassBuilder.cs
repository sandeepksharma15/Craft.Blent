using System.Text;
using Craft.Blent.Extensions;

namespace Craft.Blent.Utilities;

public class ClassBuilder(Action<ClassBuilder> buildClasses)
{
    private const char Delimiter = ' ';
    private readonly Action<ClassBuilder> buildClasses = buildClasses;
    private readonly StringBuilder builder = new();
    private string classNames;
    private bool dirty = true;

    /// <summary>
    /// Appends a copy of the specified string to this instance.
    /// </summary>
    /// <param name="value">The string to append.</param>
    public void Append(string value)
    {
        if (value == null)
            return;

        builder.Append(value).Append(Delimiter);
    }

    /// <summary>
    /// Appends a copy of the specified string to this instance if <paramref name="condition"/> is true.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <param name="condition">Condition that must be true.</param>
    public void Append(string value, bool condition)
    {
        if (condition && value != null)
            builder.Append(value).Append(Delimiter);
    }

    /// <summary>
    /// Appends a copy of the specified list of strings to this instance.
    /// </summary>
    /// <param name="values">The list of strings to append.</param>
    public void Append(IEnumerable<string> values)
        => builder
            .AppendJoin(Delimiter, values)
            .Append(Delimiter);

    public void Dirty()
        => dirty = true;

    /// <summary>
    /// Gets the class-names.
    /// </summary>
    public string Class
    {
        get
        {
            if (dirty)
            {
                builder.Clear();
                buildClasses(this);
                classNames = builder.ToString().TrimEnd()?.EmptyToNull();
                dirty = false;
            }

            return classNames;
        }
    }
}
