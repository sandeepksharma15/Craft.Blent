namespace Craft.Blent.Services.Browser;

internal static class GlobalBreakpointOptions
{
    /// <summary>
    /// Default  breakpoint definitions
    /// </summary>
    internal static Dictionary<Breakpoint, int> DefaultBreakpointDefinitions { get; set; } = new()
    {
        [Breakpoint.Xxl] = 1920,
        [Breakpoint.Xl] = 1600,
        [Breakpoint.Lg] = 1200,
        [Breakpoint.Md] = 900,
        [Breakpoint.Sm] = 600,
        [Breakpoint.Xs] = 0,
    };

    /// <summary>
    /// Retrieves the default or user-defined breakpoint definitions based on the provided <paramref name="options"/>.
    /// If user-defined breakpoint definitions are available in the <paramref name="options"/>, a copy is returned to prevent unintended modifications.
    /// Otherwise, the default <see cref="DefaultBreakpointDefinitions"/> breakpoint definitions are returned.
    /// </summary>
    /// <param name="options">The resize options containing breakpoint definitions, if any.</param>
    /// <returns>A dictionary containing the breakpoint definitions.</returns>
    internal static Dictionary<Breakpoint, int> GetDefaultOrUserDefinedBreakpointDefinition(ResizeOptions options)
    {
        if (options.BreakpointDefinitions is not null && options.BreakpointDefinitions.Count != 0)
            // Copy as we don't want any unexpected modification
            return options.BreakpointDefinitions.ToDictionary(entry => entry.Key, entry => entry.Value);

        return DefaultBreakpointDefinitions.ToDictionary(entry => entry.Key, entry => entry.Value);
    }
}
