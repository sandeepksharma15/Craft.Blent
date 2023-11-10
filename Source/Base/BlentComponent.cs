using Craft.Blent.Contracts;
using Craft.Blent.Contracts.ClassProvider;
using Craft.Blent.Contracts.Fluid;
using Craft.Blent.Contracts.JsModules;
using Craft.Blent.Contracts.Providers;
using Craft.Blent.Enums;
using Craft.Blent.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Craft.Blent.Base;

public abstract class BlentComponent : BaseBlentComponent
{
    protected ClassBuilder ClassBuilder { get; set; }
    protected StyleBuilder StyleBuilder { get; set; }

    [Inject] protected IClassProvider ClassProvider { get; set; }
    [Inject] protected IStyleProvider StyleProvider { get; set; }

    public string ClassNames => ClassBuilder.Class;
    public string StyleNames => StyleBuilder.Styles;

    [Parameter] public string Style { get; set; }
    [Parameter] public string Class { get; set; }

    [Parameter] public IFluidSpacing Margin { get; set; }
    [Parameter] public IFluidSpacing Padding { get; set; }
    [Parameter] public IFluidGap Gap { get; set; }
    [Parameter] public IFluidDisplay Display { get; set; }
    [Parameter] public IFluidBorder Border { get; set; }
    [Parameter] public IFluidFlex Flex { get; set; }
    [Parameter] public IFluidPosition Position { get; set; }
    [Parameter] public IFluidOverflow Overflow { get; set; }
    [Parameter] public IFluidSizing Width { get; set; }
    [Parameter] public IFluidSizing Height { get; set; }

    [Parameter] public Float Float { get; set; } = Float.Default;
    [Parameter] public bool Clearfix { get; set; }
    [Parameter] public Visibility Visibility { get; set; } = Visibility.Default;
    [Parameter] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Default;
    [Parameter] public CharacterCasing Casing { get; set; } = CharacterCasing.Normal;
    [Parameter] public TextColor TextColor { get; set; } = TextColor.Default;
    [Parameter] public TextAlignment TextAlignment { get; set; } = TextAlignment.Default;
    [Parameter] public TextTransform TextTransform { get; set; } = TextTransform.Default;
    [Parameter] public TextWeight TextWeight { get; set; } = TextWeight.Default;
    [Parameter] public TextOverflow TextOverflow { get; set; } = TextOverflow.Default;
    [Parameter] public TextSize TextSize { get; set; } = TextSize.Default;
    [Parameter] public Background Background { get; set; } = Background.Default;
    [Parameter] public Shadow Shadow { get; set; } = Shadow.None;

    protected BlentComponent()
    {
        ClassBuilder = new ClassBuilder(BuildClasses);
        StyleBuilder = new StyleBuilder(BuildStyles);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DirtyClasses();
        DirtyStyles();
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        object heightAttribute = null;

        // WORKAROUND for: https://github.com/dotnet/aspnetcore/issues/32252
        // HTML native width/height attributes are recognized as Width/Height parameters
        // and Blazor tries to convert them resulting in error. This workaround tries to fix it by removing
        // width/height from parameter list and moving them to Attributes(as unmatched values).
        //
        // This behavior is really an edge-case and shouldn't affect performance too much.
        // Only in some rare cases when width/height are used will the parameters be rebuilt.
        if (parameters.TryGetValue("width", out object widthAttribute)
             || parameters.TryGetValue("height", out heightAttribute))
        {
            var parametersDictionary = (Dictionary<string, object>)parameters.ToDictionary();

            UserAttributes ??= [];

            if (widthAttribute != null && parametersDictionary.Remove("width"))
                UserAttributes.Add("width", widthAttribute);

            if (heightAttribute != null && parametersDictionary.Remove("height"))
                UserAttributes.Add("height", heightAttribute);

            return base.SetParametersAsync(ParameterView.FromDictionary(parametersDictionary));
        }

        return base.SetParametersAsync(parameters);
    }

    protected virtual void BuildStyles(StyleBuilder builder)
    {
        if (Style != null)
            builder.Append(Style);
    }

    protected virtual void BuildClasses(ClassBuilder builder)
    {
        if (Class != null)
            builder.Append(Class);

        if (Margin != null)
            builder.Append(Margin.Class(ClassProvider));

        if (Padding != null)
            builder.Append(Padding.Class(ClassProvider));

        if (Gap != null)
            builder.Append(Gap.Class(ClassProvider));

        if (Display != null)
            builder.Append(Display.Class(ClassProvider));

        if (Border != null)
            builder.Append(Border.Class(ClassProvider));

        if (Flex != null)
            builder.Append(Flex.Class(ClassProvider));

        if (Position != null)
            builder.Append(Position.Class(ClassProvider));

        if (Overflow != null)
            builder.Append(Overflow.Class(ClassProvider));

        if (Float != Float.Default)
            builder.Append(ClassProvider.Float(Float));

        if (Clearfix)
            builder.Append(ClassProvider.Clearfix());

        if (Visibility != Visibility.Default)
            builder.Append(ClassProvider.Visibility(Visibility));

        if (VerticalAlignment != VerticalAlignment.Default)
            builder.Append(ClassProvider.VerticalAlignment(VerticalAlignment));

        if (Width != null)
            builder.Append(Width.Class(ClassProvider));

        if (Height != null)
            builder.Append(Height.Class(ClassProvider));

        if (Casing != CharacterCasing.Normal)
            builder.Append(ClassProvider.Casing(Casing));

        if (TextColor != TextColor.Default)
            builder.Append(ClassProvider.TextColor(TextColor));

        if (TextAlignment != TextAlignment.Default)
            builder.Append(ClassProvider.TextAlignment(TextAlignment));

        if (TextTransform != TextTransform.Default)
            builder.Append(ClassProvider.TextTransform(TextTransform));

        if (TextWeight != TextWeight.Default)
            builder.Append(ClassProvider.TextWeight(TextWeight));

        if (TextOverflow != TextOverflow.Default)
            builder.Append(ClassProvider.TextOverflow(TextOverflow));

        if (TextSize != TextSize.Default)
            builder.Append(ClassProvider.TextSize(TextSize));

        if (Background != Background.Default)
            builder.Append(ClassProvider.BackgroundColor(Background));

        if (Shadow != Shadow.None)
            builder.Append(ClassProvider.Shadow(Shadow));
    }

    protected virtual void DirtyStyles()
        => StyleBuilder?.Dirty();

    internal protected virtual void DirtyClasses()
        => ClassBuilder?.Dirty();

    protected static DotNetObjectReference<T> CreateDotNetObjectRef<T>(T value) where T : class
        => DotNetObjectReference.Create(value);

    protected static void DisposeDotNetObjectRef<T>(DotNetObjectReference<T> value) where T : class
        => value?.Dispose();
}
