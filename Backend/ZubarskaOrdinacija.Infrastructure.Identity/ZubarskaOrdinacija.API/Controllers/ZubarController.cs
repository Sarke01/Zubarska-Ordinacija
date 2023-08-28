using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZubarskaOrdinacija.API.Utils;
using ZubarskaOrdinacija.Domain.Entiteti;
using ZubarskaOrdinacija.Domain.Servisi.Zubar;
using ZubarskaOrdinacija.Domain.Servisi.Zubar.DTO;
using ZubarskaOrdinacija.Infrastructure.Identity;

namespace ZubarskaOrdinacija.API.Controllers
{
    public class ZubarController : BaseContoller
    {
        private readonly ZubarService _zubarService;
        public ZubarController(ZubarService zubarService)
        {
            _zubarService = zubarService;
        }   

        [Authorize(Roles = IdentityUloge.ADMIN)]
        [HttpPut("{IdZubara}")]
        public async Task PromeniZubara(Guid IdZubara, PromeniZubaraDTO promeniZubara)
        {
            await _zubarService.PromeniZubara(IdZubara, promeniZubara);
        }

        [Authorize(Roles = IdentityUloge.ADMIN)]
        [HttpDelete("{IdZubara}")]
        public async Task ObrisiZubara(Guid IdZubara)
        {
            await _zubarService.ObrisiZubara(IdZubara);
        }

        [HttpGet]
        public async Task<List<Zubar>> DobaviSveZubare()
        {
           return  await _zubarService.DobaviSveZubare();
        }


    }
}

