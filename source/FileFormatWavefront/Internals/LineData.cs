namespace FileFormatWavefront.Internals
{
    /// <summary>
    /// This helper class aids us in dealing with Wavefront line data.
    /// </summary>
    internal static class LineData
    {
        public static string StripComments(string line)
        {
            //  If there's a comment character, remove everything that follows it.
            var commentStartIndex = line.IndexOf('#');
            return commentStartIndex == -1 ? line : line.Substring(0, commentStartIndex);
        }

        public static bool TryReadLineType(string line, out string lineType, out string lineData)
        {
            //  The line type is the first set of characters in the line, such as:
            //  vt, vn, v etc.
            //  Remember - lines can have whitespace.
            var strippedLine = line.TrimStart();

            //  Loop through the string until we find the first whitespace character - everything before it 
            //  is the line type.
            for(var i=0; i<strippedLine.Length; i++)
            {
                if(char.IsWhiteSpace(strippedLine[i]))
                {
                    //  Bingo, we have whitespace - this means we can return a line type.
                    lineType = strippedLine.Substring(0, i);
                    lineData = strippedLine.Substring(i).Trim();
                    return true;
                }
            }

            //  We've not found any whitespace, so there's something wrong with this line.
            lineType = null;
            lineData = null;
            return false;
        }
    }
}