using Craft.Blent.Enums;
using Craft.Blent.Fluid;

namespace Craft.Blent.Contracts;

public interface IFlexClasses
{
    string Flex(FlexType flexType);

    string Flex(FlexDefinition flexDefinition);

    string Flex(FlexType flexType, IEnumerable<FlexDefinition> flexDefinitions);

    string FlexAlignment(Alignment alignment);
}
