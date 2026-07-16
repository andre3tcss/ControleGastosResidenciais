import Pessoas from './components/Pessoas';
import Transacoes from './components/Transacoes';
import Dashboard from './components/Dashboard';

export default function App() {
    return (
        <div>
            <h1 style={{ textAlign: 'center', marginTop: '20px' }}>Controle de Gastos Residenciais</h1>
            <hr style={{ margin: '20px 0' }} />

            <div style={{ display: 'flex', justifyContent: 'space-evenly', flexWrap: 'wrap' }}>
                <Pessoas />
                <Transacoes />
            </div>

            <Dashboard />
        </div>
    );
}