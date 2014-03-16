using System;

namespace FileFormatWavefront.Extensions
{
    /// <summary>
    /// String extensions used internally just make make the comparison with a string
    /// and a line type a little cleaner.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Determines whether this string matches the specified line type.
        /// </summary>
        /// <param name="this">The string.</param>
        /// <param name="lineType">The line type.</param>
        /// <returns>True if the string matches the linetype.</returns>
        public static bool IsLineType(this string @this, string lineType)
        {
            return string.Equals(@this, lineType, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}