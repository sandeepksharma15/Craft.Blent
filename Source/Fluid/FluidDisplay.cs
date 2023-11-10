using Craft.Blent.Contracts;
using Craft.Blent.Contracts.ClassProvider;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Fluid;

public record DisplayDefinition
{
    public Breakpoint Breakpoint { get; set; }
    public DisplayDirection Direction { get; set; }
    public bool? Condition { get; set; }
}

/// <summary>
/// Default implementation of <see cref="IFluidDisplay"/>.
/// </summary>
public class FluidDisplay
    : IFluidDisplay, IFluidDisplayWithDisplayOnBreakpointWithDirection, IFluidDisplayWithDisplayFlexWithDirection
{
    private DisplayDefinition currentDisplay;
    private readonly Dictionary<DisplayType, List<DisplayDefinition>> rules = [];

    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            void BuildClasses(ClassBuilder builder)
            {
                if (rules.Any(x => x.Key != DisplayType.Always))
                    builder.Append(rules.Select(r => classProvider.Display(r.Key, r.Value.Where(x => x.Condition ?? true).Select(v => v))));
            }

            var classBuilder = new ClassBuilder(BuildClasses);
            classNames = classBuilder.Class;
            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    public IFluidDisplayWithDisplayOnBreakpointWithDirection WithDisplay(DisplayType displayType)
    {
        var columnDefinition = new DisplayDefinition { Breakpoint = Breakpoint.None };

        if (rules.TryGetValue(displayType, out var rule))
            rule.Add(columnDefinition);
        else
            rules.Add(displayType, [columnDefinition]);

        currentDisplay = columnDefinition;
        Dirty();

        return this;
    }

    public IFluidDisplayWithDisplayFlexWithDirection WithFlex(DisplayType displayType)
    {
        var columnDefinition = new DisplayDefinition { Breakpoint = Breakpoint.None };

        if (rules.TryGetValue(displayType, out var rule))
            rule.Add(columnDefinition);
        else
            rules.Add(displayType, [columnDefinition]);

        currentDisplay = columnDefinition;
        Dirty();

        return this;
    }

    public IFluidDisplayWithDisplay WithBreakpoint(Breakpoint breakpoint)
    {
        currentDisplay.Breakpoint = breakpoint;
        Dirty();

        return this;
    }

    public IFluidDisplayWithDisplayOnBreakpointWithDirection WithDirection(DisplayDirection direction)
    {
        currentDisplay.Direction = direction;
        Dirty();

        return this;
    }

    public IFluidDisplayWithDisplayOnBreakpointWithDirection If(bool condition)
    {
        currentDisplay.Condition = condition;
        Dirty();

        return this;
    }

    public IFluidDisplayWithDisplay OnMobile => WithBreakpoint(Breakpoint.Mobile);
    public IFluidDisplayWithDisplay OnTablet => WithBreakpoint(Breakpoint.Tablet);
    public IFluidDisplayWithDisplay OnDesktop => WithBreakpoint(Breakpoint.Desktop);
    public IFluidDisplayWithDisplay OnWidescreen => WithBreakpoint(Breakpoint.Widescreen);
    public IFluidDisplayWithDisplay OnFullHD => WithBreakpoint(Breakpoint.FullHD);

    public IFluidDisplayWithDisplayOnBreakpointWithDirection Always { get { return WithDisplay(DisplayType.Always); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection None { get { return WithDisplay(DisplayType.None); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection Block { get { return WithDisplay(DisplayType.Block); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection Inline { get { return WithDisplay(DisplayType.Inline); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection InlineBlock { get { return WithDisplay(DisplayType.InlineBlock); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection Table { get { return WithDisplay(DisplayType.Table); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection TableRow { get { return WithDisplay(DisplayType.TableRow); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection TableCell { get { return WithDisplay(DisplayType.TableCell); } }

    public IFluidDisplayWithDisplayFlexWithDirection Flex { get { return WithFlex(DisplayType.Flex); } }
    public IFluidDisplayWithDisplayFlexWithDirection InlineFlex { get { return WithFlex(DisplayType.InlineFlex); } }

    public IFluidDisplayWithDisplayOnBreakpointWithDirection Row { get { return WithDirection(DisplayDirection.Row); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection Column { get { return WithDirection(DisplayDirection.Column); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection ReverseRow { get { return WithDirection(DisplayDirection.ReverseRow); } }
    public IFluidDisplayWithDisplayOnBreakpointWithDirection ReverseColumn { get { return WithDirection(DisplayDirection.ReverseColumn); } }
}
