namespace Xashid.Core;

/// <summary>
/// Configuration parameter key
/// </summary>
/// <param name="Value">Key value</param>
public readonly record struct Key(string Value)
{
    public static implicit operator string(Key key) => key.Value;

    public static Key Salt => new(nameof(Salt));
    public static Key MinHashLength => new(nameof(MinHashLength));
    public static Key Alphabet => new(nameof(Alphabet));
    public static Key Seps => new(nameof(Seps));
}