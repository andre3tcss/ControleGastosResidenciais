import { useState, useEffect, FormEvent } from 'react';
import { api } from '../services/api';
import type { Transacao, Pessoa } from '../types';

export default function Transacoes() {
    const [listaTransacoes, setListaTransacoes] = useState<Transacao[]>([]);
    const [listaPessoas, setListaPessoas] = useState<Pessoa[]>([]);

    // Estados do formulário
    const [descricao, setDescricao] = useState('');
    const [valor, setValor] = useState('');
    const [tipo, setTipo] = useState('0'); // 0 = Despesa por padrão
    const [pessoaId, setPessoaId] = useState('');

    useEffect(() => {
        carregarDados();
    }, []);

    // Carrega transações e pessoas de forma paralela
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

        if (!pessoaId) {
            alert("Por favor, selecione uma pessoa.");
            return;
        }

        try {
            await api.post('/Transacoes', {
                descricao: descricao,
                valor: Number(valor),
                tipo: Number(tipo),
                pessoaId: pessoaId
            });

            // Limpa o formulário e recarrega a lista
            setDescricao('');
            setValor('');
            carregarDados();

        } catch (error: any) {
            if (error.response && error.response.status === 400) {
                alert(error.response.data.erro);
            } else {
                alert("Erro genérico ao cadastrar transação.");
            }
        }
    }

    return (
        <div style={{ padding: '20px', maxWidth: '600px', margin: '0 auto' }}>
            <h2>Lançar Transações</h2>

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
                    {/* Renderiza um <option> para cada pessoa que veio do banco */}
                    {listaPessoas.map(p => (
                        <option key={p.id} value={p.id}>{p.nome}</option>
                    ))}
                </select>

                <button type="submit">Lançar</button>
            </form>

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