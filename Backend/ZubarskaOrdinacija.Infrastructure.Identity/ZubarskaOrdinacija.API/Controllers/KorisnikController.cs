using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZubarskaOrdinacija.API.Modeli.Request;
using ZubarskaOrdinacija.API.Utils;
using ZubarskaOrdinacija.Domain.Servisi.Korisnik;
//using ZubarskaOrdinacija.Domain.Servisi.Korisnik.DTO;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent.DTO;
using ZubarskaOrdinacija.Domain.Servisi.Zubar;
using ZubarskaOrdinacija.Domain.Servisi.Zubar.DTO;
using ZubarskaOrdinacija.Infrastructure.Identity;

namespace ZubarskaOrdinacija.API.Controllers
{
    public class KorisnikController : BaseContoller
    {
        private readonly KorisnikService _korisnikServis;
        private readonly PacijentService _pacijentService;
        private readonly ZubarService _zubarService;
        public KorisnikController(KorisnikService korisnikServis, PacijentService pacijentService, ZubarService zubarService)
        {
            _korisnikServis = korisnikServis;
            _pacijentService = pacijentService;
            _zubarService = zubarService;
        }

        [HttpPost("registracijaPacijenta")]
        public async Task RegistrujKorisnika(KreirajPacijentaDTO noviPacijnet) =>
            await _korisnikServis.DodajPacijentaAsync(noviPacijnet);

        
        [HttpPost("registracijaZubara")]
        public async Task RegistrujZubara(NoviZubarDTO noviZubar) =>
            await _korisnikServis.DodajZubaraAsync(noviZubar);

        [HttpPost("login")]
        public async Task UlogujKorisnika(LoginRequestModel loginRequest) =>
            await _korisnikServis.UlogujKorisnikAsync(loginRequest.Email, loginRequest.Password);

        [HttpPost("logout")]
        public async Task IzlogujKorisnika() => await _korisnikServis.IzlogujKorisnikaAsync();

        [Authorize(Roles =IdentityUloge.ADMIN)]
        [HttpDelete("obrisiZubara/{idZubara}")]
        public async Task ObrisiZubara(Guid idZubara) {
            await _korisnikServis.ObrisiKorisnikaAsync(idZubara);
            await _zubarService.ObrisiZubara(idZubara);
            }

        [Authorize(Roles = IdentityUloge.ADMIN)]
        [HttpDelete("obrisi/{idPacijenta}")]
        public async Task ObrisiPacijenta(Guid idPacijenta)
        {
            await _korisnikServis.ObrisiKorisnikaAsync(idPacijenta);
            await _zubarService.ObrisiZubara(idPacijenta);
        }

        [HttpGet]
        public async Task<string> DobaviUlogovanogKorisnika()
        {
           return await _korisnikServis.GetUserRole();
        }
       

    }
}
