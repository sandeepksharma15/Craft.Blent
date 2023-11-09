using Craft.Blent.Utilities;

namespace Craft.Blent.Enums;

public record TextColor : Enumeration<TextColor>
{
    public TextColor(string name) : base(name) { }

    public static implicit operator TextColor(string name)
        => new(name);

    public static readonly TextColor Default = new(null);
    public static readonly TextColor Primary = new("primary");
    public static readonly TextColor Secondary = new("secondary");
    public static readonly TextColor Success = new("success");
    public static readonly TextColor Danger = new("danger");
    public static readonly TextColor Warning = new("warning");
    public static readonly TextColor Info = new("info");
    public static readonly TextColor Light = new("light");
    public static readonly TextColor Dark = new("dark");
    public static readonly TextColor Body = new("body");
    public static readonly TextColor Muted = new("muted");
    public static readonly TextColor White = new("white");
    public static readonly TextColor Black50 = new("black-50");
    public static readonly TextColor White50 = new("white-50");
}
