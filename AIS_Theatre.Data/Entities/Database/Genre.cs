using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AIS_Theatre.Data
{
    public class Genre : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public Genre Clone()
        {
            return (Genre)base.MemberwiseClone();
        }

        public Genre(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Genre(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
