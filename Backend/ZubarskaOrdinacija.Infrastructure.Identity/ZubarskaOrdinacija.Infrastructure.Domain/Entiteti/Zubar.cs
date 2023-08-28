using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Entiteti.Bazni;

namespace ZubarskaOrdinacija.Domain.Entiteti
{
    public class Zubar : Entitet<Guid>
    {
        public Zubar(Guid id, string ime, string prezime, string brojTelefona, string email) : base(id)
        {
            Ime = ime;
            Prezime = prezime;
            BrojTelefona = brojTelefona;
            Email = email;
        }

        public Zubar()
        {
        }

        public string Ime { get;  set; }
        public string Prezime { get;  set; }
        public string BrojTelefona { get;  set; }
        public string Email { get; private set; }
    }

    
}
