using Craft.Blent.Enums;

namespace Craft.Blent.Contracts;

public interface ISpacingClasses
{
    string Spacing(Spacing spacing, SpacingSize spacingSize, Side side, Breakpoint breakpoint);

    string Spacing(Spacing spacing, SpacingSize spacingSize, IEnumerable<(Side side, Breakpoint breakpoint)> rules);
}
