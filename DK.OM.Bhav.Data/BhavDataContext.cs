using System;
using System.IO;
using DK.OM.Bhav.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DK.OM.Bhav.Data
{
    public class BhavDataContext : DbContext
    {
        public DbSet<BhavBSEStocks> Stocks { get; set; }
        public DbSet<BhavBSEStockPrices> Prices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnection = string.Empty;
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
                dbConnection = configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                //TODO: get configuration from DI
            }
            //System.Diagnostics.Debug.WriteLine(dbConnection);

            optionsBuilder.UseSqlite(dbConnection, options => options.MaxBatchSize(512));
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
