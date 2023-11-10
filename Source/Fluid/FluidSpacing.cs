using Craft.Blent.Contracts.ClassProvider;
using Craft.Blent.Contracts.Fluid;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Fluid;

/// <summary>
/// Default implementation of <see cref="IFluidSpacing"/>.
/// </summary>
public abstract class FluidSpacing(Spacing spacing)
    : IFluidSpacing, IFluidSpacingOnBreakpointWithSide, IFluidSpacingOnBreakpointWithSideAndSize
{
    private class SpacingDefinition
    {
        public Side Side { get; set; }
        public Breakpoint Breakpoint { get; set; }
    }
    private SpacingDefinition currentSpacing;
    private readonly Dictionary<SpacingSize, List<SpacingDefinition>> rules = [];

    private List<string> customRules;
    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            void BuildClasses(ClassBuilder builder)
            {
                if (rules.Count > 0)
                    builder.Append(rules.Select(r => classProvider.Spacing(Spacing, r.Key, r.Value.Select(v => (v.Side, v.Breakpoint)))));

                if (customRules?.Count > 0)
                    builder.Append(customRules);
            }

            var classBuilder = new ClassBuilder(BuildClasses);
            classNames = classBuilder.Class;
            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    public IFluidSpacingOnBreakpointWithSideAndSize WithSize(SpacingSize spacingSize)
    {
        var spacingDefinition = new SpacingDefinition { Breakpoint = Breakpoint.None, Side = Side.All };

        if (rules.TryGetValue(spacingSize, out var rule))
            rule.Add(spacingDefinition);
        else
            rules.Add(spacingSize, [spacingDefinition]);

        currentSpacing = spacingDefinition;
        Dirty();
        return this;
    }

    public IFluidSpacingOnBreakpointWithSideAndSize WithSide(Side side)
    {
        currentSpacing.Side = side;
        Dirty();

        return this;
    }

    public IFluidSpacingOnBreakpointWithSideAndSize WithBreakpoint(Breakpoint breakpoint)
    {
        currentSpacing.Breakpoint = breakpoint;
        Dirty();

        return this;
    }

    private IFluidSpacingWithSize WithSize(string value)
    {
        if (customRules == null)
            customRules = [value];
        else
            customRules.Add(value);

        Dirty();

        return this;
    }

    public IFluidSpacingWithSize Is(string value) => WithSize(value);

    protected Spacing Spacing { get; } = spacing;

    public IFluidSpacingOnBreakpointWithSideAndSize Is0 => WithSize(SpacingSize.Is0);
    public IFluidSpacingOnBreakpointWithSideAndSize Is1 => WithSize(SpacingSize.Is1);
    public IFluidSpacingOnBreakpointWithSideAndSize Is2 => WithSize(SpacingSize.Is2);
    public IFluidSpacingOnBreakpointWithSideAndSize Is3 => WithSize(SpacingSize.Is3);
    public IFluidSpacingOnBreakpointWithSideAndSize Is4 => WithSize(SpacingSize.Is4);
    public IFluidSpacingOnBreakpointWithSideAndSize Is5 => WithSize(SpacingSize.Is5);
    public IFluidSpacingOnBreakpointWithSideAndSize IsAuto => WithSize(SpacingSize.IsAuto);

    public IFluidSpacingOnBreakpointWithSideAndSize FromTop => WithSide(Side.Top);
    public IFluidSpacingOnBreakpointWithSideAndSize FromBottom => WithSide(Side.Bottom);
    public IFluidSpacingOnBreakpointWithSideAndSize FromStart => WithSide(Side.Start);
    public IFluidSpacingOnBreakpointWithSideAndSize FromEnd => WithSide(Side.End);
    public IFluidSpacingOnBreakpointWithSideAndSize OnX => WithSide(Side.X);
    public IFluidSpacingOnBreakpointWithSideAndSize OnY => WithSide(Side.Y);
    public IFluidSpacingOnBreakpointWithSideAndSize OnAll => WithSide(Side.All);

    public IFluidSpacingOnBreakpointWithSideAndSize OnMobile => WithBreakpoint(Breakpoint.Mobile);
    public IFluidSpacingOnBreakpointWithSideAndSize OnTablet => WithBreakpoint(Breakpoint.Tablet);
    public IFluidSpacingOnBreakpointWithSideAndSize OnDesktop => WithBreakpoint(Breakpoint.Desktop);
    public IFluidSpacingOnBreakpointWithSideAndSize OnWidescreen => WithBreakpoint(Breakpoint.Widescreen);
    public IFluidSpacingOnBreakpointWithSideAndSize OnFullHD => WithBreakpoint(Breakpoint.FullHD);
}

public sealed class FluidMargin : FluidSpacing
{
    public FluidMargin() : base(Spacing.Margin) { }
}

public sealed class FluidPadding : FluidSpacing
{
    public FluidPadding() : base(Spacing.Padding) { }
}
