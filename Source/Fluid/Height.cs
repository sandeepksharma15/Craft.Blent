using Craft.Blent.Contracts;
using Craft.Blent.Enums;

namespace Craft.Blent.Fluid;

/// <summary>
/// Set of height sizing rules to start the build process.
/// </summary>
public static class Height
{
    public static IFluidSizingMinMaxViewportOnBreakpoint Is25
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is25);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is33
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is33);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is50
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is50);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is66
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is66);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is75
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is75);

    public static IFluidSizingMinMaxViewportOnBreakpoint Is100
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is100);

    public static IFluidSizingMinMaxViewportOnBreakpoint Auto
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Auto);

    public static IFluidSizingWithSizeOnBreakpoint Max100
        => new FluidSizing(SizingType.Height).WithSize(SizingSize.Is100).Max;
}
