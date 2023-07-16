using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace UserFriendlyUrlUsingGuid;

public static class Guider
{
    private const char EqualsChar = '=';
    private const char Hypen = '-';
    private const char Underscore = '_';
    private const char Slash = '/';
    private const byte SlashByte = (byte)'/';
    private const char Plus = '+';
    private const byte PlusByte = (byte)'+';

    public static string ToStringFromGuid(Guid guid)
    {
        Span<byte> idBytes = stackalloc byte[16];
        Span<byte> base64Bytes = stackalloc byte[24];

        MemoryMarshal.TryWrite(idBytes, ref guid);
        Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);

        Span<char> finalChars = stackalloc char[22]; // to exclude == 

        for (int i = 0; i < finalChars.Length; i++)
        {
            finalChars[i] = base64Bytes[i] switch
            {
                SlashByte => Hypen,
                PlusByte => Underscore,
                _ => (char)base64Bytes[i]
            };
        }

        return new string(finalChars);
    }

    public static Guid ToGuidFromString(ReadOnlySpan<char> id)
    {
        Span<char> base64Chars = stackalloc char[24];

        for (int i = 0; i < 22; i++)
        {
            base64Chars[i] = id[i] switch
            {
                Hypen => Slash,
                Underscore => Plus,
                _ => id[i]
            };
        }

        base64Chars[22] = EqualsChar;
        base64Chars[23] = EqualsChar;

        Span<byte> idBytes = stackalloc byte[16];
        Convert.TryFromBase64Chars(base64Chars, idBytes, out _);

        return new Guid(idBytes);
    }
}
