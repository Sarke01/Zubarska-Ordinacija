import React, { useState } from 'react';
import "../Stilovi/Registracija.css"
import { useNavigate } from "react-router-dom";

export default function Registracija() {
  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [datumRodjenja, setDatumRodjenja] = useState('');
  const [adresa, setAdresa] = useState('');
  const [brojTelefona, setBrojTelefona] = useState('');
  const [email, setEmail] = useState('');
  const [lozinka, setLozinka] = useState('');
  const navigacija = useNavigate();

  const handleImeChange = (e) => {
    setIme(e.target.value);
  };

  const handlePrezimeChange = (e) => {
    setPrezime(e.target.value);
  };

  const handleDatumRodjenjaChange = (e) => {
    setDatumRodjenja(e.target.value);
  };

  const handleAdresaChange = (e) => {
    setAdresa(e.target.value);
  };

  const handleBrojTelefonaChange = (e) => {
    setBrojTelefona(e.target.value);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handleLozinkaChange = (e) => {
    setLozinka(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    e.preventDefault();
        
        const data = {
          ime,
          prezime,
          datumRodjenja,
          adresa,
          brojTelefona,
          email,
          lozinka
        };
        
        try {
          const response = await fetch('https://localhost:7014/api/Korisnik/registracijaPacijenta', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(data),
          });
      
          if (response.ok) {
            console.log('Registracija uspesna');
               
          navigacija("/login");
          } else {
            console.log('Registracija nije uspela');
          }
        } catch (error) {
          console.log('An error occurred', error);
        }
        setIme("");
        setPrezime("");
        setDatumRodjenja("");
        setAdresa("");
        setBrojTelefona("");
        setEmail("");
        setLozinka("");
  };

  return (
    <div className="registracija-container">
      <form className="registracija-forma" onSubmit={handleSubmit}>
        <h2>Registracija</h2>
        <div className='input-group2'>
          <label htmlFor="ime">Ime:</label>
          <input
            type="text"
            id="ime"
            value={ime}
            onChange={handleImeChange}
            required
          />
        </div>
        <div className='input-group2'>
          <label htmlFor="prezime">Prezime:</label>
          <input
            type="text"
            id="prezime"
            value={prezime}
            onChange={handlePrezimeChange}
            required
          />
        </div>
        <div className='input-group2'>
          <label htmlFor="datum">Datum:</label>
          <input
            type="date"
            id="datum"
            value={datumRodjenja}
            onChange={handleDatumRodjenjaChange}
            required
          />
        </div>
        <div className='input-group2'>
          <label htmlFor="adresa">Adresa:</label>
          <input
            type="text"
            id="adresa"
            value={adresa}
            onChange={handleAdresaChange}
            required
          />
        </div>
        <div className='input-group2'>
          <label htmlFor="brojTelefona">Broj Telefona:</label>
          <input
            type="text"
            id="brojTelefona"
            value={brojTelefona}
            onChange={handleBrojTelefonaChange}
            required
          />
        </div>
        <div className='input-group2'>
          <label htmlFor="email">Email:</label>
          <input
            type="Email"
            id="email"
            value={email}
            onChange={handleEmailChange}
            required
          />
        </div>
        <div className='input-group2'>
          <label htmlFor="lozinka">Lozinka:</label>
          <input
            type="password"
            id="lozinka"
            value={lozinka}
            onChange={handleLozinkaChange}
            required
          />
        </div>
        <div>
          <button type="submit" onClick={handleSubmit} className='registracija-btn'>Registruj se</button>
        </div>
      </form>
    </div>
  );
}
