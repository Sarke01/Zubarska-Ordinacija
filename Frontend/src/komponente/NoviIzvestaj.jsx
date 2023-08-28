import React,{useState} from 'react'
import "../Stilovi/NoviIzvestaj.css"

export default function NoviIzvestaj() {

    const [email, setEmail] = useState('');
  const [opis, setOpis] = useState('');
  const [dijagnoza, setDijagnoza] = useState('');
  const [recepti, setRecepti] = useState('');
  const [napomena, setNapomena] = useState('');


  const handleSubmit = async (e) => {
    e.preventDefault();

    const today = new Date();
    const isoDateTimeString = today.toISOString();

    const data = {
      EmailPacijenta: email,
      datum: isoDateTimeString ,
      Opis: opis,
      Dijagnoza: dijagnoza,
      Recepti: recepti,
      Napomena: napomena,
    };
    console.log(isoDateTimeString);


    try {
      const response = await fetch('https://localhost:7014/api/Izvestaj', {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        console.log('Zahtev uspešno poslat');
      } else {
        console.log('Došlo je do greške prilikom slanja zahteva');
      }
    } catch (error) {
      console.error('Greška prilikom slanja zahteva:', error);
    }

    setEmail('');
    setOpis('');
    setDijagnoza('');
    setRecepti('');
    setNapomena('');
  };
  

  return (
    <div className="custom-form">
      <h2>Izvestaj zubara</h2>
      <form onSubmit={handleSubmit}>
        <div className="forma-grupa ">
          <label>Email:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
        </div>
        <div className="forma-grupa ">
          <label>Opis:</label>
          <textarea value={opis} onChange={(e) => setOpis(e.target.value)} required />
        </div>
        <div className="forma-grupa">
          <label>Dijagnoza:</label>
          <textarea type="text" value={dijagnoza} onChange={(e) => setDijagnoza(e.target.value)} required />
        </div>
        <div className="forma-grupa ">
          <label>Recepti:</label>
          <textarea type="text" value={recepti} onChange={(e) => setRecepti(e.target.value)} required />
        </div>
        <div className="forma-grupa">
          <label>Napomena:</label>
          <textarea value={napomena} onChange={(e) => setNapomena(e.target.value)} />
        </div>
        <button type="submit">Pošalji</button>
      </form>
    </div>
  )
}
