using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Composition : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }

        public Composition(string name, string year, Author author, Genre genre)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Year = year;
            this.Author = author;
            this.Genre = genre;
        }

        public Composition(Guid id, string name, string year, Author author, Genre genre)
        {
            this.Id = id;
            this.Name = name;
            this.Year = year;
            this.Author = author;
            this.Genre = genre;
        }

        public Composition Clone()
        {
            return (Composition)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
