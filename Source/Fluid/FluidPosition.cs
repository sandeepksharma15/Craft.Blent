using Craft.Blent.Contracts;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;

namespace Craft.Blent.Fluid;

public class FluidPosition :
    IFluidPosition,
    IFluidPositionTranslateType,
    IFluidPositionWithAll,
    IFluidPositionWithEdgeTypeAndTranslateType
{
    private class PositionEdgeDefinition
    {
        public int EdgeOffset { get; set; }
    }

    private PositionType positionType;
    private PositionEdgeDefinition currentPositionEdgeDefinition;
    private Dictionary<PositionEdgeType, PositionEdgeDefinition> edgeRules;
    private PositionTranslateType translateType;

    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            void BuildClasses(ClassBuilder builder)
            {
                builder.Append(classProvider.Position(positionType, edgeRules?.Select(x => (x.Key, x.Value.EdgeOffset)), translateType));
            }

            var classBuilder = new ClassBuilder(BuildClasses);
            classNames = classBuilder.Class;
            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    public IFluidPositionWithAll WithPosition(PositionType positionType)
    {
        this.positionType = positionType;
        Dirty();

        return this;
    }

    public IFluidPositionWithAll WithEdge(PositionEdgeType edgeType)
    {
        edgeRules ??= [];

        var positionEdgeDefinition = new PositionEdgeDefinition();
        if (edgeRules.TryGetValue(edgeType, out _))
            _ = positionEdgeDefinition;
        else
            edgeRules.Add(edgeType, positionEdgeDefinition);

        currentPositionEdgeDefinition = positionEdgeDefinition;
        Dirty();

        return this;
    }

    public IFluidPositionWithAll WithEdgeOffset(int offset)
    {
        currentPositionEdgeDefinition.EdgeOffset = offset;
        Dirty();

        return this;
    }

    public IFluidPositionWithAll WithTranslate(PositionTranslateType translateType)
    {
        this.translateType = translateType;
        Dirty();

        return this;
    }

    public IFluidPositionWithEdgeTypeAndTranslateType Static => WithPosition(PositionType.Static);
    public IFluidPositionWithEdgeTypeAndTranslateType Relative => WithPosition(PositionType.Relative);
    public IFluidPositionWithEdgeTypeAndTranslateType Absolute => WithPosition(PositionType.Absolute);
    public IFluidPositionWithEdgeTypeAndTranslateType Fixed => WithPosition(PositionType.Fixed);
    public IFluidPositionWithEdgeTypeAndTranslateType Sticky => WithPosition(PositionType.Sticky);

    public IFluidPositionEdgeOffset Top => WithEdge(PositionEdgeType.Top);
    public IFluidPositionEdgeOffset Start => WithEdge(PositionEdgeType.Start);
    public IFluidPositionEdgeOffset Bottom => WithEdge(PositionEdgeType.Bottom);
    public IFluidPositionEdgeOffset End => WithEdge(PositionEdgeType.End);

    public IFluidPositionWithAll Is0 => WithEdgeOffset(0);
    public IFluidPositionWithAll Is50 => WithEdgeOffset(50);
    public IFluidPositionWithAll Is100 => WithEdgeOffset(100);

    public IFluidPositionTranslateType Translate => this;

    public IFluidPositionWithAll Middle => WithTranslate(PositionTranslateType.Middle);
    public IFluidPositionWithAll MiddleX => WithTranslate(PositionTranslateType.MiddleX);
    public IFluidPositionWithAll MiddleY => WithTranslate(PositionTranslateType.MiddleY);
}
