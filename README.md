# Controle de Gastos Residenciais

Sistema web full-stack desenvolvido para o controle financeiro e gestão de despesas residenciais. O projeto consolida transações, processa relatórios executivos de totais e aplica regras de negócio rigorosas baseadas na idade dos moradores.

## 🏗️ Arquitetura e Decisões Técnicas

O projeto foi construído priorizando o baixo acoplamento e a alta coesão, separando responsabilidades de forma clara:

* **Back-end (C# / .NET 8 Web API):**
  * **Arquitetura em Camadas (N-Tier):** Divisão estrita entre `Controllers` (Apresentação), `Services` (Regras de Negócio) e `Entities/Data` (Acesso a Dados).
  * **Padrão DTO (Data Transfer Object):** Utilizado para blindar as entidades de domínio contra *Over-Posting* e garantir que apenas os dados necessários transitem pela rede.
  * **Entity Framework Core (Code-First):** ORM escolhido para mapeamento do banco de dados, utilizando `Cascade Delete` para garantir a integridade referencial ao deletar moradores.
  * **Banco de Dados (SQLite):** Escolhido intencionalmente para testes técnicos (fricção zero). O avaliador não precisa configurar servidores SQL locais, bastando compilar a aplicação.

* **Front-end (React / TypeScript / Vite):**
  * **Tipagem Forte:** Espelhamento dos DTOs do C# através de interfaces TypeScript, prevenindo erros em tempo de compilação.
  * **Componentização:** Telas isoladas por domínio (`Pessoas`, `Transacoes`, `Dashboard`).
  * **Tratamento de Estado e Erros:** Interceptação de exceções de negócio (HTTP 400) vindas da API e renderização de feedback visual para o usuário.

## 🚀 Tecnologias Utilizadas

* **Back-end:** C# 12, .NET 8, Entity Framework Core, SQLite, LINQ, Swagger.
* **Front-end:** React 18, TypeScript, Vite, Axios.

## ⚙️ Como Executar o Projeto

Certifique-se de ter o [Node.js](https://nodejs.org/) e o [.NET 8 SDK](https://dotnet.microsoft.com/download) instalados na sua máquina.

### 1. Rodando a API (Back-end)
Abra um terminal, navegue até a pasta da API e execute a aplicação:
```bash
cd ControleGastos.Api
dotnet run
