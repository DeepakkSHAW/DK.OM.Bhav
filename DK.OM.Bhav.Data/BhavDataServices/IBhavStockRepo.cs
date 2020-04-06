using DK.OM.Bhav.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DK.OM.Bhav.Data.BhavDataServices
{
    public interface IBhavStockRepo
    {
        Task<int> AddBSEStockAsync(BhavBSEStocks BSEStock);
        Task<IEnumerable<BhavBSEStocks>> GetStocksAsync();
        Task<BhavBSEStocks> GetBSEStockAsync(string BSECode);
        Task AddNewBSEStockesAsync(List<BSEDownloadCSVType> bseCSVTypes);
        Task AddLatestBSEStockPrice(List<BSEDownloadCSVType> bseCSVTypes);
        Task AddNewNSEStockesAsync(List<NSEDownloadCSVType> nseCSVTypes, bool isInBSE);
        Task<bool> BulkNewExpensesAsync(List<BhavBSEStocks> BSEStocks);
        

    }
}
