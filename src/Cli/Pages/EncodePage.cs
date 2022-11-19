using Spectre.Console;
using Xashid.Cli.Helpers;
using Xashid.Core;

namespace Xashid.Cli.Pages;

/// <summary>
/// Encoding value page 
/// </summary>
public class EncodePage : Page
{
    /// <inheritdoc />
    public override void Invoke()
    {
        AnsiConsole.Clear();
        PageHelper.WriteHeader();
        var value = PageHelper.ReadFromTextPrompt(0);
        var encoded = HashidsEncoder.Encode(ConfigurationHelper.GetConfiguration(), value);
        PageHelper.ShowSpinner();
        PageHelper.WriteResultAndCopyToClipboard(value.ToString(), encoded);
    }
}