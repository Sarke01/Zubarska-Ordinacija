using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Entiteti.Bazni;

namespace ZubarskaOrdinacija.Domain.Entiteti
{
    public class Pacijent : Entitet<Guid>
    {     
        public Pacijent(Guid id,string ime, string prezime, DateTime datumRodjenja, string adresa, string brojTelefona, string email) : base(id)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Adresa = adresa;
            BrojTelefona = brojTelefona;
            Email = email;
        }
        public Pacijent()
        {
        }

        public string Ime { get; private set; }
        public string Prezime { get; private set; }
        public DateTime DatumRodjenja { get; private set; }
        public string Adresa { get; private set; }
        public string BrojTelefona { get; private set; }
        public string Email { get; private set; }

    }
}
