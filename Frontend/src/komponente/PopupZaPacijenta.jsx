import React, { useState } from 'react'

export default function PopupZaPacijenta(props) {

const [napomena,setNapomena]=useState(props.napomena);
const handleChange = (event) => {
    setNapomena(event.target.value);
};

    return (props.trigger)? (
        <td className='popup'>
            <div className='popup-inner'>
            <h2>Napomena</h2>
                <div className='close-btn' onClick={()=>props.setTrigger(false)}><img src={require("../slike/x-button.png")} alt='x'/></div>
                <div className='input-container'>
                  <textarea value={napomena} onChange={(e)=>handleChange(e)}></textarea>
                </div>
                {/* <div className='flex-container'>
                  <button className='zakazi-button' onClick={zakazi}>Zakazi</button>
                  <button className='odbij-button' onClick={odbij}>Odbij</button>
                </div> */}
            </div>
        </td>
      ) :"";
}
