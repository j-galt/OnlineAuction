﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OnlineAuction.Infrastructure;
using System;

namespace OnlineAuction.Infrastructure.Migrations
{
    [DbContext(typeof(OnlineAuctionDbContext))]
    [Migration("20180511192125_SeedReferenceData")]
    partial class SeedReferenceData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineAuction.Core.Entities.Bid", b =>
                {
                    b.Property<int>("BidID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("BidTime");

                    b.Property<int>("LotID");

                    b.Property<int>("UserID");

                    b.HasKey("BidID");

                    b.HasIndex("LotID");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("OnlineAuction.Core.Entities.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<int?>("ParentCategoryID");

                    b.HasKey("CategoryID");

                    b.HasIndex("ParentCategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OnlineAuction.Core.Entities.Lot", b =>
                {
                    b.Property<int>("LotID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("LotName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("LotStateID");

                    b.Property<decimal>("MinBid");

                    b.Property<int>("SellerID");

                    b.Property<decimal>("StartPrice");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("LotID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("LotStateID");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("OnlineAuction.Core.Entities.LotState", b =>
                {
                    b.Property<int>("LotStateID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LotStateName");

                    b.HasKey("LotStateID");

                    b.ToTable("LotStates");
                });

            modelBuilder.Entity("OnlineAuction.Core.Entities.Bid", b =>
                {
                    b.HasOne("OnlineAuction.Core.Entities.Lot")
                        .WithMany("Bids")
                        .HasForeignKey("LotID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineAuction.Core.Entities.Category", b =>
                {
                    b.HasOne("OnlineAuction.Core.Entities.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryID");
                });

            modelBuilder.Entity("OnlineAuction.Core.Entities.Lot", b =>
                {
                    b.HasOne("OnlineAuction.Core.Entities.Category")
                        .WithMany("Lots")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineAuction.Core.Entities.LotState", "LotState")
                        .WithMany()
                        .HasForeignKey("LotStateID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
