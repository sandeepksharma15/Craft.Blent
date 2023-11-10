using System.Text;
using Craft.Blent.Extensions;

namespace Craft.Blent.Utilities;

public class StyleBuilder(Action<StyleBuilder> buildStyles)
{
    const char Delimiter = ';';

    private readonly Action<StyleBuilder> buildStyles = buildStyles;
    private readonly StringBuilder builder = new();

    private string styles;
    private bool dirty = true;

    public void Append(string value)
    {
        if (value is not null)
            builder.Append(value).Append(Delimiter);
    }

    public void Append(string value, bool condition)
    {
        if (value is not null && condition)
            builder.Append(value).Append(Delimiter);
    }

    public void Dirty()
        => dirty = true;

    public string Styles
    {
        get
        {
            if (dirty)
            {
                builder.Clear();
                buildStyles(this);
                styles = builder.ToString().TrimEnd(' ', Delimiter)?.EmptyToNull();
                dirty = false;
            }

            return styles;
        }
    }
}
