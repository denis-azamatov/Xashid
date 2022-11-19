using System.Reflection;
using Spectre.Console;
using Xashid.Cli.Helpers;
using Xashid.Core;

namespace Xashid.Cli.Pages;

/// <summary>
/// Configuration page
/// </summary>
public class ConfigurationPage : Page
{
    private readonly HashidsEncoderConfiguration _configuration;

    public ConfigurationPage()
    {
        _configuration = ConfigurationHelper.GetConfiguration();
    }

    /// <inheritdoc />
    public override void Invoke()
    {
        while (true)
        {
            AnsiConsole.Clear();
            PageHelper.WriteHeader();
            ListAllParameters();
            var parameter = SelectParameter();

            switch (parameter)
            {
                case "Back":
                    return;
                default:
                    AnsiConsole.Clear();
                    ChangeParameter(parameter);
                    break;
            }
        }
    }

    /// <summary>
    /// Lists all configuration parameters
    /// </summary>
    private void ListAllParameters()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.PaleGreen1)
            .AddColumn(new TableColumn("№"))
            .AddColumn(new TableColumn("Key"))
            .AddColumn(new TableColumn("Value"));

        AnsiConsole
            .Live(table)
            .Start(ctx =>
            {
                ctx.Refresh();
                foreach (var (idx, (key, value)) in Enumerable.Range(1, _configuration.Configuration.Count).Zip(_configuration.Configuration))
                {
                    Thread.Sleep(200);
                    table.AddRow(idx.ToString(), key, value);
                    ctx.Refresh();
                }
            });
    }

    /// <summary>
    /// Selects parameter to change
    /// </summary>
    /// <returns>Parameter key</returns>
    private string SelectParameter()
    {
        var choices = typeof(Key)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Select(x => (Key)(x.GetValue(null) ?? throw new Exception("Incorrect configuration")))
            .Select(x => x.Value)
            .Concat(new[] { "Back" });
        var prompt = new SelectionPrompt<string>()
            .AddChoices(choices)
            .HighlightStyle(Style.Parse("palegreen1"))
            .Title("Select parameter to change:");
        return AnsiConsole.Prompt(prompt);
    }

    /// <summary>
    /// Reads new parameter value and overwrites configuration
    /// </summary>
    /// <param name="parameter">Parameter key</param>
    private void ChangeParameter(string parameter)
    {
        PageHelper.WriteHeader();
        var key = new Key(parameter);
        var prompt = new TextPrompt<string>($"Enter new value for [palegreen1]{parameter}[/]: ")
            .DefaultValue(_configuration.Get(key))
            .DefaultValueStyle(Style.Parse("palegreen1"));
        var value = AnsiConsole.Prompt(prompt);
        _configuration.Set(key, value);
        ConfigurationHelper.SaveConfiguration(_configuration);
    }
}