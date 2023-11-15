namespace Craft.Blent.Services.Browser;

public class ResizeOptions : IEquatable<ResizeOptions>
{
    /// <summary>
    /// Rate in milliseconds that the browsers `resize()` event should report a change.
    /// Setting this value too low can cause poor application performance.
    /// Default value is <c>100</c>.
    /// </summary>
    public int ReportRate { get; set; } = 100;

    /// <summary>
    /// Report resize events and media queries in the browser's console.
    /// Default value is <c>false</c>.
    /// </summary>
    public bool EnableLogging { get; set; } = false;

    /// <summary>
    /// Suppress the first OnResized that is invoked when a new event handler is added.
    /// Default value is <c>true</c>.
    /// </summary>
    public bool SuppressInitEvent { get; set; } = true;

    /// <summary>
    /// If true, RaiseOnResized is called only when breakpoint has changed.
    /// Default value is <c>true</c>.
    /// </summary>
    public bool NotifyOnBreakpointOnly { get; set; } = true;

    /// <summary>
    /// Gets or sets the breakpoint definitions, representing specific breakpoints and their associated width.
    /// </summary>
    /// <value>
    /// A dictionary where each entry represents a breakpoint, and the corresponding <c>int</c> value represents the width.
    /// </value>
    /// <remarks>
    /// When the dictionary is null or empty, the default breakpoint definitions will be used.
    /// </remarks>
    public Dictionary<Breakpoint, int>? BreakpointDefinitions { get; set; } = [];

    public static bool operator ==(ResizeOptions? left, ResizeOptions? right)
    {
        if (ReferenceEquals(left, right)) return true;

        if (left is null) return false;
        if (right is null) return false;

        return left.Equals(right);
    }

    public static bool operator !=(ResizeOptions left, ResizeOptions right) => !(left == right);

    public override bool Equals(object? obj) => obj is ResizeOptions options && Equals(options);

    public bool Equals(ResizeOptions? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (ReportRate != other.ReportRate ||
            EnableLogging != other.EnableLogging ||
            SuppressInitEvent != other.SuppressInitEvent ||
            NotifyOnBreakpointOnly != other.NotifyOnBreakpointOnly)
            return false;

        if (BreakpointDefinitions is null)
            return other.BreakpointDefinitions is null;

        if (other.BreakpointDefinitions is null || BreakpointDefinitions.Count != other.BreakpointDefinitions.Count)
            return false;

        foreach (var breakpoint in BreakpointDefinitions.Keys)
            if (!other.BreakpointDefinitions.TryGetValue(breakpoint, out var otherWidth) || BreakpointDefinitions[breakpoint] != otherWidth)
                return false;

        return true;
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(ReportRate);
        hashCode.Add(EnableLogging);
        hashCode.Add(SuppressInitEvent);
        hashCode.Add(NotifyOnBreakpointOnly);
        hashCode.Add(ReportRate);

        if (BreakpointDefinitions is not null)
            foreach (var pair in BreakpointDefinitions)
            {
                hashCode.Add(pair.Key);
                hashCode.Add(pair.Value);
            }

        return hashCode.ToHashCode();
    }
}

internal static class ResizeOptionsExtensions
{
    /// <summary>
    /// Clones the <paramref name="options"/> object by creating a new instance of <see cref="ResizeOptions"/> with the same property values.
    /// </summary>
    /// <param name="options">The <see cref="ResizeOptions"/> object to clone.</param>
    /// <returns>A new instance of <see cref="ResizeOptions"/> with the same property values as the original object.</returns>
    internal static ResizeOptions Clone(this ResizeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        return new ResizeOptions
        {
            BreakpointDefinitions = (options.BreakpointDefinitions ?? []).ToDictionary(entry => entry.Key, entry => entry.Value),
            EnableLogging = options.EnableLogging,
            NotifyOnBreakpointOnly = options.NotifyOnBreakpointOnly,
            ReportRate = options.ReportRate,
            SuppressInitEvent = options.SuppressInitEvent
        };
    }
}
