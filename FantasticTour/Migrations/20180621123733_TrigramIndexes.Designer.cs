﻿// <auto-generated />
using System;
using FantasticTour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FantasticTour.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180621123733_TrigramIndexes")]
    partial class TrigramIndexes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FantasticTour.Models.Atraccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CiudadId");

                    b.Property<decimal>("Costo");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.ToTable("Atracciones");
                });

            modelBuilder.Entity("FantasticTour.Models.Ciudad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<int?>("PaisId");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Ciudades");
                });

            modelBuilder.Entity("FantasticTour.Models.Estadia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Costo");

                    b.Property<DateTime>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<int?>("HotelId");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Estadias");
                });

            modelBuilder.Entity("FantasticTour.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CiudadId");

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre");

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.ToTable("Hoteles");
                });

            modelBuilder.Entity("FantasticTour.Models.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("FantasticTour.Models.Paquete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AtraccionId");

                    b.Property<bool>("Disponible");

                    b.Property<int?>("EstadiaId");

                    b.Property<DateTime>("FechaVencimiento");

                    b.Property<string>("Nombre");

                    b.Property<int?>("TransporteId");

                    b.HasKey("Id");

                    b.HasIndex("AtraccionId");

                    b.HasIndex("EstadiaId");

                    b.HasIndex("TransporteId");

                    b.ToTable("Paquetes");
                });

            modelBuilder.Entity("FantasticTour.Models.PaqueteContratado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<DateTime>("Fecha");

                    b.Property<int?>("PaqueteId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PaqueteId");

                    b.ToTable("PaquetesContratados");
                });

            modelBuilder.Entity("FantasticTour.Models.Transporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Costo");

                    b.Property<int?>("DestinoId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int?>("OrigenId");

                    b.Property<int>("TipoTransporte");

                    b.HasKey("Id");

                    b.HasIndex("DestinoId");

                    b.HasIndex("OrigenId");

                    b.ToTable("Transporte");
                });

            modelBuilder.Entity("FantasticTour.Models.Atraccion", b =>
                {
                    b.HasOne("FantasticTour.Models.Ciudad", "Ciudad")
                        .WithMany()
                        .HasForeignKey("CiudadId");
                });

            modelBuilder.Entity("FantasticTour.Models.Ciudad", b =>
                {
                    b.HasOne("FantasticTour.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId");
                });

            modelBuilder.Entity("FantasticTour.Models.Estadia", b =>
                {
                    b.HasOne("FantasticTour.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("FantasticTour.Models.Hotel", b =>
                {
                    b.HasOne("FantasticTour.Models.Ciudad", "Ciudad")
                        .WithMany("Hoteles")
                        .HasForeignKey("CiudadId");
                });

            modelBuilder.Entity("FantasticTour.Models.Paquete", b =>
                {
                    b.HasOne("FantasticTour.Models.Atraccion", "Atraccion")
                        .WithMany()
                        .HasForeignKey("AtraccionId");

                    b.HasOne("FantasticTour.Models.Estadia", "Estadia")
                        .WithMany()
                        .HasForeignKey("EstadiaId");

                    b.HasOne("FantasticTour.Models.Transporte", "Transporte")
                        .WithMany()
                        .HasForeignKey("TransporteId");
                });

            modelBuilder.Entity("FantasticTour.Models.PaqueteContratado", b =>
                {
                    b.HasOne("FantasticTour.Models.Paquete", "Paquete")
                        .WithMany()
                        .HasForeignKey("PaqueteId");
                });

            modelBuilder.Entity("FantasticTour.Models.Transporte", b =>
                {
                    b.HasOne("FantasticTour.Models.Ciudad", "Destino")
                        .WithMany()
                        .HasForeignKey("DestinoId");

                    b.HasOne("FantasticTour.Models.Ciudad", "Origen")
                        .WithMany()
                        .HasForeignKey("OrigenId");
                });
#pragma warning restore 612, 618
        }
    }
}
