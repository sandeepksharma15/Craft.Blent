namespace Craft.Blent.Contracts;

public interface IFluidSizing
{
    /// <summary>
    /// Builds the classnames based on sizing rules.
    /// </summary>
    /// <param name="classProvider">Currently used class provider.</param>
    /// <returns>List of classnames for the given rules and the class provider.</returns>
    string Class(IClassProvider classProvider);
}

/// <summary>
/// Contains all the allowed sizing rules
/// </summary>
public interface IFluidSizingWithSizeWithMinMaxWithViewportAll :
    IFluidSizing,
    IFluidSizingSize,
    IFluidSizingMinMaxViewport,
    IFluidSizingViewport
{
}

/// <summary>
/// Contains sizing rules for size and breakpoint
/// </summary>
public interface IFluidSizingWithSizeOnBreakpoint :
    IFluidSizing,
    IFluidSizingOnBreakpoint
{
}

/// <summary>
/// Allowed breakpoints for sizing rules.
/// </summary>
public interface IFluidSizingOnBreakpoint :
    IFluidSizing,
    IFluidSizingSize,
    IFluidSizingMinMaxViewport,
    IFluidSizingViewport
{
    IFluidSizingWithSizeWithMinMaxWithViewportAll OnMobile { get; }
    IFluidSizingWithSizeWithMinMaxWithViewportAll OnTablet { get; }
    IFluidSizingWithSizeWithMinMaxWithViewportAll OnDesktop { get; }
    IFluidSizingWithSizeWithMinMaxWithViewportAll OnWidescreen { get; }
    IFluidSizingWithSizeWithMinMaxWithViewportAll OnFullHD { get; }
}

/// <summary>
/// Allowed rules for sizing rule sizes.
/// </summary>
public interface IFluidSizingSize : IFluidSizing
{
    IFluidSizingOnBreakpoint Is25 { get; }
    IFluidSizingOnBreakpoint Is33 { get; }
    IFluidSizingOnBreakpoint Is50 { get; }
    IFluidSizingOnBreakpoint Is66 { get; }
    IFluidSizingOnBreakpoint Is75 { get; }
    IFluidSizingMinMaxViewportOnBreakpoint Is100 { get; }
    IFluidSizing Auto { get; }
}

/// <summary>
/// Contains the min, max and viewport rules.
/// </summary>
public interface IFluidSizingMinMaxViewport :
    IFluidSizing,
    IFluidSizingMin,
    IFluidSizingMax,
    IFluidSizingViewport
{
}

/// <summary>
/// Contains the min, max and viewport rules.
/// </summary>
public interface IFluidSizingMinMaxViewportOnBreakpoint :
    IFluidSizing,
    IFluidSizingOnBreakpoint
{
}

public interface IFluidSizingMin : IFluidSizing
{
    IFluidSizingViewport Min { get; }
}

public interface IFluidSizingViewport : IFluidSizing
{
    IFluidSizing Viewport { get; }
}

public interface IFluidSizingMax : IFluidSizing
{
    IFluidSizingWithSizeOnBreakpoint Max { get; }
}
