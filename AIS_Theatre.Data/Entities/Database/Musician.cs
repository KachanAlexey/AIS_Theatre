using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Musician : Worker
    {
        public string MisucalInstrument { get; set; }

        public Musician(string fullName, string birthDate, int salary, string experience, string misucalInstrument)
        {
            this.Id = Guid.NewGuid();
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.Experience = experience;
            this.MisucalInstrument = misucalInstrument;
        }

        public Musician(Guid id, string fullName, string birthDate, int salary, string experience, string misucalInstrument)
        {
            this.Id = id;
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.Experience = experience;
            this.MisucalInstrument = misucalInstrument;
        }

        public Musician Clone()
        {
            return (Musician)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
