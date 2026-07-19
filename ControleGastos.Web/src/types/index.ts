// Define o contrato de dados para a entidade Pessoa retornado pelos endpoints de leitura (GET)
export interface Pessoa {
    id: string;
    nome: string;
    idade: number;
}

// Define a estrutura do Data Transfer Object (DTO) para persistência de novos registros (POST)
export interface PessoaCreate {
    nome: string;
    idade: number;
}

// Define o contrato de dados para operações de Transações Financeiras
export interface Transacao {
    id: string;
    descricao: string;
    valor: number;
    tipo: number; // Mapeia o Enum de domínio: 0 para Despesa, 1 para Receita
    pessoaId: string;
    pessoa?: Pessoa; // Propriedade condicional para suporte a projeções com lazy loading ou Eager Loading
}

// Representa a projeção de dados consolidados de desempenho financeiro por indivíduo
export interface PessoaTotal {
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldoLiquido: number;
}

// Define a estrutura do payload para relatórios consolidados e auditoria geral do sistema
export interface RelatorioGeral {
    pessoas: PessoaTotal[];
    totalGeralReceitas: number;
    totalGeralDespesas: number;
    saldoLiquidoGeral: number;
}