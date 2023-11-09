using Craft.Blent.Contracts;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Fluid;

public record FlexDefinition
{
    public static readonly FlexDefinition Empty = new();

    public Breakpoint Breakpoint { get; set; }
    public FlexDirection Direction { get; set; }
    public FlexJustifyContent JustifyContent { get; set; }
    public FlexAlignItems AlignItems { get; set; }
    public FlexAlignSelf AlignSelf { get; set; }
    public FlexAlignContent AlignContent { get; set; }
    public FlexGrowShrink GrowShrink { get; set; }
    public FlexGrowShrinkSize GrowShrinkSize { get; set; }
    public FlexWrap Wrap { get; set; }
    public FlexOrder Order { get; set; }

    public bool Fill { get; set; }

    public bool? Condition { get; set; }
}

public class FluentFlex :
    IFluidFlex,
    IFluidFlexJustifyContentPositions,
    IFluidFlexAlignItems,
    IFluidFlexAlignItemsPosition,
    IFluidFlexAlignSelf,
    IFluidFlexAlignSelfPosition,
    IFluidFlexAlignContent,
    IFluidFlexAlignContentPosition,
    IFluidFlexWrap,
    IFluidFlexFill,
    IFluidFlexGrowShrink,
    IFluidFlexGrowShrinkSize,
    IFluidFlexOrder,
    IFluidFlexOrderNumber,
    IFluidFlexCondition,
    IFluidFlexAll
{
    private FlexType currentFlexType;
    private FlexDefinition currentFlexDefinition;
    private Dictionary<FlexType, List<FlexDefinition>> rules;

    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            void BuildClasses(ClassBuilder builder)
            {
                if (rules?.Count > 0)
                    builder.Append(rules.Select(r => classProvider.Flex(r.Key, r.Value.Where(x => x.Condition ?? true).Select(v => v))));
                else if (currentFlexDefinition != null && currentFlexDefinition != FlexDefinition.Empty && (currentFlexDefinition.Condition ?? true))
                    builder.Append(classProvider.Flex(currentFlexDefinition));
                else if (currentFlexType != FlexType.Default)
                    builder.Append(classProvider.Flex(currentFlexType));
            }

            var classBuilder = new ClassBuilder(BuildClasses);
            classNames = classBuilder.Class;
            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    public IFluidFlexAll WithFlexType(FlexType flexType)
    {
        currentFlexType = flexType;
        Dirty();

        return this;
    }

    private FlexDefinition GetDefinition()
    {
        if (currentFlexDefinition == null)
            currentFlexDefinition = CreateDefinition();

        return currentFlexDefinition;
    }

    private FlexDefinition CreateDefinition()
    {
        rules ??= [];

        var flexDefinition = new FlexDefinition();

        if (rules.TryGetValue(currentFlexType, out var rule))
            rule.Add(flexDefinition);
        else
            rules.Add(currentFlexType, [flexDefinition]);

        return flexDefinition;
    }

    public IFluidFlexAll WithBreakpoint(Breakpoint breakpoint)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.Breakpoint = breakpoint;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithDirection(FlexDirection direction)
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.Direction = direction;
        Dirty();

        return this;
    }

    public IFluidFlexJustifyContentPositions WithJustifyContent()
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.JustifyContent = FlexJustifyContent.Default;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithJustifyContent(FlexJustifyContent justifyContent)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.JustifyContent = justifyContent;
        Dirty();

        return this;
    }

    public IFluidFlexAlignItemsPosition WithAlignItems()
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.AlignItems = FlexAlignItems.Default;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithAlignItems(FlexAlignItems alignItems)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.AlignItems = alignItems;
        Dirty();

        return this;
    }

    public IFluidFlexAlignSelfPosition WithAlignSelf()
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.AlignSelf = FlexAlignSelf.Default;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithAlignSelf(FlexAlignSelf alignSelf)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.AlignSelf = alignSelf;
        Dirty();

        return this;
    }

    public IFluidFlexAlignContentPosition WithAlignContent()
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.AlignContent = FlexAlignContent.Default;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithAlignContent(FlexAlignContent alignContent)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.AlignContent = alignContent;
        Dirty();

        return this;
    }

    public IFluidFlexGrowShrinkSize WithGrowShrink(FlexGrowShrink growShrink)
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.GrowShrink = growShrink;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithGrowShrinkSize(FlexGrowShrinkSize growShrinkSize)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.GrowShrinkSize = growShrinkSize;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithWrap(FlexWrap wrap)
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.Wrap = wrap;
        Dirty();

        return this;
    }

    public IFluidFlexOrderNumber WithOrder()
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.Order = FlexOrder.Default;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithOrder(FlexOrder order)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.Order = order;
        Dirty();

        return this;
    }

    public IFluidFlexAll WithFill()
    {
        currentFlexDefinition = CreateDefinition();
        currentFlexDefinition.Fill = true;
        Dirty();

        return this;
    }

    public IFluidFlexAll If(bool condition)
    {
        currentFlexDefinition = GetDefinition();
        currentFlexDefinition.Condition = condition;
        Dirty();

        return this;
    }

    public IFluidFlexAll OnMobile => WithBreakpoint(Breakpoint.Mobile);
    public IFluidFlexAll OnTablet => WithBreakpoint(Breakpoint.Tablet);
    public IFluidFlexAll OnDesktop => WithBreakpoint(Breakpoint.Desktop);
    public IFluidFlexAll OnWidescreen => WithBreakpoint(Breakpoint.Widescreen);
    public IFluidFlexAll OnFullHD => WithBreakpoint(Breakpoint.FullHD);

    public IFluidFlexAll Row => WithDirection(FlexDirection.Row);
    public IFluidFlexAll ReverseRow => WithDirection(FlexDirection.ReverseRow);
    public IFluidFlexAll Column => WithDirection(FlexDirection.Column);
    public IFluidFlexAll ReverseColumn => WithDirection(FlexDirection.ReverseColumn);

    public IFluidFlexAll Wrap => WithWrap(FlexWrap.Wrap);
    public IFluidFlexAll ReverseWrap => WithWrap(FlexWrap.ReverseWrap);
    public IFluidFlexAll NoWrap => WithWrap(FlexWrap.NoWrap);

    public IFluidFlexJustifyContentPositions JustifyContent => WithJustifyContent();
    IFluidFlexAll IFluidFlexJustifyContentPositions.Start => WithJustifyContent(FlexJustifyContent.Start);
    IFluidFlexAll IFluidFlexJustifyContentPositions.End => WithJustifyContent(FlexJustifyContent.End);
    IFluidFlexAll IFluidFlexJustifyContentPositions.Center => WithJustifyContent(FlexJustifyContent.Center);
    IFluidFlexAll IFluidFlexJustifyContentPositions.Between => WithJustifyContent(FlexJustifyContent.Between);
    IFluidFlexAll IFluidFlexJustifyContentPositions.Around => WithJustifyContent(FlexJustifyContent.Around);

    IFluidFlexAlignItemsPosition IFluidFlexAlignItems.AlignItems => WithAlignItems();
    IFluidFlexAll IFluidFlexAlignItemsPosition.Start => WithAlignItems(FlexAlignItems.Start);
    IFluidFlexAll IFluidFlexAlignItemsPosition.End => WithAlignItems(FlexAlignItems.End);
    IFluidFlexAll IFluidFlexAlignItemsPosition.Center => WithAlignItems(FlexAlignItems.Center);
    IFluidFlexAll IFluidFlexAlignItemsPosition.Baseline => WithAlignItems(FlexAlignItems.Baseline);
    IFluidFlexAll IFluidFlexAlignItemsPosition.Stretch => WithAlignItems(FlexAlignItems.Stretch);

    IFluidFlexAlignSelfPosition IFluidFlexAlignSelf.AlignSelf => WithAlignSelf();
    IFluidFlexAll IFluidFlexAlignSelfPosition.Auto => WithAlignSelf(FlexAlignSelf.Auto);
    IFluidFlexAll IFluidFlexAlignSelfPosition.Start => WithAlignSelf(FlexAlignSelf.Start);
    IFluidFlexAll IFluidFlexAlignSelfPosition.End => WithAlignSelf(FlexAlignSelf.End);
    IFluidFlexAll IFluidFlexAlignSelfPosition.Center => WithAlignSelf(FlexAlignSelf.Center);
    IFluidFlexAll IFluidFlexAlignSelfPosition.Baseline => WithAlignSelf(FlexAlignSelf.Baseline);
    IFluidFlexAll IFluidFlexAlignSelfPosition.Stretch => WithAlignSelf(FlexAlignSelf.Stretch);

    IFluidFlexAlignContentPosition IFluidFlexAlignContent.AlignContent => WithAlignContent();
    IFluidFlexAll IFluidFlexAlignContentPosition.Start => WithAlignContent(FlexAlignContent.Start);
    IFluidFlexAll IFluidFlexAlignContentPosition.End => WithAlignContent(FlexAlignContent.End);
    IFluidFlexAll IFluidFlexAlignContentPosition.Center => WithAlignContent(FlexAlignContent.Center);
    IFluidFlexAll IFluidFlexAlignContentPosition.Between => WithAlignContent(FlexAlignContent.Between);
    IFluidFlexAll IFluidFlexAlignContentPosition.Around => WithAlignContent(FlexAlignContent.Around);
    IFluidFlexAll IFluidFlexAlignContentPosition.Stretch => WithAlignContent(FlexAlignContent.Stretch);

    IFluidFlexGrowShrinkSize IFluidFlexGrowShrink.Grow => WithGrowShrink(FlexGrowShrink.Grow);
    IFluidFlexGrowShrinkSize IFluidFlexGrowShrink.Shrink => WithGrowShrink(FlexGrowShrink.Shrink);
    IFluidFlexAll IFluidFlexGrowShrinkSize.Is0 => WithGrowShrinkSize(FlexGrowShrinkSize.Is0);
    IFluidFlexAll IFluidFlexGrowShrinkSize.Is1 => WithGrowShrinkSize(FlexGrowShrinkSize.Is1);

    IFluidFlexAll IFluidFlexFill.Fill => WithFill();

    IFluidFlexOrderNumber IFluidFlexOrder.Order => WithOrder();
    IFluidFlexAll IFluidFlexOrderNumber.Is0 => WithOrder(FlexOrder.Is0);
    IFluidFlexAll IFluidFlexOrderNumber.Is1 => WithOrder(FlexOrder.Is1);
    IFluidFlexAll IFluidFlexOrderNumber.Is2 => WithOrder(FlexOrder.Is2);
    IFluidFlexAll IFluidFlexOrderNumber.Is3 => WithOrder(FlexOrder.Is3);
    IFluidFlexAll IFluidFlexOrderNumber.Is4 => WithOrder(FlexOrder.Is4);
    IFluidFlexAll IFluidFlexOrderNumber.Is5 => WithOrder(FlexOrder.Is5);
    IFluidFlexAll IFluidFlexOrderNumber.Is6 => WithOrder(FlexOrder.Is6);
    IFluidFlexAll IFluidFlexOrderNumber.Is7 => WithOrder(FlexOrder.Is7);
    IFluidFlexAll IFluidFlexOrderNumber.Is8 => WithOrder(FlexOrder.Is8);
    IFluidFlexAll IFluidFlexOrderNumber.Is9 => WithOrder(FlexOrder.Is9);
    IFluidFlexAll IFluidFlexOrderNumber.Is10 => WithOrder(FlexOrder.Is10);
    IFluidFlexAll IFluidFlexOrderNumber.Is11 => WithOrder(FlexOrder.Is11);
    IFluidFlexAll IFluidFlexOrderNumber.Is12 => WithOrder(FlexOrder.Is12);
}
