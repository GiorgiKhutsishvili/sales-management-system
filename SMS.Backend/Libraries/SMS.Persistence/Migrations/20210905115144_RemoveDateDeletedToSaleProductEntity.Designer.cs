﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMS.Persistence;

namespace SMS.Persistence.Migrations
{
    [DbContext(typeof(SMSDbContext))]
    [Migration("20210905115144_RemoveDateDeletedToSaleProductEntity")]
    partial class RemoveDateDeletedToSaleProductEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SMS.Core.Entities.ConsultantEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateChanged")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RecommendatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RecommendatorUniqueNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Consultants");
                });

            modelBuilder.Entity("SMS.Core.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateChanged")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(16,4)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SMS.Core.Entities.SaleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConsultantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateChanged")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("UniqueNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConsultantId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("SMS.Core.Entities.SaleProductEntity", b =>
                {
                    b.Property<Guid>("SaleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.HasKey("SaleId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("SalesProducts");
                });

            modelBuilder.Entity("SMS.Core.Entities.SaleEntity", b =>
                {
                    b.HasOne("SMS.Core.Entities.ConsultantEntity", "Consultant")
                        .WithMany("Sales")
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consultant");
                });

            modelBuilder.Entity("SMS.Core.Entities.SaleProductEntity", b =>
                {
                    b.HasOne("SMS.Core.Entities.ProductEntity", "Product")
                        .WithMany("SalesProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SMS.Core.Entities.SaleEntity", "Sale")
                        .WithMany("SalesProducts")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("SMS.Core.Entities.ConsultantEntity", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("SMS.Core.Entities.ProductEntity", b =>
                {
                    b.Navigation("SalesProducts");
                });

            modelBuilder.Entity("SMS.Core.Entities.SaleEntity", b =>
                {
                    b.Navigation("SalesProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
