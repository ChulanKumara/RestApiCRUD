﻿// <auto-generated />
using EmissionBreakdownApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmissionBreakdownApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Atmoz.EmissionBreakdownApi.Models.EmissionBreakdownRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SubCategoryId")
                        .HasColumnType("TEXT");

                    b.Property<double>("TonsOfCO2")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("EmissionBreakdownRow");
                });

            modelBuilder.Entity("Atmoz.EmissionBreakdownApi.Models.EmissionCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmissionCategory");
                });

            modelBuilder.Entity("Atmoz.EmissionBreakdownApi.Models.EmissionSubCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmissionSubCategory");
                });

            modelBuilder.Entity("Atmoz.EmissionBreakdownApi.Models.EmissionBreakdownRow", b =>
                {
                    b.HasOne("Atmoz.EmissionBreakdownApi.Models.EmissionCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Atmoz.EmissionBreakdownApi.Models.EmissionSubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId");

                    b.Navigation("Category");

                    b.Navigation("SubCategory");
                });
#pragma warning restore 612, 618
        }
    }
}