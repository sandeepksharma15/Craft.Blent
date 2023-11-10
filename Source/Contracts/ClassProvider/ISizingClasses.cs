using Craft.Blent.Enums;
using Craft.Blent.Fluid;

namespace Craft.Blent.Contracts;

public interface ISizingClasses
{
    string Sizing(SizingType sizingType, SizingSize sizingSize, SizingDefinition sizingDefinition);

    string Sizing(SizingType sizingType, SizingSize sizingSize, IEnumerable<SizingDefinition> rules);
}
