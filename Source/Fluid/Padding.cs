using Craft.Blent.Contracts;

namespace Craft.Blent.Fluid;

public static class Padding
{
    public static IFluidSpacingOnBreakpointWithSideAndSize Is0 => new FluidPadding().Is0;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is1 => new FluidPadding().Is1;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is2 => new FluidPadding().Is2;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is3 => new FluidPadding().Is3;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is4 => new FluidPadding().Is4;
    public static IFluidSpacingOnBreakpointWithSideAndSize Is5 => new FluidPadding().Is5;
    public static IFluidSpacingOnBreakpointWithSideAndSize IsAuto => new FluidPadding().IsAuto;
    public static IFluidSpacingWithSize Is(string value) => new FluidPadding().Is(value);
}
