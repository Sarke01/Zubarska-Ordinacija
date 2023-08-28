using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Servisi.Korisnik.DTO
{
    public class NoviZubarDTO
    {
        public NoviZubarDTO(string ime, string prezime, string brojTelefona, string email, string lozinka) 
        {
            Ime = ime;
            Prezime = prezime;
            BrojTelefona = brojTelefona;
            BrojTelefona = brojTelefona;
            Email = email;
            Lozinka = lozinka;
        }

        public string Ime { get; private set; }
        public string Prezime { get; private set; }
        public string BrojTelefona { get; private set; }
        public string Email { get; private set; }
        public string Lozinka { get; private set; }
    }
}
