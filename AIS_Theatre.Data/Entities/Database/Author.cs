using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Author : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Country { get; set; }
        public Genre Genre { get; set; }

        public Author(string fullName, string birthDate, string deathDate, string country, Genre genre)
        {
            this.Id = Guid.NewGuid();
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.DeathDate = deathDate;
            this.Country = country;
            this.Genre = genre;
        }

        public Author(Guid id, string fullName, string birthDate, string deathDate, string country, Genre genre)
        {
            this.Id = id;
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.DeathDate = deathDate;
            this.Country = country;
            this.Genre = genre;
        }

        public Author Clone()
        {
            return (Author)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
