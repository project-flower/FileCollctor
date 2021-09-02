using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileCollector
{
    public static class MainEngine
    {
        #region Public Classes

        public struct Options
        {
            public string Destination;
            public bool DirectoryTree;
            public string Filter;
            public bool Recursive;
            public bool RegExpression;
            public string Source;
        }

        #endregion

        #region Private Fields

        private static bool cancellationRequired = false;
        private static bool cancelled = false;
        private static readonly object locker = new object();

        #endregion

        #region Public Properties

        public static bool CancellationRequired
        {
            get
            {
                bool result;

                lock (locker)
                {
                    result = cancellationRequired;
                }

                return result;
            }

            set
            {
                lock (locker)
                {
                    cancellationRequired = value;
                }
            }
        }

        public static bool Cancelled
        {
            get { return cancelled; }
        }

        #endregion

        #region Public Methods

        public static void Collect(Options options)
        {
            cancelled = false;
            DirectoryInfo sourceDirectoryInfo;

            try
            {
                sourceDirectoryInfo = new DirectoryInfo(options.Source);
            }
            catch
            {
                throw;
            }

            IEnumerable<FileInfo> files;
            SearchOption searchOption = (options.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            string filter = options.Filter;

            if (options.RegExpression)
            {
                try
                {
                    var regex = new Regex(filter, RegexOptions.Compiled);
                    files = sourceDirectoryInfo.EnumerateFiles("*", searchOption).Where(n => regex.IsMatch(n.Name));
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                string searchPattern;

                if (string.IsNullOrEmpty(filter))
                {
                    searchPattern = "*";
                }
                else
                {
                    searchPattern = filter;
                }

                try
                {
                    files = sourceDirectoryInfo.EnumerateFiles(searchPattern, searchOption);
                }
                catch
                {
                    throw;
                }
            }

            int rootDirectoryNameEndIndex = (sourceDirectoryInfo.FullName.Length + 1);

            try
            {
                string destination = options.Destination;

                foreach (FileInfo fileInfo in files)
                {
                    if (CancellationRequired)
                    {
                        cancelled = true;
                        return;
                    }

                    string destFileName;

                    if (options.DirectoryTree)
                    {
                        destFileName = Path.Combine(destination, fileInfo.FullName.Substring(rootDirectoryNameEndIndex));
                    }
                    else
                    {
                        destFileName = Path.Combine(destination, fileInfo.Name);
                    }

                    try
                    {
                        string directoryName = Path.GetDirectoryName(destFileName);

                        if (!Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        File.Copy(fileInfo.FullName, destFileName, true);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
