using Craft.Blent.Contracts.ClassProvider;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Contracts.Fluid;

public interface IFluidFlex
{
    string Class(IClassProvider classProvider);
}

public interface IFluidFlexAll :
    IFluidFlex,
    IFluidFlexBreakpoint,
    IFluidFlexDirection,
    IFluidFlexJustifyContent,
    IFluidFlexAlignItems,
    IFluidFlexAlignSelf,
    IFluidFlexAlignContent,
    IFluidFlexWrap,
    IFluidFlexFill,
    IFluidFlexGrowShrink,
    IFluidFlexOrder,
    IFluidFlexCondition
{
}

public interface IFluidFlexBreakpoint : IFluidFlex
{
    IFluidFlexAll OnMobile { get; }
    IFluidFlexAll OnTablet { get; }
    IFluidFlexAll OnDesktop { get; }
    IFluidFlexAll OnWidescreen { get; }
    IFluidFlexAll OnFullHD { get; }
}

public interface IFluidFlexDirection : IFluidFlex
{
    IFluidFlexAll Row { get; }
    IFluidFlexAll ReverseRow { get; }
    IFluidFlexAll Column { get; }
    IFluidFlexAll ReverseColumn { get; }
}

public interface IFluidFlexJustifyContent : IFluidFlex
{
    IFluidFlexJustifyContentPositions JustifyContent { get; }
}

public interface IFluidFlexJustifyContentPositions : IFluidFlex
{
    IFluidFlexAll Start { get; }
    IFluidFlexAll End { get; }
    IFluidFlexAll Center { get; }
    IFluidFlexAll Between { get; }
    IFluidFlexAll Around { get; }
}

public interface IFluidFlexAlignItems : IFluidFlex
{
    IFluidFlexAlignItemsPosition AlignItems { get; }
}

public interface IFluidFlexAlignItemsPosition : IFluidFlex
{
    IFluidFlexAll Start { get; }
    IFluidFlexAll End { get; }
    IFluidFlexAll Center { get; }
    IFluidFlexAll Baseline { get; }
    IFluidFlexAll Stretch { get; }
}

public interface IFluidFlexAlignSelf : IFluidFlex
{
    IFluidFlexAlignSelfPosition AlignSelf { get; }
}

public interface IFluidFlexAlignSelfPosition : IFluidFlex
{
    IFluidFlexAll Auto { get; }
    IFluidFlexAll Start { get; }
    IFluidFlexAll End { get; }
    IFluidFlexAll Center { get; }
    IFluidFlexAll Baseline { get; }
    IFluidFlexAll Stretch { get; }
}

public interface IFluidFlexAlignContent : IFluidFlex
{
    IFluidFlexAlignContentPosition AlignContent { get; }
}

public interface IFluidFlexAlignContentPosition : IFluidFlex
{
    IFluidFlexAll Start { get; }
    IFluidFlexAll End { get; }
    IFluidFlexAll Center { get; }
    IFluidFlexAll Between { get; }
    IFluidFlexAll Around { get; }
    IFluidFlexAll Stretch { get; }
}

public interface IFluidFlexGrowShrink : IFluidFlex
{
    IFluidFlexGrowShrinkSize Grow { get; }
    IFluidFlexGrowShrinkSize Shrink { get; }
}

public interface IFluidFlexGrowShrinkSize : IFluidFlex
{
    IFluidFlexAll Is0 { get; }
    IFluidFlexAll Is1 { get; }
}

public interface IFluidFlexOrder : IFluidFlex
{
    IFluidFlexOrderNumber Order { get; }
}

public interface IFluidFlexOrderNumber : IFluidFlex
{
    IFluidFlexAll Is0 { get; }
    IFluidFlexAll Is1 { get; }
    IFluidFlexAll Is2 { get; }
    IFluidFlexAll Is3 { get; }
    IFluidFlexAll Is4 { get; }
    IFluidFlexAll Is5 { get; }
    IFluidFlexAll Is6 { get; }
    IFluidFlexAll Is7 { get; }
    IFluidFlexAll Is8 { get; }
    IFluidFlexAll Is9 { get; }
    IFluidFlexAll Is10 { get; }
    IFluidFlexAll Is11 { get; }
    IFluidFlexAll Is12 { get; }
}

public interface IFluidFlexWrap : IFluidFlex
{
    IFluidFlexAll Wrap { get; }
    IFluidFlexAll ReverseWrap { get; }
    IFluidFlexAll NoWrap { get; }
}

public interface IFluidFlexFill : IFluidFlex
{
    IFluidFlexAll Fill { get; }
}

public interface IFluidFlexCondition : IFluidFlex
{
    IFluidFlexAll If(bool condition);
}
