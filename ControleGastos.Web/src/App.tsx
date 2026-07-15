// Arquivo: src/App.tsx
import Pessoas from './components/Pessoas';

export default function App() {
    return (
        <div>
            <h1 style={{ textAlign: 'center', marginTop: '20px' }}>Controle de Gastos Residenciais</h1>
            <hr style={{ margin: '20px 0' }} />

            {/* Chamando o nosso componente */}
            <Pessoas />
        </div>
    );
}