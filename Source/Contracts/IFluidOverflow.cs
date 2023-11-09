namespace Craft.Blent.Contracts;

public interface IFluidOverflow
{
    string Class(IClassProvider classProvider);
}

public interface IFluidOverflowSecondRule : IFluidOverflow
{
    IFluidOverflow Visible { get; }
    IFluidOverflow Hidden { get; }
    IFluidOverflow Scroll { get; }
    IFluidOverflow Auto { get; }
}
