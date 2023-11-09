using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Contracts;

public class FluidBorder : IFluidBorder, IFluidBorderWithAll
{
    private class BorderDefinition
    {
        public BorderSide Side { get; set; }
        public BorderColor Color { get; set; }
    }

    private BorderDefinition currentBorderDefinition;
    private Dictionary<BorderSize, List<BorderDefinition>> rules;
    private BorderRadius borderRadius = BorderRadius.Default;

    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            void BuildClasses(ClassBuilder builder)
            {
                if (rules != null)
                    if (rules.Count > 0)
                        builder.Append(rules.Select(r => classProvider.Border(r.Key, r.Value.Select(v => (v.Side, v.Color)))));

                if (borderRadius != BorderRadius.Default)
                    builder.Append(classProvider.BorderRadius(borderRadius));
            }

            var classBuilder = new ClassBuilder(BuildClasses);
            classNames = classBuilder.Class;
            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    public IFluidBorderWithAll WithSize(BorderSize borderSize)
    {
        rules ??= [];

        var borderDefinition = new BorderDefinition { Side = BorderSide.All };

        if (rules.TryGetValue(borderSize, out var rule))
            rule.Add(borderDefinition);
        else
            rules.Add(borderSize, [borderDefinition]);

        currentBorderDefinition = borderDefinition;
        Dirty();

        return this;
    }

    public IFluidBorderWithAll WithSide(BorderSide borderSide)
    {
        currentBorderDefinition.Side = borderSide;
        Dirty();

        return this;
    }

    public IFluidBorderColorWithSide WithColor(BorderColor borderColor)
    {
        currentBorderDefinition.Color = borderColor;
        Dirty();

        return this;
    }

    public IFluidBorderWithAll WithRadius(BorderRadius borderRadius)
    {
        this.borderRadius = borderRadius;
        Dirty();

        return this;
    }

    public IFluidBorderWithAll Is0 => WithSize(BorderSize.Is0);
    public IFluidBorderWithAll Is1 => WithSize(BorderSize.Is1);
    public IFluidBorderWithAll Is2 => WithSize(BorderSize.Is2);
    public IFluidBorderWithAll Is3 => WithSize(BorderSize.Is3);
    public IFluidBorderWithAll Is4 => WithSize(BorderSize.Is4);
    public IFluidBorderWithAll Is5 => WithSize(BorderSize.Is5);

    public IFluidBorderWithAll OnTop => WithSide(BorderSide.Top);
    public IFluidBorderWithAll OnEnd => WithSide(BorderSide.End);
    public IFluidBorderWithAll OnBottom => WithSide(BorderSide.Bottom);
    public IFluidBorderWithAll OnStart => WithSide(BorderSide.Start);
    public IFluidBorderWithAll OnAll => WithSide(BorderSide.All);

    public IFluidBorderColorWithSide Primary => WithColor(BorderColor.Primary);
    public IFluidBorderColorWithSide Secondary => WithColor(BorderColor.Secondary);
    public IFluidBorderColorWithSide Success => WithColor(BorderColor.Success);
    public IFluidBorderColorWithSide Danger => WithColor(BorderColor.Danger);
    public IFluidBorderColorWithSide Warning => WithColor(BorderColor.Warning);
    public IFluidBorderColorWithSide Info => WithColor(BorderColor.Info);
    public IFluidBorderColorWithSide Light => WithColor(BorderColor.Light);
    public IFluidBorderColorWithSide Dark => WithColor(BorderColor.Dark);
    public IFluidBorderColorWithSide White => WithColor(BorderColor.White);

    public IFluidBorderWithAll Rounded => WithRadius(BorderRadius.Rounded);
    public IFluidBorderWithAll RoundedTop => WithRadius(BorderRadius.RoundedTop);
    public IFluidBorderWithAll RoundedEnd => WithRadius(BorderRadius.RoundedEnd);
    public IFluidBorderWithAll RoundedBottom => WithRadius(BorderRadius.RoundedBottom);
    public IFluidBorderWithAll RoundedStart => WithRadius(BorderRadius.RoundedStart);
    public IFluidBorderWithAll RoundedCircle => WithRadius(BorderRadius.RoundedCircle);
    public IFluidBorderWithAll RoundedPill => WithRadius(BorderRadius.RoundedPill);
    public IFluidBorderWithAll RoundedZero => WithRadius(BorderRadius.RoundedZero);
}
