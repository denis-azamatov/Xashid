using Xashid.Cli.Helpers.Clipboard;
using OperatingSystem = Xashid.Cli.Helpers.Clipboard.OperatingSystem;

namespace Xashid.Cli.Helpers;

/// <summary>
/// Helper to work with clipboard
/// </summary>
public static class ClipboardHelper
{
    /// <summary>
    /// Copies value to clipboard
    /// </summary>
    /// <param name="val">Value to copy</param>
    public static void Copy(string val)
    {
        if (OperatingSystem.IsWindows())
        {
            $"echo {val} | clip".Bat();
        }

        if (OperatingSystem.IsMacOS())
        {
            $"echo \"{val}\" | pbcopy".Bash();
        }
    }
}