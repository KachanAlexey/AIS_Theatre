using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface ITicketRepository : IBaseRepository<Guid, Ticket>
    {
        IList<Ticket> GetByPerformance(Performance performance);
        IList<Ticket> GetBySubscription(Subscription subscription);
        IList<Ticket> GetFree();
        IList<Ticket> GetFreeByPerformance(Performance performance);
        IList<Ticket> GetByPrice(int price);
        IList<Ticket> GetByPlaceAndRow(int place, string row);
        IList<Ticket> GetByPlace(int place);
        IList<Ticket> GetByRow(string row);
    }
}
