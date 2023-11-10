using Craft.Blent.Enums;

namespace Craft.Blent.Contracts.ClassProvider;

public interface ITextClasses
{
    string Casing(CharacterCasing characterCasing);

    string TextColor(TextColor textColor);

    string TextAlignment(TextAlignment textAlignment);

    string TextTransform(TextTransform textTransform);

    string TextWeight(TextWeight textWeight);

    string TextOverflow(TextOverflow textOverflow);

    string TextSize(TextSize textSize);

    string TextItalic();
}
