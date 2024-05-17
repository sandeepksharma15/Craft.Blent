class ResizeListener {

    constructor(id) {
        this.logger = function (message) { };
        this.options = {};
        this.throttleResizeHandlerId = -1;
        this.dotnet = undefined;
        this.breakpoint = -1;
        this.id = id;
        this.handleResize = this.throttleResizeHandler.bind(this);
    }

    listenForResize(dotnetRef, options) {
        if (this.dotnet) {
            this.options = options;
            return;
        }

        this.options = options;
        this.dotnet = dotnetRef;
        this.logger = options.enableLogging ? console.log : (message) => { };
        this.logger(`[Craft.Blent] Reporting resize events at rate of: ${(this.options || {}).reportRate || 100}ms`);
        window.addEventListener("resize", this.handleResize, false);
        if (!this.options.suppressInitEvent) {
            this.resizeHandler();
        }
        this.breakpoint = this.getBreakpoint(window.innerWidth);
    }

    throttleResizeHandler() {
        this.logger("[Craft.Blent] Throttled resize event");
        clearTimeout(this.throttleResizeHandlerId);
        this.throttleResizeHandlerId = window.setTimeout(this.resizeHandler.bind(this), ((this.options || {}).reportRate || 100));
    }

    resizeHandler() {
        this.logger("[Craft.Blent] Resize event");
        if (this.options.notifyOnBreakpointOnly) {
            this.logger("[Craft.Blent] Notify on breakpoint only");
            let bp = this.getBreakpoint(window.innerWidth);
            if (bp == this.breakpoint) {
                return;
            }
            this.breakpoint = bp;
        }

        try {
            if (this.id) {
                this.logger("[Craft.Blent] Invoking dotnet method with id:", this.id);
                this.dotnet.invokeMethodAsync('BrowserViewportService.RaiseOnResized',
                    {
                        height: window.innerHeight,
                        width: window.innerWidth
                    },
                    this.getBreakpoint(window.innerWidth),
                    this.id);
            }
            else {
                this.logger("[Craft.Blent] Invoking dotnet method");
                this.dotnet.invokeMethodAsync('BrowserViewportService.RaiseOnResized',
                    {
                        height: window.innerHeight,
                        width: window.innerWidth
                    },
                    this.getBreakpoint(window.innerWidth));
            }

        } catch (error) {
            this.logger("[Craft.Blent] Error in resizeHandler:", { error });
        }
    }

    cancelListener() {
        this.logger("[Craft.Blent] Canceling listener");
        this.dotnet = undefined;
        window.removeEventListener("resize", this.handleResize);
    }

    matchMedia(query) {
        this.logger("[Craft.Blent] Match media:", query);
        let m = window.matchMedia(query).matches;
        return m;
    }

    getBrowserWindowSize() {
        this.logger("[Craft.Blent] Get browser window size");
        return {
            height: window.innerHeight,
            width: window.innerWidth
        };
    }

    getBreakpoint(width) {
        this.logger("[Craft.Blent] Get breakpoint for width:", width);
        if (width >= this.options.breakpointDefinitions["FullHD"])
            return 5;
        else if (width >= this.options.breakpointDefinitions["Widescreen"])
            return 4;
        else if (width >= this.options.breakpointDefinitions["Desktop"])
            return 3;
        else if (width >= this.options.breakpointDefinitions["Tablet"])
            return 2;
        else if (width >= this.options.breakpointDefinitions["Mobile"])
            return 1;
        else
            return 0;
    }
};

window.resizeListener = new ResizeListener();

window.resizeListenerFactory = {
    mapping: {},
    listenForResize: (dotnetRef, options, id) => {
        var map = window.resizeListenerFactory.mapping;
        if (map[id]) {
            return;
        }

        var listener = new ResizeListener(id);
        listener.listenForResize(dotnetRef, options);
        map[id] = listener;
    },

    cancelListener: (id) => {
        var map = window.resizeListenerFactory.mapping;

        if (!map[id]) {
            return;
        }

        var listener = map[id];
        listener.cancelListener();
        delete map[id];
    },

    cancelListeners: (ids) => {
        for (let i = 0; i < ids.length; i++) {
            window.resizeListenerFactory.cancelListener(ids[i]);
        }
    }
}
