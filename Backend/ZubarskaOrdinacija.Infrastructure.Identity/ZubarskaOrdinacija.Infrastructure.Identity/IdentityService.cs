using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Apstrakcije;
using ZubarskaOrdinacija.Infrastructure.Identity.Entiteti;

namespace ZubarskaOrdinacija.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task IzlogujKorisnikaAsync() =>
        await _signInManager.SignOutAsync();


        public async Task RegistrujPacijentaAsync(Guid id, string email, string password)
        {
            ApplicationUser noviKorisnik = new(id, email);
            IdentityResult rezultatRegistracije = await _userManager.CreateAsync(noviKorisnik, password);
            if (!rezultatRegistracije.Succeeded)
            {
                throw new ArgumentException(rezultatRegistracije.ToString());
            }
            await _userManager.AddToRoleAsync(noviKorisnik, IdentityUloge.PACIJENT);
        }

        public async Task RegistrujZubaraAsync(Guid id, string email, string password)
        {
            ApplicationUser noviKorisnik = new(id, email);
            IdentityResult rezultatRegistracije = await _userManager.CreateAsync(noviKorisnik, password);
            if (!rezultatRegistracije.Succeeded)
            {
                throw new ArgumentException(rezultatRegistracije.ToString());
            }
            await _userManager.AddToRoleAsync(noviKorisnik, IdentityUloge.ZUBAR);
        }

        public async Task UlogujKorisnikaAsync(string email, string password)
        {
            SignInResult rezultat = await _signInManager.PasswordSignInAsync(email,
                                                              password,
                                                              isPersistent: false,
                                                              lockoutOnFailure: false);

            if (!rezultat.Succeeded)
                throw new ArgumentException(rezultat.ToString());
        }

        public async Task<bool> ObrisiKorisnika(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }       
    }
}
