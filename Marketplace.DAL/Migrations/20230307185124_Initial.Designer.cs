// <auto-generated />
using System;
using Marketplace.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Marketplace.DAL.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230307185124_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Marketplace.Domain.Entity.Cart", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CartId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SubCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Category = 31,
                            DateCreate = new DateTime(2023, 3, 7, 20, 51, 24, 122, DateTimeKind.Local).AddTicks(5914),
                            Description = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                            Name = "Apple Air Pods",
                            OwnerName = "Admin",
                            Price = 0m,
                            SubCategory = 4314
                        },
                        new
                        {
                            Id = 2L,
                            Category = 31,
                            DateCreate = new DateTime(2023, 3, 7, 20, 51, 24, 122, DateTimeKind.Local).AddTicks(5945),
                            Description = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                            Name = "Pods",
                            OwnerName = "Admin",
                            Price = 0m,
                            SubCategory = 4314
                        },
                        new
                        {
                            Id = 3L,
                            Category = 31,
                            DateCreate = new DateTime(2023, 3, 7, 20, 51, 24, 122, DateTimeKind.Local).AddTicks(5947),
                            Description = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                            Name = "Air",
                            OwnerName = "Admin",
                            Price = 0m,
                            SubCategory = 4312
                        });
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Profile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Age = (byte)0,
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Admin",
                            Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                            Role = 2
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Moderator",
                            Password = "481f6cc0511143ccdd7e2d1b1b94faf0a700a8b49cd13922a70b5ae28acaa8c5",
                            Role = 1
                        });
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Cart", b =>
                {
                    b.HasOne("Marketplace.Domain.Entity.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("Marketplace.Domain.Entity.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Order", b =>
                {
                    b.HasOne("Marketplace.Domain.Entity.Cart", "Cart")
                        .WithMany("Orders")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Profile", b =>
                {
                    b.HasOne("Marketplace.Domain.Entity.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Marketplace.Domain.Entity.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.Cart", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Marketplace.Domain.Entity.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
