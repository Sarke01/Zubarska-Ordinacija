using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
using ZubarskaOrdinacija.Domain.Servisi.Korisnik;
using ZubarskaOrdinacija.Domain.Servisi.Zubar.DTO;

namespace ZubarskaOrdinacija.Domain.Servisi.Zubar
{
    public class ZubarService
    {
        private readonly IAplikacioniDbContext _aplikacioniDbContext;
        private readonly KorisnikService _korisnikService;

        public ZubarService(IAplikacioniDbContext aplikacioniDbContext, KorisnikService korisnikService)
        {
            _aplikacioniDbContext = aplikacioniDbContext;
            _korisnikService = korisnikService;
        }

        public Entiteti.Zubar DobaviZubaraPoId(Guid zubarId)
        {
            return _aplikacioniDbContext.Zubari.FirstOrDefault(z => z.ID == zubarId);
        }


        public Entiteti.Zubar DobaviZubaraPoImenu(string ime)
        {
            var zubar = _aplikacioniDbContext.Zubari.FirstOrDefault(z => z.Ime == ime);
            return zubar;
        }

        public async Task<List<Entiteti.Zubar>> DobaviSveZubare()
        {
            return await _aplikacioniDbContext.Zubari.ToListAsync();
        }


        //public async Task NapraviNovogZubara(NoviZubarDTO noviZubarDTO)
        //{ 
        //    Guid zubarId = Guid.NewGuid();
        //    Entiteti.Zubar noviZubar= new(id: zubarId,
        //                            ime: noviZubarDTO.Ime,
        //                            prezime: noviZubarDTO.Prezime,
        //                            brojTelefona: noviZubarDTO.BrojTelefona,
        //                            email: noviZubarDTO.Email);

        //    _aplikacioniDbContext.Zubari.Add(noviZubar);
        //    await _aplikacioniDbContext.SaveChangesAsync();
        //}

        public async Task PromeniZubara(Guid id,PromeniZubaraDTO promeniZubara)
        {
            Entiteti.Zubar zubarDb = await _aplikacioniDbContext.Zubari.FirstOrDefaultAsync(z => z.ID == id) ??
            throw new KeyNotFoundException("Uneti Zubar ne postoji!");

            zubarDb.Ime = promeniZubara.Ime;
            zubarDb.Prezime = promeniZubara.Prezime;
            zubarDb.BrojTelefona = promeniZubara.BrojTelefona;

            await _aplikacioniDbContext.SaveChangesAsync();
        }

        public async Task ObrisiZubara(Guid zubarId)
        {
            var zubar = _aplikacioniDbContext.Zubari.FirstOrDefault(z => z.ID == zubarId);

            if (zubar != null)
            {
                _aplikacioniDbContext.Zubari.Remove(zubar);
                await _aplikacioniDbContext.SaveChangesAsync();
            }
        }      
    }
}
