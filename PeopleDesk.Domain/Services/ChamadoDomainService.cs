using PeopleDesk.Domain.Entities;
using PeopleDesk.Domain.Enums;
using PeopleDesk.Domain.Exceptions;
using PeopleDesk.Domain.Interfaces.Repositories;
using PeopleDesk.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Services
{
    public class ChamadoDomainService : IChamadoDomainService
    {
        private readonly IChamadoRepository _chamadoRepository;

        private const int LimiteChamadosCriticosAbertos = 10;

        public ChamadoDomainService(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        public async Task<Chamado> CriarChamadoAsync(
            string titulo,
            string descricao,
            PrioridadeChamado prioridade,
            Guid usuarioCriadorId,
            CancellationToken cancellationToken = default)
        {
            var existeChamadoAbertoComMesmoTitulo =
                await _chamadoRepository.ExisteChamadoAbertoComMesmoTituloAsync(
                    titulo,
                    usuarioCriadorId,
                    cancellationToken);

            if (existeChamadoAbertoComMesmoTitulo)
                throw new ExcecaoDominio("Já existe um chamado aberto com o mesmo título para este usuário.");

            if (prioridade == PrioridadeChamado.Critica)
            {
                var quantidadeCriticosAbertos =
                    await _chamadoRepository.ContarChamadosCriticosAbertosAsync(cancellationToken);

                if (quantidadeCriticosAbertos >= LimiteChamadosCriticosAbertos)
                    throw new ExcecaoDominio("O limite de chamados críticos abertos foi atingido.");
            }

            return new Chamado(
                titulo,
                descricao,
                prioridade,
                usuarioCriadorId);
        }

        public Task AtribuirChamadoAsync(
            Chamado chamado,
            Guid usuarioResponsavelId,
            CancellationToken cancellationToken = default)
        {
            if (chamado is null)
                throw new ExcecaoDominio("O chamado é obrigatório.");

            chamado.AtribuirPara(usuarioResponsavelId);

            return Task.CompletedTask;
        }

        public Task IniciarAtendimentoAsync(
            Chamado chamado,
            Guid usuarioResponsavelId,
            CancellationToken cancellationToken = default)
        {
            if (chamado is null)
                throw new ExcecaoDominio("O chamado é obrigatório.");

            chamado.IniciarAtendimento(usuarioResponsavelId);

            return Task.CompletedTask;
        }

        public Task FecharChamadoAsync(
            Chamado chamado,
            string observacaoFechamento,
            CancellationToken cancellationToken = default)
        {
            if (chamado is null)
                throw new ExcecaoDominio("O chamado é obrigatório.");

            chamado.Fechar(observacaoFechamento);

            return Task.CompletedTask;
        }

        public Task CancelarChamadoAsync(
            Chamado chamado,
            string motivoCancelamento,
            CancellationToken cancellationToken = default)
        {
            if (chamado is null)
                throw new ExcecaoDominio("O chamado é obrigatório.");

            chamado.Cancelar(motivoCancelamento);

            return Task.CompletedTask;
        }
    }
}
