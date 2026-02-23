# 🎓 School Management System

Sistema web para gestão básica de uma escola de educação infantil (0 a 5 anos), desenvolvido como **projeto real** aplicado a um cenário verdadeiro, com **foco em arquitetura de software e boas práticas de mercado, com intuito de aprendizado prático e evolução profissional**.

Este projeto está sendo inteiramente desenvolvido utilizando o ChatGPT e meus conhecimentos, sem auxílio de nenhuma outra pessoa ou qualquer material que tenha sido usado como referência. O objetivo da criação do projeto foi me desenvolver através de uma situação real de mercado e vivenciar uma experiência qual um desenvolvedor é contratado para desenvolver uma solução para uma empresa, tendo de compreender o problema, buscar, implementar e testar soluções, afim de entregar o melhor produto ao cliente final.
Assim sendo, o ChatGPT entrou como um tutor, auxiliando na escolha das tecnologias, mas também como um professor, ensinando e orientando durante o desenvolvimento do software e suas funcionalidades. 
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

3. **Comprovação de experiência**
   - Demonstrar evolução contínua no GitHub
   - Criar um histórico real de decisões técnicas
   - Servir como portfólio para vagas de estágio / dev júnior
<br>


## 🧩 Tecnologias Utilizadas

|    Camada     |          Tecnologia           |         Motivo da escolha         |
|---------------|-------------------------------|-----------------------------------|
| Frontend      | React + TypeScript            | Alta demanda no mercado           |
| Backend       | ASP.Net Core MVC (C#)         | Robusto, padrão de mercado        |
| ORM           | Entity Framework Core         | Padrão de mercado                 |
| Banco         | PostgreSQL em Docker          | Open-source e baixo custo         |
| Auth          | JWT                           | Segurança moderna                 |
| Versionamento | Git + GitHub                  | Histórico e colaboração           |

<br>

## 🏫 Funcionalidades Planejadas

- Cadastro e gestão de alunos
- Cadastro de responsáveis
- Cadastro e gestão de funcionários (diretor(a), coordenador(a), pegadogo(a), professores, cozinheiro(a), serviços gerais)
- Organização de turmas (relacionado-as com professores e alunos)
- Controle de acesso por usuário
- Relatórios
<br>

## 🚀 Metodologia de Desenvolvimento

O projeto é desenvolvido por **fases**, como em metodologias ágeis como SCRUM, sempre priorizando entendimento antes do código:

1. Fundamentos e arquitetura
2. Backend mínimo
3. Persistência de dados
4. Autenticação
5. Frontend
6. Funcionalidades reais
7. Deploy econômico

Cada etapa é explicada e versionada neste repositório no GitHub.
<br><br>

## 📈 Status do Projeto
<br>

🟢 Fase inicial concluída — API estruturada e funcional

   O projeto App Escolinha já possui a base arquitetural configurada e uma API REST funcional construída com:

   - ASP.NET Core
   - Entity Framework Core
   - PostgreSQL (rodando em Docker)
   - Swagger para documentação e testes
<br>

✅ O que já foi realizado

   - Criação da solução e estrutura inicial do projeto
   - Configuração do DbContext e conexão com PostgreSQL (rodando em Docker)
   - Criação da entidade Student
   - Criação e aplicação da migration inicial (InitialCreate)
   - Geração automática do banco via EF Core
   - Implementação do StudentsController
   - Endpoint GET para listagem de alunos ativos
   - Endpoint GET por ID
   - Endpoint POST para cadastro de alunos
   - Endpoint PUT para atualizar o cadastro de alunos
   - Endpoint DELETE para exclusão lógica (soft delete)
   - Implementação de DTOs para entrada e saída de dados com validações via DataAnnotations
   - Ajuste de inconsistências entre Entity, Migration e Banco
   - Adoção completa de operações assíncronas (ToListAsync, FirstOrDefaultAsync, SaveChangesAsync)
   - Implementação de Soft Delete utilizando IsActive
   - Refatoração arquitetural: extração das regras de negócio do Controller para a camada de Service
   - Criação da interface IStudentService seguindo o princípio da Inversão de Dependência (SOLID)
   - Implementação da classe StudentService centralizando regras de negócio
   - Desacoplamento do Controller em relação ao DbContext
   - Remoção do campo IsActive dos DTOs (tratado como controle interno de domínio)
   - Testes completos via Swagger com todos os endpoints funcionando corretamente
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

   🔹 Curto Prazo (Evolução Arquitetural)

   - Implementar tratamento global de exceções (Exception Middleware)
   - Criar exceções personalizadas para regras de negócio
   - Implementar validação de regra de negócio (ex: email único)
   - Criar índice único no banco para garantir integridade de dados
   - Padronizar respostas da API (Response Pattern)

   🔹 Médio Prazo (Crescimento do Sistema)

   - Implementar autenticação e autorização com JWT
   - Criar entidade de Responsáveis (Parents/Guardians)
   - Criar entidade de Funcionários
   - Introduzir logging estruturado
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

🔴 Problema: Exclusão física de registros (Delete Hard)

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