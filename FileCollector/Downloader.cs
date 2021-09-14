using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace FileCollector
{
    public class Downloader : Cancellable
    {
        #region Public Classes

        public class DownloadResult
        {
            public string Address
            {
                get;
            }

            public string Contents
            {
                get;
            }

            public DownloadResult(string address, string contents)
            {
                Address = address;
                Contents = contents;
            }
        }

        #endregion

        #region Public Methods

        public Downloader()
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
            }
            catch
            {
            }
        }

        public void Download(string address, string directory)
        {
            CancellationRequired = false;

            try
            {
                var buffer = new byte[1024];

                using (WebResponse response = GetResponse(address))
                using (Stream stream = response.GetResponseStream())
                using (var fileStream = new FileStream(Path.Combine(directory, (UriManager.ParseLocalFileName(UriManager.GetFileName(new Uri(address))))), FileMode.Create))
                {
                    while (true)
                    {
                        if (CancellationRequired)
                        {
                            throw new CancelledException();
                        }

                        int length = stream.Read(buffer, 0, buffer.Length);

                        if (length < 1)
                        {
                            break;
                        }

                        fileStream.Write(buffer, 0, length);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public DownloadResult DownloadPage(string address, Encoding encoding)
        {
            string contents;

            try
            {
                using (WebResponse response = GetResponse(address))
                using (Stream stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream, encoding);
                    contents = reader.ReadToEnd();
                }
            }
            catch
            {
                throw;
            }

            return new DownloadResult(address, contents);
        }

        #endregion

        #region Private Methods

        private WebResponse GetResponse(string address)
        {
            try
            {
                var request = WebRequest.CreateHttp(address);
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:90.0) Gecko/20100101 Firefox/90.0";
                return request.GetResponse();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
