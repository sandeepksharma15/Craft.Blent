using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Contracts;

public interface IFluidSpacing
{
    string Class(IClassProvider classProvider);
}

public interface IFluidSpacingOnBreakpointWithSide :
    IFluidSpacing,
    IFluidSpacingOnBreakpoint
{
}

public interface IFluidSpacingOnBreakpointWithSideAndSize :
    IFluidSpacing,
    IFluidSpacingOnBreakpoint,
    IFluidSpacingWithSize
{
}

public interface IFluidSpacingFromSide : IFluidSpacing
{
    IFluidSpacingOnBreakpointWithSideAndSize FromTop { get; }
    IFluidSpacingOnBreakpointWithSideAndSize FromBottom { get; }
    IFluidSpacingOnBreakpointWithSideAndSize FromStart { get; }
    IFluidSpacingOnBreakpointWithSideAndSize FromEnd { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnX { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnY { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnAll { get; }
}

/// <summary>
/// Allowed breakpoints for spacing rules.
/// </summary>
public interface IFluidSpacingOnBreakpoint :
    IFluidSpacing,
    IFluidSpacingFromSide
{
    IFluidSpacingOnBreakpointWithSideAndSize OnMobile { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnTablet { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnDesktop { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnWidescreen { get; }
    IFluidSpacingOnBreakpointWithSideAndSize OnFullHD { get; }
}

/// <summary>
/// Allowed sizes for spacing rules.
/// </summary>
public interface IFluidSpacingWithSize : IFluidSpacing
{
    IFluidSpacingOnBreakpointWithSideAndSize Is0 { get; }
    IFluidSpacingOnBreakpointWithSideAndSize Is1 { get; }
    IFluidSpacingOnBreakpointWithSideAndSize Is2 { get; }
    IFluidSpacingOnBreakpointWithSideAndSize Is3 { get; }
    IFluidSpacingOnBreakpointWithSideAndSize Is4 { get; }
    IFluidSpacingOnBreakpointWithSideAndSize Is5 { get; }
    IFluidSpacingOnBreakpointWithSideAndSize IsAuto { get; }
    IFluidSpacingWithSize Is(string value);
}
