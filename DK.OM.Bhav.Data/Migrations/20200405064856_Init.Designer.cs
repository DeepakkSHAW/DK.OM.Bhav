﻿// <auto-generated />
using System;
using DK.OM.Bhav.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DK.OM.Bhav.Data.Migrations
{
    [DbContext(typeof(BhavDataContext))]
    [Migration("20200405064856_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("DK.OM.Bhav.Data.BhavDataModels.BhavStockPrices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BhavStockId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Close")
                        .HasColumnType("REAL");

                    b.Property<double>("High")
                        .HasColumnType("REAL");

                    b.Property<double>("Last")
                        .HasColumnType("REAL");

                    b.Property<double>("Low")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("OnDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Open")
                        .HasColumnType("REAL");

                    b.Property<double>("PreClose")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("DK.OM.Bhav.Data.BhavDataModels.BhavStocks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BSECode")
                        .HasColumnType("TEXT");

                    b.Property<int?>("BhavStockPricesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NSECode")
                        .HasColumnType("TEXT");

                    b.Property<string>("StockFullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StockGroup")
                        .HasColumnType("TEXT");

                    b.Property<string>("StockName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StockType")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("inDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BhavStockPricesId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("DK.OM.Bhav.Data.BhavDataModels.BhavStocks", b =>
                {
                    b.HasOne("DK.OM.Bhav.Data.BhavDataModels.BhavStockPrices", null)
                        .WithMany("BhavStocks")
                        .HasForeignKey("BhavStockPricesId");
                });
#pragma warning restore 612, 618
        }
    }
}
