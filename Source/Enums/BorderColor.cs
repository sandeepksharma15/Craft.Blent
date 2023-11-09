namespace Craft.Blent.Enums;

public readonly record struct BorderColor
{
    public string Name { get; }

    public BorderColor(string name)
        => Name = name;

    public static readonly BorderColor None = new(null);
    public static readonly BorderColor Primary = new("primary");
    public static readonly BorderColor Secondary = new("secondary");
    public static readonly BorderColor Success = new("success");
    public static readonly BorderColor Danger = new("danger");
    public static readonly BorderColor Warning = new("warning");
    public static readonly BorderColor Info = new("info");
    public static readonly BorderColor Light = new("light");
    public static readonly BorderColor Dark = new("dark");
    public static readonly BorderColor White = new("white");
}
