using Craft.Blent.Contracts;
using Craft.Blent.Contracts.Fluid;

namespace Craft.Blent.Fluid;

public static class Border
{
    public static IFluidBorderWithAll Is0 => new FluidBorder().Is0;
    public static IFluidBorderWithAll Is1 => new FluidBorder().Is1;
    public static IFluidBorderWithAll Is2 => new FluidBorder().Is2;
    public static IFluidBorderWithAll Is3 => new FluidBorder().Is3;
    public static IFluidBorderWithAll Is4 => new FluidBorder().Is4;
    public static IFluidBorderWithAll Is5 => new FluidBorder().Is5;

    public static IFluidBorderWithAll OnTop => new FluidBorder().Is1.OnTop;
    public static IFluidBorderWithAll OnEnd => new FluidBorder().Is1.OnEnd;
    public static IFluidBorderWithAll OnBottom => new FluidBorder().Is1.OnBottom;
    public static IFluidBorderWithAll OnStart => new FluidBorder().Is1.OnStart;
    public static IFluidBorderColorWithSide Primary => new FluidBorder().Is1.Primary;
    public static IFluidBorderColorWithSide Secondary => new FluidBorder().Is1.Secondary;
    public static IFluidBorderColorWithSide Success => new FluidBorder().Is1.Success;
    public static IFluidBorderColorWithSide Danger => new FluidBorder().Is1.Danger;
    public static IFluidBorderColorWithSide Warning => new FluidBorder().Is1.Warning;
    public static IFluidBorderColorWithSide Info => new FluidBorder().Is1.Info;
    public static IFluidBorderColorWithSide Light => new FluidBorder().Is1.Light;
    public static IFluidBorderColorWithSide Dark => new FluidBorder().Is1.Dark;
    public static IFluidBorderColorWithSide White => new FluidBorder().Is1.White;

    public static IFluidBorderWithAll Rounded => new FluidBorder().Rounded;
    public static IFluidBorderWithAll RoundedTop => new FluidBorder().RoundedTop;
    public static IFluidBorderWithAll RoundedEnd => new FluidBorder().RoundedEnd;
    public static IFluidBorderWithAll RoundedBottom => new FluidBorder().RoundedBottom;
    public static IFluidBorderWithAll RoundedStart => new FluidBorder().RoundedStart;
    public static IFluidBorderWithAll RoundedCircle => new FluidBorder().RoundedCircle;
    public static IFluidBorderWithAll RoundedPill => new FluidBorder().RoundedPill;
    public static IFluidBorderWithAll RoundedZero => new FluidBorder().RoundedZero;
}
