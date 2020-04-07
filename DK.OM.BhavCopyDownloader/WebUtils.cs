using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DK.OM.BhavCopyDownloader
{
    internal static class WebUtils
    {
        internal static void WebClientDonload(Uri uri, string filename)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    //TODO: Check how to utilize async method DownloadFileAsync
                    //client.DownloadFileAsync(uri, filename);
                    //await Task.Run(() => client.DownloadFileAsync(uri, filename));
                    client.DownloadFile(uri, filename);
                }
            }
            catch (Exception ex)
            {
                //Log if any exception and continue
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }
        internal static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var value = e.ProgressPercentage;
            System.Diagnostics.Debug.WriteLine($"Download in progress:[{value}%]");
        }
    }
}
