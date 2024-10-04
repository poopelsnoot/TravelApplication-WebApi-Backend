﻿// <auto-generated />
using System;
using DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    [DbContext(typeof(csMainDbContext.SqlServerDbContext))]
    partial class SqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbModels.csAddressDbM", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Zip")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.HasIndex("Street", "Zip", "City", "Country")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DbModels.csAttractionDbM", b =>
                {
                    b.Property<Guid>("AttractionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AttractionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("AttractionId");

                    b.HasIndex("AddressId");

                    b.HasIndex("AttractionName", "Category", "Description")
                        .IsUnique();

                    b.ToTable("Attractions");
                });

            modelBuilder.Entity("DbModels.csCommentDbM", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttractionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentId");

                    b.HasIndex("AttractionId");

                    b.HasIndex("UserId");

                    b.HasIndex("Comment", "Date");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DbModels.csUserDbM", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("FirstName", "LastName", "Age")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DbModels.csAttractionDbM", b =>
                {
                    b.HasOne("DbModels.csAddressDbM", "AddressDbM")
                        .WithMany("AttractionsDbM")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressDbM");
                });

            modelBuilder.Entity("DbModels.csCommentDbM", b =>
                {
                    b.HasOne("DbModels.csAttractionDbM", "AttractionDbM")
                        .WithMany("CommentsDbM")
                        .HasForeignKey("AttractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbModels.csUserDbM", "UserDbM")
                        .WithMany("CommentDbM")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttractionDbM");

                    b.Navigation("UserDbM");
                });

            modelBuilder.Entity("DbModels.csAddressDbM", b =>
                {
                    b.Navigation("AttractionsDbM");
                });

            modelBuilder.Entity("DbModels.csAttractionDbM", b =>
                {
                    b.Navigation("CommentsDbM");
                });

            modelBuilder.Entity("DbModels.csUserDbM", b =>
                {
                    b.Navigation("CommentDbM");
                });
#pragma warning restore 612, 618
        }
    }
}
