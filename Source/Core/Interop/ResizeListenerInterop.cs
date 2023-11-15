using System.Diagnostics.CodeAnalysis;
using Craft.Blent.Extensions;
using Craft.Blent.Services.Browser;
using Microsoft.JSInterop;

namespace Craft.Blent.Interop;

internal class ResizeListenerInterop(IJSRuntime jsRuntime)
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public ValueTask<bool> MatchMedia(string mediaQuery)
    {
        return _jsRuntime.InvokeAsync<bool>("resizeListener.matchMedia", mediaQuery);
    }

    public ValueTask<bool> ListenForResize<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] T>(DotNetObjectReference<T> dotNetObjectReference, ResizeOptions options, Guid javaScriptListerId) where T : class
    {
        return _jsRuntime.InvokeVoidAsyncWithErrorHandling("resizeListenerFactory.listenForResize", dotNetObjectReference, options, javaScriptListerId);
    }

    public ValueTask<bool> CancelListener(Guid javaScriptListerId)
    {
        return _jsRuntime.InvokeVoidAsyncWithErrorHandling("resizeListenerFactory.cancelListener", javaScriptListerId);
    }

    public ValueTask<bool> CancelListeners(Guid[] jsListenerIds)
    {
        return _jsRuntime.InvokeVoidAsyncWithErrorHandling("resizeListenerFactory.cancelListeners", jsListenerIds);
    }

    public ValueTask<BrowserWindowSize> GetBrowserWindowSize()
    {
        return _jsRuntime.InvokeAsync<BrowserWindowSize>("resizeListener.getBrowserWindowSize");
    }
}
