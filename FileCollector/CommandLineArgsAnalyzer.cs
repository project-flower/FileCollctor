using System.Text.RegularExpressions;

namespace FileCollector
{
    public static class CommandLineArgsAnalyzer
    {
        #region Public Methods

        public static void Analyze(string[] commandLineArgs, ref MainEngine.Options options)
        {
            foreach (string arg in commandLineArgs)
            {
                if (GetOption(arg, ref options)) continue;
                if (GetOption('d', arg, ref options.Destination)) continue;
                if (GetOption('f', arg, ref options.Filter)) continue;
                if (GetOption('s', arg, ref options.Source)) continue;
            }
        }

        #endregion

        #region Private Methods

        private static bool GetOption(char key, string value, ref string result)
        {
            Match match;

            try
            {
                match = new Regex($"^/{key}:(?<value>.+)", RegexOptions.IgnoreCase).Match(value);
            }
            catch
            {
                return false;
            }

            if (match.Success)
            {
                result = match.Groups["value"].Value;
                return true;
            }

            return false;
        }

        private static bool GetOption(string argument, ref MainEngine.Options options)
        {
            Match match = Regex.Match(argument, "^/(?<value>\\w)$");

            if (!match.Success)
            {
                return false;
            }

            switch (match.Groups["value"].Value.ToLower())
            {
                case "e":
                    options.RegExpression = true;
                    break;
                case "r":
                    options.Recursive = true;
                    break;
                case "t":
                    options.DirectoryTree = true;
                    break;
                default:
                    return false;
            }

            return true;
        }

        #endregion
    }
}
