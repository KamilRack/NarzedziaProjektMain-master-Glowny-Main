﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Narzedzia.Data;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231110124803_narzedziaedit1")]
    partial class narzedziaedit1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Narzedzia.Models.Awaria", b =>
                {
                    b.Property<int>("IdAwaria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAwaria"), 1L, 1);

                    b.Property<DateTime>("DataPrzyjecia")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionAwaria")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NarzedzieId")
                        .HasColumnType("int");

                    b.Property<string>("NotatkaTechniczna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberAwaria")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UzytkownikId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UzytkownikRealizujacyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ZdjecieFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAwaria");

                    b.HasIndex("NarzedzieId");

                    b.HasIndex("UzytkownikId");

                    b.HasIndex("UzytkownikRealizujacyId");

                    b.ToTable("Awarie");
                });

            modelBuilder.Entity("Narzedzia.Models.Kategoria", b =>
                {
                    b.Property<int>("KategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategoriaId"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NazwaKategorii")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("KategoriaId");

                    b.ToTable("Kategorie");
                });

            modelBuilder.Entity("Narzedzia.Models.Narzedzie", b =>
                {
                    b.Property<int>("NarzedzieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NarzedzieId"), 1L, 1);

                    b.Property<DateTime>("DataPrzyjecia")
                        .HasColumnType("datetime2");

                    b.Property<int>("KategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("NumerNarzedzia")
                        .HasColumnType("int");

                    b.Property<int>("ProducentId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("UsunZdjecie")
                        .HasColumnType("bit");

                    b.Property<string>("Uwagi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UzytkownikId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ZdjecieFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NarzedzieId");

                    b.HasIndex("KategoriaId");

                    b.HasIndex("ProducentId");

                    b.HasIndex("UzytkownikId");

                    b.ToTable("Narzedzia");
                });

            modelBuilder.Entity("Narzedzia.Models.Producent", b =>
                {
                    b.Property<int>("ProducentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProducentId"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NazwaProducenta")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ProducentId");

                    b.ToTable("Producenci");
                });

            modelBuilder.Entity("Narzedzia.Models.Stanowisko", b =>
                {
                    b.Property<int>("StanowiskoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StanowiskoId"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NazwaStanowiska")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("StanowiskoId");

                    b.ToTable("Stanowiska");
                });

            modelBuilder.Entity("Narzedzia.Models.Uzytkownik", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Imie")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nazwisko")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("NrKontrolny")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StanowiskoId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("WydzialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("StanowiskoId");

                    b.HasIndex("WydzialId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Narzedzia.Models.Wydzial", b =>
                {
                    b.Property<int>("WydzialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WydzialId"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NazwaWydzialu")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("WydzialId");

                    b.ToTable("Wydzialy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Narzedzia.Models.Uzytkownik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Narzedzia.Models.Uzytkownik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Narzedzia.Models.Uzytkownik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Narzedzia.Models.Uzytkownik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Narzedzia.Models.Awaria", b =>
                {
                    b.HasOne("Narzedzia.Models.Narzedzie", "Narzedzie")
                        .WithMany("Awarie")
                        .HasForeignKey("NarzedzieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Narzedzia.Models.Uzytkownik", "Uzytkownicy")
                        .WithMany("Awarie")
                        .HasForeignKey("UzytkownikId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Narzedzia.Models.Uzytkownik", "UzytkownikRealizujacy")
                        .WithMany()
                        .HasForeignKey("UzytkownikRealizujacyId");

                    b.Navigation("Narzedzie");

                    b.Navigation("Uzytkownicy");

                    b.Navigation("UzytkownikRealizujacy");
                });

            modelBuilder.Entity("Narzedzia.Models.Narzedzie", b =>
                {
                    b.HasOne("Narzedzia.Models.Kategoria", "Kategorie")
                        .WithMany("Narzedzia")
                        .HasForeignKey("KategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Narzedzia.Models.Producent", "Producenci")
                        .WithMany("Narzedzia")
                        .HasForeignKey("ProducentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Narzedzia.Models.Uzytkownik", "Uzytkownicy")
                        .WithMany("Narzedzia")
                        .HasForeignKey("UzytkownikId");

                    b.Navigation("Kategorie");

                    b.Navigation("Producenci");

                    b.Navigation("Uzytkownicy");
                });

            modelBuilder.Entity("Narzedzia.Models.Uzytkownik", b =>
                {
                    b.HasOne("Narzedzia.Models.Stanowisko", "Stanowiska")
                        .WithMany("Uzytkownicy")
                        .HasForeignKey("StanowiskoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Narzedzia.Models.Wydzial", "Wydzialy")
                        .WithMany("Uzytkownicy")
                        .HasForeignKey("WydzialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stanowiska");

                    b.Navigation("Wydzialy");
                });

            modelBuilder.Entity("Narzedzia.Models.Kategoria", b =>
                {
                    b.Navigation("Narzedzia");
                });

            modelBuilder.Entity("Narzedzia.Models.Narzedzie", b =>
                {
                    b.Navigation("Awarie");
                });

            modelBuilder.Entity("Narzedzia.Models.Producent", b =>
                {
                    b.Navigation("Narzedzia");
                });

            modelBuilder.Entity("Narzedzia.Models.Stanowisko", b =>
                {
                    b.Navigation("Uzytkownicy");
                });

            modelBuilder.Entity("Narzedzia.Models.Uzytkownik", b =>
                {
                    b.Navigation("Awarie");

                    b.Navigation("Narzedzia");
                });

            modelBuilder.Entity("Narzedzia.Models.Wydzial", b =>
                {
                    b.Navigation("Uzytkownicy");
                });
#pragma warning restore 612, 618
        }
    }
}
