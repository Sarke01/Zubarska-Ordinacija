using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Servisi.Korisnik.DTO
{
    public class NoviPacijentDTO
    {
        public NoviPacijentDTO(string ime, string prezime, DateTime datumRodjenja, string adresa, string brojTelefona, string email,string lozinka)
        {
            
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Adresa = adresa;
            BrojTelefona = brojTelefona;
            Email = email;
            Lozinka = lozinka;
        }

        public string Ime { get; private set; }
        public string Prezime { get; private set; }
        public DateTime DatumRodjenja { get; private set; }
        public string Adresa { get; private set; }
        public string BrojTelefona { get; private set; }
        public string Email { get; private set; }
        public string Lozinka { get; private set; }
    }
}
