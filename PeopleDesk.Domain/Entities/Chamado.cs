using PeopleDesk.Domain.Enums;
using PeopleDesk.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Entities
{
    public class Chamado
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public StatusChamado Status { get; set; }
        public PrioridadeChamado Prioridade { get; set; }
        public Guid UsuarioCriadorId { get; set; }
        public Guid? UsuarioResponsavelId { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public DateTime? IniciadoEm { get; set; }
        public DateTime? FechadoEm { get; set; }
        public string? ObservacaoFechamento { get; set; }

        public Chamado()
        {

        }

        public Chamado(
            string titulo,
            string descricao,
            PrioridadeChamado prioridade,
            Guid usuarioCriadorId)
        {
            ValidarTitulo(titulo);
            ValidarDescricao(descricao);

            if (usuarioCriadorId == Guid.Empty)
                throw new ExcecaoDominio("O usuário criador do chamado é obrigatório.");

            Id = Guid.NewGuid();
            Titulo = titulo.Trim();
            Descricao = descricao.Trim();
            Prioridade = prioridade;
            UsuarioCriadorId = usuarioCriadorId;
            Status = StatusChamado.Aberto;
            CriadoEm = DateTime.UtcNow;
        }

        public void AtualizarDescricao(string descricao)
        {
            ValidarDescricao(descricao);

            if (Status == StatusChamado.Fechado)
                throw new ExcecaoDominio("Não é possível alterar um chamado fechado.");

            Descricao = descricao.Trim();
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AlterarPrioridade(PrioridadeChamado prioridade)
        {
            if (Status == StatusChamado.Fechado)
                throw new ExcecaoDominio("Não é possível alterar a prioridade de um chamado fechado.");

            Prioridade = prioridade;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AtribuirPara(Guid usuarioResponsavelId)
        {
            if (usuarioResponsavelId == Guid.Empty)
                throw new ExcecaoDominio("O usuário responsável é obrigatório.");

            if (Status == StatusChamado.Fechado)
                throw new ExcecaoDominio("Não é possível atribuir um chamado fechado.");

            UsuarioResponsavelId = usuarioResponsavelId;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void IniciarAtendimento(Guid usuarioResponsavelId)
        {
            if (usuarioResponsavelId == Guid.Empty)
                throw new ExcecaoDominio("O usuário responsável é obrigatório.");

            if (Status != StatusChamado.Aberto)
                throw new ExcecaoDominio("Somente chamados abertos podem ser iniciados.");

            UsuarioResponsavelId = usuarioResponsavelId;
            Status = StatusChamado.EmAtendimento;
            IniciadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Fechar(string observacaoFechamento)
        {
            if (Status == StatusChamado.Fechado)
                throw new ExcecaoDominio("O chamado já está fechado.");

            if (Status == StatusChamado.Cancelado)
                throw new ExcecaoDominio("Não é possível fechar um chamado cancelado.");

            if (Prioridade == PrioridadeChamado.Critica && Status != StatusChamado.EmAtendimento)
                throw new ExcecaoDominio("Chamados críticos precisam estar em atendimento antes de serem fechados.");

            if (string.IsNullOrWhiteSpace(observacaoFechamento))
                throw new ExcecaoDominio("A observação de fechamento é obrigatória.");

            Status = StatusChamado.Fechado;
            ObservacaoFechamento = observacaoFechamento.Trim();
            FechadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Cancelar(string motivoCancelamento)
        {
            if (Status == StatusChamado.Fechado)
                throw new ExcecaoDominio("Não é possível cancelar um chamado fechado.");

            if (Status == StatusChamado.Cancelado)
                throw new ExcecaoDominio("O chamado já está cancelado.");

            if (string.IsNullOrWhiteSpace(motivoCancelamento))
                throw new ExcecaoDominio("O motivo do cancelamento é obrigatório.");

            Status = StatusChamado.Cancelado;
            ObservacaoFechamento = motivoCancelamento.Trim();
            AtualizadoEm = DateTime.UtcNow;
        }

        public bool EstaCritico()
        {
            return Prioridade == PrioridadeChamado.Critica;
        }

        public bool EstaAberto()
        {
            return Status == StatusChamado.Aberto;
        }

        public bool EstaFechado()
        {
            return Status == StatusChamado.Fechado;
        }

        private static void ValidarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ExcecaoDominio("O título do chamado é obrigatório.");

            if (titulo.Trim().Length < 5)
                throw new ExcecaoDominio("O título do chamado deve ter pelo menos 5 caracteres.");

            if (titulo.Trim().Length > 100)
                throw new ExcecaoDominio("O título do chamado deve ter no máximo 100 caracteres.");
        }

        private static void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ExcecaoDominio("A descrição do chamado é obrigatória.");

            if (descricao.Trim().Length < 10)
                throw new ExcecaoDominio("A descrição do chamado deve ter pelo menos 10 caracteres.");

            if (descricao.Trim().Length > 1000)
                throw new ExcecaoDominio("A descrição do chamado deve ter no máximo 1000 caracteres.");
        }
    }
}
