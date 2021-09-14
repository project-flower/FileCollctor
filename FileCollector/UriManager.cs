using System;
using System.Text.RegularExpressions;

namespace FileCollector
{
    public static class UriManager
    {
        #region Public Methods

        public static string GetFileName(Uri uri)
        {
            string[] segments = uri.Segments;
            int segmentsLength = segments.Length;

            if (segmentsLength < 1)
            {
                return uri.ToString();
            }

            return segments[segmentsLength - 1];
        }

        public static string GetPath(Uri uri)
        {
            try
            {
                return uri.GetLeftPart(UriPartial.Path);
            }
            catch
            {
                throw;
            }
        }

        public static string ParseLocalFileName(string fileName)
        {
            return Regex.Replace(fileName, "%(?<value>\\w{2})", ReplaceToLocalFileName);
        }

        #endregion

        #region Private Methods

        private static string ReplaceToLocalFileName(Match match)
        {
            char c;

            try
            {
                c = Convert.ToChar(Convert.ToInt32(match.Groups["value"].Value, 16));
            }
            catch
            {
                return match.Value;
            }

            return c.ToString();
        }

        #endregion
    }
}
