using System.Diagnostics;

namespace Xashid.Cli.Helpers.Clipboard;

/// <summary>
/// Provides methods to run cli
/// </summary>
public static class Shell
{
    /// <summary>
    /// Runs bash
    /// </summary>
    /// <param name="cmd">Arguments</param>
    public static string Bash(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");
        string result = Run("/bin/bash", $"-c \"{escapedArgs}\"");
        return result;
    }

    /// <summary>
    /// Runs cmd
    /// </summary>
    /// <param name="cmd">Arguments</param>
    public static string Bat(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");
        string result = Run("cmd.exe", $"/c \"{escapedArgs}\"");
        return result;
    }

    /// <summary>
    /// Runs application with arguments 
    /// </summary>
    /// <param name="filename">Application name</param>
    /// <param name="arguments">Arguments</param>
    private static string Run(string filename, string arguments)
    {
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            }
        };
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return result;
    }
}