﻿// <auto-generated />
using AtencionMedica_v1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AtencionMedica_v1.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AtencionMedica_v1.Models.Atencion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConsultorioId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsultorioId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Atencion");
                });

            modelBuilder.Entity("AtencionMedica_v1.Models.Consultorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("NombreMedico")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Consultorio");
                });

            modelBuilder.Entity("AtencionMedica_v1.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoADN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnostico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("Identificacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoTriageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoTriageId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("AtencionMedica_v1.Models.TipoTriage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoTriage");
                });

            modelBuilder.Entity("AtencionMedica_v1.Models.Atencion", b =>
                {
                    b.HasOne("AtencionMedica_v1.Models.Consultorio", "Consultorio")
                        .WithMany("Atenciones")
                        .HasForeignKey("ConsultorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AtencionMedica_v1.Models.Paciente", "Paciente")
                        .WithMany("Atenciones")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AtencionMedica_v1.Models.Paciente", b =>
                {
                    b.HasOne("AtencionMedica_v1.Models.TipoTriage", "TipoTriage")
                        .WithMany("Paciente")
                        .HasForeignKey("TipoTriageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
