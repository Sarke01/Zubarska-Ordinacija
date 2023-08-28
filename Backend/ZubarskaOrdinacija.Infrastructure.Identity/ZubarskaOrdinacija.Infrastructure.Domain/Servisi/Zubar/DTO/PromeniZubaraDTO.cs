using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Servisi.Zubar.DTO
{
    public  class PromeniZubaraDTO
    {
        public PromeniZubaraDTO(string ime, string prezime, string brojTelefona)
        {
            Ime = ime;
            Prezime = prezime;
            BrojTelefona = brojTelefona;
        }

        public string Ime { get; private set; }
        public string Prezime { get; private set; }
        public string BrojTelefona { get; private set; }
    }
}
