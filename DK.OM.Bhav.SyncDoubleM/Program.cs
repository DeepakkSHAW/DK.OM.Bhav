using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;

namespace DK.OM.Bhav.SyncDoubleM
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("--------------------DoubleM Synchronization started---------");
            var syncDate = new DateTime(2020, 04, 3);
            try
            {
                var dm = GetDataFromDoubleM(syncDate);
                Console.WriteLine($"DoubleM Stock Count: {dm.Count()}");

                var bse = GetDataFromBSE(syncDate);
                Console.WriteLine($"DoubleM Stock Count: {bse.Count()}");
                foreach (var dmItem in dm)
                {
                    Console.WriteLine(dmItem["BSECode"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            Console.WriteLine("--------------------DoubleM Synchronization Completed---------");
            Console.ReadKey();
        }
        private static List<DataRow> GetDataFromDoubleM(DateTime syncDate)
        {
            try
            {
                //*Ref: Current Running executable path
                var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                string file = $"{exePath}\\MDB\\Stock.mdb";
                var sqlQuary = $"SELECT TStockName.StockID, TStockName.BSECode, TRates.RateID, TRates.Price, TRates.Ondate FROM TStockName INNER JOIN TRates ON TStockName.StockID = TRates.StockID " +
                            $"WHERE(((TStockName.Active) = True) AND " +
                            $"((TRates.Ondate)>=#{syncDate.Month}/{syncDate.Day}/{syncDate.Year}#) AND " +
                            $"((TRates.Ondate)<#{syncDate.Month}/{syncDate.AddDays(1).Day}/{syncDate.Year}#));";
                var connetionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};User ID=Admin; Jet OLEDB:Database Password=Rups";
                //1/1/2020

                OdbcConnection odbcConnection = new OdbcConnection(connetionString);
                var _db = new OleDbConnection(connetionString);

                DataSet ds = new DataSet();

                using (OleDbConnection _dbConn = new OleDbConnection(connetionString))
                {
                    _dbConn.Open();
                    OleDbCommand cmd = new OleDbCommand(sqlQuary, _dbConn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    da.Fill(ds);

                }
                //Console.WriteLine($"Total Stock found {ds.Tables[0].Rows.Count}");
                //*Ref: Convert DataSet to DataRow List 
                List<DataRow> doubleMrows = ds.Tables[0].Rows.Cast<DataRow>().ToList();
                //foreach (var item in doubleMrows)
                //{
                //    Console.WriteLine(item[0]);
                //}
                return doubleMrows;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static List<DataRow> GetDataFromBSE(DateTime syncDate)
        {
            List<DataRow> rowsBSE = new List<DataRow>();
            try
            {
                //SELECT * FROM BSEStockPrices where OnDate >='2020-04-03 00:00:00'
                //var v = syncDate.ToString("yyyy-MM-dd hh:mm:ss");
                var sqlQuary = $"SELECT * FROM BSEStockPrices where OnDate>=" +
                    $"'{syncDate.ToString("yyyy-MM-dd")} 00:00:00' and OnDate <'{syncDate.AddDays(1).ToString("yyyy-MM-dd")} 00:00:00'";

                SQLiteConnection ObjConnection = new SQLiteConnection("Data Source=MDB/Bhav.db;");
                SQLiteCommand ObjCommand = new SQLiteCommand(sqlQuary, ObjConnection);
                ObjCommand.CommandType = CommandType.Text;
                SQLiteDataAdapter ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                DataSet ds = new DataSet();
                ObjDataAdapter.Fill(ds, "BSEStockPrices");
                //dataGridView1.DataSource = dataSet.Tables["Person"];
                rowsBSE = ds.Tables[0].Rows.Cast<DataRow>().ToList();
                return rowsBSE;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
