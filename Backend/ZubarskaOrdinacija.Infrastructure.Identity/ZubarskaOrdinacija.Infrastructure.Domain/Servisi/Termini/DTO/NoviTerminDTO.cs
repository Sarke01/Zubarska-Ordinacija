using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Entiteti;

namespace ZubarskaOrdinacija.Domain.Servisi.Termini.DTO
{
    public class NoviTerminDTO
    {
        public NoviTerminDTO(string imeZubara, DateTime? datum, string? napomena)
        {
            ImeZubara = imeZubara;
            Datum = datum;
            Napomena = napomena;
        }

        public string ImeZubara { get; private set; }
        public DateTime? Datum { get; private set; }
        public string? Napomena { get; private set; }
    }
}

