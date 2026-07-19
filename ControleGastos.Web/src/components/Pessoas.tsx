import { useState, useEffect, FormEvent } from 'react';
import { api } from '../services/api';
import type { Pessoa } from '../types';

export default function Pessoas() {
    // 1. GERENCIAMENTO DE ESTADO LOCAL
    // Mantém o estado sincronizado com a base de dados de pessoas e os inputs do formulário
    const [listaPessoas, setListaPessoas] = useState<Pessoa[]>([]);
    const [nome, setNome] = useState('');
    const [idade, setIdade] = useState('');

    // 2. CICLO DE VIDA DO COMPONENTE
    // Inicializa a carga de dados no momento da montagem do componente na árvore de renderização
    useEffect(() => {
        carregarPessoas();
    }, []);

    // 3. INTEGRAÇÃO COM A API E SERVIÇOS externo
    async function carregarPessoas() {
        try {
            const resposta = await api.get('/Pessoas');
            setListaPessoas(resposta.data);
        } catch (error) {
            console.error("Erro ao buscar pessoas", error);
        }
    }

    async function cadastrarPessoa(evento: FormEvent) {
        evento.preventDefault(); // Intercepta o comportamento nativo do formulário para processamento assíncrono

        try {
            await api.post('/Pessoas', {
                nome: nome,
                idade: Number(idade) // Tipagem explícita exigida pelo contrato do endpoint
            });

            // Restaura o estado inicial dos campos após a persistência bem-sucedida
            setNome('');
            setIdade('');

            // Atualiza a listagem para garantir a consistência dos dados em tela
            carregarPessoas();
        } catch (error) {
            alert("Erro ao cadastrar pessoa.");
        }
    }

    async function deletarPessoa(id: string) {
        try {
            await api.delete(`/Pessoas/${id}`);
            carregarPessoas(); // Sincroniza o estado local após a exclusão no banco de dados
        } catch (error) {
            alert("Erro ao deletar pessoa.");
        }
    }

    // 4. INTERFACE DO USUÁRIO (JSX)
    return (
        <div style={{ padding: '20px', maxWidth: '600px', margin: '0 auto' }}>
            <h2>Gerenciar Pessoas</h2>

            {/* Formulário de Captura de Dados */}
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

            {/* Renderização Dinâmica da Coleção de Dados */}
            <ul style={{ listStyle: 'none', padding: 0 }}>
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