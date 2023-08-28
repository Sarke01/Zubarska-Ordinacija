import React from 'react'
import { useState } from 'react';
import { useEffect } from 'react';
import TerminZaPacijenta from '../komponente/TerminZaPacijenta';
import "../Stilovi/PacijentDashboard.css";
import NoviTermin from '../komponente/NoviTermin';



export default function PacijentDashboard() {

    const [termini,setTermini]=useState([]);

    async function fetchData() {
        try {
          const response = await fetch('https://localhost:7014/api/Termin/zaPacijenta', {
            credentials: 'include'
          });
    
          if (response.ok) {
            const data = await response.json();
            setTermini(data);
          } else {
            console.log('Request failed:', response.status);
          }
        } catch (error) {
          console.log('An error occurred:', error);
        }
      }
      
    
      useEffect(() => {
        fetchData();
      }, []);

  return (
    
    <>
    <NoviTermin/>
      <table className="termini-table">
      <thead>
        <tr>
          <th>Datum termina</th>
          <th>Ime zubara</th>
          <th>Napomena</th>
          <th>Status termina</th>
        </tr>
      </thead>
      <tbody>
        {termini.map((termin) => (
          <TerminZaPacijenta key={termin.id} termin={termin} />
        ))}
      </tbody>
    </table>
    </>
  )
}
