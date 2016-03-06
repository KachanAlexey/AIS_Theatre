using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IMusicianRepository : IWorkerRepository
    {
        IList<Musician> GetByInstrument(string Instrument);
    }
}
