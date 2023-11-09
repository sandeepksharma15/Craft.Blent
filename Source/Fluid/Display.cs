using Craft.Blent.Contracts;

namespace Craft.Blent.Fluid;

public static class Display
{
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection Always { get { return new FluidDisplay().Always; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection None { get { return new FluidDisplay().None; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection Block { get { return new FluidDisplay().Block; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection Inline { get { return new FluidDisplay().Inline; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection InlineBlock { get { return new FluidDisplay().InlineBlock; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection Table { get { return new FluidDisplay().Table; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection TableRow { get { return new FluidDisplay().TableRow; } }
    public static IFluidDisplayWithDisplayOnBreakpointWithDirection TableCell { get { return new FluidDisplay().TableCell; } }
    public static IFluidDisplayWithDisplayFlexWithDirection Flex { get { return new FluidDisplay().Flex; } }
    public static IFluidDisplayWithDisplayFlexWithDirection InlineFlex { get { return new FluidDisplay().InlineFlex; } }
}
