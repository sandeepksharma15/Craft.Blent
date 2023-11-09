using Craft.Blent.Enums;

namespace Craft.Blent.Fluid;

/// <summary>
/// Holds the build information for current sizing rules.
/// </summary>
public record SizingDefinition
{
    /// <summary>
    /// Size will be defined for the min attribute(s) of the element style.
    /// </summary>
    public bool IsMin { get; set; }

    /// <summary>
    /// Size will be defined for the max attribute(s) of the element style.
    /// </summary>
    public bool IsMax { get; set; }

    /// <summary>
    /// Size will be defined for the viewport.
    /// </summary>
    public bool IsViewport { get; set; }

    /// <summary>
    /// Defines the media breakpoint where the sizing rule will be applied.
    /// </summary>
    public Breakpoint Breakpoint { get; set; }
}
