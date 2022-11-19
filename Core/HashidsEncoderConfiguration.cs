namespace Xashid.Core;

/// <summary>
/// Encoder configuration 
/// </summary>
public class HashidsEncoderConfiguration
{
    /// <summary>
    /// Configuration file path
    /// </summary>
    public const string ConfigPath = "user.config";

    public const string DefaultAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    public const string DefaultSeps = "cfhistuCFHISTU";

    /// <summary>
    /// Contains configuration parameters
    /// </summary>
    public Dictionary<string, string> Configuration { get; set; } = new()
    {
        { Key.Salt, "" },
        { Key.MinHashLength, "0" },
        { Key.Alphabet, DefaultAlphabet },
        { Key.Seps, DefaultSeps },
    };

    /// <summary>
    /// Sets value of parameter by key
    /// </summary>
    /// <param name="key">Parameter key</param>
    /// <param name="value">New value</param>
    public void Set(Key key, string value) => Configuration[key] = value;

    /// <summary>
    /// Reads configuration parameter by key
    /// </summary>
    /// <param name="key">Parameter key</param>
    /// <returns>Parameter value</returns>
    public string Get(Key key) => Configuration[key];

    
    /// <summary>
    /// Reads configuration parameter by key and converts value to <see cref="int"/>
    /// </summary>
    /// <param name="key">Parameter key</param>
    /// <returns>Parameter value</returns>
    public int GetInt(Key key) => int.Parse(Configuration[key]);
}