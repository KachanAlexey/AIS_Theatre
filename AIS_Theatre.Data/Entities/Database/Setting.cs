using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Setting : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string PremiereDate { get; set; }
        public Composition Composition { get; set; }

        public Setting(string name, string premiereDate, Composition composition)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.PremiereDate = premiereDate;
            this.Composition = composition;
        }

        public Setting(Guid id, string name, string premiereDate, Composition composition)
        {
            this.Id = id;
            this.Name = name;
            this.PremiereDate = premiereDate;
            this.Composition = composition;
        }

        public Setting Clone()
        {
            return (Setting)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
