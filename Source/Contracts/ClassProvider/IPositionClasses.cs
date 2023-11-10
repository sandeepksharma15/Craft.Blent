using Craft.Blent.Enums;

namespace Craft.Blent.Contracts;

public interface IPositionClasses
{
    string Position(PositionType positionType, PositionEdgeType edgeType, int edgeOffset, PositionTranslateType translateType);

    string Position(PositionType positionType, IEnumerable<(PositionEdgeType edgeType, int edgeOffset)> edges, PositionTranslateType translateType);
}
