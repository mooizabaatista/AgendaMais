﻿// <auto-generated />
using System;
using AgendaMais.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgendaMais.Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241031125701_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("AgendaMais.Domain.Entities.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Cancelado")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Concluido")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("HoraFim")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.AgendamentoServico", b =>
                {
                    b.Property<int>("AgendamentoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ServicoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AgendamentoId", "ServicoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("AgendamentoServicos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EstabelecimentoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EstabelecimentoId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Estabelecimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Estabelecimentos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EstabelecimentoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EstabelecimentoId");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Agendamento", b =>
                {
                    b.HasOne("AgendaMais.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.AgendamentoServico", b =>
                {
                    b.HasOne("AgendaMais.Domain.Entities.Agendamento", "Agendamento")
                        .WithMany("AgendamentoServicos")
                        .HasForeignKey("AgendamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgendaMais.Domain.Entities.Servico", "Servico")
                        .WithMany("AgendamentoServicos")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agendamento");

                    b.Navigation("Servico");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Cliente", b =>
                {
                    b.HasOne("AgendaMais.Domain.Entities.Estabelecimento", "Estabelecimento")
                        .WithMany("Clientes")
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estabelecimento");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Servico", b =>
                {
                    b.HasOne("AgendaMais.Domain.Entities.Estabelecimento", "Estabelecimento")
                        .WithMany("Servicos")
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estabelecimento");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Agendamento", b =>
                {
                    b.Navigation("AgendamentoServicos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Agendamentos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Estabelecimento", b =>
                {
                    b.Navigation("Clientes");

                    b.Navigation("Servicos");
                });

            modelBuilder.Entity("AgendaMais.Domain.Entities.Servico", b =>
                {
                    b.Navigation("AgendamentoServicos");
                });
#pragma warning restore 612, 618
        }
    }
}
