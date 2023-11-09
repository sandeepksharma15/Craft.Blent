using Craft.Blent.Enums;
using Craft.Blent.Fluid;

namespace Craft.Blent.Contracts;

public interface IDisplayClasses
{
    string Display(DisplayType displayType, DisplayDefinition displayDefinition);

    string Display(DisplayType displayType, IEnumerable<DisplayDefinition> displayDefinitions);
}
