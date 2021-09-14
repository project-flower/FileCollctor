using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileCollector
{
    public class MainEngine : Cancellable
    {
        #region Public Classes

        public struct Options
        {
            public string Destination;
            public bool DirectoryTree;
            public string Encoding;
            public string Filter;
            public bool Recursive;
            public bool RegExpression;
            public string Source;
        }

        #endregion

        #region Private Fields

        private PageAnalyzer pageAnalyzer = new PageAnalyzer();

        #endregion

        #region Public Properties

        public new bool CancellationRequired
        {
            get
            {
                return base.CancellationRequired;
            }

            set
            {
                pageAnalyzer.CancellationRequired = value;
                base.CancellationRequired = value;
            }
        }

        #endregion

        #region Public Methods

        public void Collect(Options options)
        {
            CancellationRequired = false;
            DirectoryInfo sourceDirectoryInfo;
            string source = options.Source;

            try
            {
                var uri = new Uri(source);

                if (uri.Port != -1)
                {
                    pageAnalyzer.Collect(options);
                    return;
                }
            }
            catch
            {
                throw;
            }

            try
            {
                sourceDirectoryInfo = new DirectoryInfo(source);
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
                        throw new CancelledException();
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
