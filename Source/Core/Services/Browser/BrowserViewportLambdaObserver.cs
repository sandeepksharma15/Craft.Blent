namespace Craft.Blent.Services.Browser;

internal class BrowserViewportLambdaObserver(Guid id,
    Action<BrowserViewportEventArgs> lambda,
    ResizeOptions? options) : IBrowserViewportObserver
{
    private readonly Action<BrowserViewportEventArgs> _lambda = lambda;

    public Guid Id { get; } = id;

    public ResizeOptions? ResizeOptions { get; } = options;

    public Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        _lambda(browserViewportEventArgs);

        return Task.CompletedTask;
    }
}
