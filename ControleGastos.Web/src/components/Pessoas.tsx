// Arquivo: src/components/Pessoas.tsx
import { useState, useEffect, FormEvent } from 'react';
import { api } from '../services/api';
import type { Pessoa } from '../types';

export default function Pessoas() {
    // 1. ESTADOS (A "Memória" do Componente)
    // listaPessoas guarda o array de pessoas vindas da API. setListaPessoas é a função para alterá-la.
    const [listaPessoas, setListaPessoas] = useState<Pessoa[]>([]);

    // Estados para controlar o formulário
    const [nome, setNome] = useState('');
    const [idade, setIdade] = useState('');

    // 2. EFEITOS COLATERAIS (useEffect)
    // O useEffect roda automaticamente quando a tela abre pela primeira vez (devido ao array vazio [] no final).
    useEffect(() => {
        carregarPessoas();
    }, []);

    // 3. FUNÇÕES DE COMUNICAÇÃO COM A API
    async function carregarPessoas() {
        try {
            const resposta = await api.get('/Pessoas');
            setListaPessoas(resposta.data); // Atualiza o estado. O React redesenha a tela sozinho!
        } catch (error) {
            console.error("Erro ao buscar pessoas", error);
        }
    }

    async function cadastrarPessoa(evento: FormEvent) {
        evento.preventDefault(); // Impede a página de recarregar (comportamento padrão do HTML)

        try {
            await api.post('/Pessoas', {
                nome: nome,
                idade: Number(idade) // Converte a string do input para número
            });

            // Limpa os campos do formulário
            setNome('');
            setIdade('');
            // Recarrega a lista do banco de dados
            carregarPessoas();
        } catch (error) {
            alert("Erro ao cadastrar pessoa.");
        }
    }

    async function deletarPessoa(id: string) {
        try {
            await api.delete(`/Pessoas/${id}`);
            carregarPessoas(); // Recarrega a lista após deletar
        } catch (error) {
            alert("Erro ao deletar pessoa.");
        }
    }

    // 4. O VISUAL (JSX)
    return (
        <div style={{ padding: '20px', maxWidth: '600px', margin: '0 auto' }}>
            <h2>Gerenciar Pessoas</h2>

            {/* Formulário */}
            <form onSubmit={cadastrarPessoa} style={{ display: 'flex', gap: '10px', marginBottom: '20px' }}>
                <input
                    type="text"
                    placeholder="Nome"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    required
                />
                <input
                    type="number"
                    placeholder="Idade"
                    value={idade}
                    onChange={(e) => setIdade(e.target.value)}
                    required
                />
                <button type="submit">Cadastrar</button>
            </form>

            {/* Listagem */}
            <ul style={{ listStyle: 'none', padding: 0 }}>
                {/* O .map() percorre o array e devolve um pedaço de HTML para cada item */}
                {listaPessoas.map((pessoa) => (
                    <li key={pessoa.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '10px', borderBottom: '1px solid #ccc' }}>
                        <span>{pessoa.nome} - {pessoa.idade} anos</span>
                        <button onClick={() => deletarPessoa(pessoa.id)} style={{ color: 'red' }}>Excluir</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}