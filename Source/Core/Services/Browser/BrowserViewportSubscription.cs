using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Craft.Blent.Services.Browser;

[DebuggerDisplay("{DebuggerToString(),nq}")]
internal class BrowserViewportSubscription(Guid javaScriptListenerId,
    Guid observerId,
    ResizeOptions? resizeOptions) : IEquatable<BrowserViewportSubscription>
{
    public Guid JavaScriptListenerId { get; } = javaScriptListenerId;

    public Guid ObserverId { get; } = observerId;

    public ResizeOptions? Options { get; } = resizeOptions;

    public BrowserViewportSubscription(Guid javaScriptListenerId, Guid observerId)
        : this(javaScriptListenerId, observerId, null)
    {
    }

    public bool Equals(BrowserViewportSubscription? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;

        return JavaScriptListenerId.Equals(other.JavaScriptListenerId) && ObserverId.Equals(other.ObserverId);
    }

    public override bool Equals(object? obj)
        => obj is BrowserViewportSubscription browserViewportSubscription && Equals(browserViewportSubscription);

    public override int GetHashCode()
        => HashCode.Combine(JavaScriptListenerId, ObserverId);

    [ExcludeFromCodeCoverage]
    private string DebuggerToString()
    {
        return $"JavaScript Listener Id = {JavaScriptListenerId}, Observer Id = {ObserverId}";
    }
}
