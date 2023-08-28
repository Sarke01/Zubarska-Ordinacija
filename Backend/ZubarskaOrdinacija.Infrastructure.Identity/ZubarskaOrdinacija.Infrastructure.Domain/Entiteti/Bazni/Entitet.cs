using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZubarskaOrdinacija.Domain.Entiteti.Bazni
{
    public class Entitet<TId>
    {
        public TId ID { get; private set; }

        protected Entitet(TId id)
        {
            ID = id;
        }
        protected Entitet()
        {
        }
    }
}
