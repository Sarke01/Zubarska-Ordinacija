using Microsoft.AspNetCore.Mvc;

namespace ZubarskaOrdinacija.API.Utils
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseContoller : ControllerBase
    {
        protected Guid ID => Guid.Parse(HttpContext.User.Claims.ElementAt(0).Value);
    }
}
