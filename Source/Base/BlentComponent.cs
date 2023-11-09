﻿using Craft.Blent.Contracts;
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
}
