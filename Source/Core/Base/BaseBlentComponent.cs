using System.Globalization;
using Craft.Blent.Contracts;
using Craft.Blent.Contracts.Providers;
using Craft.Blent.Extensions;
using Craft.Blent.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Craft.Blent.Base;

public abstract class BaseBlentComponent : ComponentBase, IAsyncDisposable, IStateHasChanged
{
    private CultureInfo _culture;
    private readonly Debouncer _debouncer = new();
    private bool _visibleChanged = false;
    private bool _firstRender = true;
    private DotNetObjectReference<BaseBlentComponent> _reference;
    private ILogger? _logger;

    internal protected ILogger Logger => _logger ??= LoggerFactory.CreateLogger(GetType());

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="JSRuntime" /> is available.
    /// </summary>
    internal protected bool IsJsRuntimeAvailable { get; set; }

    internal bool disposed = false;

    [Inject] protected IJSRuntime JsRuntime { get; set; }
    [Inject] protected IUniqueIdProvider IdGenerator { get; set; }
    [Inject] private ILoggerFactory LoggerFactory { get; set; } = null!;

    [CascadingParameter(Name = nameof(DefaultCulture))]
    public CultureInfo DefaultCulture { get; set; }

    /// <summary>
    /// Specifies the additional attributes that should be applied to the element
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> UserAttributes { get; set; }

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
    /// Gets or sets the unique id of the element.
    /// </summary>
    public string ElementId { get; set; }

    /// <summary>
    /// Use Tag to attach any user data object to the component for your convenience.
    /// </summary>
    [Parameter] public object? Tag { get; set; }

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
    public ElementReference ElementRef { get; protected internal set; }

    protected override void OnInitialized()
    {
        ElementId ??= GetElementId();

        base.OnInitialized();
    }

    protected internal string GetElementId()
    {
        string elementId = null;

        if (ElementId != null) return ElementId;

        if (UserAttributes != null && UserAttributes.TryGetValue("id", out object id)
                && !string.IsNullOrEmpty(Convert.ToString(@id)))
            elementId = Convert.ToString(id);

        ElementId ??= elementId ?? IdGenerator.Generate();

        return ElementId;
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
                    await JsRuntime.InvokeVoidAsync("CraftBlent.addContextMenu", ElementId, Reference);

                if (MouseEnter.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("CraftBlent.addMouseEnter", ElementId, Reference);

                if (MouseLeave.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("CraftBlent.addMouseLeave", ElementId, Reference);

                // TODO: Remove this if Mouse Events don't work
                Debounce(OnMouseEnter);
                Debounce(OnMouseLeave);
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

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        _visibleChanged = parameters.DidParameterChange(nameof(Visible), Visible);

        await base.SetParametersAsync(parameters);

        if (_visibleChanged && !_firstRender && !Visible)
            await DisposeAsync();
    }

    /// <summary>
    /// Raises <see cref="MouseEnter" />
    /// </summary>
    public async Task OnMouseEnter()
        => await MouseEnter.InvokeAsync(ElementRef);

    /// <summary>
    /// Raises <see cref="MouseLeave" />
    /// </summary>
    public async Task OnMouseLeave()
        => await MouseLeave.InvokeAsync(ElementRef);

    /// <summary>
    /// Raises <see cref="ContextMenu" />.
    /// </summary>
    /// <param name="args">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    public virtual async Task OnContextMenu(MouseEventArgs args)
        => await ContextMenu.InvokeAsync(args);

    /// <summary>
    /// Detaches event handlers and disposes <see cref="Reference" />.
    /// </summary>
    public virtual async ValueTask DisposeAsync()
    {
        disposed = true;

#pragma warning disable RCS1075 // Avoid empty catch clause that catches System.Exception.
        try
        {
            if (_reference is null) return;

            if (IsJsRuntimeAvailable)
            {
                if (ContextMenu.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("CraftBlent.removeContextMenu", ElementId);

                if (MouseEnter.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("CraftBlent.removeMouseEnter", ElementId);

                if (MouseLeave.HasDelegate)
                    await JsRuntime.InvokeVoidAsync("CraftBlent.removeMouseLeave", ElementId);
            }

            _reference?.Dispose();
            _reference = null;

            GC.SuppressFinalize(this);
        }
        catch (Exception)
        {
            // https://stackoverflow.com/questions/72488563/blazor-server-side-application-throwing-system-invalidoperationexception-javas
        }
#pragma warning restore RCS1075 // Avoid empty catch clause that catches System.Exception.
    }

    void IStateHasChanged.StateHasChanged() => StateHasChanged();
}
