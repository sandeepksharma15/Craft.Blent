using Microsoft.JSInterop;

namespace Craft.Blent.Contracts.JsModules;

public interface IJsModule
{
    string ModuleFileName { get; }
    Task<IJSObjectReference> Module { get; }
}
