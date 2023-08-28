using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Servisi.Izvestaj.DTO
{
    public class PromeniIzvestajDTO
    {
        public PromeniIzvestajDTO(DateTime datum, string? opis, string? dijagnoza, string? recepti, string? napomena)
        {
            this.datum = datum;
            Opis = opis;
            Dijagnoza = dijagnoza;
            Recepti = recepti;
            Napomena = napomena;
        }

        public DateTime datum { get; private set; }
        public string? Opis { get; private set; }
        public string? Dijagnoza { get; private set; }
        public string? Recepti { get; private set; }
        public string? Napomena { get; private set; }
    }
}
