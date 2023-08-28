import React ,{useState,useEffect}from 'react'
import NoviIzvestaj from '../komponente/NoviIzvestaj'
import ZubarovIzvestaj from '../komponente/ZubarovIzvestaj';

export default function IzvestajZaZubara() {

  const [izvestaji, setIzvestaji] = useState([]);


  useEffect(() => {
    fetch('https://localhost:7014/api/Izvestaj/zaZubara',{
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
    <div>
      <NoviIzvestaj/>
      <div className='kontejner'>
        <h2>Izveštaji za zubara</h2>
        <div className="izvestaji-grid">
          {izvestaji.map((izvestaj, index) => (
            <ZubarovIzvestaj izvestaj={izvestaj} key={index} />
          ))}
        </div>
      </div>
    </div>
  )
}
