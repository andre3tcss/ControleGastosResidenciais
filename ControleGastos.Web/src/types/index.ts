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