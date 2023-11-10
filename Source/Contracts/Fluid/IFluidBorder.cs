using Craft.Blent.Contracts.ClassProvider;

namespace Craft.Blent.Contracts.Fluid;

public interface IFluidBorder
{
    string Class(IClassProvider classProvider);
}

public interface IFluidBorderSize : IFluidBorder
{
    IFluidBorderWithAll Is0 { get; }
    IFluidBorderWithAll Is1 { get; }
    IFluidBorderWithAll Is2 { get; }
    IFluidBorderWithAll Is3 { get; }
    IFluidBorderWithAll Is4 { get; }
    IFluidBorderWithAll Is5 { get; }
}

public interface IFluidBorderSide : IFluidBorder
{
    IFluidBorderWithAll OnTop { get; }
    IFluidBorderWithAll OnEnd { get; }
    IFluidBorderWithAll OnBottom { get; }
    IFluidBorderWithAll OnStart { get; }
    IFluidBorderWithAll OnAll { get; }
}

public interface IFluidBorderColor : IFluidBorder
{
    IFluidBorderColorWithSide Primary { get; }
    IFluidBorderColorWithSide Secondary { get; }
    IFluidBorderColorWithSide Success { get; }
    IFluidBorderColorWithSide Danger { get; }
    IFluidBorderColorWithSide Warning { get; }
    IFluidBorderColorWithSide Info { get; }
    IFluidBorderColorWithSide Light { get; }
    IFluidBorderColorWithSide Dark { get; }
    IFluidBorderColorWithSide White { get; }
}

public interface IFluidBorderRadius : IFluidBorder
{
    IFluidBorderWithAll Rounded { get; }
    IFluidBorderWithAll RoundedTop { get; }
    IFluidBorderWithAll RoundedEnd { get; }
    IFluidBorderWithAll RoundedBottom { get; }
    IFluidBorderWithAll RoundedStart { get; }
    IFluidBorderWithAll RoundedCircle { get; }
    IFluidBorderWithAll RoundedPill { get; }
    IFluidBorderWithAll RoundedZero { get; }
}

public interface IFluidBorderWithSizeAndSide :
    IFluidBorder,
    IFluidBorderSize,
    IFluidBorderSide
{
}

public interface IFluidBorderColorWithSide :
    IFluidBorder,
    IFluidBorderSide
{
}

public interface IFluidBorderWithAll :
    IFluidBorder,
    IFluidBorderWithSizeAndSide,
    IFluidBorderColor,
    IFluidBorderColorWithSide,
    IFluidBorderRadius
{
}
