using PeopleDesk.Domain.Entities;
using PeopleDesk.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Interfaces.Repositories
{
    public interface IChamadoRepository : IRepository<Chamado>
    {
        Task<List<Chamado>> ObterPorUsuarioCriadorAsync(Guid usuarioCriadorId, CancellationToken cancellationToken = default);

        Task<List<Chamado>> ObterPorUsuarioResponsavelAsync(Guid usuarioResponsavelId, CancellationToken cancellationToken = default);

        Task<List<Chamado>> ObterPorStatusAsync(StatusChamado status, CancellationToken cancellationToken = default);

        Task<List<Chamado>> ObterPorPrioridadeAsync(PrioridadeChamado prioridade, CancellationToken cancellationToken = default);

        Task<bool> ExisteChamadoAbertoComMesmoTituloAsync(string titulo, Guid usuarioCriadorId, CancellationToken cancellationToken = default);

        Task<int> ContarChamadosCriticosAbertosAsync(CancellationToken cancellationToken = default);
    }
}
