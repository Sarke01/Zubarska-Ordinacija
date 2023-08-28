using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Entiteti.Bazni;

namespace ZubarskaOrdinacija.Domain.Entiteti
{
    public class Izvestaj : Entitet<Guid>
    {
        public Izvestaj(Guid id,Pacijent pacijent, Zubar zubar, DateTime datum, string? opis, string? dijagnoza, string? recepti, string? napomena) : base(id)
        {
            Pacijent = pacijent;
            Zubar = zubar;
            this.datum = datum;
            Opis = opis;
            Dijagnoza = dijagnoza;
            Recepti = recepti;
            Napomena = napomena;
        }

        public Izvestaj()
        {
        }

        public Pacijent Pacijent { get;  set; }
        public Zubar Zubar { get;  set; }
        public DateTime datum { get;  set; }
        public string? Opis { get;  set; }
        public string? Dijagnoza { get;  set; }
        public string? Recepti { get;  set; }
        public string? Napomena { get;  set; }
    }


}
