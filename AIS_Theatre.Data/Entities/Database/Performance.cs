using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Performance : BaseEntity<Guid>
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public Setting Setting { get; set; }

        public Performance(string date, string time, Setting setting)
        {
            this.Id = Guid.NewGuid();
            this.Date = date;
            this.Time = time;
            this.Setting = setting;
        }

        public Performance(Guid id, string date, string time, Setting setting)
        {
            this.Id = id;
            this.Date = date;
            this.Time = time;
            this.Setting = setting;
        }

        public Performance Clone()
        {
            return (Performance)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} at {2}", Setting.ToString(), Date, Time);
        }
    }
}
