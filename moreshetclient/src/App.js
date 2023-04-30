import logo from './logo.svg';
import './App.css';
import ProductActives from './ManagerComp/ProdactActives';
import Schedule from './SecretaryComp/Schedule';
import Catalog from './CommonComp/Catalog';
import Login from './CommonComp/Login';
function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
          </header>
          <Login/>
          <ProductActives />
          <Schedule />
    </div>
  );
}

export default App;
