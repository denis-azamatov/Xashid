namespace Xashid.Cli.Pages;

/// <summary>
/// Base class for pages
/// </summary>
public abstract class Page
{
    /// <summary>
    /// Page execution endpoint
    /// </summary>
    public abstract void Invoke();
}