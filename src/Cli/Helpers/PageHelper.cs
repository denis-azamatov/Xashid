using Spectre.Console;

namespace Xashid.Cli.Helpers;

/// <summary>
/// Helper that contains common render components
/// </summary>
public static class PageHelper
{
    private static readonly Rule Header = new Rule("[palegreen1]Xashid[/]").RightAligned();

    /// <summary>
    /// Writes header
    /// </summary>
    public static void WriteHeader() => AnsiConsole.Write(Header);

    /// <summary>
    /// Reads value from console input
    /// </summary>
    /// <param name="defaultValue">Default value</param>
    /// <typeparam name="T">Type of result</typeparam>
    /// <returns>Read value converted to <typeparamref name="T"/></returns>
    public static T ReadFromTextPrompt<T>(T defaultValue)
    {
        var prompt = new TextPrompt<T>("Value to process")
            .DefaultValue(defaultValue)
            .ShowDefaultValue()
            .DefaultValueStyle(new Style(Color.PaleGreen1));
        return AnsiConsole.Prompt(prompt);
    }

    /// <summary>
    /// Shows spinner
    /// </summary>
    public static void ShowSpinner() => AnsiConsole
        .Status()
        .Spinner(Spinner.Known.Monkey)
        .SpinnerStyle(Style.Parse("palegreen1"))
        .Start("Processing...", ctx => { Thread.Sleep(600); });

    /// <summary>
    /// Writes input and result to console and copies result value to clipboard
    /// </summary>
    /// <param name="value">Input value</param>
    /// <param name="result">Result value</param>
    public static void WriteResultAndCopyToClipboard(string value, string result)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.PaleGreen1)
            .AddColumn(new TableColumn("Input"))
            .AddColumn(new TableColumn("Result"))
            .AddRow(value, result);
        AnsiConsole.Write(table);
        ClipboardHelper.Copy(result);
        AnsiConsole.MarkupLine("[bold]Result copied to [palegreen1]clipboard[/][/]");
        AnsiConsole.Write("Press any key...");
        Console.ReadKey(true);
    }
}