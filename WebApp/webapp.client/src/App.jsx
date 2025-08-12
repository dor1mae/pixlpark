import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginPage from './LoginPage';
import Success from './Success';

function App() {
  return (
      <BrowserRouter>
          <Routes>
              <Route path="/" element={<LoginPage />} />
              <Route path="/Success" element={ <Success/>} />
          </Routes>
      </BrowserRouter>
  );
}

export default App;