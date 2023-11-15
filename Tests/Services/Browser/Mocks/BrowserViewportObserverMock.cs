using Craft.Blent.Services.Browser;

namespace Craft.Blent.Tests.Services.Browser.Mocks;

#nullable enable
internal class BrowserViewportObserverMock(ResizeOptions? resizeOptions) : IBrowserViewportObserver
{
    public Guid Id { get; } = Guid.NewGuid();

    public ResizeOptions? ResizeOptions { get; } = resizeOptions;

    public List<BrowserViewportEventArgs> Notifications { get; } = [];

    public BrowserViewportObserverMock() : this(null)
    {
    }

    public Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        Notifications.Add(browserViewportEventArgs);

        return Task.CompletedTask;
    }
}
