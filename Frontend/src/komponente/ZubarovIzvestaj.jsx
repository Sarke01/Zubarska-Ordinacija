import React ,{useState} from 'react'
import "../Stilovi/ZubarovIzvestaj.css"

export default function ZubarovIzvestaj(props) {

  const {izvestaj}=props
  const formattedDate = new Date(izvestaj.datum).toLocaleDateString('sr-RS');

  const [opis, setOpis] = useState(izvestaj.opis);
  const [dijagnoza, setDijagnoza] = useState(izvestaj.dijagnoza);
  const [recepti, setRecepti] = useState(izvestaj.recepti);
  const [napomena, setNapomena] = useState(izvestaj.napomena);

  const handleOpisChange = (e) => {
    setOpis(e.target.value);
  };

  const handleDijagnozaChange = (e) => {
    setDijagnoza(e.target.value);
  };

  const handleReceptiChange = (e) => {
    setRecepti(e.target.value);
  };

  const handleNapomenaChange = (e) => {
    setNapomena(e.target.value);
  };

  const handlePromeniClick = async () => {
    const url = `https://localhost:7014/api/Izvestaj?id=${izvestaj.id}`;
    const today = new Date().toISOString();

    console.log(izvestaj.id);
    
    const data = {
      datum: today,
      opis: opis,        
      dijagnoza: dijagnoza,  
      recepti: recepti,    
      napomena: napomena   
    };

    console.log(JSON.stringify(data));
    
    try {
      const response = await fetch(url, {
        method: 'PATCH',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
      });
  
      if (response.ok) {
        console.log("Uspesno izmenjen izvestaj");
      } else {
        console.log("Greska prilikom izmene izvestaja");
      }
    } catch (error) {
      console.error('Greška prilikom slanja zahteva:', error);
    }
  };

  const handleObrisiClick = async () => {
    const potvrda = window.confirm("Da li ste sigurni da želite da obrišete izveštaj?");
    const url = `https://localhost:7014/api/Izvestaj?id=${izvestaj.id}`;

    if(!potvrda) return;
    
    try {
      const response = await fetch(url, {
        method: 'DELETE',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json'
        }
      });
  
      if (response.ok) {
        console.log("Uspesno obrisan izvestaj");
      } else {
        console.log("Greska prilikom brisanja izvestaja");
      }
    } catch (error) {
      console.error('Greška prilikom slanja zahteva:', error);
    }
  };


  return (
    <div className="izvestaj-card" >
        <div className="datum">{formattedDate}
        </div>
        <div className="pacijent">{izvestaj.pacijent.ime} {izvestaj.pacijent.prezime}</div>
        <div className='text-areas'>
          <textarea className="opis" value={opis} onChange={handleOpisChange}></textarea>
          <textarea className="dijagnoza" value={dijagnoza} onChange={handleDijagnozaChange}></textarea>
          <textarea className="recepti" value={recepti} onChange={handleReceptiChange}></textarea>
          <textarea className="napomena" value={napomena} onChange={handleNapomenaChange}></textarea>
        </div>
        <button className='btn-promeni' onClick={handlePromeniClick}>Promeni</button>
        <button className='btn-obrisi' onClick={handleObrisiClick}>Obrisi</button>
    </div>
  )
}
