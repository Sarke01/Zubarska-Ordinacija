using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Entiteti.Bazni;

namespace ZubarskaOrdinacija.Domain.Entiteti
{
    public class Termin : Entitet<Guid>
    {
        public Termin(Guid id,Zubar zubar, Pacijent pacijent, DateTime? datum, string? napomena, StatusTermina status) : base(id)
        {
            Zubar = zubar;
            Pacijent = pacijent;
            Datum = datum;
            Napomena = napomena;
            Status = status;
        }

        public Termin()
        {
        }

        public Zubar Zubar { get;  set; }
        public Pacijent Pacijent { get;  set; }
        public DateTime? Datum { get;  set; }
        public string? Napomena { get;  set; }
        public StatusTermina Status { get;  set; }
    }

    public enum StatusTermina
    {
        Na_Cekanju,
        Odbijen,
        Zakazan
    }
}
