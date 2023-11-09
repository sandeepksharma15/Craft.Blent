using Craft.Blent.Contracts;

namespace Craft.Blent.Fluid;

public static class Margin
{
    public static IFluidSpacingOnBreakpointWithSideAndSize Is0 => new FluidMargin().Is0;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is1 => new FluidMargin().Is1;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is2 => new FluidMargin().Is2;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is3 => new FluidMargin().Is3;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is4 => new FluidMargin().Is4;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is5 => new FluidMargin().Is5;
    public static IFluidSpacingOnBreakpointWithSideAndSize IsAuto => new FluidMargin().IsAuto;
    public static IFluidSpacingWithSize Is(string value) => new FluidMargin().Is(value);
}
