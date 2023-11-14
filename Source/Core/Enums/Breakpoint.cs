using System.ComponentModel;

namespace Craft.Blent.Enums;
public enum Breakpoint
{
    [Description("xs")]
    None = 0,

    [Description("sm")]
    Mobile = 600,

    [Description("md")]
    Tablet = 900,

    [Description("lg")]
    Desktop = 1200,

    [Description("xl")]
    Widescreen = 1600,

    [Description("xxl")]
    FullHD = 1920
}
