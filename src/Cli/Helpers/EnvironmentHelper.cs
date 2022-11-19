using System.Diagnostics;
using Xashid.Core;

namespace Xashid.Cli.Helpers;

/// <summary>
/// Contains environment values
/// </summary>
public static class EnvironmentValues
{
    /// <summary>
    /// Platform independent Application data path
    /// </summary>
    public static string ApplicationData =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Process.GetCurrentProcess().ProcessName);

    /// <summary>
    /// Platform independent Configuration file path
    /// </summary>
    public static string ConfigPath =>
        Path.Combine(ApplicationData, HashidsEncoderConfiguration.ConfigPath);
}