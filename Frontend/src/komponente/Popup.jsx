import React, { useState } from 'react'
import "../Stilovi/TerminZaPacijenta.css"

export default function Popup(props) {

  const [napomena,setNapomena]=useState(props.napomena);
  const handleChange = (event) => {
    setNapomena(event.target.value);
  };

async function zakazi(){
  try {
    const response = await fetch(`https://localhost:7014/api/Termin/potvrdi?id=${props.termin.id}&napomena=${napomena}`, {
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
  props.setTrigger(false)
}

async function odbij(){
  try {
    const response = await fetch(`https://localhost:7014/api/Termin/odbij?id=${props.termin.id}&napomena=${napomena}`, {
      method: 'PUT',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json'
      },
    });
    

    if (response.ok) {
      console.log("Termin je odbijen");
    } else {
      console.error('Greška prilikom zakazivanja:', response.statusText);
    }
  } catch (error) {
    console.error('Greška prilikom zakazivanja:', error);
  }
  props.setTrigger(false)
}

  return (props.trigger)? (
    <td className='popup'>
        <div className='popup-inner'>
        <h2>Napomena</h2>
            <div className='close-btn' onClick={()=>props.setTrigger(false)}><img src={require("../slike/x-button.png")} alt='x'/></div>
            <div className='input-container'>
              <textarea value={napomena} onChange={(e)=>handleChange(e)}></textarea>
            </div>
            <div className='flex-container'>
              <button className='zakazi-button' onClick={zakazi}>Zakazi</button>
              <button className='odbij-button' onClick={odbij}>Odbij</button>
            </div>
        </div>
    </td>
  ) :"";
}
