window.CraftBlent = {
    addContextMenu: function (id, ref) {
        var el = document.getElementById(id);
        if (el) {
            var handler = function (e) {
                e.stopPropagation();
                e.preventDefault();
                ref.invokeMethodAsync('BaseBlentComponent.RaiseContextMenu',
                    {
                        ClientX: e.clientX,
                        ClientY: e.clientY,
                        ScreenX: e.screenX,
                        ScreenY: e.screenY,
                        AltKey: e.altKey,
                        ShiftKey: e.shiftKey,
                        CtrlKey: e.ctrlKey,
                        MetaKey: e.metaKey,
                        Button: e.button,
                        Buttons: e.buttons,
                    });
                return false;
            };
            CraftBlent[id + 'contextmenu'] = handler;
            el.addEventListener('contextmenu', handler, false);
        }
    },
    addMouseEnter: function (id, ref) {
        var el = document.getElementById(id);
        if (el) {
            var handler = function (e) {
                ref.invokeMethodAsync('BaseBlentComponent.RaiseMouseEnter');
            };
            CraftBlent[id + 'mouseenter'] = handler;
            el.addEventListener('mouseenter', handler, false);
        }
    },
    addMouseLeave: function (id, ref) {
        var el = document.getElementById(id);
        if (el) {
            var handler = function (e) {
                ref.invokeMethodAsync('BaseBlentComponent.RaiseMouseLeave');;
            };
            CraftBlent[id + 'mouseleave'] = handler;
            el.addEventListener('mouseleave', handler, false);
        }
    },
    removeContextMenu: function (id) {
        var el = document.getElementById(id);
        if (el && CraftBlent[id + 'contextmenu']) {
            el.removeEventListener('contextmenu', Radzen[id + 'contextmenu']);
        }
    },
    removeMouseEnter: function (id) {
        var el = document.getElementById(id);
        if (el && CraftBlent[id + 'mouseenter']) {
            el.removeEventListener('mouseenter', Radzen[id + 'mouseenter']);
        }
    },
    removeMouseLeave: function (id) {
        var el = document.getElementById(id);
        if (el && CraftBlent[id + 'mouseleave']) {
            el.removeEventListener('mouseleave', Radzen[id + 'mouseleave']);
        }
    },
}