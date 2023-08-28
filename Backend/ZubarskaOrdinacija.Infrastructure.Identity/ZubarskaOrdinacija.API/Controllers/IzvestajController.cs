using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
using ZubarskaOrdinacija.API.Utils;
using ZubarskaOrdinacija.Domain.Entiteti;
using ZubarskaOrdinacija.Domain.Servisi.Izvestaj;
using ZubarskaOrdinacija.Domain.Servisi.Izvestaj.DTO;
using ZubarskaOrdinacija.Infrastructure.Identity;

namespace ZubarskaOrdinacija.API.Controllers
{
    public class IzvestajController : BaseContoller
    {
        private readonly IzvestajService _izvestajService;
        public IzvestajController(IzvestajService izvestajService)
        {
            _izvestajService = izvestajService;
        }

        [HttpGet("zaPacijenta")]
        [Authorize(Roles =IdentityUloge.PACIJENT)]
        public async Task<List<Izvestaj>> DobaviIzvestajeZaPacijenta()
        {
            return await _izvestajService.DobaviIzvestajePoPacijentId();
        }

        [HttpGet("zaZubara")]
        [Authorize(Roles = IdentityUloge.ZUBAR)]
        public async Task<List<Izvestaj>> DobaviIzvestajeZaZubara()
        {
            return await _izvestajService.DobaviIzvestajePoZubarId();
        }

        [HttpPost]
        //[Authorize(Roles = IdentityUloge.ZUBAR)]
        public async Task KreirajIzvestaj(NoviIzvestajDto noviIzvestaj)
        {
             await _izvestajService.NapraviNoviIzvestaj(noviIzvestaj);
        }



        [HttpPatch]
        [Authorize(Roles = IdentityUloge.ZUBAR)]
        public async Task PromeniIzvestaj(Guid id,PromeniIzvestajDTO promeniIzvestaj)
        {
            await _izvestajService.IzmeniIzvestaj(id,promeniIzvestaj);
        }

        [HttpDelete]
        [Authorize(Roles = IdentityUloge.ZUBAR)]
        public async Task ObrisiIzvestaj(Guid id)
        {
            await _izvestajService.ObrisiIzvestaj(id);
        }
    }
}
