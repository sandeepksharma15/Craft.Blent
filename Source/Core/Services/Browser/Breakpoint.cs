namespace Craft.Blent.Services.Browser;

public enum Breakpoint
{
    ExtraSmall = 0,
    Xs = ExtraSmall,

    Mobile = 600,
    Sm = Mobile,

    Tablet = 900,
    Md = Tablet,

    Desktop = 1200,
    Lg = Desktop,

    Widescreen = 1600,
    Xl = Widescreen,

    FullHD = 1920,
    Xxl = FullHD,

    SmAndDown,
    MobileAndDown = SmAndDown,

    MdAndDown,
    TabletAndDown = MdAndDown,

    LgAndDown,
    DesktopAndDown = LgAndDown,

    XlAndDown,
    WidescreenAndDown = XlAndDown,

    SmAndUp,
    MobileAndUp = SmAndUp,

    MdAndUp,
    TabletAndUp = MdAndUp,

    LgAndUp,
    DesktopAndUp = LgAndUp,

    XlAndUp,
    WidescreenAndUp = XlAndUp,

    None,
    Always
}
