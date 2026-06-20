# CrC_CrlV 

# iShopping

Aplicação de gestão de compras desenvolvida em C# WinForms com Entity Framework e SQL Server LocalDB, seguindo a arquitetura MVC.

---

### Instituição e Contexto
* **Instituição:** Instituto Politécnico de Leiria (IPL)
* **Curso:** TeSP em Programação de Sistemas de Informação (PSI)
* **Unidade Curricular:** Desenvolvimento de Aplicações (DA) e Metodologias de Desenvolvimento de Software (MDS) 
* **Ano Letivo:** 2025/2026

### Equipa de Desenvolvimento

| Número | Nome |
|--------|------|
| 2025177996 | Tiago Silva |
| 2025184807 | Leonor Azevedo |

---

## Requisitos

- Visual Studio 2019 ou superior
- .NET Framework 4.7.2 ou superior
- SQL Server LocalDB
- Entity Framework 6

---

## Instalação e configuração

1. Clonar ou extrair o projeto para uma pasta local.
2. Abrir o ficheiro de solução `IShopping.sln` no Visual Studio.
3. A base de dados será criada automaticamente e os dados iniciais (seed) serão inseridos.
4. Compilar e executar o projeto com **F5** ou clicando em **Start**.

---

## Utilizadores de teste

| Username | Password |
|----------|----------|
| admin | 12345 |

---

### Arquitetura MVC (Model-View-Controller)
A aplicação está estruturada em três camadas principais para facilitar a manutenção e escalabilidade:
* **View (Camada de Apresentação):** Formulários WinForms (`FormMain`, `FormLogin`, etc.) responsáveis pela interface com o utilizador e captura de eventos.
* **Controller (Camada de Lógica de Negócio):** Controladores que gerem o fluxo de dados, aplicam as regras de negócio (ex: validação de orçamento) e comunicam com os modelos.
* **Model (Camada de Dados):** Classes que representam as entidades do negócio (Artigos, Compras, Orçamentos) e o contexto do Entity Framework (`IShoppingContext`).
  
---

## Funcionalidades implementadas

- [x] **Autenticação:** Página de Login segura para acesso à plataforma.
- [x] **Menu principal:** Página Principal com navegação centralizada.
- [x] **Gestão de Artigos e Tipo de Artigo** Controlo total sobre Artigos e Tipos de Artigo.
- [x] **Gestão de Orçamento Mensal:** Gestão de Orçamento com alertas de teto máximo.
- [x] **Planeamento de compras:** Criação, alteração e estruturação de Compras Planeadas.
- [x] **Modo de compra:** Modo de Compra ativo para utilização em tempo real no supermercado.
- [x] **Estatisticas:** Painel de Estatísticas com cruzamento de gastos vs. orçamento.
- [x] **Exportar CSV:** Exportação de dados em formato CSV.
      
---

## Repositório GitHub:

- https://github.com/o-seu-utilizador/CrC_CrlV

---

## Diagrama de Classes

<img width="669" height="446" alt="image" src="https://github.com/user-attachments/assets/f9d09fc8-eed8-485b-a230-8634e8d694f1" />

--- 
