using Craft.Blent.Base;
using Craft.Blent.Services.Browser;
using Microsoft.AspNetCore.Components;

namespace Craft.Blent.Components.BreakpointProvider;

public partial class BlentBreakpointProvider : BlentComponent, IBrowserViewportObserver, IAsyncDisposable
{
    // private Guid _breakPointListenerSubscriptionId;

    public Breakpoint Breakpoint { get; private set; } = Breakpoint.Always;
    public Guid Id { get; } = Guid.NewGuid();

    [Inject] protected IBrowserViewportService BrowserViewportService { get; set; }

    [Parameter] public EventCallback<Breakpoint> OnBreakpointChanged { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
            await BrowserViewportService.SubscribeAsync(this, fireImmediately: true);
    }

    public override async ValueTask DisposeAsync()
    {
        if (IsJsRuntimeAvailable)
            await BrowserViewportService.UnsubscribeAsync(this);

        await base.DisposeAsync();
    }

    public async Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        Breakpoint = browserViewportEventArgs.Breakpoint;

        await OnBreakpointChanged.InvokeAsync(browserViewportEventArgs.Breakpoint);
        await InvokeAsync(StateHasChanged);
    }
}
