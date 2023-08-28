import './App.css';
import {  useLocation } from 'react-router-dom';
import AppRoutes from './komponente/AppRoutes';
import Header from './komponente/Header';

function App() {

  const location = useLocation();

  const isLoginPage = location.pathname === '/login' || location.pathname==="/" || location.pathname==="/registracija";

  return (
    <div className="App">
    {!isLoginPage && <Header />}
        <AppRoutes/>
    </div>
  );
}

export default App;
