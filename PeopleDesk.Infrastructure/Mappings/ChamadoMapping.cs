using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Infrastructure.Mappings
{
    public class ChamadoMapping : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("Chamados");

            builder.HasKey(chamado => chamado.Id);

            builder.Property(chamado => chamado.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever();

            builder.Property(chamado => chamado.Titulo)
                .HasColumnName("Titulo")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(chamado => chamado.Descricao)
                .HasColumnName("Descricao")
                .HasColumnType("varchar(1000)")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(chamado => chamado.Status)
                .HasColumnName("Status")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(chamado => chamado.Prioridade)
                .HasColumnName("Prioridade")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(chamado => chamado.UsuarioCriadorId)
                .HasColumnName("UsuarioCriadorId")
                .IsRequired();

            builder.Property(chamado => chamado.UsuarioResponsavelId)
                .HasColumnName("UsuarioResponsavelId")
                .IsRequired(false);

            builder.Property(chamado => chamado.CriadoEm)
                .HasColumnName("CriadoEm")
                .IsRequired();

            builder.Property(chamado => chamado.AtualizadoEm)
                .HasColumnName("AtualizadoEm")
                .IsRequired(false);

            builder.Property(chamado => chamado.IniciadoEm)
                .HasColumnName("IniciadoEm")
                .IsRequired(false);

            builder.Property(chamado => chamado.FechadoEm)
                .HasColumnName("FechadoEm")
                .IsRequired(false);

            builder.Property(chamado => chamado.ObservacaoFechamento)
                .HasColumnName("ObservacaoFechamento")
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasIndex(chamado => chamado.Status)
                .HasDatabaseName("IX_Chamados_Status");

            builder.HasIndex(chamado => chamado.Prioridade)
                .HasDatabaseName("IX_Chamados_Prioridade");

            builder.HasIndex(chamado => chamado.UsuarioCriadorId)
                .HasDatabaseName("IX_Chamados_UsuarioCriadorId");

            builder.HasIndex(chamado => chamado.UsuarioResponsavelId)
                .HasDatabaseName("IX_Chamados_UsuarioResponsavelId");
        }
    }
}
