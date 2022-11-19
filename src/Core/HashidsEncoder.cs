using HashidsNet;

namespace Xashid.Core;

/// <summary>
/// Hashids encoder
/// </summary>
public class HashidsEncoder
{
    private readonly Hashids _encoder;

    public HashidsEncoder(HashidsEncoderConfiguration configuration)
    {
        _encoder = new Hashids(
            configuration.Get(Key.Salt),
            configuration.GetInt(Key.MinHashLength),
            configuration.Get(Key.Alphabet),
            configuration.Get(Key.Seps));
    }

    /// <summary>
    /// Encodes number to hashid 
    /// </summary>
    /// <param name="number">Input value</param>
    /// <returns>Encoded value</returns>
    public string Encode(int number) => _encoder.Encode(number);

    /// <summary>
    /// Decodes string to number
    /// </summary>
    /// <param name="encoded">Input value</param>
    /// <returns>Decoded value</returns>
    public int Decode(string encoded) => _encoder.Decode(encoded).FirstOrDefault();

    /// <summary>
    /// Encodes number to hashid 
    /// </summary>
    /// <param name="value">Input value</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Encoded value</returns>
    public static string Encode(HashidsEncoderConfiguration configuration, int value) => new HashidsEncoder(configuration).Encode(value);

    /// <summary>
    /// Decodes string to number
    /// </summary>
    /// <param name="value">Input value</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Decoded value</returns>
    public static int Decode(HashidsEncoderConfiguration configuration, string value) => new HashidsEncoder(configuration).Decode(value);
}