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
        List<Ticket> GetByPerformance(Performance performance);
        List<Ticket> GetBySubscription(Subscription subscription);
        List<Ticket> GetFree();
        List<Ticket> GetFreeByPerformance(Performance performance);
        List<Ticket> GetByPrice(int price);
        List<Ticket> GetByPlaceAndRow(int place, string row);
        List<Ticket> GetByPlace(int place);
        List<Ticket> GetByRow(string row);
    }
}
