using Craft.Blent.Contracts;

namespace Craft.Blent.Fluid;

public static class Gap
{
    public static IFluidGapWithSideAndSize Is0 => new FluidGap().Is0;
    public static IFluidGapWithSideAndSize Is1 => new FluidGap().Is1;
    public static IFluidGapWithSideAndSize Is2 => new FluidGap().Is2;
    public static IFluidGapWithSideAndSize Is3 => new FluidGap().Is3;
    public static IFluidGapWithSideAndSize Is4 => new FluidGap().Is4;
    public static IFluidGapWithSideAndSize Is5 => new FluidGap().Is5;
    public static IFluidGapWithSize Is(string value) => new FluidGap().Is(value);
}
