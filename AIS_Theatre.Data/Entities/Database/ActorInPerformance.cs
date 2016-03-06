using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class ActorInPerformance : BaseEntity<Guid>
    {
        public Actor Actor { get; set; }
        public Performance Performance { get; set; }

        public ActorInPerformance(Actor actor, Performance performance)
        {
            this.Id = Guid.NewGuid();
            this.Actor = actor;
            this.Performance = performance;
        }
        public ActorInPerformance(Guid id, Actor actor, Performance performance)
        {
            this.Id = id;
            this.Actor = actor;
            this.Performance = performance;
        }

        public ActorInPerformance Clone()
        {
            return (ActorInPerformance)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0} in {1}", Actor.ToString(), Performance.ToString());
        }
    }
}
