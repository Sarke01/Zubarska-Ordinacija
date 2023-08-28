using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
using ZubarskaOrdinacija.Domain.Servisi.Izvestaj.DTO;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent;
using ZubarskaOrdinacija.Domain.Servisi.Zubar;

namespace ZubarskaOrdinacija.Domain.Servisi.Izvestaj
{
    public class IzvestajService
    {
        private readonly IAplikacioniDbContext _aplikacioniDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PacijentService _pacijentService;
        private readonly ZubarService _zubarService;

        public IzvestajService(IAplikacioniDbContext aplikacioniDbContext,
                                IHttpContextAccessor httpContextAccessor,
                                PacijentService pacijentService,
                                ZubarService zubarService)
        {
            _aplikacioniDbContext = aplikacioniDbContext;
            _httpContextAccessor = httpContextAccessor;
            _pacijentService = pacijentService;
            _zubarService = zubarService;
        }

        public async Task NapraviNoviIzvestaj(NoviIzvestajDto noviIzvestaj)
        {

            var ulogovaniZubarId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(ulogovaniZubarId, out Guid ulogovaniZubarIdGuid))
            {
                var ulogovaniZubar = _zubarService.DobaviZubaraPoId(ulogovaniZubarIdGuid);
                var pacijent = _pacijentService.DobaviPacijentaPoEmail(noviIzvestaj.EmailPacijenta);
                Guid izvestajId = Guid.NewGuid();
                Entiteti.Izvestaj izvestajZaDodati = new(id: izvestajId,
                                        pacijent: pacijent,
                                        zubar: ulogovaniZubar,
                                        datum: noviIzvestaj.datum,
                                        opis: noviIzvestaj.Opis,
                                        dijagnoza: noviIzvestaj.Dijagnoza,
                                        recepti: noviIzvestaj.Recepti,
                                        napomena: noviIzvestaj.Napomena);
                _aplikacioniDbContext.Izvestaji.Add(izvestajZaDodati);
            }

            await _aplikacioniDbContext.SaveChangesAsync();
        }

        public async Task<List<Entiteti.Izvestaj>> DobaviIzvestajePoPacijentId()
        {
            var ulogovaniPacijentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(ulogovaniPacijentId, out Guid ulogovaniPacijentIdGuid))
            {
                return await _aplikacioniDbContext.Izvestaji.
                    Where(i => i.Pacijent.ID == ulogovaniPacijentIdGuid)
                    .Include(i=>i.Zubar)
                    .Include(i=>i.Pacijent)
                    .ToListAsync() ?? throw new KeyNotFoundException("Izvestaj nije pronadjen");
            }
            throw new ArgumentException("Pogresan id");
        }

        public async Task<List<Entiteti.Izvestaj>> DobaviIzvestajePoZubarId()
        {
            var ulogovaniZubarId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(ulogovaniZubarId, out Guid ulogovaniZubarIdGuid))
            {
                return await _aplikacioniDbContext.Izvestaji
                    .Where(i => i.Zubar.ID == ulogovaniZubarIdGuid)
                    .Include(i => i.Zubar)
                    .Include(i => i.Pacijent)
                    .ToListAsync() ?? throw new KeyNotFoundException("Izvestaj nije pronadjen");
            }
            throw new ArgumentException("Pogresan id");
        }

        public async Task ObrisiIzvestaj(Guid izvestajId)
        {
            var izvestaj = _aplikacioniDbContext.Izvestaji.FirstOrDefault(i => i.ID == izvestajId);

            if (izvestaj != null)
            {
                _aplikacioniDbContext.Izvestaji.Remove(izvestaj);
                await _aplikacioniDbContext.SaveChangesAsync();
            }
        }

        //public async Task<Entiteti.Izvestaj> DobaviIzvestajPoId(Guid id)
        //{
        //    Entiteti.Izvestaj izvestaj = _aplikacioniDbContext.Izvestaji.SingleOrDefault(i => i.ID == id);
        //    return izvestaj ?? throw new KeyNotFoundException("Izvestaj nije pronađen.");
        //}

        public async Task IzmeniIzvestaj(Guid idIzvestaja,PromeniIzvestajDTO promeniIzvestaj)
        {
            Entiteti.Izvestaj izvestaj=await _aplikacioniDbContext.Izvestaji.FindAsync(idIzvestaja);

            if (izvestaj != null)
            {
                izvestaj.datum = promeniIzvestaj.datum;
                izvestaj.Opis=promeniIzvestaj.Opis;
                izvestaj.Recepti=promeniIzvestaj.Recepti;
                izvestaj.Dijagnoza = promeniIzvestaj.Dijagnoza;
                izvestaj.Napomena=promeniIzvestaj.Napomena;
            }

            await _aplikacioniDbContext.SaveChangesAsync();
        }
    }
}
