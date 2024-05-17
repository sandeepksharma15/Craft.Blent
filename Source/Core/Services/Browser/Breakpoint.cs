namespace Craft.Blent.Services.Browser;

public enum Breakpoint
{
    ExtraSmall,
    Xs = ExtraSmall,

    Mobile,
    Sm = Mobile,

    Tablet,
    Md = Tablet,

    Desktop,
    Lg = Desktop,

    Widescreen,
    Xl = Widescreen,

    FullHD,
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
