using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class ActorsFeatures : BaseEntity<Guid>
    {
        public string Height { get; set; }
        public string Voice { get; set; }
        public string Gender { get; set; }
        public string SkinColor { get; set; }
        public string HairColor { get; set; }

        public ActorsFeatures(string height, string voice, string gender, string skinColor, string hairColor)
        {
            this.Id = Guid.NewGuid();
            this.Height = height;
            this.Voice = voice;
            this.Gender = gender;
            this.SkinColor = skinColor;
            this.HairColor = hairColor;
        }
        public ActorsFeatures(Guid id, string height, string voice, string gender, string skinColor, string hairColor)
        {
            this.Id = id;
            this.Height = height;
            this.Voice = voice;
            this.Gender = gender;
            this.SkinColor = skinColor;
            this.HairColor = hairColor;
        }

        public Actor Clone()
        {
            return (Actor)base.MemberwiseClone();
        }

        public override string ToString()
        {
            string stringed = string.Format("Height: {0} \n", Height);
            stringed += string.Format("Voice: {0} \n", Voice);
            stringed += string.Format("Gender: {0} \n", Gender);
            stringed += string.Format("Skin color: {0} \n", SkinColor);
            stringed += string.Format("Hair color: {0} \n", HairColor);
            return stringed;
        }
    }
}
