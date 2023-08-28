import React, { useState } from 'react'
import "../Stilovi/TerminZaPacijenta.css"
import PopupZaPacijenta from './PopupZaPacijenta';

export default function TerminZaPacijenta(props) {

const {termin } = props;

const [trigger,setTrigger]=useState(false)
const [napomena,setNapomena]=useState(termin.napomena)


function prikaziSve(celaNapomena){
   setNapomena(celaNapomena)
   setTrigger(true)
}

  return (
  <tr>
    <td>{new Date(termin.datum).toLocaleString('sr-RS', { dateStyle: 'medium', timeStyle: 'short' })}</td>
    <td>{termin.zubar.ime} {termin.zubar.prezime}</td>
    <td className='napomena2' onClick={()=>prikaziSve(termin.napomena)}>{termin.napomena.length >20 ? termin.napomena.slice(0,20)+"..." : termin.napomena  } 
    </td>
    <td>{termin.status === 0 ? 'Na čekanju' : termin.status === 1 ? 'Odbijen' : 'Zakazan'}</td>
    <PopupZaPacijenta trigger={trigger} napomena={napomena} setTrigger={setTrigger} />
  </tr>

  )
}
