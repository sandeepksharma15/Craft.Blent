using Craft.Blent.Enums;

namespace Craft.Blent.Contracts.ClassProvider;

public interface IBorderClasses
{
    string Border(BorderSize borderSize, BorderSide borderSide, BorderColor borderColor);

    string Border(BorderSize borderSize, IEnumerable<(BorderSide borderSide, BorderColor borderColor)> rules);

    string BorderRadius(BorderRadius borderRadius);
}
