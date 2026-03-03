# 🎓 School Management System

Sistema web para gestão básica de uma escola de educação infantil (0 a 5 anos), desenvolvido como **projeto real aplicado a um cenário verdadeiro**, com foco em **arquitetura de software e boas práticas de mercado**, com intuito de aprendizado prático e evolução profissional.

Este projeto está sendo desenvolvido utilizando meus conhecimentos somados ao uso do ChatGPT como ferramenta de apoio técnico e mentoria. O ChatGPT auxilia na geração de código, sugestões arquiteturais e
explicações técnicas, enquanto eu realizo o estudo, compreensão, validação, testes, ajustes e decisões estruturais do projeto.

O objetivo da criação do projeto é vivenciar uma experiência próxima à realidade de mercado, simulando o cenário onde um desenvolvedor é contratado para desenvolver uma solução para uma empresa, tendo de
compreender o problema, buscar, implementar, testar e evoluir soluções até entregar o melhor produto possível.
<br><br>

## 🎯 Objetivos do Projeto

Este projeto possui três objetivos principais:

1. **Uso real**
   - Atender uma escola pequena (≈ 60 alunos)
   - Resolver problemas reais do dia a dia administrativo

2. **Aprendizado técnico**
   - Aprender desenvolvimento backend e frontend moderno
   - Entender arquitetura de software aplicada ao mercado
   - Desenvolver APIs, autenticação e persistência de dados
   - Aplicar princípios SOLID e boas práticas

3. **Comprovação de experiência**
   - Demonstrar evolução contínua no GitHub
   - Criar um histórico real de decisões técnicas
   - Servir como portfólio para vagas de estágio / dev júnior
<br>


## 🧩 Tecnologias Utilizadas

