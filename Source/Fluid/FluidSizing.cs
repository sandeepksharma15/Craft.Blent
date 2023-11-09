using Craft.Blent.Contracts;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Fluid;

public class FluidSizing(SizingType sizingType) :
    IFluidSizing,
    IFluidSizingMinMaxViewportOnBreakpoint,
    IFluidSizingWithSizeWithMinMaxWithViewportAll,
    IFluidSizingWithSizeOnBreakpoint
{
    private readonly SizingType sizingType = sizingType;
    private SizingDefinition currentSizingDefinition;
    private readonly Dictionary<SizingSize, List<SizingDefinition>> rules = [];

    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            void BuildClasses(ClassBuilder builder)
            {
                if (rules.Count > 0)
                    builder.Append(rules.Select(r => classProvider.Sizing(sizingType, r.Key, r.Value)));
            }

            var classBuilder = new ClassBuilder(BuildClasses);
            classNames = classBuilder.Class;
            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    /// <summary>
    /// Starts the new sizing rule.
    /// </summary>
    /// <param name="sizingSize">Size of the element.</param>
    /// <returns>Next rule reference.</returns>returns>
    public IFluidSizingMinMaxViewportOnBreakpoint WithSize(SizingSize sizingSize)
    {
        var sizingDefinition = new SizingDefinition { Breakpoint = Breakpoint.None };

        if (rules.TryGetValue(sizingSize, out var rule))
            rule.Add(sizingDefinition);
        else
            rules.Add(sizingSize, [sizingDefinition]);

        currentSizingDefinition = sizingDefinition;
        Dirty();

        return this;
    }

    /// <summary>
    /// Sets the min rule for the current definition.
    /// </summary>
    /// <returns>Next rule reference.</returns>
    public IFluidSizingViewport WithMin()
    {
        currentSizingDefinition.IsMin = true;
        Dirty();

        return this;
    }

    /// <summary>
    /// Sets the max rule for the current definition.
    /// </summary>
    /// <returns>Next rule reference.</returns>
    public IFluidSizingWithSizeOnBreakpoint WithMax()
    {
        currentSizingDefinition.IsMax = true;
        Dirty();

        return this;
    }

    /// <summary>
    /// Sets the viewport rule for the current definition.
    /// </summary>
    /// <returns>Next rule reference.</returns>
    public IFluidSizing WithViewport()
    {
        currentSizingDefinition.IsViewport = true;
        Dirty();

        return this;
    }

    /// <summary>
    /// Appends the new breakpoint rule.
    /// </summary>
    /// <param name="breakpoint">Breakpoint to append.</param>
    /// <returns>Next rule reference.</returns>
    public IFluidSizingWithSizeWithMinMaxWithViewportAll WithBreakpoint(Breakpoint breakpoint)
    {
        currentSizingDefinition.Breakpoint = breakpoint;
        Dirty();

        return this;
    }

    IFluidSizingOnBreakpoint IFluidSizingSize.Is25 => WithSize(SizingSize.Is25);
    IFluidSizingOnBreakpoint IFluidSizingSize.Is33 => WithSize(SizingSize.Is33);
    IFluidSizingOnBreakpoint IFluidSizingSize.Is50 => WithSize(SizingSize.Is50);
    IFluidSizingOnBreakpoint IFluidSizingSize.Is66 => WithSize(SizingSize.Is66);
    IFluidSizingOnBreakpoint IFluidSizingSize.Is75 => WithSize(SizingSize.Is75);
    IFluidSizingMinMaxViewportOnBreakpoint IFluidSizingSize.Is100 => WithSize(SizingSize.Is100);
    IFluidSizing IFluidSizingSize.Auto => WithSize(SizingSize.Auto);

    IFluidSizingViewport IFluidSizingMin.Min => WithMin();
    IFluidSizingWithSizeOnBreakpoint IFluidSizingMax.Max => WithMax();
    IFluidSizing IFluidSizingViewport.Viewport => WithViewport();

    public IFluidSizingWithSizeWithMinMaxWithViewportAll OnMobile => WithBreakpoint(Breakpoint.Mobile);
    public IFluidSizingWithSizeWithMinMaxWithViewportAll OnTablet => WithBreakpoint(Breakpoint.Tablet);
    public IFluidSizingWithSizeWithMinMaxWithViewportAll OnDesktop => WithBreakpoint(Breakpoint.Desktop);
    public IFluidSizingWithSizeWithMinMaxWithViewportAll OnWidescreen => WithBreakpoint(Breakpoint.Widescreen);
    public IFluidSizingWithSizeWithMinMaxWithViewportAll OnFullHD => WithBreakpoint(Breakpoint.FullHD);
}
