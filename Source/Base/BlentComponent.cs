using Craft.Blent.Contracts;
using Craft.Blent.Enums;

namespace Craft.Blent.Base;

public abstract class BlentComponent : BaseBlentComponent
{
    private string _customClass;
    private string _customStyle;
    private bool _clearFix;

    private Float _float = Float.Default;
    private Visibility _visibility = Visibility.Default;

    private IFluidSizing width;
    private IFluidSizing height;

    private IFluidSpacing margin;
    private IFluidSpacing padding;

    private IFluidGap gap;
    private IFluidDisplay display;
    private IFluidBorder border;
    private IFluidFlex flex;
    private IFluidPosition position;
    private IFluidOverflow overflow;

    private CharacterCasing characterCasing = CharacterCasing.Normal;

    private TextColor textColor = TextColor.Default;
    private TextAlignment textAlignment = TextAlignment.Default;
    private TextTransform textTransform = TextTransform.Default;
    private TextWeight textWeight = TextWeight.Default;
    private TextOverflow textOverflow = TextOverflow.Default;
    private TextSize textSize = TextSize.Default;

    private VerticalAlignment verticalAlignment = VerticalAlignment.Default;

    private Background background = Background.Default;

    private Shadow shadow = Shadow.None;

}
