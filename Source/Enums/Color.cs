using Craft.Blent.Utilities;

namespace Craft.Blent.Enums;

public record Color : Enumeration<Color>
{
    public Color(string name) : base(name) { }

    public static implicit operator Color(string name)
        => new(name);

    public static readonly Color Default = new(null);
    public static readonly Color Primary = new("primary");
    public static readonly Color Secondary = new("secondary");
    public static readonly Color Success = new("success");
    public static readonly Color Danger = new("danger");
    public static readonly Color Warning = new("warning");
    public static readonly Color Info = new("info");
    public static readonly Color Light = new("light");
    public static readonly Color Dark = new("dark");
    public static readonly Color Link = new("link");
}
