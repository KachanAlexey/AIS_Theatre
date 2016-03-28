using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        //IActorInPerformanceRepository ActorInPerformanceRepository { get; }
        //IActorRepository ActorRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        //ICompositionRepository CompositionRepository { get; }
        //IEmployeeInPerformanceRepository EmployeeInPerformanceRepository { get; }
        //IEmployeeRepository EmployeeRepository { get; }
        //IMusicianInPerformanceRepository MusicianInPerformanceRepository { get; }
        //IMusicianRepository MusicianRepository { get; }
        //IPerformanceRepository PerformanceRepository { get; }
        //ISettingRepository SettingRepository { get; }
        //ISubscriptionRepository SubscriptionRepository { get; }
        //ITicketRepository TicketRepository { get; }
        void Commit();
        void RollBack();
    }
}
