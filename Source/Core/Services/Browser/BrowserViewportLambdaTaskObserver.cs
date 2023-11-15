namespace Craft.Blent.Services.Browser;

internal class BrowserViewportLambdaTaskObserver(Guid id,
    Func<BrowserViewportEventArgs, Task> lambda,
    ResizeOptions? options) : IBrowserViewportObserver
{
    private readonly Func<BrowserViewportEventArgs, Task> _lambda = lambda;

    public Guid Id { get; } = id;

    public ResizeOptions? ResizeOptions { get; } = options;

    public Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        return _lambda(browserViewportEventArgs);
    }
}
