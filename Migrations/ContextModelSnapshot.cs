﻿// <auto-generated />
using System;
using FarmProduceManagement.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FarmProduceManagement.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Admin", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("CompanyWallet")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescriptionOfCategory")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NameOfCategory")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Wallet")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Farmer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FarmerRegStatus")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Wallet")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.FarmerManager", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FarmerId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("FarmerManagers");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Manager", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.OrderProduct", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProductId")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Produce", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProduceName")
                        .HasColumnType("longtext");

                    b.Property<double>("QuantityToBuy")
                        .HasColumnType("double");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Produces");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProduceName")
                        .HasColumnType("longtext");

                    b.Property<double>("QuantityToSell")
                        .HasColumnType("double");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UnitOfMeasurement")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FarmerId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TransactionNum")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.TransactionProduce", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProduceId")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<string>("TransactionId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProduceId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionProduces");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Email", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Admin", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Customer", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Farmer", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.FarmerManager", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Farmer", "Farmer")
                        .WithMany("FarmerManagers")
                        .HasForeignKey("FarmerId");

                    b.HasOne("FarmProduceManagement.Models.Entities.Manager", "Manager")
                        .WithMany("FarmerManagers")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Farmer");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Manager", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Order", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.OrderProduct", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId");

                    b.HasOne("FarmProduceManagement.Models.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Produce", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Category", "Category")
                        .WithMany("Produces")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Product", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Transaction", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Farmer", "Farmer")
                        .WithMany("Transactions")
                        .HasForeignKey("FarmerId");

                    b.HasOne("FarmProduceManagement.Models.Entities.Manager", null)
                        .WithMany("Transactions")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.TransactionProduce", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Produce", "Produce")
                        .WithMany("TransactionProduces")
                        .HasForeignKey("ProduceId");

                    b.HasOne("FarmProduceManagement.Models.Entities.Transaction", "Transaction")
                        .WithMany("TransactionProduces")
                        .HasForeignKey("TransactionId");

                    b.Navigation("Produce");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.User", b =>
                {
                    b.HasOne("FarmProduceManagement.Models.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Category", b =>
                {
                    b.Navigation("Produces");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Farmer", b =>
                {
                    b.Navigation("FarmerManagers");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Manager", b =>
                {
                    b.Navigation("FarmerManagers");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Produce", b =>
                {
                    b.Navigation("TransactionProduces");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("FarmProduceManagement.Models.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionProduces");
                });
#pragma warning restore 612, 618
        }
    }
}
