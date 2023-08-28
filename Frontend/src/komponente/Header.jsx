import React ,{useState,useEffect}from 'react'
import { Link } from 'react-router-dom';
import "../Stilovi/Header.css"
import { useNavigate } from "react-router-dom";


export default function Header() {

  const navigacija = useNavigate();
  const [userRole,setUserRole]=useState("");

  const handleLogout = async () => {
    try {
      const response = await fetch('https://localhost:7014/api/Korisnik/logout', {
        method: 'POST'
      });

      if (response.ok) {
          navigacija("/login");
      } else {
        console.error('Greška prilikom odjave:', response.statusText);
      }
    } catch (error) {
      console.error('Greška prilikom odjave:', error);
    }
  };


  async function fetchUserRole() {
    try {
      const response = await fetch('https://localhost:7014/api/Korisnik',{
        credentials: 'include'
      });
  
      if (response.ok) {
        const data = await response.text();
        setUserRole(data);
        console.log(data);
      } else {
        throw new Error('Error occurred while fetching data.');
      }
    } catch (error) {
      // Obrada grešaka
      console.error(error);
    }
  }

  useEffect(()=>{
    fetchUserRole()
  },[])

  return (
    <header>
    <nav>
      <ul>
        <li><Link to="/dashboard">Početna</Link></li>
        {userRole === 'Zubar' && <li><Link to="/izvestaj-za-zubara">Izvestaji</Link></li>}
        {userRole === 'Pacijent' && <li><Link to="/izvestaj-za-pacijenta">Izvestaji</Link></li>}
        <li><button onClick={handleLogout}>Odjava</button></li>
      </ul>
    </nav>
  </header>
  )
}
