using Craft.Blent.Enums;

namespace Craft.Blent.Contracts;

public interface ITextEditClasses
{
    string TextEdit(bool plaintext);
    string TextEditSize(Size size);
    string TextEditColor(Color color);
    string TextEditValidation(ValidationStatus validationStatus);
}
