using System;
using System.Linq;
using System.Text;
using EFCore.BulkExtensions;
using System.Threading.Tasks;
using System.Collections.Generic;
using DK.OM.Bhav.Data.BhavDataServices;
using Microsoft.EntityFrameworkCore;
using DK.OM.Bhav.Models;

namespace DK.OM.Bhav.Data
{
    public class BhavStockRepo : IBhavStockRepo
    {
        private BhavDataContext _ctx;
        public BhavStockRepo() { _ctx = new BhavDataContext(); }
        //public string ADummyMethod(string s = null)
        //{
        //    string defaultName = "Deepak";
        //    return "Hello " + s ?? defaultName;
        //}
        public async Task<IEnumerable<BhavStocks>> GetStocksAsync()
        {
            try
            {
                var quary = from c in _ctx.Stocks select c;
                return await quary.ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<BhavStocks> GetBSEStockAsync(string BSECode)
        {
            var quary = await _ctx.Stocks
                .Where(x => x.BSECode == BSECode).FirstOrDefaultAsync();
            return quary;
        }
        public async Task<int> AddBSEStockAsync(BhavStocks BSEStock)
        {
            var result = -1;
            try
            {
                _ctx.Add(BSEStock);
                result = await _ctx.SaveChangesAsync();
                if (result < 1)
                    throw new Exception("Error occurred while adding new stock into database");
                result = BSEStock.Id;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public async Task AddNewBSEStockesAsync(List<BSEDownloadCSVType> bseCSVTypes)
        {
            var dbStocks = await GetStocksAsync();
            var bseStocks = new List<BhavStocks>();
            try
            {
                foreach (var bseItem in bseCSVTypes.Where(e => e.SC_TYPE == "Q"))
                {
                    var isExist = dbStocks.Any(e => e.BSECode.Contains(bseItem.SC_CODE.ToString()));
                    if (!isExist)
                    {
                        var newStock = new BhavStocks
                        {
                            BSECode = bseItem.SC_CODE.ToString(),
                            StockName = bseItem.SC_NAME.Trim(),
                            StockGroup = bseItem.SC_GROUP.Trim(),
                            StockType = "Q"
                        };
                        bseStocks.Add(newStock); //needed for Bulk Insert, if we use can it.
                        try
                        {
                            await AddBSEStockAsync(newStock);
                        }
                        catch (Exception ex)
                        {
                            //Log if any exception and continue
                            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                            throw;
                        }
                    }
                }
                //Ready to Bulk Insert of new stocks
                System.Diagnostics.Debug.WriteLine($"Number of new stocks Added: {bseStocks.Count()}");
                //if (bseStocks.Count() > 0)
                //{
                //    await BulkNewExpensesAsync(bseStocks);
                //}
                //else
                //    System.Diagnostics.Debug.WriteLine($"No new stocks found, could {bseStocks.Count()}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AddLatestBSEStockPrice(List<BSEDownloadCSVType> bseCSVTypes)
        {

        }
        public async Task AddNewNSEStockesAsync(List<NSEDownloadCSVType> nseCSVTypes, bool isInBSE)
        {
            //TODO: Not completed yet, need to change the approach probably, may be add add nse in sperate table and line bse-nse stocks with another table
            var dbStocks = await GetStocksAsync();
            var bseStocks = dbStocks.Select(e => e.StockName).ToList();
            var nseStocks = new List<BhavStocks>();
            try
            {
                foreach (var nseItem in nseCSVTypes.GroupBy(p => p.SYMBOL))
                {
                    var theKey = nseItem.Key.Trim().ToUpper();
                    //theKey = "E2E";
                    //var v = dbStocks.Any(e => e.NSECode == "NCC");
                    var isExist = dbStocks.Any(e => e.NSECode==theKey); //Check if its already in DB
                    if (!isExist)
                    {
                        if (isInBSE)
                        {
                            var found = CheckInBSEStocks(bseStocks, theKey);
                            Console.WriteLine($"{theKey},{found}");
                            switch (found)
                            {
                                case 0://No Match with BSE Code, Insert new stock with NSE details
                                    Console.WriteLine($"Not Found:{theKey}");
                                    break;
                                case 1://found uniques record compare to BSE, update the record with NSE code
                                    Console.WriteLine($"Match:{theKey}");
                                    break;
                                //some intelligence need to match the right one
                                case int n when (n > 1): //*Ref: range-based switch case
                                    Console.WriteLine($"Multi Match:{theKey}");
                                    break;
                                default://Log if only
                                    Console.WriteLine(theKey);
                                    break;
                            }
                        }
                        else
                        { }
                    }
                }

            }
            catch (Exception ex)
            {
                //Log if any exception and continue
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        public async Task<bool> BulkNewExpensesAsync(List<BhavStocks> BSEStocks)
        {
            bool vReturn = false;
            if (BSEStocks != null)
            {
                try
                {
                    //TODO: bulk Insert is not working properly, need to check
                    await _ctx.BulkInsertAsync(BSEStocks);
                    vReturn = true;
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return vReturn;
        }

        #region PRIVATE METHODS
        private int CheckInBSEStocks(List<String> bseStocks, string nseStocks)
        {
            var searchText = string.Empty;
            int ifound = 0;
            foreach (char c in nseStocks.Trim())
            {
                searchText = searchText + c;
                //var found = bseStocks.Where(e => e.Trim().ToUpper().Contains(searchText));
                var found = bseStocks.Where(e => e.Trim().ToUpper().StartsWith(searchText));
                System.Diagnostics.Debug.WriteLine(found.Count());
                if (found.Count() == 0) return ifound;
                ifound = found.Count();
                if (ifound == 1) return 1;
            }
            return ifound;
        }
        #endregion
    }
}
