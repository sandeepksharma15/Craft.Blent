'use strict';

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

var ResizeListener = (function () {
    function ResizeListener(id) {
        _classCallCheck(this, ResizeListener);

        this.logger = function (message) {};
        this.options = {};
        this.throttleResizeHandlerId = -1;
        this.dotnet = undefined;
        this.breakpoint = -1;
        this.id = id;
        this.handleResize = this.throttleResizeHandler.bind(this);
    }

    _createClass(ResizeListener, [{
        key: 'listenForResize',
        value: function listenForResize(dotnetRef, options) {
            if (this.dotnet) {
                this.options = options;
                return;
            }

            this.options = options;
            this.dotnet = dotnetRef;
            this.logger = options.enableLogging ? console.log : function (message) {};
            this.logger('[Craft.Blent] Reporting resize events at rate of: ' + ((this.options || {}).reportRate || 100) + 'ms');
            window.addEventListener("resize", this.handleResize, false);
            if (!this.options.suppressInitEvent) {
                this.resizeHandler();
            }
            this.breakpoint = this.getBreakpoint(window.innerWidth);
        }
    }, {
        key: 'throttleResizeHandler',
        value: function throttleResizeHandler() {
            clearTimeout(this.throttleResizeHandlerId);
            this.throttleResizeHandlerId = window.setTimeout(this.resizeHandler.bind(this), (this.options || {}).reportRate || 100);
        }
    }, {
        key: 'resizeHandler',
        value: function resizeHandler() {
            if (this.options.notifyOnBreakpointOnly) {
                var bp = this.getBreakpoint(window.innerWidth);
                if (bp == this.breakpoint) {
                    return;
                }
                this.breakpoint = bp;
            }

            try {
                if (this.id) {
                    this.dotnet.invokeMethodAsync('RaiseOnResized', {
                        height: window.innerHeight,
                        width: window.innerWidth
                    }, this.getBreakpoint(window.innerWidth), this.id);
                } else {
                    this.dotnet.invokeMethodAsync('RaiseOnResized', {
                        height: window.innerHeight,
                        width: window.innerWidth
                    }, this.getBreakpoint(window.innerWidth));
                }
            } catch (error) {
                this.logger("[Craft.Blent] Error in resizeHandler:", { error: error });
            }
        }
    }, {
        key: 'cancelListener',
        value: function cancelListener() {
            this.dotnet = undefined;
            window.removeEventListener("resize", this.handleResize);
        }
    }, {
        key: 'matchMedia',
        value: function matchMedia(query) {
            var m = window.matchMedia(query).matches;
            return m;
        }
    }, {
        key: 'getBrowserWindowSize',
        value: function getBrowserWindowSize() {
            return {
                height: window.innerHeight,
                width: window.innerWidth
            };
        }
    }, {
        key: 'getBreakpoint',
        value: function getBreakpoint(width) {
            if (width >= this.options.breakpointDefinitions["Xxl"]) return 5;else if (width >= this.options.breakpointDefinitions["Xl"]) return 4;else if (width >= this.options.breakpointDefinitions["Lg"]) return 3;else if (width >= this.options.breakpointDefinitions["Md"]) return 2;else if (width >= this.options.breakpointDefinitions["Sm"]) return 1;else //Xs
                return 0;
        }
    }]);

    return ResizeListener;
})();

;

window.resizeListener = new ResizeListener();

window.resizeListenerFactory = {
    mapping: {},
    listenForResize: function listenForResize(dotnetRef, options, id) {
        var map = window.resizeListenerFactory.mapping;
        if (map[id]) {
            return;
        }

        var listener = new ResizeListener(id);
        listener.listenForResize(dotnetRef, options);
        map[id] = listener;
    },

    cancelListener: function cancelListener(id) {
        var map = window.resizeListenerFactory.mapping;

        if (!map[id]) {
            return;
        }

        var listener = map[id];
        listener.cancelListener();
        delete map[id];
    },

    cancelListeners: function cancelListeners(ids) {
        for (var i = 0; i < ids.length; i++) {
            window.resizeListenerFactory.cancelListener(ids[i]);
        }
    }
};

