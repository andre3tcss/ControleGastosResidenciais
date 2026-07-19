import { useState, useEffect } from 'react';
import { api } from '../services/api';
import type { RelatorioGeral } from '../types';

export default function Dashboard() {
    // Inicializa como null, pois a requisição demora alguns milissegundos
    const [relatorio, setRelatorio] = useState<RelatorioGeral | null>(null);

    useEffect(() => {
        carregarTotais();
    }, []);

    async function carregarTotais() {
        try {
            const resposta = await api.get('/Relatorios/totais');
            setRelatorio(resposta.data);
        } catch (error) {
            console.error("Erro ao carregar totais", error);
        }
    }

    // Função utilitária para formatar números no padrão financeiro do Brasil
    function formatarMoeda(valor: number) {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(valor);
    }

    // Se o relatório ainda for null (API ainda não respondeu), mostra um texto de carregamento
    if (!relatorio) {
        return <p style={{ textAlign: 'center' }}>Carregando painel de controle...</p>;
    }

    return (
        <div style={{ padding: '20px', maxWidth: '800px', margin: '40px auto', backgroundColor: '#fff', borderRadius: '8px', boxShadow: '0 4px 8px rgba(0,0,0,0.1)' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <h2>Painel Executivo de Totais</h2>
                {/* Botão para atualizar os dados manualmente sem precisar dar F5 */}
                <button onClick={carregarTotais} style={{ padding: '5px 15px', cursor: 'pointer' }}>Atualizar Dados</button>
            </div>

            {/* Tabela de Totais por Pessoa */}
            <table style={{ width: '100%', marginTop: '20px', borderCollapse: 'collapse', textAlign: 'left' }}>
                <thead>
                    <tr style={{ backgroundColor: '#f0f0f0', borderBottom: '2px solid #ccc' }}>
                        <th style={{ padding: '10px' }}>Morador</th>
                        <th style={{ padding: '10px', color: 'green' }}>Receitas</th>
                        <th style={{ padding: '10px', color: 'red' }}>Despesas</th>
                        <th style={{ padding: '10px' }}>Saldo Líquido</th>
                    </tr>
                </thead>
                <tbody>
                    {relatorio.pessoas.map((pessoa, index) => (
                        <tr key={index} style={{ borderBottom: '1px solid #eee' }}>
                            <td style={{ padding: '10px' }}>{pessoa.nome}</td>
                            <td style={{ padding: '10px', color: 'green' }}>{formatarMoeda(pessoa.totalReceitas)}</td>
                            <td style={{ padding: '10px', color: 'red' }}>{formatarMoeda(pessoa.totalDespesas)}</td>
                            <td style={{ padding: '10px', fontWeight: 'bold', color: pessoa.saldoLiquido >= 0 ? 'green' : 'red' }}>
                                {formatarMoeda(pessoa.saldoLiquido)}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* Rodapé de Consolidação Global */}
            <div style={{ marginTop: '30px', padding: '15px', backgroundColor: '#eef', borderRadius: '5px', display: 'flex', justifyContent: 'space-between', fontWeight: 'bold', fontSize: '1.1em' }}>
                <span>TOTAL GERAL DO SISTEMA</span>
                <span style={{ color: 'green' }}>{formatarMoeda(relatorio.totalGeralReceitas)}</span>
                <span style={{ color: 'red' }}>{formatarMoeda(relatorio.totalGeralDespesas)}</span>
                <span style={{ color: relatorio.saldoLiquidoGeral >= 0 ? 'green' : 'red' }}>
                    {formatarMoeda(relatorio.saldoLiquidoGeral)}
                </span>
            </div>
        </div>
    );
}