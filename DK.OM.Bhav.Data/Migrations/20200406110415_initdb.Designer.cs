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
    [Migration("20200406110415_initdb")]
    partial class initdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("DK.OM.Bhav.Models.BhavBSEStockPrices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BhavBSEStockId")
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

                    b.Property<long>("Turnover")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BSEStockPrices");
                });

            modelBuilder.Entity("DK.OM.Bhav.Models.BhavBSEStocks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BSECode")
                        .HasColumnType("TEXT");

                    b.Property<int?>("BhavBSEStockPricesId")
                        .HasColumnType("INTEGER");

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

                    b.HasIndex("BhavBSEStockPricesId");

                    b.ToTable("BSEStocks");
                });

            modelBuilder.Entity("DK.OM.Bhav.Models.BhavBSEStocks", b =>
                {
                    b.HasOne("DK.OM.Bhav.Models.BhavBSEStockPrices", null)
                        .WithMany("BhavStocks")
                        .HasForeignKey("BhavBSEStockPricesId");
                });
#pragma warning restore 612, 618
        }
    }
}