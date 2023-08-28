import React from 'react'
import { useState } from 'react';
import Popup from './Popup';
import "../Stilovi/TerminZaZubara.css"

export default function TerminZaZubara(props) {

const {termin } = props;

const [trigger,setTrigger]=useState(false)
const [napomena,setNapomena]=useState(termin.napomena)


function prikaziSve(celaNapomena){
setNapomena(celaNapomena)
setTrigger(true)
}

async function  zakaziTermin(){
    try {
        console.log(termin.id);
        const response = await fetch(`https://localhost:7014/api/Termin/potvrdi?id=${termin.id}&napomena=nesto`, {
          method: 'PUT',
          credentials: 'include',
          headers: {
            'Content-Type': 'application/json'
          },
        });
        
  
        if (response.ok) {
          console.log("Termin je zakazan");
        } else {
          console.error('Greška prilikom zakazivanja:', response.statusText);
        }
      } catch (error) {
        console.error('Greška prilikom zakazivanja:', error);
      }
}

async function  odbijTermin(){
  try {
      console.log(termin.id);
      const response = await fetch(`https://localhost:7014/api/Termin/odbij?id=${termin.id}&napomena=nesto`, {
        method: 'PUT',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json'
        },
      });
      

      if (response.ok) {
        console.log("Termin je zakazan");
      } else {
        console.error('Greška prilikom zakazivanja:', response.statusText);
      }
    } catch (error) {
      console.error('Greška prilikom zakazivanja:', error);
    }
}

  return (
    <tr>
    <td>{new Date(termin.datum).toLocaleString('sr-RS', { dateStyle: 'medium', timeStyle: 'short' })}</td>
    <td>{termin.pacijent.ime} {termin.pacijent.prezime}</td>
    <td className='napomena' onClick={()=>prikaziSve(termin.napomena)}>{termin.napomena.length >20 ? termin.napomena.slice(0,20)+"..." : termin.napomena  } 
    </td>
    <td>{termin.status === 0 ? 'Na čekanju' : termin.status === 1 ? 'Odbijen' : 'Zakazan'}</td>
    <Popup trigger={trigger} napomena={napomena} setTrigger={setTrigger} termin={termin} />
    <td colSpan="2" className='dugmad-za-termine'>
        <button className='pregledaj-termin' onClick={zakaziTermin}>Zakazi</button>
        <button className='pregledaj-termin crveno' onClick={odbijTermin}>Odbij</button>
    </td>
  </tr>
  )
}
