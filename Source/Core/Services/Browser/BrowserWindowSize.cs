﻿namespace Craft.Blent.Services.Browser;

public class BrowserWindowSize : EventArgs
{
    /// <summary>
    /// Gets or sets the height of the browser window.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the width of the browser window.
    /// </summary>
    public int Width { get; set; }
}
