using Craft.Blent.Contracts.Fluid;
using Craft.Blent.Enums;

namespace Craft.Blent.Fluid;

public static class Overflow
{
    public static IFluidOverflowSecondRule Visible => new FluidOverflow().WithOverflow(OverflowType.Visible);
    public static IFluidOverflowSecondRule Hidden => new FluidOverflow().WithOverflow(OverflowType.Hidden);
    public static IFluidOverflowSecondRule Scroll => new FluidOverflow().WithOverflow(OverflowType.Scroll);
    public static IFluidOverflowSecondRule Auto => new FluidOverflow().WithOverflow(OverflowType.Auto);
}
