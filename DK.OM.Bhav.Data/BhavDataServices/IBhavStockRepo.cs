using DK.OM.Bhav.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DK.OM.Bhav.Data.BhavDataServices
{
    public interface IBhavStockRepo
    {
        Task<int> AddBSEStockAsync(BhavStocks BSEStock);
        Task<IEnumerable<BhavStocks>> GetStocksAsync();
        Task<BhavStocks> GetBSEStockAsync(string BSECode);
        Task AddNewBSEStockesAsync(List<BSEDownloadCSVType> bseCSVTypes);
        Task AddLatestBSEStockPrice(List<BSEDownloadCSVType> bseCSVTypes);
        Task AddNewNSEStockesAsync(List<NSEDownloadCSVType> nseCSVTypes, bool isInBSE);
        Task<bool> BulkNewExpensesAsync(List<BhavStocks> BSEStocks);
        

    }
}
