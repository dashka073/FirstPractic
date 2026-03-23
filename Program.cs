using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChessExample;

public class CheckerBoardPosition(byte x, byte y) : IParsable<CheckerBoardPosition>
{

    [AllowedValues(1, 2, 3, 4, 5, 6, 7, 8)]
    public byte X { get; } = x is > 0 and <= 8 ? x : throw new ArgumentOutOfRangeException(nameof(x));

    [AllowedValues(1, 2, 3, 4, 5, 6, 7, 8)]
    public byte Y { get; } = y is > 0 and <= 8 ? y : throw new ArgumentOutOfRangeException(nameof(y));

    private const char LetterOffset = '@';

    public char XLetter => (char)(LetterOffset + X);

    public override string ToString() => $"{XLetter}{Y}";

    public static CheckerBoardPosition Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result)
            ? result
            : throw new FormatException($"Invalid {nameof(CheckerBoardPosition)}: {s}");

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [NotNullWhen(true)] out CheckerBoardPosition? result)
    {
        if (s is [var x and >= 'A' and <= 'H', var y and >= '1' and <= '8'])
        {
            result = new CheckerBoardPosition((byte)(x - LetterOffset), byte.Parse([y]));
            return true;
        }

        result = null;
        return false;
    }
}