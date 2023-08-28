using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZubarskaOrdinacija.API.Utils;
using ZubarskaOrdinacija.Domain.Entiteti;
using ZubarskaOrdinacija.Domain.Servisi.Termini;
using ZubarskaOrdinacija.Domain.Servisi.Termini.DTO;
using ZubarskaOrdinacija.Infrastructure.Identity;

namespace ZubarskaOrdinacija.API.Controllers
{
    public class TerminController : BaseContoller
    {
        private readonly TerminService _terminService;
        public TerminController(TerminService terminService)
        {
            _terminService = terminService;
        }

        [HttpPost]
        [Authorize]
        public async Task NapraviNoviTermin(NoviTerminDTO noviTermin) =>
            await _terminService.NapraviNoviTermin(noviTermin);

        [HttpGet("zaZubara")]
        [Authorize(Roles =IdentityUloge.ZUBAR)]
        public  List<Termin> DobaviTerminePoIdZubara()
        {
            return _terminService.DobaviTerminePoIdZubara();
        }

        [HttpGet("zaPacijenta")]
        [Authorize(Roles = IdentityUloge.PACIJENT)]
        public List<Termin> DobaviTerminePoIdPacijenta()
        {
            return _terminService.DobaviTerminePoIdPacijenta();
        }

        [Authorize(Roles = IdentityUloge.ZUBAR)]
        [HttpPut("potvrdi")]
        public async Task PotvrdiTermin(Guid id, string? napomena = null)
        {
            await _terminService.PotvrdiTermin(id, napomena);
        }
        [Authorize(Roles = IdentityUloge.ZUBAR)]
        [HttpPut("odbij")]
        public async Task OdbijTermin(Guid id, string? napomena = null)
        {
            await _terminService.OdbijTermin(id, napomena);
        }

    }
}
