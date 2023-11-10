using Craft.Blent.Contracts.ClassProvider;
using Craft.Blent.Contracts.Fluid;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Fluid;

/// <summary>
/// Default implementation of <see cref="IFluidGap"/>.
/// </summary>
public class FluidGap : IFluidGap, IFluidGapWithSide, IFluidGapWithSideAndSize
{
    private class GapDefinition
    {
        public GapSide Side { get; set; }
    }

    private GapDefinition currentGap;
    private readonly Dictionary<GapSize, List<GapDefinition>> rules = [];
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
                    builder.Append(rules.Select(r => classProvider.Gap(r.Key, r.Value.Select(v => v.Side))));

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

    public IFluidGapWithSideAndSize WithSize(GapSize gapSize)
    {
        var gapDefinition = new GapDefinition { Side = GapSide.All };

        if (rules.TryGetValue(gapSize, out var rule))
            rule.Add(gapDefinition);
        else
            rules.Add(gapSize, [gapDefinition]);

        currentGap = gapDefinition;
        Dirty();

        return this;
    }

    public IFluidGapWithSideAndSize WithSide(GapSide side)
    {
        currentGap.Side = side;
        Dirty();

        return this;
    }

    private IFluidGapWithSize WithSize(string value)
    {
        if (customRules == null)
            customRules = [value];
        else
            customRules.Add(value);

        Dirty();

        return this;
    }

    public IFluidGapWithSize Is(string value) => WithSize(value);

    public IFluidGapWithSideAndSize Is0 => WithSize(GapSize.Is0);
    public IFluidGapWithSideAndSize Is1 => WithSize(GapSize.Is1);
    public IFluidGapWithSideAndSize Is2 => WithSize(GapSize.Is2);
    public IFluidGapWithSideAndSize Is3 => WithSize(GapSize.Is3);
    public IFluidGapWithSideAndSize Is4 => WithSize(GapSize.Is4);
    public IFluidGapWithSideAndSize Is5 => WithSize(GapSize.Is5);

    public IFluidGapWithSideAndSize OnX => WithSide(GapSide.X);
    public IFluidGapWithSideAndSize OnY => WithSide(GapSide.Y);
    public IFluidGapWithSideAndSize OnAll => WithSide(GapSide.All);
}
