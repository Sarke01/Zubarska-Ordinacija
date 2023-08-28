import React, { useState, useEffect } from 'react';
import PacijentovIzvestaj from '../komponente/PacijentovIzvestaj';
export default function IzvestajZaPacijenta() {

  const [izvestaji, setIzvestaji] = useState([]);


  useEffect(() => {
    // Fetch izveštaje za pacijenta sa URL-a
    fetch('https://localhost:7014/api/Izvestaj/zaPacijenta',{
      credentials: 'include'
    })
      .then(response => response.json())
      .then(data => {
        setIzvestaji(data);
      })
      .catch(error => {
        console.error('Greška prilikom dobijanja izveštaja:', error);
      });

  }, []);


  return (
    <div className='kontejner'>
      <h2>Izveštaji za pacijenta</h2>
      <div className="izvestaji-grid">
        {izvestaji.map((izvestaj, index) => (
          <PacijentovIzvestaj izvestaj={izvestaj} key={index} />
        ))}
      </div>
  </div>
  )
}
