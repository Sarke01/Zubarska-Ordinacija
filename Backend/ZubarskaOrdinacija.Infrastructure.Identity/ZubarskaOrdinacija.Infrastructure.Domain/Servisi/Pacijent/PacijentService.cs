using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent.DTO;

namespace ZubarskaOrdinacija.Domain.Servisi.Pacijent
{
    public class PacijentService
    {
        private readonly IAplikacioniDbContext _aplikacioniDbContext;

        public PacijentService(IAplikacioniDbContext aplikacioniDbContext)
        {
            _aplikacioniDbContext = aplikacioniDbContext;
        }

        public Entiteti.Pacijent DobaviPacijentaPoId(Guid id)
        {
            Entiteti.Pacijent pacijent = _aplikacioniDbContext.Pacijenti.SingleOrDefault(p => p.ID == id);
            return pacijent ?? throw new Exception("Pacijent nije pronađen.");
        }

        public async Task NapraviNovogPacijenta(KreirajPacijentaDTO noviPacijent)
        {
            Guid pacijentId = Guid.NewGuid();
            Entiteti.Pacijent pacijentZaDodati = new(id: pacijentId,
                                    ime: noviPacijent.Ime,
                                    prezime: noviPacijent.Prezime,
                                    datumRodjenja: noviPacijent.DatumRodjenja,
                                    adresa: noviPacijent.Adresa,
                                    brojTelefona: noviPacijent.BrojTelefona,
                                    email: noviPacijent.Email);

            _aplikacioniDbContext.Pacijenti.Add(pacijentZaDodati);
            await _aplikacioniDbContext.SaveChangesAsync();
        }

        public  Entiteti.Pacijent DobaviPacijentaPoEmail(string email)
        {
            Entiteti.Pacijent pacijent = _aplikacioniDbContext.Pacijenti.SingleOrDefault(p => p.Email == email);
            return pacijent ?? throw new Exception("Pacijent nije pronađen.");
        }
    }
}
