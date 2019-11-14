using System.Collections.Generic;

namespace Guide.Extensions
{
    public static class SimpleDatatypeExtensions
    {
        public static bool IsAsciiPrintable(this char c)
            => c >= 32 && c < 127;
    }
}
