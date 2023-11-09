using Craft.Blent.Contracts;
using Craft.Blent.Enums;

namespace Craft.Blent.Fluid;

/// <summary>
/// Set of width sizing rules to start the build process.
/// </summary>
public static class Width
{
    public static IFluidSizingMinMaxViewportOnBreakpoint Is25
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is25);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is33
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is33);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is50
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is50);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is66
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is66);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is75
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is75);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is100
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is100);

    public static IFluidSizingMinMaxViewportOnBreakpoint Auto
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Auto);

    public static IFluidSizingWithSizeOnBreakpoint Max100
        => new FluidSizing(SizingType.Width).WithSize(SizingSize.Is100).Max;
}
