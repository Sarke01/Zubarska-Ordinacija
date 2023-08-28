using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Apstrakcije;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
//using ZubarskaOrdinacija.Domain.Servisi.Korisnik.DTO;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent.DTO;
using ZubarskaOrdinacija.Domain.Servisi.Zubar.DTO;

namespace ZubarskaOrdinacija.Domain.Servisi.Korisnik
{
    public class KorisnikService
    {
        private readonly IIdentityService _identityServis;
        private readonly IAplikacioniDbContext _aplikacioniDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public KorisnikService(IIdentityService identityServis, IAplikacioniDbContext aplikacioniDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _identityServis = identityServis;
            _aplikacioniDbContext = aplikacioniDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DodajPacijentaAsync(KreirajPacijentaDTO noviPacijent)
        {
            Guid noviPacijentId = Guid.NewGuid();
            Entiteti.Pacijent pacijentZaDodati = new(id: noviPacijentId,
                                                     ime: noviPacijent.Ime,
                                                     prezime: noviPacijent.Prezime,
                                                     datumRodjenja: noviPacijent.DatumRodjenja,
                                                     adresa: noviPacijent.Adresa,
                                                     brojTelefona:noviPacijent.BrojTelefona,
                                                     email:noviPacijent.Email);
            _aplikacioniDbContext.Pacijenti.Add(pacijentZaDodati);
            await _aplikacioniDbContext.SaveChangesAsync();

            await _identityServis.RegistrujPacijentaAsync(noviPacijentId, noviPacijent.Email, noviPacijent.Lozinka);
        }

        public async Task DodajZubaraAsync(NoviZubarDTO noviZubar)
        {
            Guid noviZubarId = Guid.NewGuid();
            Entiteti.Zubar zubarZaDodati = new(id: noviZubarId,
                                                     ime: noviZubar.Ime,
                                                     prezime: noviZubar.Prezime,
                                                     brojTelefona: noviZubar.BrojTelefona,
                                                     email: noviZubar.Email);
            _aplikacioniDbContext.Zubari.Add(zubarZaDodati);
            await _aplikacioniDbContext.SaveChangesAsync();
            await _identityServis.RegistrujZubaraAsync(noviZubarId, noviZubar.Email, noviZubar.Lozinka);
        }

        public async Task UlogujKorisnikAsync(string email, string password) =>
            await _identityServis.UlogujKorisnikaAsync(email, password);

        public async Task IzlogujKorisnikaAsync() =>
            await _identityServis.IzlogujKorisnikaAsync();


        public async Task ObrisiKorisnikaAsync(Guid id) =>
            await _identityServis.ObrisiKorisnika(id);

        public async Task<string> GetUserRole()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User;
                var userRoleClaim = user.FindFirst(ClaimTypes.Role);

                if (userRoleClaim != null)
                {
                    return userRoleClaim.Value;
                }

                // Ukoliko korisnik nema ulogu ili nije autentifikovan, možete definisati podrazumevanu ulogu koju treba vratiti.
                // Na primer, ako je podrazumevana uloga "Gost":
                // return "Gost";

                // Ukoliko korisnik nema ulogu ili nije autentifikovan, a nema ni podrazumevanu ulogu, možete vratiti null ili baciti odgovarajući izuzetak.

                return null;
            }
            catch (Exception ex)
            {
                // Obrada grešaka
                throw new Exception("Error occurred while retrieving user role.", ex);
            }
        }




        //public async Task GenerisiTokenZaResetovanjeSifreAsync(string email)
        //{
        //    string token = await _identityServis.PreuzmiTokenZaZaboravljenuSifruAsync(email);
        //    await _emailServis.PosaljiAsync(email, $"Vas token za resetovanje lozinke je: {token}", "Resetovanje lozinke");
        //}

        //public async Task ResetujSifruAsync(string email, string token, string noviPassword) =>
        //    await _identityServis.ResetujSifruAsync(email, token, noviPassword);

        //public async Task ModifikujKorisnikaAsync(Guid id, NoviKorisnikDTO izmenjenKorisnik)
        //{
        //    Entiteti.Korisnik korisnikZaIzmeniti = await _aplikacioniDbContext.Kornisici.FindAsync(id) ??
        //        throw new KeyNotFoundException("Korisnik sa unetim ID-em ne postoji.");

        //    korisnikZaIzmeniti.DatumRodjenja = izmenjenKorisnik.DatumRodjenja;
        //    korisnikZaIzmeniti.Adresa = izmenjenKorisnik.Adresa;
        //    korisnikZaIzmeniti.Ime = izmenjenKorisnik.Ime;
        //    korisnikZaIzmeniti.Prezime = izmenjenKorisnik.Prezime;

        //    await _aplikacioniDbContext.SaveChangesAsync();
        //}

        //public async Task<Entiteti.Korisnik> DobaviKorisnikaPoId(Guid korisnikId) =>
        //    await _aplikacioniDbContext.Kornisici.FindAsync(korisnikId) ?? throw new KeyNotFoundException($"Korisnik sa ID-em: {korisnikId} ne postoji!");
    }
}
