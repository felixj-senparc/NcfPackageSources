﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Senparc.Xncf.PromptRange.Models;

#nullable disable

namespace Senparc.Xncf.PromptRange.Domain.Migrations.PostgreSQL
{
    [DbContext(typeof(PromptRangeSenparcEntities_PostgreSQL))]
    partial class PromptRangeSenparcEntities_PostgreSQLModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Senparc.Xncf.PromptRange.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AdditionNote")
                        .HasColumnType("text");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("Blue")
                        .HasColumnType("integer");

                    b.Property<bool>("Flag")
                        .HasColumnType("boolean");

                    b.Property<int>("Green")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Red")
                        .HasColumnType("integer");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Senparc_PromptRange_Color");
                });
#pragma warning restore 612, 618
        }
    }
}
