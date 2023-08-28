using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
using ZubarskaOrdinacija.Domain.Entiteti;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent;
using ZubarskaOrdinacija.Domain.Servisi.Termini.DTO;
using ZubarskaOrdinacija.Domain.Servisi.Zubar;

namespace ZubarskaOrdinacija.Domain.Servisi.Termini
{
    public class TerminService
    {
        private readonly IAplikacioniDbContext _aplikacioniDbContext;
        private readonly ZubarService _zubarService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PacijentService _pacijentService;


        public TerminService(IAplikacioniDbContext aplikacioniDbContext,ZubarService zubarService, 
            IHttpContextAccessor httpContextAccessor, 
            PacijentService pacijentService)
        {
            _aplikacioniDbContext = aplikacioniDbContext;
            _zubarService = zubarService;
            _httpContextAccessor = httpContextAccessor;
            _pacijentService = pacijentService;
        }
        
        public async Task NapraviNoviTermin(NoviTerminDTO noviTermin)
        {
            Guid terminId= Guid.NewGuid();
            Entiteti.Zubar zubarDb = _zubarService.DobaviZubaraPoImenu(noviTermin.ImeZubara);
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userId, out Guid userIdGuid))
            {
                Entiteti.Pacijent pacijentUlogovan = _pacijentService.DobaviPacijentaPoId(userIdGuid);
                Entiteti.Termin termin = new(id: terminId,
                                zubar: zubarDb,
                                pacijent: pacijentUlogovan,
                                datum: noviTermin.Datum,
                                napomena: noviTermin?.Napomena,
                                status: StatusTermina.Na_Cekanju);
                _aplikacioniDbContext.Termini.Add(termin);
            }
                                              
            await _aplikacioniDbContext.SaveChangesAsync();
        }

        public Entiteti.Termin DobaviTerminPoId(Guid id)
        {
            Entiteti.Termin termin = _aplikacioniDbContext.Termini.FirstOrDefault(t => t.ID == id);
            return termin ?? throw new KeyNotFoundException("Termin nije pronađen.");
        }

        public List<Entiteti.Termin> DobaviTerminePoIdZubara()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userId, out Guid userIdGuid))
            {
                List<Entiteti.Termin> termin = _aplikacioniDbContext.Termini.Where(t => t.Zubar.ID == userIdGuid)
                    .Include(t => t.Zubar)
                    .Include(t => t.Pacijent)
                    .ToList();
                return termin ?? throw new KeyNotFoundException("Termini nije pronađen.");
            }
            
            throw new Exception("Greska servera");
            
        }

        public List<Entiteti.Termin> DobaviTerminePoIdPacijenta()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userId, out Guid userIdGuid))
            {
                List<Entiteti.Termin> termin = _aplikacioniDbContext.Termini.Where(t => t.Pacijent.ID == userIdGuid)
                     .Include(t => t.Zubar)
                     .Include(t => t.Pacijent)
                     .OrderByDescending(t => t.Datum)
                    .ToList();
                return termin ?? throw new Exception("Termini nije pronađen.");
            }

            throw new Exception("Greska servera");
        }

        public async Task PotvrdiTermin(Guid idTermina,string? napomena=null)
        {
            Entiteti.Termin terminDb = _aplikacioniDbContext.Termini
                                              .Where(t => t.ID == idTermina)
                                              .Include(t => t.Zubar)
                                              .Include(t=>t.Pacijent)
                                              .FirstOrDefault() ??
            throw new KeyNotFoundException("Ne postoji Termin sa unetim Id-em!");

            terminDb.Status = StatusTermina.Zakazan;
            if(napomena!=null)
            terminDb.Napomena=napomena;

            await _aplikacioniDbContext.SaveChangesAsync();
        }

        public async Task OdbijTermin(Guid idTermina, string? napomena = null)
        {
            Entiteti.Termin terminDb =  _aplikacioniDbContext.Termini.FirstOrDefault(t => t.ID == idTermina) ??
            throw new KeyNotFoundException("Ne postoji Termin sa unetim Id-em!");

            terminDb.Status = StatusTermina.Odbijen;
            if (napomena != null)
                terminDb.Napomena = napomena;

            await _aplikacioniDbContext.SaveChangesAsync();
        }

        public async void ObrisiTermin(Guid terminId)
        {
            var termin = _aplikacioniDbContext.Termini.FirstOrDefault(t => t.ID == terminId);

            if (termin != null)
            {
                _aplikacioniDbContext.Termini.Remove(termin);
                await _aplikacioniDbContext.SaveChangesAsync();
            }
        }
    }
}
