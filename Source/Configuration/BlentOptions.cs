using Microsoft.Extensions.DependencyInjection;

namespace Craft.Blent.Configuration;

public class BlentOptions
{
    public BlentOptions(IServiceProvider serviceProvider, Action<BlentOptions> configureOptions)
    {
        Services = serviceProvider;
        configureOptions?.Invoke(this);
    }

    /// <summary>
    /// If true, the component that can control it's parent will automatically close it.
    /// </summary>
    public bool? AutoCloseParent { get; set; } = true;

    /// <summary>
    /// if true, the spin button will be shown on the numeric input component.
    /// </summary>
    public bool? ShowNumericStepButtons { get; set; } = true;

    /// <summary>
    /// If true, modal will keep focus inside it.
    /// </summary>
    public bool? ModalFocusTrap { get; set; } = true;

    /// <summary>
    /// Gets the service provider.
    /// </summary>
    public IServiceProvider Services { get; }
}
