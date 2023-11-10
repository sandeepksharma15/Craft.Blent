using Craft.Blent.Contracts.ClassProvider;

namespace Craft.Blent.Contracts.Fluid;

public interface IFluidGap
{
    string Class(IClassProvider classProvider);
}

public interface IFluidGapWithSide :
    IFluidGap,
    IFluidGapFromSide
{
}

public interface IFluidGapWithSideAndSize :
    IFluidGap,
    IFluidGapFromSide,
    IFluidGapWithSize
{
}

public interface IFluidGapFromSide : IFluidGap
{
    IFluidGapWithSideAndSize OnX { get; }
    IFluidGapWithSideAndSize OnY { get; }
    IFluidGapWithSideAndSize OnAll { get; }
}

public interface IFluidGapWithSize : IFluidGap
{
    IFluidGapWithSideAndSize Is0 { get; }
    IFluidGapWithSideAndSize Is1 { get; }
    IFluidGapWithSideAndSize Is2 { get; }
    IFluidGapWithSideAndSize Is3 { get; }
    IFluidGapWithSideAndSize Is4 { get; }
    IFluidGapWithSideAndSize Is5 { get; }
    IFluidGapWithSize Is(string value);
}
