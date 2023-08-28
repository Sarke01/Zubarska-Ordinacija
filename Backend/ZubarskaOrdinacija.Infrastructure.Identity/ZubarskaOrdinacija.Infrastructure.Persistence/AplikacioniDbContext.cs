using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
using ZubarskaOrdinacija.Domain.Entiteti;

namespace ZubarskaOrdinacija.Infrastructure.Persistence
{
    public class AplikacioniDbContext : DbContext, IAplikacioniDbContext
    {
        public AplikacioniDbContext(DbContextOptions<AplikacioniDbContext> opcije) : base(opcije)
        {

        }
        public DbSet<Zubar> Zubari => Set<Zubar>();
        public DbSet<Izvestaj> Izvestaji => Set<Izvestaj>();
        public DbSet<Termin> Termini => Set<Termin>();
        public DbSet<Pacijent> Pacijenti => Set<Pacijent>();
    }


}
