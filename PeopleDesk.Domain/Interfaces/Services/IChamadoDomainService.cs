using PeopleDesk.Domain.Entities;
using PeopleDesk.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Interfaces.Services
{
    public interface IChamadoDomainService
    {
        Task<Chamado> CriarChamadoAsync(
            string titulo,
            string descricao,
            PrioridadeChamado prioridade,
            Guid usuarioCriadorId,
            CancellationToken cancellationToken = default);

        Task AtribuirChamadoAsync(
            Chamado chamado,
            Guid usuarioResponsavelId,
            CancellationToken cancellationToken = default);

        Task IniciarAtendimentoAsync(
            Chamado chamado,
            Guid usuarioResponsavelId,
            CancellationToken cancellationToken = default);

        Task FecharChamadoAsync(
            Chamado chamado,
            string observacaoFechamento,
            CancellationToken cancellationToken = default);

        Task CancelarChamadoAsync(
            Chamado chamado,
            string motivoCancelamento,
            CancellationToken cancellationToken = default);
    }
}
