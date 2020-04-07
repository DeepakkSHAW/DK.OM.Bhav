using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using DK.OM.BhavCopyDownloader;
using Microsoft.Extensions.Configuration;
using DK.OM.Bhav.Data;
using DK.OM.Bhav.Models;

namespace DK.OM.Bhav
{
    public class DownloadConfig
    {
        public string BaseURL { get; set; }
        public string OutputFolder { get; set; }
        public string DateFormate { get; set; }
    }


    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Bhav Copy Download");
            DateTime dtOperation = new DateTime(2020, 04, 7);
            try
            {
                BhavStockRepo bhavStockSrv = new BhavStockRepo();

                //---------Configuration---------------//
                //string dbConn = configuration.GetSection("Position").GetSection("Title").Value;
                //var builder = new ConfigurationBuilder()
                //                .SetBasePath(Directory.GetCurrentDirectory())
                //                .AddJsonFile("appsettings.json");

                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                Console.WriteLine($" Version { config["Version"] } !");
                string dbConn = config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                //string dbConn1 = config.GetValue<string>("DBSetting:ConnectionString");
                //--------------------------------------------//

                //-------FOR BSE Stocks---------//
                var csvBSE = DownloadFromBSE(dtOperation);
                Console.WriteLine($"BSE Downloaded Stock Count: {csvBSE.Count()}");
                //Add new stock new stock from BSE//
                if (csvBSE.Count() > 0)
                {
                    await bhavStockSrv.AddNewBSEStockesAsync(csvBSE.ToList());
                    Console.WriteLine($"DB Stock Count: {(await bhavStockSrv.GetBSEStockAsync()).Count()}");
                    await bhavStockSrv.AddLatestBSEStockPrice(csvBSE.ToList(), dtOperation);
                }
                //-------FOR NSE Stocks---------//
                //var csvNSE = DownloadFromNSE();
                //Console.WriteLine($"NSE Downloaded Stock Count: {csvNSE.Count()}");
                //////Add new stock new stock from BSE//
                //await bhavStockSrv.AddNewNSEStockesAsync(csvNSE.ToList(), true);
                //Console.WriteLine($"DB Stock Count After NSE stock addition: {(await bhavStockSrv.GetStocksAsync()).Count()}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to close........");
            Console.ReadKey();
        }
        private static List<BSEDownloadCSVType> DownloadFromBSE(DateTime forDate)
        {
            IEnumerable<BSEDownloadCSVType> recordsBSE = new List<BSEDownloadCSVType>();
            //List<BSEDownloadCSVType> records = new List<BSEDownloadCSVType>();
            //Option: Process data from BSE
            try
            {
                var t = BhavCopy.BhavCopyDownloadFromBSE(forDate);
                t.Wait();
                var resultBSE = t.Result;
                //var resultBSE = Task.Run(async () => await BhavCopy.BhavCopyDownloadFromBSE(new DateTime(2020, 03, 12))).Result;
                if (resultBSE.Success)
                {
                    // unzip the file
                    var zipName = resultBSE.FileName;
                    var rawFileStream = File.OpenRead(zipName);
                    byte[] zippedtoTextBuffer = new byte[rawFileStream.Length];
                    rawFileStream.Read(zippedtoTextBuffer, 0, (int)rawFileStream.Length);

                    var csvFromBSE = Utilities.ZipHelper.Unzip(zippedtoTextBuffer);
                    //Console.WriteLine(csvFromBSE);
                    using (var reader = new StringReader(csvFromBSE))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        recordsBSE = csv.GetRecords<Models.BSEDownloadCSVType>().ToList();
                    }
                    //Console.WriteLine(records.Count());
                    //Console.WriteLine(recordsBSE.Count());
                }
                else
                {
                    Console.WriteLine(resultBSE.exception.Message);
                }
                return recordsBSE.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static List<NSEDownloadCSVType> DownloadFromNSE(DateTime forDate)
        {
            //Option1: Processing data downloaded from NSE
            //var resultNSE = await BhavCopy.BhavCopyDownloadFromNSE(new DateTime(2020, 02, 04));
            IEnumerable<NSEDownloadCSVType> recordsNSE = new List<NSEDownloadCSVType>();
            try
            {
                var t = BhavCopy.BhavCopyDownloadFromNSE(forDate);
                t.Wait();
                var resultNSE = t.Result;
                if (resultNSE.Success)
                {
                    // unzip the file
                    var zipName = resultNSE.FileName;
                    var rawFileStream = File.OpenRead(zipName);
                    byte[] zippedtoTextBuffer = new byte[rawFileStream.Length];
                    rawFileStream.Read(zippedtoTextBuffer, 0, (int)rawFileStream.Length);

                    var csvFromNSE = Utilities.ZipHelper.Unzip(zippedtoTextBuffer);
                    //Console.WriteLine(csvFromBSE);
                    using (var reader = new StringReader(csvFromNSE))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        recordsNSE = csv.GetRecords<NSEDownloadCSVType>().ToList();
                    }
                }
                else
                {
                    Console.WriteLine(resultNSE.exception.Message);
                }
                return recordsNSE.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
