using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Infrastructure.Identity.Entiteti
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser(Guid id, string email)
        {
            Id = id;
            Email = email;
            UserName = email;
        }
    }
}
