namespace Craft.Blent.Contracts;

public interface IFluidDisplay
{
    string Class(IClassProvider classProvider);
}

public interface IFluidDisplayWithDisplayOnBreakpointWithDirection :
    IFluidDisplay,
    IFluidDisplayOnBreakpoint,
    IFluidDisplayWithDisplay,
    IFluidDisplayOnCondition
{
}

public interface IFluidDisplayOnBreakpoint : IFluidDisplay
{
    IFluidDisplayWithDisplay OnMobile { get; }
    IFluidDisplayWithDisplay OnTablet { get; }
    IFluidDisplayWithDisplay OnDesktop { get; }
    IFluidDisplayWithDisplay OnWidescreen { get; }
    IFluidDisplayWithDisplay OnFullHD { get; }
}

public interface IFluidDisplayWithDisplayFlexWithDirection : IFluidDisplay
{
    IFluidDisplayWithDisplayOnBreakpointWithDirection Row { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection ReverseRow { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection Column { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection ReverseColumn { get; }
}

public interface IFluidDisplayWithDisplay : IFluidDisplay
{
    IFluidDisplayWithDisplayOnBreakpointWithDirection None { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection Block { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection Inline { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection InlineBlock { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection Table { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection TableRow { get; }
    IFluidDisplayWithDisplayOnBreakpointWithDirection TableCell { get; }
    IFluidDisplayWithDisplayFlexWithDirection Flex { get; }
    IFluidDisplayWithDisplayFlexWithDirection InlineFlex { get; }
}

public interface IFluidDisplayOnCondition : IFluidDisplay
{
    IFluidDisplayWithDisplayOnBreakpointWithDirection If(bool condition);
}
