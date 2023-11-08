using System.Globalization;
using System.Xml;
using Craft.Blent.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Craft.Blent.Base;

public class BaseBlentComponent : ComponentBase, IDisposable
{
    private CultureInfo _culture;
    private readonly Debouncer _debouncer = new();
    private bool _visibleChanged = false;
    private bool _firstRender = true;
    private DotNetObjectReference<BaseBlentComponent> _reference;

    internal bool disposed = false;

    [Inject] protected IJSRuntime JsRuntime { get; set; }

    [CascadingParameter(Name = nameof(DefaultCulture))]
    public CultureInfo DefaultCulture { get; set; }

    /// <summary>
    /// Specifies the additional attributes that should be applied to the element
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object> UserAttributes { get; set; }

    /// <summary>
    /// A callback that is invoked when the user hovers the component
    /// </summary>
    [Parameter] public EventCallback<ElementReference> MouseEnter { get; set; }

    /// <summary>
    /// A callback that is invoked when the user stops hovering the component
    /// </summary>
    [Parameter] public EventCallback<ElementReference> MouseLeave { get; set; }

    /// <summary>
    /// A callback that is invoked when the user right-clicks the component
    /// </summary>
    [Parameter] public EventCallback<MouseEventArgs> ContextMenu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="BaseBlentComponent"/> is visible.
    /// </summary>
    [Parameter] public virtual bool Visible { get; set; } = true;

    /// <summary>
    /// Gets or sets the culture used by this component. Defaults to <see cref="CultureInfo.CurrentCulture"/>.
    /// </summary>
    [Parameter]
    public CultureInfo Culture
    {
        get => _culture ?? DefaultCulture ?? CultureInfo.CurrentCulture;
        set => _culture = value;
    }

    /// <summary>
    /// Gets a reference to the HTML element rendered for the component
    /// </summary>
    public ElementReference Element { get; protected internal set; }

    public string Id { get; set; }

    protected override void OnInitialized()
    {
        Id = Guid.NewGuid().ToString("N")[..10];
    }

    protected internal string GetId()
    {
        return UserAttributes != null && UserAttributes.TryGetValue("id", out object id)
                && !string.IsNullOrEmpty(Convert.ToString(@id))
            ? Convert.ToString(id)
            : Id;
    }

    protected DotNetObjectReference<BaseBlentComponent> Reference
    {
        get
        {
            _reference ??= DotNetObjectReference.Create(this);

            return _reference;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="JSRuntime" /> is available.
    /// </summary>
    protected bool IsJsRuntimeAvailable { get; set; }

    /// <summary>
    /// Debounces the specified action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="milliseconds">The milliseconds.</param>
    protected void Debounce(Func<Task> action, int milliseconds = 500)
        => _debouncer.Debounce(milliseconds, action);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        IsJsRuntimeAvailable = true;

        _firstRender = firstRender;

        if (firstRender || _visibleChanged)
        {
            _visibleChanged = false;

            if (Visible)
            {
                if (ContextMenu.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("Radzen.addContextMenu", GetId(), Reference);

                if (MouseEnter.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("Radzen.addMouseEnter", GetId(), Reference);

                if (MouseLeave.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("Radzen.addMouseLeave", GetId(), Reference);
            }
        }
    }

    /// <summary>
    /// Invoked via interop when the browser "oncontextmenu" event is raised for this component.
    /// </summary>
    /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    [JSInvokable("BaseBlentComponent.RaiseContextMenu")]
    public async Task RaiseContextMenu(MouseEventArgs e)
    {
        if (ContextMenu.HasDelegate)
            await OnContextMenu(e);
    }

    /// <summary>
    /// Invoked via interop when the browser "onmouseenter" event is raised for this component.
    /// </summary>
    [JSInvokable("BaseBlentComponent.RaiseMouseEnter")]
    public async Task RaiseMouseEnter()
    {
        if (MouseEnter.HasDelegate)
            await OnMouseEnter();
    }

    /// <summary>
    /// Invoked via interop when the browser "onmouseleave" event is raised for this component.
    /// </summary>
    [JSInvokable("BaseBlentComponent.RaiseMouseLeave")]
    public async Task RaiseMouseLeave()
    {
        if (MouseLeave.HasDelegate)
            await OnMouseLeave();
    }

    /// <summary>
    /// Called by the Blazor runtime when parameters are set.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        _visibleChanged = parameters.DidParameterChange(nameof(Visible), Visible);

        await base.SetParametersAsync(parameters);

        if (_visibleChanged && !_firstRender && !Visible)
            Dispose();
    }

    /// <summary>
    /// Raises <see cref="MouseEnter" />
    /// </summary>
    public async Task OnMouseEnter()
        => await MouseEnter.InvokeAsync(Element);

    /// <summary>
    /// Raises <see cref="MouseLeave" />
    /// </summary>
    public async Task OnMouseLeave()
        => await MouseLeave.InvokeAsync(Element);

    /// <summary>
    /// Raises <see cref="ContextMenu" />.
    /// </summary>
    /// <param name="args">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    public virtual async Task OnContextMenu(MouseEventArgs args)
        => await ContextMenu.InvokeAsync(args);

    /// <summary>
    /// Detaches event handlers and disposes <see cref="Reference" />.
    /// </summary>
    public virtual void Dispose()
    {
        disposed = true;

        _reference?.Dispose();
        _reference = null;

        if (IsJsRuntimeAvailable)
        {
            if (ContextMenu.HasDelegate)
                _ = JsRuntime.InvokeVoidAsync("Radzen.removeContextMenu", Id);

            if (MouseEnter.HasDelegate)
                _ = JsRuntime.InvokeVoidAsync("Radzen.removeMouseEnter", Id);

            if (MouseLeave.HasDelegate)
                _ = JsRuntime.InvokeVoidAsync("Radzen.removeMouseLeave", Id);
        }
    }
}
