import React from 'react'
import "../Stilovi/PacijentovIzvestaj.css"

export default function PacijentovIzvestaj(props) {
    const {izvestaj}=props
    const formattedDate = new Date(izvestaj.datum).toLocaleDateString('sr-RS');
  return (
    <div className="izvestaj-card2">
        <div className="datum">
            Datum izvestaja : {formattedDate}
        </div>
        <div className="zubar">Zubar : {izvestaj.zubar.ime} {izvestaj.zubar.prezime}</div>
        <div className='dodatak'>
          <p className="opis">Opis :{izvestaj.opis}</p>
          <p className="dijagnoza">Dijagnoza :{izvestaj.dijagnoza}</p>
          <p className="recepti">Recepti :{izvestaj.recepti}</p>
          <p className="napomena">Napomena :{izvestaj.napomena}</p>
        </div>
    </div>
  )
}
