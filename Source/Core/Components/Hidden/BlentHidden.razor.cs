using Craft.Blent.Base;
using Craft.Blent.Services.Browser;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Craft.Blent.Components.Hidden;

public partial class BlentHidden : BlentComponent, IBrowserViewportObserver, IAsyncDisposable
{
    private bool _isHidden = true;
    private bool _serviceIsReady = false;
    private Breakpoint _currentBreakpoint = Breakpoint.None;
    private Guid _breakpointServiceSubscriptionId;

    ResizeOptions IBrowserViewportObserver.ResizeOptions { get; } = new()
    {
        ReportRate = 250,
        NotifyOnBreakpointOnly = false
    };

    public Guid Id { get; } = Guid.NewGuid();

    [Inject] protected IBrowserViewportService BrowserViewportService { get; set; }

    [CascadingParameter] public Breakpoint CurrentBreakpointFromProvider { get; set; } = Breakpoint.None;

    [Parameter] public Breakpoint Breakpoint { get; set; }
    [Parameter] public bool Invert { get; set; }
    [Parameter] public EventCallback<bool> IsHiddenChanged { get; set; }

    [Parameter]
    public bool IsHidden
    {
        get => _isHidden;
        set
        {
            if (_isHidden != value)
            {
                _isHidden = value;
                IsHiddenChanged.InvokeAsync(_isHidden);
            }
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        Logger.LogDebug($"OnParametersSetAsync: {Id}");

        await base.OnParametersSetAsync();
        await UpdateAsync(_currentBreakpoint);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        Logger.LogDebug($"OnAfterRenderAsync: {Id}");

        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (CurrentBreakpointFromProvider == Breakpoint.None)
            {
                _serviceIsReady = true;
                await BrowserViewportService.SubscribeAsync(this, fireImmediately: true);
            }
            else
            {
                _serviceIsReady = true;
            }
        }
    }

    public override async ValueTask DisposeAsync()
    {
        Logger.LogDebug($"DisposeAsync: {Id}");

        if (IsJsRuntimeAvailable)
            await BrowserViewportService.UnsubscribeAsync(this);

        await base.DisposeAsync();
    }

    public async Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        Logger.LogDebug($"NotifyBrowserViewportChangeAsync: {Id}");

        await UpdateAsync(browserViewportEventArgs.Breakpoint);
        await InvokeAsync(StateHasChanged);
    }

    protected async Task UpdateAsync(Breakpoint currentBreakpoint)
    {
        Logger.LogDebug($"UpdateAsync: {Id}");

        if (CurrentBreakpointFromProvider != Breakpoint.None)
            currentBreakpoint = CurrentBreakpointFromProvider;
        else
            if (!_serviceIsReady)
                return;

        if (currentBreakpoint == Breakpoint.None)
            return;

        _currentBreakpoint = currentBreakpoint;

        var hidden = await BrowserViewportService.IsBreakpointWithinReferenceSizeAsync(Breakpoint, currentBreakpoint);

        if (Invert)
            hidden = !hidden;

        IsHidden = hidden;
    }
}
