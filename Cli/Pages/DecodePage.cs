using Spectre.Console;
using Xashid.Cli.Helpers;
using Xashid.Core;

namespace Xashid.Cli.Pages;

/// <summary>
/// Decoding value page
/// </summary>
public class DecodePage : Page
{
    /// <inheritdoc />
    public override void Invoke()
    {
        AnsiConsole.Clear();
        PageHelper.WriteHeader();
        string value;
        int decoded;
        while (true)
        {
            value = PageHelper.ReadFromTextPrompt("");
            try
            {
                decoded = HashidsEncoder.Decode(ConfigurationHelper.GetConfiguration(), value);
                break;
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine($"[indianred1]Invalid value[/] {value}");
            }
        }

        PageHelper.ShowSpinner();
        PageHelper.WriteResultAndCopyToClipboard(value, decoded.ToString());
    }
}