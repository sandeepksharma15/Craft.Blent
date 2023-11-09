using Craft.Blent.Contracts;

namespace Craft.Blent.Fluid;

public static class Position
{
    public static IFluidPositionWithEdgeTypeAndTranslateType Static => new FluidPosition().Static;
    public static IFluidPositionWithEdgeTypeAndTranslateType Relative => new FluidPosition().Relative;
    public static IFluidPositionWithEdgeTypeAndTranslateType Absolute => new FluidPosition().Absolute;
    public static IFluidPositionWithEdgeTypeAndTranslateType Fixed => new FluidPosition().Fixed;
    public static IFluidPositionWithEdgeTypeAndTranslateType Sticky => new FluidPosition().Sticky;
}
