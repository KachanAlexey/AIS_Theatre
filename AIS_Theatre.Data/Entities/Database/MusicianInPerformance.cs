using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class MusicianInPerformance : BaseEntity<Guid>
    {
        public Musician Musician { get; set; }
        public Performance Performance { get; set; }

        public MusicianInPerformance(Musician musician, Performance performance)
        {
            this.Id = Guid.NewGuid();
            this.Musician = musician;
            this.Performance = performance;
        }

        public MusicianInPerformance(Guid id, Musician musician, Performance performance)
        {
            this.Id = id;
            this.Musician = musician;
            this.Performance = performance;
        }

        public MusicianInPerformance Clone()
        {
            return (MusicianInPerformance)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0} in {1}", Musician.ToString(), Performance.ToString());
        }
    }
}
