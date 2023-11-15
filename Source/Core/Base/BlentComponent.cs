using Microsoft.AspNetCore.Components;

namespace Craft.Blent.Base;

public abstract class BlentComponent : BaseBlentComponent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}
