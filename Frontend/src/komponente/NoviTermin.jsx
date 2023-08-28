import React, { useState,useEffect } from 'react';
import "../Stilovi/NoviTermin.css";
import { useRef } from 'react';


const NoviTermin = () => {
  const [datum, setDatum] = useState('');
  const [imeZubara, setImeZubara] = useState();
  const [napomena, setNapomena] = useState('');
  const [zubari,setZubari]=useState([]);
  const errorMessageRef = useRef(null);
  

  const handleDatumChange = (e) => {
    setDatum(e.target.value);
    
  };

  const handleImeZubaraChange = (e) => {
    setImeZubara(e.target.value);
   
  };

  const handleNapomenaChange = (e) => {
    setNapomena(e.target.value);
   
  };

  function validateDateTime(dateTimeString) {
    const currentDate = new Date();
    const selectedDate = new Date(dateTimeString);
    const selectedTime = selectedDate.getHours();
  
    const isDateValid = selectedDate >= currentDate;
    const isTimeValid = selectedTime >= 9 && selectedTime <= 20;
  
    return isDateValid && isTimeValid;
  }
  
const formData = {
    datum: datum,
    imeZubara: imeZubara,
    napomena: napomena,
  };  

  const handleSubmit = async (e) => {
    e.preventDefault();

    console.log(validateDateTime(datum));

    if(!validateDateTime(datum)) 
    {
      console.log("Los format datuma");
      return;
    }

    try {
        const response = await fetch("https://localhost:7014/api/Termin", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
          body: JSON.stringify(formData),
        });
        console.log(JSON.stringify(formData));
        if (response.ok) {
          console.log("Termin uspešno kreiran.");
          window.location.reload();
        } else {
          throw new Error("Greška prilikom kreiranja termina.");
        }
      } catch (error) {
        console.error(error);
      }
  };

  useEffect(() => {
    const fetchZubari = async () => {
      try {
        const response = await fetch('https://localhost:7014/api/Zubar');
        if (response.ok) {
          const data = await response.json();
          setZubari(data);
          setImeZubara(data[0].ime)
        } else {
          throw new Error('Error occurred while fetching zubari.');
        }
      } catch (error) {
        console.error(error);
      }
    };

    fetchZubari();
  }, []);

  

  return (
    <div className="novi-termin-container">
      <h2>Zakazi novi termin</h2>
      <form className="novi-termin-form" onSubmit={handleSubmit}>
        <div className="form-group" id='form-group-datum'>
          <label htmlFor="datum">Datum:</label>
          <input
            type="datetime-local"
            id="datum"
            value={datum}
            onChange={handleDatumChange}
            pattern="\d{4}-\d{2}-\d{2}T\d{2}:\d{2}"
            title="Unesite validan datum i vreme u formatu YYYY-MM-DDTHH:MM"
            required
          />
         <p ref={errorMessageRef} className="error-message" >Datum nije validan.</p>
        </div>
        <div className="form-group">
          <label htmlFor="imeZubara">Ime zubara:</label>
          <select
            id="imeZubara"
            value={imeZubara}
            onChange={handleImeZubaraChange} 
            required
          >
            {zubari.map((zubar) => (
              <option key={zubar.id} value={zubar.ime}>{zubar.ime}</option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="napomena">Napomena:</label>
          <textarea
            id="napomena"
            value={napomena}
            onChange={handleNapomenaChange}
            style={{ resize: "none" }}
          ></textarea>
        </div>
        <button type="submit">Sačuvaj</button>
      </form>
    </div>
  );
};

export default NoviTermin;
