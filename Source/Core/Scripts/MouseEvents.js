var CraftBlent = [];

window.CraftBlent = {
    addContextMenu: function (id, ref) {
        var el = document.getElementById(id);

        console.log('addContextMenu', id, el);

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

        console.log('addMouseEnter', id, el);

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

        console.log('addMouseLeave', id, el);

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

        console.log('removeContextMenu', id, el);

        if (el && CraftBlent[id + 'contextmenu']) {
            el.removeEventListener('contextmenu', CraftBlent[id + 'contextmenu']);
        }
    },
    removeMouseEnter: function (id) {
        var el = document.getElementById(id);

        console.log('removeMouseEnter', id, el);

        if (el && CraftBlent[id + 'mouseenter']) {
            el.removeEventListener('mouseenter', CraftBlent[id + 'mouseenter']);
        }
    },
    removeMouseLeave: function (id) {
        var el = document.getElementById(id);

        console.log('removeMouseLeave', id, el);

        if (el && CraftBlent[id + 'mouseleave']) {
            el.removeEventListener('mouseleave', CraftBlent[id + 'mouseleave']);
        }
    },
}