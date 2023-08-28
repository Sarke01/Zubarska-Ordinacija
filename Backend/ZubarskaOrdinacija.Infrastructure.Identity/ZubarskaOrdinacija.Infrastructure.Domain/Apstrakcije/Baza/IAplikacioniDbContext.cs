using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZubarskaOrdinacija.Domain.Entiteti;
using ZubarskaOrdinacija.Domain.Entiteti.Bazni;
using Microsoft.EntityFrameworkCore;


namespace ZubarskaOrdinacija.Domain.Apstrakcije.Baza
{
    public interface IAplikacioniDbContext
    {
        public DbSet<Zubar> Zubari { get; }
        public DbSet<Pacijent> Pacijenti { get; }
        public DbSet<Izvestaj> Izvestaji { get; }
        public DbSet<Termin> Termini { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
