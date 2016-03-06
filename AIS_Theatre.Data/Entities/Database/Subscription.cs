using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Subscription : BaseEntity<Guid>
    {
        public Season Season { get; set; }

        public Subscription(Season season)
        {
            this.Id = Guid.NewGuid();
            this.Season = season;
        }

        public Subscription(Guid id, Season season)
        {
            this.Id = id;
            this.Season = season;
        }

        public Subscription Clone()
        {
            return (Subscription)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Subscription for {0}", Season);
        }
    }
}
