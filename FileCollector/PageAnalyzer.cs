using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static FileCollector.MainEngine;

namespace FileCollector
{
    public class PageAnalyzer : Cancellable
    {
        #region Private Fields

        private readonly Downloader downloader = new Downloader();
        private readonly string pathSeparator = Path.AltDirectorySeparatorChar.ToString();

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
                downloader.CancellationRequired = value;
                base.CancellationRequired = true;
            }
        }

        #endregion

        #region Public Methods

        public void Collect(Options options)
        {
            string source = options.Source;
            string path;

            try
            {
                path = UriManager.GetPath(new Uri(source));
            }
            catch
            {
                throw;
            }

            Collect(source, options, new List<string>() { path });
        }

        #endregion

        #region Private Methods

        private void Collect(string source, Options options, List<string> analyzedPaths)
        {
            CancellationRequired = true;
            Encoding encoding;

            try
            {
                encoding = Encoding.GetEncoding(options.Encoding);
            }
            catch
            {
                throw;
            }

            Uri sourceUri;
            string contents;
            string sourcePath;

            try
            {
                sourceUri = new Uri(source);
                Downloader.DownloadResult result = downloader.DownloadPage(sourceUri.ToString(), encoding);
                contents = result.Contents;
                sourcePath = UriManager.GetPath(sourceUri);
            }
            catch
            {
                throw;
            }

            MatchCollection matches = Regex.Matches(contents, "<\\s*a\\s+[^>]*href\\s*=\\s*\"(?<link>[^\"]+)\"", RegexOptions.IgnoreCase);
            var links = new List<string>();
            var files = new List<string>();

            foreach (Match match in matches)
            {
                string link = match.Groups["link"].Value;
                Uri linkUri;

                try
                {
                    linkUri = new Uri(link);
                }
                catch (UriFormatException)
                {
                    linkUri = new Uri(sourceUri, link);
                }

                try
                {
                    string path = UriManager.GetPath(linkUri);

                    if (sourcePath.Contains(path))
                    {
                        // 上位階層
                        continue;
                    }

                    string fileName;

                    try
                    {
                        fileName = UriManager.ParseLocalFileName(UriManager.GetFileName(linkUri));
                    }
                    catch
                    {
                        continue;
                    }

                    if (fileName.EndsWith(pathSeparator))
                    {
                        if (!analyzedPaths.Contains(path))
                        {
                            links.Add(path);
                            continue;
                        }
                    }

                    bool matched;
                    string filter = options.Filter;

                    if (options.RegExpression)
                    {
                        matched = Regex.IsMatch(fileName, filter);
                    }
                    else
                    {
                        matched = fileName.Contains(filter);
                    }

                    if (matched)
                    {
                        files.Add(linkUri.ToString());
                    }
                }
                catch (CancelledException)
                {
                    throw;
                }
                catch
                {
                    continue;
                }
            }

            foreach (string file in files)
            {
                try
                {
                    string destination = options.Destination;

                    if (string.IsNullOrEmpty(destination))
                    {
                        destination = Directory.GetCurrentDirectory();
                    }

                    if (!Directory.Exists(destination))
                    {
                        Directory.CreateDirectory(destination);
                    }

                    downloader.Download(file, options.Destination);
                }
                catch (CancelledException)
                {
                    throw;
                }
                catch
                {
                    continue;
                }
            }

            if (!options.Recursive)
            {
                return;
            }

            foreach (string link in links)
            {
                if (analyzedPaths.Contains(link))
                {
                    continue;
                }

                analyzedPaths.Add(link);

                try
                {
                    Collect(link, options, analyzedPaths);
                }
                catch (CancelledException)
                {
                    throw;
                }
                catch
                {
                    continue;
                }
            }
        }

        #endregion
    }
}
