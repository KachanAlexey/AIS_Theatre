using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Ticket : BaseEntity<Guid>
    {
        public int Price { get; set; }
        public string Row { get; set; }
        public int Place { get; set; }
        public bool IsFree { get; set; }
        public Performance Performance { get; set; }
        public Subscription Subscription { get; set; }

        public Ticket(int Price, string Row, int Place, bool IsFree, Performance Performance, Subscription Subscription)
        {
            this.Id = Guid.NewGuid();
            this.Price = Price;
            this.Row = Row;
            this.Place = Place;
            this.IsFree = IsFree;
            this.Performance = Performance;
            if (Subscription != null)
            {
                this.Subscription = Subscription;
            }
        }

        public Ticket(Guid id, int Price, string Row, int Place, bool IsFree, Performance Performance, Subscription Subscription)
        {
            this.Id = id;
            this.Price = Price;
            this.Row = Row;
            this.Place = Place;
            this.IsFree = IsFree;
            this.Performance = Performance;
            if (Subscription != null)
            {
                this.Subscription = Subscription;
            }
        }

        public Ticket Clone()
        {
            return (Ticket)base.MemberwiseClone();
        }

        public override string ToString()
        {
            string stringed = string.Format("Performance: {0} \n", Performance.ToString());
            stringed += string.Format("Row: {0} \n", Row);
            stringed += string.Format("Place: {0} \n", Place.ToString());
            if (IsFree)
            {
                stringed += string.Format("Is free \n");
            }
            else
            {
                stringed += string.Format("Is sold \n");
            }
            if (Subscription != null)
            {
                stringed += string.Format("Subscription: {0} \n", Subscription.ToString());
            }
            return stringed;
        }
    }
}
