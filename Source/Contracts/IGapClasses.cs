using Craft.Blent.Enums;

namespace Craft.Blent.Contracts;

public interface IGapClasses
{
    string Gap(GapSize gapSize, GapSide gapSide);

    string Gap(GapSize gapSize, IEnumerable<GapSide> rules);
}
