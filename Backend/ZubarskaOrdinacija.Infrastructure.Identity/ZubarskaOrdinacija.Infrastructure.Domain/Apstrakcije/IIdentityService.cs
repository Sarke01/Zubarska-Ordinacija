using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Apstrakcije
{
    public interface IIdentityService
    {
        public Task RegistrujPacijentaAsync(Guid id, string email, string password);
        public Task UlogujKorisnikaAsync(string email, string password);
        public Task IzlogujKorisnikaAsync();
        public Task<bool> ObrisiKorisnika(Guid userId);
        public Task RegistrujZubaraAsync(Guid id, string email, string password);
        //public Task<string> PreuzmiTokenZaZaboravljenuSifruAsync(string email);
        //public Task ResetujSifruAsync(string email, string token, string noviPassword);
    }
}
