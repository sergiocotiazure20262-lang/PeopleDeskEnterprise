# PeopleDesk Enterprise

Este repositório contém a solução **PeopleDeskEnterprise**, organizada em camadas para uma aplicação .NET modular. A estrutura sugere uma arquitetura separada por responsabilidades, com API, aplicação, domínio, infraestrutura, interface Blazor e projetos de testes.

> Observação: este README foi gerado a partir da estrutura visível do projeto. Ajuste nomes de banco, variáveis de ambiente, comandos específicos e regras de negócio conforme a implementação real.

## Estrutura do projeto

```text
PeopleDeskEnterprise.slnx
├── PeopleDesk.Api
├── PeopleDesk.Application
├── PeopleDesk.Blazor
├── PeopleDesk.Domain
├── PeopleDesk.Infrastructure
├── PeopleDesk.Shared
├── PeopleDesk.IntegrationTests
├── PeopleDesk.UnitTests
├── docker-compose.yml
├── docker-compose.override.yml
├── docker-compose.dcproj
├── launchSettings.json
├── .dockerignore
├── .gitattributes
└── .gitignore
```

## Visão geral

O projeto é dividido em componentes independentes para facilitar manutenção, testes, evolução e separação de responsabilidades.

### PeopleDesk.Api

Camada responsável por expor os endpoints HTTP da aplicação.

Possíveis responsabilidades:

* Configuração de controllers ou minimal APIs.
* Autenticação e autorização.
* Registro de middlewares.
* Exposição de contratos REST.
* Configuração de Swagger/OpenAPI, quando aplicável.

### PeopleDesk.Application

Camada de aplicação, onde normalmente ficam os casos de uso e regras de orquestração.

Possíveis responsabilidades:

* Services de aplicação.
* Handlers de comandos e consultas.
* Validações de entrada.
* DTOs de entrada e saída.
* Interfaces usadas pela camada de domínio ou infraestrutura.

### PeopleDesk.Blazor

Interface web construída com Blazor.

Possíveis responsabilidades:

* Páginas e componentes visuais.
* Consumo da API.
* Estado da interface.
* Formulários, validações e telas do sistema.

### PeopleDesk.Domain

Camada de domínio, onde devem ficar as regras de negócio centrais.

Possíveis responsabilidades:

* Entidades.
* Value Objects.
* Regras de negócio.
* Eventos de domínio.
* Interfaces de repositórios quando a arquitetura adotar inversão de dependência.

### PeopleDesk.Infrastructure

Camada responsável por integrações externas e detalhes técnicos.

Possíveis responsabilidades:

* Acesso a banco de dados.
* Migrations.
* Implementações de repositórios.
* Integração com serviços externos.
* Configurações de persistência, mensageria, cache ou armazenamento.

### PeopleDesk.Shared

Projeto compartilhado entre as demais camadas.

Possíveis responsabilidades:

* Constantes compartilhadas.
* Tipos utilitários.
* Contratos comuns.
* Extensões reutilizáveis.
* Classes usadas tanto no front-end quanto no back-end.

### PeopleDesk.UnitTests

Projeto destinado a testes unitários.

Objetivo:

* Validar regras de negócio isoladas.
* Testar services, handlers, entidades e validadores.
* Garantir comportamento previsível sem depender de banco, rede ou serviços externos.

### PeopleDesk.IntegrationTests

Projeto destinado a testes de integração.

Objetivo:

* Validar o comportamento entre camadas.
* Testar endpoints da API.
* Verificar integração com banco de dados, infraestrutura e configurações reais ou simuladas.

## Pré-requisitos

Antes de executar o projeto, verifique se possui instalado:

* .NET SDK compatível com a versão usada pela solução.
* Docker e Docker Compose, caso utilize os arquivos `docker-compose.yml`.
* Editor ou IDE compatível, como Visual Studio, Visual Studio Code ou JetBrains Rider.

## Como executar localmente

### 1\. Restaurar dependências

```bash
dotnet restore PeopleDeskEnterprise.slnx
```

### 2\. Compilar a solução

```bash
dotnet build PeopleDeskEnterprise.slnx
```

### 3\. Executar a API

```bash
dotnet run --project PeopleDesk.Api
```

### 4\. Executar a aplicação Blazor

```bash
dotnet run --project PeopleDesk.Blazor
```

## Execução com Docker

O repositório contém arquivos Docker Compose, indicando suporte a execução containerizada.

Para subir os serviços:

```bash
docker compose up --build
```

Para parar os serviços:

```bash
docker compose down
```

Caso existam serviços como banco de dados, cache ou mensageria no `docker-compose.yml`, confirme as portas e variáveis de ambiente antes da execução.

## Configuração

Verifique os arquivos de configuração de cada projeto, como:

* `appsettings.json`
* `appsettings.Development.json`
* `launchSettings.json`
* Variáveis de ambiente definidas no Docker Compose

Configurações comuns que podem existir:

* String de conexão do banco de dados.
* URL da API consumida pelo Blazor.
* Configurações de autenticação.
* Chaves de integração com serviços externos.
* Configurações de logging.

## Testes

### Executar todos os testes

```bash
dotnet test PeopleDeskEnterprise.slnx
```

### Executar testes unitários

```bash
dotnet test PeopleDesk.UnitTests
```

### Executar testes de integração

```bash
dotnet test PeopleDesk.IntegrationTests
```

## Convenções recomendadas

### Organização por camadas

* A camada `Domain` não deve depender de `Infrastructure`, `Api` ou `Blazor`.
* A camada `Application` deve orquestrar casos de uso e depender de abstrações.
* A camada `Infrastructure` deve implementar detalhes técnicos.
* A camada `Api` deve expor a aplicação para clientes externos.
* A camada `Blazor` deve ser responsável pela experiência do usuário.

### Boas práticas

* Manter regras de negócio no domínio ou na aplicação, evitando lógica excessiva em controllers e componentes visuais.
* Usar injeção de dependência para desacoplar implementações.
* Criar testes unitários para regras críticas.
* Criar testes de integração para fluxos importantes da API.
* Documentar variáveis de ambiente obrigatórias.
* Manter o Docker Compose atualizado com as dependências reais do ambiente local.

## Fluxo sugerido de desenvolvimento

1. Criar ou atualizar entidades e regras na camada `PeopleDesk.Domain`.
2. Implementar casos de uso na camada `PeopleDesk.Application`.
3. Implementar persistência ou integrações em `PeopleDesk.Infrastructure`.
4. Expor endpoints em `PeopleDesk.Api`.
5. Atualizar telas e componentes em `PeopleDesk.Blazor`.
6. Adicionar ou atualizar testes em `PeopleDesk.UnitTests` e `PeopleDesk.IntegrationTests`.

## Troubleshooting

### Erro ao restaurar pacotes

Execute:

```bash
dotnet nuget locals all --clear
dotnet restore PeopleDeskEnterprise.slnx
```

### Erro ao subir containers

Verifique se o Docker está em execução e se as portas configuradas no `docker-compose.yml` não estão sendo usadas por outros processos.

```bash
docker compose down
docker compose up --build
```

### Erro de conexão com banco de dados

Confirme:

* A string de conexão.
* O nome do host no Docker Compose.
* As credenciais configuradas.
* Se o container do banco está saudável.

## Licença

Informe aqui a licença do projeto, caso exista.

## Mantenedores

Adicione aqui os responsáveis pelo projeto e canais de contato internos.

