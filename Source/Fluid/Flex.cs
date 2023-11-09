using Craft.Blent.Contracts;
using Craft.Blent.Enums;

namespace Craft.Blent.Fluid;

public static class Flex
{
    public static IFluidFlexAll _ => new FluentFlex().WithFlexType(FlexType.Flex);

    public static IFluidFlexAll InlineFlex => new FluentFlex().WithFlexType(FlexType.InlineFlex);
    public static IFluidFlexAll Row => new FluentFlex().WithFlexType(FlexType.Flex).Row;
    public static IFluidFlexAll ReverseRow => new FluentFlex().WithFlexType(FlexType.Flex).ReverseRow;
    public static IFluidFlexAll Column => new FluentFlex().WithFlexType(FlexType.Flex).Column;
    public static IFluidFlexAll ReverseColumn => new FluentFlex().WithFlexType(FlexType.Flex).ReverseColumn;

    public static IFluidFlexJustifyContentPositions JustifyContent => new FluentFlex().WithFlexType(FlexType.Flex).JustifyContent;

    public static IFluidFlexAlignItemsPosition AlignItems => new FluentFlex().WithFlexType(FlexType.Flex).AlignItems;

    public static IFluidFlexAlignSelfPosition AlignSelf => new FluentFlex().WithAlignSelf();

    public static IFluidFlexAlignContentPosition AlignContent => new FluentFlex().WithFlexType(FlexType.Flex).AlignContent;

    public static IFluidFlexGrowShrinkSize Grow => new FluentFlex().WithGrowShrink(FlexGrowShrink.Grow);
    public static IFluidFlexGrowShrinkSize Shrink => new FluentFlex().WithGrowShrink(FlexGrowShrink.Shrink);

    public static IFluidFlexOrderNumber Order => new FluentFlex().WithOrder();

    public static IFluidFlexAll Wrap => new FluentFlex().WithFlexType(FlexType.Flex).Wrap;
    public static IFluidFlexAll ReverseWrap => new FluentFlex().WithFlexType(FlexType.Flex).ReverseWrap;
    public static IFluidFlexAll NoWrap => new FluentFlex().WithFlexType(FlexType.Flex).NoWrap;
    public static IFluidFlexAll Fill => new FluentFlex().WithFill();
}
