namespace Craft.Blent.Services.Browser;

public class BrowserViewportEventArgs(Guid javaScriptListenerId,
    BrowserWindowSize browserWindowSize,
    Breakpoint breakpoint,
    bool isImmediate = false) : EventArgs
{
    /// <summary>
    /// Gets the ID of the JavaScript listener.
    /// </summary>
    public Guid JavaScriptListenerId { get; } = javaScriptListenerId;

    /// <summary>
    /// Gets the browser window size.
    /// </summary>
    public BrowserWindowSize BrowserWindowSize { get; } = browserWindowSize;

    /// <summary>
    /// Gets the breakpoint associated with the browser size.
    /// </summary>
    public Breakpoint Breakpoint { get; } = breakpoint;

    /// <summary>
    /// Gets a value indicating whether this is the first event that was fired.
    /// This is true when you set <c>fireImmediately</c> to <c>true</c> in the <see cref="IBrowserViewportService.SubscribeAsync(IBrowserViewportObserver, bool)"/>, <see cref="IBrowserViewportService.SubscribeAsync(Guid, Action{BrowserViewportEventArgs}, ResizeOptions?, bool)"/>, <see cref="IBrowserViewportService.SubscribeAsync(Guid, Func{BrowserViewportEventArgs, Task}, ResizeOptions?, bool)"/>  method.
    /// </summary>
    public bool IsImmediate { get; } = isImmediate;
}
