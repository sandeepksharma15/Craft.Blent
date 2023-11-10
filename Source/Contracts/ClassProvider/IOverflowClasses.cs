using Craft.Blent.Enums;

namespace Craft.Blent.Contracts;

public interface IOverflowClasses
{
    string Overflow(OverflowType overflowType, OverflowType secondOverflowType);
}
