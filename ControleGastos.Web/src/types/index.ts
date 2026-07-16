// Arquivo: src/types/index.ts

// Esta interface espelha o que a API nos devolve no GET
export interface Pessoa {
    id: string;
    nome: string;
    idade: number;
}

// Esta interface espelha o DTO que enviamos no POST (sem o ID)
export interface PessoaCreate {
    nome: string;
    idade: number;
}

export interface Transacao {
    id: string;
    descricao: string;
    valor: number;
    tipo: number; // 0 = Despesa, 1 = Receita
    pessoaId: string;
    pessoa?: Pessoa; // A interrogação significa que este campo pode vir vazio em algumas listagens
}

export interface PessoaTotal {
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldoLiquido: number;
}

export interface RelatorioGeral {
    pessoas: PessoaTotal[];
    totalGeralReceitas: number;
    totalGeralDespesas: number;
    saldoLiquidoGeral: number;
}