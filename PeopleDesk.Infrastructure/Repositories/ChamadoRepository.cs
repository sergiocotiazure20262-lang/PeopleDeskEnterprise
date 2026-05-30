using Microsoft.EntityFrameworkCore;
using PeopleDesk.Domain.Entities;
using PeopleDesk.Domain.Enums;
using PeopleDesk.Domain.Interfaces.Repositories;
using PeopleDesk.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Infrastructure.Repositories
{
    public class ChamadoRepository : Repository<Chamado>, IChamadoRepository
    {
        public ChamadoRepository(PeopleDeskDbContext context)
            : base(context)
        {
        }

        public override async Task<Chamado?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .FirstOrDefaultAsync(chamado => chamado.Id == id, cancellationToken);
        }

        public async Task<List<Chamado>> ObterPorUsuarioCriadorAsync(
            Guid usuarioCriadorId,
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .AsNoTracking()
                .Where(chamado => chamado.UsuarioCriadorId == usuarioCriadorId)
                .OrderByDescending(chamado => chamado.CriadoEm)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Chamado>> ObterPorUsuarioResponsavelAsync(
            Guid usuarioResponsavelId,
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .AsNoTracking()
                .Where(chamado => chamado.UsuarioResponsavelId == usuarioResponsavelId)
                .OrderByDescending(chamado => chamado.CriadoEm)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Chamado>> ObterPorStatusAsync(
            StatusChamado status,
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .AsNoTracking()
                .Where(chamado => chamado.Status == status)
                .OrderByDescending(chamado => chamado.CriadoEm)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Chamado>> ObterPorPrioridadeAsync(
            PrioridadeChamado prioridade,
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .AsNoTracking()
                .Where(chamado => chamado.Prioridade == prioridade)
                .OrderByDescending(chamado => chamado.CriadoEm)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExisteChamadoAbertoComMesmoTituloAsync(
            string titulo,
            Guid usuarioCriadorId,
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .AsNoTracking()
                .AnyAsync(chamado =>
                    chamado.UsuarioCriadorId == usuarioCriadorId &&
                    chamado.Titulo == titulo.Trim() &&
                    chamado.Status != StatusChamado.Fechado &&
                    chamado.Status != StatusChamado.Cancelado,
                    cancellationToken);
        }

        public async Task<int> ContarChamadosCriticosAbertosAsync(
            CancellationToken cancellationToken = default)
        {
            return await Context.Chamados
                .AsNoTracking()
                .CountAsync(chamado =>
                    chamado.Prioridade == PrioridadeChamado.Critica &&
                    chamado.Status != StatusChamado.Fechado &&
                    chamado.Status != StatusChamado.Cancelado,
                    cancellationToken);
        }
    }
}
