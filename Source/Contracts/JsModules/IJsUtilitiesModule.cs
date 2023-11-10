using Craft.Blent.Models;
using Microsoft.AspNetCore.Components;

namespace Craft.Blent.Contracts.JsModules;

public interface IJsUtilitiesModule : IJsModule
{
    ValueTask AddClass(ElementReference elementRef, string classname);

    ValueTask RemoveClass(ElementReference elementRef, string classname);

    ValueTask ToggleClass(ElementReference elementRef, string classname);

    ValueTask AddClassToBody(string classname);

    ValueTask RemoveClassFromBody(string classname);

    ValueTask<bool> ParentHasClass(ElementReference elementRef, string classname);

    ValueTask Focus(ElementReference elementRef, string elementId, bool scrollToElement);

    ValueTask Select(ElementReference elementRef, string elementId, bool focus);

    ValueTask ShowPicker(ElementReference elementRef, string elementId);

    ValueTask ScrollAnchorIntoView(string anchorTarget);

    ValueTask ScrollElementIntoView(string elementId, bool smooth = true);

    ValueTask SetCaret(ElementReference elementRef, int caret);

    ValueTask<int> GetCaret(ElementReference elementRef);

    ValueTask SetTextValue(ElementReference elementRef, object value);

    ValueTask SetProperty(ElementReference elementRef, string property, object value);

    ValueTask<DomElement> GetElementInfo(ElementReference elementRef, string elementId);

    ValueTask<string> GetUserAgent();

    ValueTask CopyToClipboard(ElementReference elementRef, string elementId);

    ValueTask Log(string message, params string[] args);
}
