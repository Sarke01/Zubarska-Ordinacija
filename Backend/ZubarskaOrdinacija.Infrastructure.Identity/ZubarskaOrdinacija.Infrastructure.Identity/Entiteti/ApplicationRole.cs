using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Infrastructure.Identity.Entiteti;

    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(Guid id, string name)
        {
            Id = id;
            Name = name;
            NormalizedName = name;
        }
    }

