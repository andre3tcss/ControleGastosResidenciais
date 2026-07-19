import { useState, useEffect, FormEvent } from 'react';
import { api } from '../services/api';
import type { Transacao, Pessoa } from '../types';

export default function Transacoes() {
    // 1. GERENCIAMENTO DE ESTADO LOCAL
    // Armazena as coleções sincronizadas da API e os estados de controle do formulário
    const [listaTransacoes, setListaTransacoes] = useState<Transacao[]>([]);
    const [listaPessoas, setListaPessoas] = useState<Pessoa[]>([]);

    const [descricao, setDescricao] = useState('');
    const [valor, setValor] = useState('');
    const [tipo, setTipo] = useState('0'); // Define 'Despesa' como estado padrão (Enum indexado em 0)
    const [pessoaId, setPessoaId] = useState('');

    // 2. CICLO DE VIDA DO COMPONENTE
    // Dispara a carga de dados inicial assim que o componente é montado na interface
    useEffect(() => {
        carregarDados();
    }, []);

    // 3. INTEGRAÇÃO COM A API E REGRAS DE NEGÓCIO
    // Executa requisições assíncronas concorrentes para otimizar o tempo de carregamento da interface
    async function carregarDados() {
        try {
            const resTransacoes = await api.get('/Transacoes');
            const resPessoas = await api.get('/Pessoas');
            setListaTransacoes(resTransacoes.data);
            setListaPessoas(resPessoas.data);
        } catch (error) {
            console.error("Erro ao carregar dados", error);
        }
    }

    async function cadastrarTransacao(evento: FormEvent) {
        evento.preventDefault();

        // Validação de pré-condição no cliente antes do envio da requisição
        if (!pessoaId) {
            alert("Por favor, selecione uma pessoa.");
            return;
        }

        try {
            await api.post('/Transacoes', {
                descricao: descricao,
                valor: Number(valor),
                tipo: Number(tipo), // Converte o valor do input para o formato do Enum esperado pela API
                pessoaId: pessoaId
            });

            // Restaura o estado inicial do formulário e revalida a listagem
            setDescricao('');
            setValor('');
            carregarDados();

        } catch (error: any) {
            // Intercepta a validação de domínio (HTTP 400) da API e exibe a notificação ao usuário
            if (error.response && error.response.status === 400) {
                alert(error.response.data.erro);
            } else {
                alert("Erro genérico ao cadastrar transação.");
            }
        }
    }

    // 4. INTERFACE DO USUÁRIO (JSX)
    return (
        <div style={{ padding: '20px', maxWidth: '600px', margin: '0 auto' }}>
            <h2>Lançar Transações</h2>

            {/* Formulário de Captura e Registro de Transações Financeiras */}
            <form onSubmit={cadastrarTransacao} style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginBottom: '20px' }}>
                <input
                    type="text"
                    placeholder="Descrição (Ex: Conta de Luz)"
                    value={descricao}
                    onChange={(e) => setDescricao(e.target.value)}
                    required
                />

                <input
                    type="number"
                    step="0.01"
                    placeholder="Valor"
                    value={valor}
                    onChange={(e) => setValor(e.target.value)}
                    required
                />

                <select value={tipo} onChange={(e) => setTipo(e.target.value)}>
                    <option value="0">Despesa</option>
                    <option value="1">Receita</option>
                </select>

                <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} required>
                    <option value="">Selecione o Morador</option>
                    {/* Popula dinamicamente as opções com base na coleção de entidades de apoio */}
                    {listaPessoas.map(p => (
                        <option key={p.id} value={p.id}>{p.nome}</option>
                    ))}
                </select>

                <button type="submit">Lançar</button>
            </form>

            {/* Apresentação de Dados Consolidados */}
            <ul style={{ listStyle: 'none', padding: 0 }}>
                {listaTransacoes.map((t) => (
                    <li key={t.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '10px', borderBottom: '1px solid #ccc' }}>
                        <span>{t.descricao} - R$ {t.valor} ({t.tipo === 0 ? 'Despesa' : 'Receita'})</span>
                        <strong>{t.pessoa?.nome}</strong>
                    </li>
                ))}
            </ul>
        </div>
    );
}
