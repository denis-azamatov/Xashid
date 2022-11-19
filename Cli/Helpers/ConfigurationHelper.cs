using System.Text.Json;
using Xashid.Core;

namespace Xashid.Cli.Helpers;

/// <summary>
/// Helper to work with configuration
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// Reads configuration from disk, if not exists creates new with default values
    /// </summary>
    /// <returns>Configuration</returns>
    public static HashidsEncoderConfiguration GetConfiguration()
    {
        try
        {
            var cfgJson = File.ReadAllText(EnvironmentValues.ConfigPath);
            return JsonSerializer.Deserialize<HashidsEncoderConfiguration>(cfgJson) ?? throw new Exception("Incorrect of empty configuration");
        }
        catch (FileNotFoundException)
        {
            var cfg = new HashidsEncoderConfiguration();
            SaveConfiguration(cfg);
            return cfg;
        }
        catch (DirectoryNotFoundException)
        {
            var cfg = new HashidsEncoderConfiguration();
            SaveConfiguration(cfg);
            return cfg;
        }
    }

    /// <summary>
    /// Updates or saves configuration to disk
    /// </summary>
    /// <param name="configuration">Configuration to save</param>
    public static void SaveConfiguration(HashidsEncoderConfiguration configuration)
    {
        var cfgJson = JsonSerializer.Serialize(configuration);
        if (!Directory.Exists(EnvironmentValues.ApplicationData))
        {
            Directory.CreateDirectory(EnvironmentValues.ApplicationData);
        }

        File.WriteAllText(EnvironmentValues.ConfigPath, cfgJson);
    }
}