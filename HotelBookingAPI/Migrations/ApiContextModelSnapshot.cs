﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViberMessageSenderAPI.Data;

#nullable disable

namespace ViberMessageSenderAPI.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ViberMessageSenderAPI.Models.PhoneReceiver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdReceiver")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PhoneReceivers");
                });

            modelBuilder.Entity("ViberMessageSenderAPI.Models.PhoneSender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneReceiverId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhoneReceiverId");

                    b.ToTable("PhoneSenders");
                });

            modelBuilder.Entity("ViberMessageSenderAPI.Models.PhoneSender", b =>
                {
                    b.HasOne("ViberMessageSenderAPI.Models.PhoneReceiver", "PhoneReceiver")
                        .WithMany()
                        .HasForeignKey("PhoneReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PhoneReceiver");
                });
#pragma warning restore 612, 618
        }
    }
}
