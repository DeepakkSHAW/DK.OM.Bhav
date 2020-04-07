using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DK.OM.BhavCopyDownloader
{
    public static class BhavCopy
    {
        //NSE - BhavCopy
        //https://www1.nseindia.com/products/content/equities/equities/archieve_eq.htm
        //https://www1.nseindia.com/content/historical/EQUITIES/2020/MAR/cm04MAR2020bhav.csv.zip
        public static async Task<(bool Success, string FileName, Exception exception)> BhavCopyDownloadFromNSE(DateTime dt)
        {
            //await GetAttachment(1);
            //filename = string.IsNullOrEmpty(filename) ? DateTime.Now.ToString("dd-MMM-yy") + ".zip" : filename + ".zip";
            try
            {
                string day = dt.ToString("dd");
                string month = dt.ToString("MMM").ToUpper();
                string year = dt.ToString("yyyy");
                string filename = $"cm{day}{month}{year}bhav.csv.zip";

                //https://www1.nseindia.com/content/historical/EQUITIES/2020/FEB/cm04FEB2020bhav.csv.zip";
                var uri = new Uri(@"https://" + $"www1.nseindia.com/content/historical/EQUITIES/{year}/{month}/{filename}");
                //await WebUtils.WebClientDonload(uri, filename);
                await Task.Run(() => WebUtils.WebClientDonload(uri, filename));
                return (true, filename, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex);
            }
        }

        //BSE - BhavCopy
        //https://www.bseindia.com/markets/MarketInfo/BhavCopy.aspx
        //https://www.bseindia.com/download/BhavCopy/Equity/EQ_ISINCODE_030420.zip
        //https://www.bseindia.com/download/BhavCopy/Equity/EQ030420_CSV.ZIP
        public static async Task<(bool Success, string FileName, Exception exception)> BhavCopyDownloadFromBSE(DateTime dt)
        {
            try
            {
                string day = dt.ToString("dd");
                string month = dt.ToString("MM");
                string year = dt.ToString("yy");
                string filename = $"EQ{day}{month}{year}_CSV.ZIP";
                //https://www.bseindia.com/download/BhavCopy/Equity/EQ030420_CSV.ZIP
                var uri = new Uri(@"https://" + $"www.bseindia.com/download/BhavCopy/Equity/{filename}");
                ////await WebUtils.WebClientDonload(uri, filename);
                await Task.Run(() => WebUtils.WebClientDonload(uri, filename));
                return (true, filename, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex);
            }
        }

        private static async Task GetAttachment(int FileID)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "www1.nseindia.com/content/historical/EQUITIES/2020/MAR/cm04MAR2020bhav.csv.zip";
            //https://www1.nseindia.com/content/historical/EQUITIES/2020/MAR/cm04MAR2020bhav.csv.zip
            var Path = "/files/download";
            uriBuilder.Path = Path;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriBuilder.ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Add("authorization", access_token); //if any
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(uriBuilder.ToString());

                if (response.IsSuccessStatusCode)
                {
                    System.Net.Http.HttpContent content = response.Content;
                    var contentStream = await content.ReadAsStreamAsync(); // get the actual content stream
                                                                           // return File(contentStream, content_type, filename);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
        }
    }
}
