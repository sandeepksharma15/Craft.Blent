namespace Craft.Blent.Contracts;

public interface IFluidPosition
{
    string Class(IClassProvider classProvider);
}

public interface IFluidPositionType : IFluidPosition
{
    IFluidPositionWithEdgeTypeAndTranslateType Static { get; }
    IFluidPositionWithEdgeTypeAndTranslateType Relative { get; }
    IFluidPositionWithEdgeTypeAndTranslateType Absolute { get; }
    IFluidPositionWithEdgeTypeAndTranslateType Fixed { get; }
    IFluidPositionWithEdgeTypeAndTranslateType Sticky { get; }
}

public interface IFluidPositionEdgeType : IFluidPosition
{
    IFluidPositionEdgeOffset Top { get; }
    IFluidPositionEdgeOffset Start { get; }
    IFluidPositionEdgeOffset Bottom { get; }
    IFluidPositionEdgeOffset End { get; }
}

public interface IFluidPositionEdgeOffset :
    IFluidPosition,
    IFluidPositionTranslate
{
    IFluidPositionWithAll Is0 { get; }
    IFluidPositionWithAll Is50 { get; }
    IFluidPositionWithAll Is100 { get; }
}

public interface IFluidPositionTranslate : IFluidPosition
{
    IFluidPositionTranslateType Translate { get; }
}

public interface IFluidPositionTranslateType : IFluidPosition
{
    IFluidPositionWithAll Middle { get; }
    IFluidPositionWithAll MiddleX { get; }
    IFluidPositionWithAll MiddleY { get; }
}

public interface IFluidPositionWithEdgeTypeAndTranslateType :
    IFluidPosition,
    IFluidPositionEdgeType,
    IFluidPositionTranslate
{
}

public interface IFluidPositionWithAll :
    IFluidPosition,
    IFluidPositionType,
    IFluidPositionEdgeType,
    IFluidPositionEdgeOffset,
    IFluidPositionTranslate,
    IFluidPositionWithEdgeTypeAndTranslateType
{
}
