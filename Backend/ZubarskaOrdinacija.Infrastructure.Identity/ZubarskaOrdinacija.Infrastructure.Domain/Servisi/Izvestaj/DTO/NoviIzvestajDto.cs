using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Servisi.Izvestaj.DTO
{
    public class NoviIzvestajDto
    {
        public NoviIzvestajDto(string emailPacijenta, DateTime datum, string? opis, string? dijagnoza, string? recepti, string? napomena)
        {
            EmailPacijenta = emailPacijenta;
            this.datum = datum;
            Opis = opis;
            Dijagnoza = dijagnoza;
            Recepti = recepti;
            Napomena = napomena;
        }

        public string EmailPacijenta { get; private set; }
        public DateTime datum { get; private set; }
        public string? Opis { get; private set; }
        public string? Dijagnoza { get; private set; }
        public string? Recepti { get; private set; }
        public string? Napomena { get; private set; }
    }


}
