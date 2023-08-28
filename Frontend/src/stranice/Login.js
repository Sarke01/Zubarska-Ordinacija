import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import "../Stilovi/Login.css"

// export let userRole = 'admin';

// export const setMyState = (newValue) => {
//   userRole = newValue;
// };

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigacija = useNavigate();


  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  
    const handleSubmit = async (e) => {
        e.preventDefault();
        
        const data = {
          email,
          password
        };
        
        try {
          const response = await fetch('https://localhost:7014/api/Korisnik/login', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(data),
            credentials: 'include' // Dodajte ovu opciju kako biste uključili kolačiće
          });
      
          // Rukovanje odgovorom kao i pre
          if (response.ok) {
            console.log('Login successful');
            
            // Izvadite kolačiće iz odgovora
          const cookies = response.headers.get('Set-Cookie');
          if (cookies) {
            // Sačuvajte kolačiće u pregledaču
            document.cookie = cookies;
          }       
       

          navigacija("/dashboard");
          } else {
            console.log('Login failed');
          }
          
        } catch (error) {
          console.log('An error occurred', error);
        }
      
        setEmail('');
        setPassword('');
      };

      function handleRegisterClick(){
        navigacija("/registracija")
      }
      

      return (
        <div className='login-container'>
          <div className="login-card" >
          <h2>Prijava</h2>
          <form className="login-form" onSubmit={handleSubmit}>
            <div className="input-group">
              <label htmlFor="email">Email:</label>
              <input
                type="email"
                id="email"
                className="input-field"
                value={email}
                onChange={handleEmailChange}
              />
            </div>
            <div className="input-group">
              <label htmlFor="password">Lozinka:</label>
              <input
                type="password"
                id="password"
                className="input-field"
                value={password}
                onChange={handlePasswordChange}
              />
            </div>
            <button type="submit" className="submit-btn">Prijavi se</button>
            <button className="submit-btn" onClick={handleRegisterClick}>Registruj se</button>
          </form>
        </div>
        </div>
      );
}

export default Login;
