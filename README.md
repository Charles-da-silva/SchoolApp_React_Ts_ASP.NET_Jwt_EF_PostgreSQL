# 🎓 School Management System

Sistema web para gestão básica de uma escola de educação infantil (0 a 5 anos),
desenvolvido como **projeto real** aplicado a um cenário verdadeiro, com foco em
**aprendizado prático, arquitetura de software e boas práticas de mercado**.

Este projeto está sendo inteiramente desenvolvido utilizando o ChatGPT, sem auxílio de nenhuma outra pessoa
ou qualquer material que tenha sido usado como referência. O intuito foi aprender e vivenciar
uma situação real qual um desenvolvedor é contratado para desenvolver uma solução para uma empresa.
Assim sendo, o ChatGPT entrou como um tutor, auxiliando na escolha das tecnologias, mas também um professor
ensinando e orientando durante o desenvolvimento do software e suas funcionalidades. 

---

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

---

## 🧠 Visão Geral da Arquitetura

A aplicação segue uma arquitetura moderna baseada em separação de responsabilidades:

<p align="center">
  <img src="docs/architecture1.0.png" alt="Diagrama de Arquitetura" width="400" hight=100%>
</p>

---

## 🧩 Tecnologias Utilizadas

|    Camada     |          Tecnologia           |         Motivo da escolha         |
|---------------|-------------------------------|-----------------------------------|
| Frontend      | React + TypeScript            | Alta demanda no mercado           |
| Backend       | ASP.Net Core MVC (C#)         | Robusto, padrão de mercado        |
| ORM           | Entity Framework Core         | Padrão de mercado                 |
| Banco         | PostgreSQL em Docker          | Open-source e baixo custo         |
| Auth          | JWT                           | Segurança moderna                 |
| Versionamento | Git + GitHub                  | Histórico e colaboração           |


---

## 🔐 Autenticação

A aplicação utilizará **JWT (JSON Web Token)** para autenticação.
O token será gerado pela API e enviado pelo frontend em cada requisição protegida.

---

## 🏫 Funcionalidades Planejadas

- Cadastro e gestão de alunos
- Cadastro de responsáveis
- Cadastro e gestão de funcionários (diretor(a), professores, cozinheira, limpeza)
- Organização de turmas (relacionado-as com professores e alunos)
- Controle de acesso por usuário
- Relatórios

---

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

---

## 📌 Observação Importante

Este projeto é desenvolvido como atividade voluntária e educacional, com **foco em aprendizado, prática e evolução profissional**.
Futuramente tende a ser colocado em produção em uma escola real, qual já está em negociação.

---

## 📈 Status do Projeto

🟢 Fase inicial concluída — API estruturada e funcional

O projeto App Escolinha já possui a base arquitetural configurada e uma API REST funcional construída com:

   - ASP.NET Core
   - Entity Framework Core
   - PostgreSQL (rodando em Docker)
   - Swagger para documentação e testes


✅ O que já foi implementado

   - Criação da solução e estrutura inicial do projeto
   - Configuração do DbContext e conexão com PostgreSQL (rodando em Docker)
   - Criação da entidade Student
   - Criação e aplicação da migration inicial (InitialCreate)
   - Geração automática do banco via EF Core
   - Implementação do StudentsController
   - Endpoint GET para listagem de alunos ativos
   - Endpoint POST para cadastro de alunos
   - Endpoint PUT para atualizar o cadastro de alunos
   - Endpoint DELETE para excluir alunos (soft delete)
   - Implementação de DTOs para entrada e saída de dados, fazendo validações com DataAnnotations 
   - Correção e padronização do modelo (FullName e DateOfBirth)
   - Ajuste de inconsistências entre Entity, Migration e Banco
   - Testes via Swagger funcionando corretamente


🧱 Arquitetura Atual

A aplicação segue uma estrutura baseada em:

   - Controllers → Responsáveis pelos endpoints
   - DTOs → Responsáveis pela comunicação externa
   - Entities → Representação das tabelas no banco
   - DbContext → Mapeamento ORM via EF Core
   - Migrations → Controle de versionamento do banco


🎯 Próximos Passos

      - Implementar PUT e DELETE
      - Adicionar validações
      - Melhorar tratamento de erros
      - Evoluir para camadas de Service
      - Aplicar boas práticas de arquitetura


## 🚧 Alguns do problemas Enfrentados e Como Foram Resolvidos

Operações síncronas que poderiam causar bloqueio de thread

   🔴 Problema

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

Exclusão física de registros (Delete Hard)

   🔴 Problema

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

Falta de validação robusta na entrada de dados

   🔴 Problema

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

Falta de padronização nos retornos HTTP

   🔴 Problema

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