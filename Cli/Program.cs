using System.Text;
using Spectre.Console;
using Xashid.Cli.Helpers;
using Xashid.Cli.Pages;
using Xashid.Core;

namespace Xashid.Cli;

class Program
{
    public static void Main(string[] args)
    {
        CheckConfiguration();

        if (HandleArgs(args))
            return;

        while (true)
        {
            AnsiConsole.Clear();

            PageHelper.WriteHeader();

            var exit = ProcessOperation(SelectOperation());
            if (exit) return;
        }
    }

    /// <summary>
    /// Inspects configuration exists. otherwise opens configuration page
    /// </summary>
    private static void CheckConfiguration()
    {
        var configExists = File.Exists(EnvironmentValues.ConfigPath);
        if (!configExists)
        {
            new ConfigurationPage().Invoke();
        }
    }

    /// <summary>
    /// Prints and handle operation selection
    /// </summary>
    /// <returns>Operation type to process</returns>
    private static Operation SelectOperation()
    {
        var selection = new SelectionPrompt<Operation>()
            .Title("Select operation:")
            .HighlightStyle(Style.Parse("palegreen1"))
            .AddChoices(Operation.Encode, Operation.Decode, Operation.ChangeParameters, Operation.Exit)
            .UseConverter(x =>
            {
                var val = Enum.GetName(x) ?? throw new Exception("Operation name error");
                var sb = new StringBuilder();
                foreach (var symbol in val)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(symbol);
                        continue;
                    }

                    if (char.IsUpper(symbol))
                    {
                        sb.Append(' ');
                    }

                    sb.Append(symbol);
                }

                return sb.ToString();
            });
        return AnsiConsole.Prompt(selection);
    }

    /// <summary>
    /// Processes the operation and invokes corresponding page
    /// </summary>
    /// <param name="operation">Operation to process</param>
    /// <returns>true for application close</returns>
    /// <exception cref="ArgumentOutOfRangeException">Unknown operation type</exception>
    private static bool ProcessOperation(Operation operation)
    {
        switch (operation)
        {
            case Operation.Encode:
                new EncodePage().Invoke();
                return false;
            case Operation.Decode:
                new DecodePage().Invoke();
                return false;
            case Operation.ChangeParameters:
                new ConfigurationPage().Invoke();
                return false;
            case Operation.Exit:
                return true;
            default:
                throw new ArgumentOutOfRangeException(nameof(operation), "Unknown operation");
        }
    }

    /// <summary>
    /// Handles args for fast encoding and decoding
    /// </summary>
    /// <param name="args">Application arguments</param>
    /// <returns>true for application close</returns>
    private static bool HandleArgs(IReadOnlyCollection<string> args)
    {
        if (args.Count == 0)
            return false;

        if (args.Count != 2)
        {
            PrintUsage();
            return true;
        }

        PageHelper.ShowSpinner();

        var mode = args.First();
        var value = args.Skip(1).First();

        switch (mode)
        {
            case "-e":
                try
                {
                    var encoder = new HashidsEncoder(ConfigurationHelper.GetConfiguration());
                    var encoded = encoder.Encode(int.Parse(value));
                    AnsiConsole.MarkupLine($"Result copied to [palegreen1]clipboard[/]: {encoded}");
                    ClipboardHelper.Copy(encoded);
                }
                catch (Exception)
                {
                    AnsiConsole.MarkupLine($"[indianred1]Invalid value[/] {value}");
                    return true;
                }

                break;
            case "-d":
                try
                {
                    var decoder = new HashidsEncoder(ConfigurationHelper.GetConfiguration());
                    var decoded = decoder.Decode(value);
                    AnsiConsole.MarkupLine($"Result copied to [palegreen1]clipboard[/]: {decoded}");
                    ClipboardHelper.Copy(decoded.ToString());
                }
                catch (Exception)
                {
                    AnsiConsole.MarkupLine($"[indianred1]Invalid value[/] {value}");
                    return true;
                }

                break;
            default:
                AnsiConsole.MarkupLine("[indianred1]Unknown mode argument. Use -e for encode or -d for decode[/]");
                break;
        }

        return true;
    }

    /// <summary>
    /// Prints application usage instruction
    /// </summary>
    private static void PrintUsage()
    {
        AnsiConsole.MarkupLine("[indianred1]Invalid arguments[/]");
        AnsiConsole.MarkupLine("Usage:");
        AnsiConsole.MarkupLine("\tApplication: ");
        AnsiConsole.MarkupLine("\t\txashid");
        AnsiConsole.MarkupLine("\tFast encoding and decoding:");
        AnsiConsole.MarkupLine("\t\txashid -[[mode]] [[value]]");
        AnsiConsole.MarkupLine("\t\t\tmodes: -e=encode -d=decode");
    }
}