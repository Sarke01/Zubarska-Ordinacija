import React, { useState,useEffect } from 'react'
import TerminZaZubara from '../komponente/TerminZaZubara';


export default function ZubarDashboard() {

  const [termini,setTermini]=useState([]);

  async function fetchData() {
    try {
      const response = await fetch('https://localhost:7014/api/Termin/zaZubara', {
        credentials: 'include'
      });

      if (response.ok) {
        const data = await response.json();
        setTermini(data);
      } 
    } catch (error) {
      console.log('An error occurred:', error);
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <table className="termini-table">
    <thead>
      <tr>
        <th>Datum termina</th>
        <th>Ime pacijenta</th>
        <th>Napomena</th>
        <th>Status termina</th>
        <th colSpan="2">Zakazi / Odbij</th>
      </tr>
    </thead>
    <tbody>
      {termini.map((termin) => (
        <TerminZaZubara key={termin.id} termin={termin} />
      ))}
    </tbody>
  </table>
  )
}
