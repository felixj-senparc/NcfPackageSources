﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Senparc.Xncf.DatabaseToolkit.Models.MultipleDatabase;

namespace Senparc.Xncf.DatabaseToolkit.Migrations.Migrations.MySql
{
    [DbContext(typeof(DatabaseToolkitEntities_MySql))]
    partial class DatabaseToolkitEntities_MySqlModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Senparc.Xncf.DatabaseToolkit.DbConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4")
                        .HasMaxLength(300);

                    b.Property<int>("BackupCycleMinutes")
                        .HasColumnType("int");

                    b.Property<string>("BackupPath")
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4")
                        .HasMaxLength(300);

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastBackupTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Remark")
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("DatabaseToolkitDbConfig");
                });
#pragma warning restore 612, 618
        }
    }
}
