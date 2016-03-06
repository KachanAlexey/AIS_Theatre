using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Season : BaseEntity<Guid>
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }

        public Season(string beginDate, string endDate)
        {
            this.Id = Guid.NewGuid();
            this.BeginDate = beginDate;
            this.EndDate = endDate;
        }

        public Season(Guid id, string beginDate, string endDate)
        {
            this.Id = id;
            this.BeginDate = beginDate;
            this.EndDate = endDate;
        }

        public Season Clone()
        {
            return (Season)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Theatrical season from {0} to {1}", BeginDate, EndDate);
        }
    }
}