|    Camada     |          Tecnologia           |         Motivo da escolha         |
|---------------|-------------------------------|-----------------------------------|
| Frontend      | React + TypeScript (Vite)     | Alta demanda no mercado           |
| Backend       | ASP.Net Core MVC (C#)         | Robusto e amplamente utilizado    |
| ORM           | Entity Framework Core         | Padrão consolidado                |
| Banco         | PostgreSQL em Docker          | Open-source e baixo custo         |
| Auth          | JWT                           | Segurança moderna                 |
| Versionamento | Git + GitHub                  | Histórico e colaboração           |

<br>

## 🏫 Funcionalidades Planejadas

- Cadastro e gestão de alunos
- Cadastro de responsáveis
- Cadastro e gestão de funcionários
- Organização de turmas (relacionado-as com professores e alunos)
- Controle de acesso por usuário
- Relatórios
<br>

## 🚀 Metodologia de Desenvolvimento

O projeto é desenvolvido por **fases**, como em metodologias ágeis como SCRUM, sempre priorizando entendimento antes do código:

1.  Fundamentos e arquitetura\
2.  Backend mínimo\
3.  Persistência de dados\
4.  Padronização de respostas e regras de negócio\
5.  Frontend React\
6.  Autenticação\
7.  Evolução funcional e deploy

Cada etapa é explicada e versionada neste repositório.
<br><br>

## 📈 Status do Projeto
<br>

🟢 Fase inicial concluída — API estruturada e funcional

   O projeto App Escolinha já possui a base arquitetural configurada e uma API REST funcional construída com:

   - ASP.NET Core
   - Entity Framework Core
   - PostgreSQL (rodando em Docker)
   - Swagger para documentação e testes

   Frontend inicial configurado com React + Vite + TypeScript
<br>

## ✅ O que já foi realizado

🔹 Arquitetura

   - Separação em camadas (Controller → Service → Infrastructure)
   - Extração das regras de negócio para a camada de Service
   - Implementação da interface `IStudentService`
   - Aplicação do princípio da Inversão de Dependência (SOLID)
   - Desacoplamento do Controller do DbContext
   - Implementação de `BaseController` para padronização de respostas
   - Implementação de `ErrorType` e `Result<T>` para controle padronizado
      de erros e retornos

🔹 Banco de Dados

   - PostgreSQL rodando via Docker
   - Migrations aplicadas com sucesso
   - Índice único implementado com:

   ``` csharp
   [Index(nameof(Email), IsUnique = true)]
   ```

   Garantindo integridade de dados no nível do banco.

🔹 Gestão de Alunos

   - GET All com filtros avançados via `StudentFilterDTO`:
      - Idade mínima
      - Idade máxima
      - Nome
      - Email
      - Apenas ativos
      - Data de cadastro
   - GET por ID
   - POST (criação)
   - PUT (atualização)
   - `DeactivateAsync()` → Soft Delete
   - `DeleteAsync()` → Hard Delete
   - Validação de email único
   - DTOs com validações via DataAnnotations
   - Operações 100% assíncronas

🔹 Frontend

   - Inicialização do projeto com React + Vite + TypeScript
   - Estrutura base da aplicação
   - Criação de serviços para consumo da API
   - Definição de tipos TypeScript para Student
   - Integração com backend validada
   - Testes realizados via Swagger e aplicação React com sucesso
<br>

🧱 Arquitetura Atual

   A aplicação segue uma estrutura baseada em camadas:

   - Controllers → Responsáveis apenas pela camada HTTP (entrada e saída)
   - Services → Responsáveis pelas regras de negócio e casos de uso
   - DTOs → Contratos de comunicação externa
   - Entities (Domain) → Representação do domínio e estrutura das tabelas
   - Infrastructure (DbContext) → Persistência de dados via EF Core
   - Migrations → Controle de versionamento do banco
<br>

📌 Fluxo atual:

   Controller → Service → DbContext → Banco de Dados

   Essa estrutura permite:

   - Maior testabilidade
   - Melhor manutenção
   - Desacoplamento entre camadas
   - Evolução futura do sistema sem impacto estrutural
<br>

🎯 Próximos Passos

   🔹 Curto Prazo

   - Middleware global de exceções
   - Exceções personalizadas de domínio
   - Logging estruturado   

   🔹 Médio Prazo (Crescimento do Sistema)

   - Autenticação e autorização com JWT
   - Cadastro de responsáveis
   - Cadastro de funcionários
   - Controle de acesso por perfil
<br>

## 🚧 Alguns do problemas enfrentados e como Foram Resolvidos
<br>

🔴 Problema: Operações síncronas que poderiam causar bloqueio de thread

   - Uso potencial de métodos síncronos do EF Core poderia:
   - Bloquear threads
   - Reduzir escalabilidade
   - Comprometer performance sob carga

✅ Solução

   - Adoção completa de:
   - ToListAsync()
   - FirstOrDefaultAsync()
   - SaveChangesAsync()

💡 Aprendizado

   - APIs modernas devem ser 100% assíncronas para suportar alta concorrência.
<br>

🔴 Problema: Só existia Endpoint para exclusão física de registros (Delete Hard)

   - Remover registros permanentemente pode causar:
   - Perda irreversível de dados
   - Problemas de auditoria
   - Quebra de integridade relacional

✅ Solução

   - Implementação de Soft Delete:
   - student.IsActive = false;
   - Filtragem apenas de registros ativos.

💡 Aprendizado

   - Soft Delete é padrão em sistemas reais que exigem histórico e rastreabilidade.
<br>

🔴 Problema: Falta de validação robusta na entrada de dados

   - Sem validações, a API poderia aceitar:
   - Emails inválidos
   - Campos vazios
   - Dados inconsistentes

✅ Solução

   Uso de:

   - [Required]
   - [EmailAddress]
   - [MinLength]
   - [ApiController] para validação automática

💡 Aprendizado

   - Validação automática reduz código manual e aumenta confiabilidade.
<br>

🔴 Problema: Falta de padronização nos retornos HTTP

   - Sem retorno estruturado:
   - API inconsistente
   - Difícil consumo por frontend
   - Falta de semântica REST

✅ Solução

   Uso adequado de:

   - Ok()
   - CreatedAtAction()
   - NotFound()
   - BadRequest()
   - NoContent()

💡 Aprendizado

   - Semântica HTTP correta melhora interoperabilidade e profissionalismo, seguindo os padrões REST.
<br>

🔴 Problema com DateTime e Fuso Horário

   Inicialmente os campos `DateOfBirth`, `CreatedAt` e `DeactivatedAt`
   utilizavam `DateTime`.

   Ao consumir no frontend, ocorria conversão automática de UTC para horário local (pt-BR), causando em alguns casos a exibição de um dia a menos, dependendo do horário armazenado.

✅ Solução 

   - Alteração dos campos para o tipo `DateOnly`
   - Ajuste nas Entities e Services
   - Correção da serialização para evitar problemas de fuso horário

💡 Aprendizado:

   - Nem sempre `DateTime` é a melhor escolha.
   - Problemas de timezone são comuns em aplicações reais.
   - Modelagem correta de dados evita bugs sutis no frontend.