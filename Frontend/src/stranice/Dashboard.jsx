import React, { useEffect, useState } from 'react'
import PacijentDashboard from './PacijentDashboard';
import ZubarDashboard from './ZubarDashboard';


export default function Dashboard() {

  const [userRole,setUserRole]=useState("");

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
    <div>
      {userRole === 'Pacijent' && <PacijentDashboard />}
      {userRole === 'Zubar' && <ZubarDashboard />}
    </div>
  );
}
