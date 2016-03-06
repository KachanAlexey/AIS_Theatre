using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Actor : Worker
    {
        public ActorsFeatures ActorsFeatures { get; set; }
        public Actor Stunt { get; set; }

        public Actor(string fullName, string birthDate, int salary, string experience, ActorsFeatures actorsFeatures, Actor stunt)
        {
            this.Id = Guid.NewGuid();
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.Experience = experience;
            this.ActorsFeatures = actorsFeatures;
            if (stunt != null)
            {
                this.Stunt = stunt;
            }
        }
        public Actor(Guid id, string fullName, string birthDate, int salary, string experience, ActorsFeatures actorsFeatures, Actor stunt)
        {
            this.Id = id;
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.Experience = experience;
            this.ActorsFeatures = actorsFeatures;
            if (stunt != null)
            {
                this.Stunt = stunt;
            }
        }

        public Actor Clone()
        {
            return (Actor)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
