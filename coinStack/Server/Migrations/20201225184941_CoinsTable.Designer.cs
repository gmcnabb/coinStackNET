﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using coinStack.Server.Data;

namespace coinStack.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201225184941_CoinsTable")]
    partial class CoinsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("coinStack.Shared.Coin", b =>
                {
                    b.Property<int>("CoinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("WatchlistId")
                        .HasColumnType("int");

                    b.Property<string>("id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CoinId");

                    b.HasIndex("WatchlistId");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("coinStack.Shared.Watchlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Watchlists");
                });

            modelBuilder.Entity("coinStack.Shared.Coin", b =>
                {
                    b.HasOne("coinStack.Shared.Watchlist", null)
                        .WithMany("Coins")
                        .HasForeignKey("WatchlistId");
                });

            modelBuilder.Entity("coinStack.Shared.Watchlist", b =>
                {
                    b.Navigation("Coins");
                });
#pragma warning restore 612, 618
        }
    }
}
