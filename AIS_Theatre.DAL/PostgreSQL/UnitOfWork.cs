using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NpgsqlTransaction _transaction;
        private readonly NpgsqlConnection _connection;
        /*
        private IActorInPerformanceRepository _actorInPerformanceRepository;
        private IActorRepository _actorRepository;
        private IAuthorRepository _authorRepository;
        private ICompositionRepository _compositionRepository;
        private IEmployeeInPerformanceRepository _employeeInPerformanceRepository;
        private IEmployeeRepository _employeeRepository;
        private IEmployeePositionRepository _employeePositionRepository;
        */private IGenreRepository _genreRepository;/*
        private IMusicianInPerformanceRepository _musicianInPerformanceRepository;
        private IMusicianRepository _musicianRepository;
        private IPerformanceRepository _performanceRepository;
        private ISeasonRepository _seasonRepository;
        private ISettingRepository _settingRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private ITicketRepository _ticketRepository;*/
        
        public UnitOfWork()
        {
            var connectionString = ConnectionStringBuilder.getConnectionString("PostgreSQL");
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction(/*IsolationLevel.ReadCommitted*/);
        }
        /*
        public IActorInPerformanceRepository ActorInPerformanceRepository
        {
            get
            {
                if (_actorInPerformanceRepository == null)
                    _actorInPerformanceRepository = new ActorInPerformanceRepository(_connection, _transaction);
                return _actorInPerformanceRepository;
            }
        }

        public IActorRepository ActorRepository
        {
            get
            {
                if (_actorRepository == null)
                    _actorRepository = new ActorRepository(_connection, _transaction);
                return _actorRepository;
            }
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(_connection, _transaction);
                return _authorRepository;
            }
        }

        public ICompositionRepository CompositionRepository
        {
            get
            {
                if (_compositionRepository == null)
                    _compositionRepository = new CompositionRepository(_connection, _transaction);
                return _compositionRepository;
            }
        }

        public IEmployeeInPerformanceRepository EmployeeInPerformanceRepository
        {
            get
            {
                if (_employeeInPerformanceRepository == null)
                    _employeeInPerformanceRepository = new EmployeeInPerformanceRepository(_connection, _transaction);
                return _employeeInPerformanceRepository;
            }
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_connection, _transaction);
                return _employeeRepository;
            }
        }

        public IEmployeePositionRepository EmployeePositionRepository
        {
            get
            {
                if (_employeePositionRepository == null)
                    _employeePositionRepository = new EmployeePositionRepository(_connection, _transaction);
                return _employeePositionRepository;
            }
        }*/

        public IGenreRepository GenreRepository
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_connection, _transaction);
                return _genreRepository;
            }
        }/*
        
        public IMusicianInPerformanceRepository MusicianInPerformanceRepository
        {
            get
            {
                if (_musicianInPerformanceRepository == null)
                    _musicianInPerformanceRepository = new MusicianInPerformanceRepository(_connection, _transaction);
                return _musicianInPerformanceRepository;
            }
        }
        
        public IMusicianRepository MusicianRepository
        {
            get
            {
                if (_musicianRepository == null)
                    _musicianRepository = new MusicianRepository(_connection, _transaction);
                return _musicianRepository;
            }
        }
        
        public IPerformanceRepository PerformanceRepository
        {
            get
            {
                if (_performanceRepository == null)
                    _performanceRepository = new PerformanceRepository(_connection, _transaction);
                return _performanceRepository;
            }
        }

        public ISeasonRepository SeasonRepository
        {
            get
            {
                if (_seasonRepository == null)
                    _seasonRepository = new SeasonRepository(_connection, _transaction);
                return _seasonRepository;
            }
        }

        public ISettingRepository SettingRepository
        {
            get
            {
                if (_settingRepository == null)
                    _settingRepository = new SettingRepository(_connection, _transaction);
                return _settingRepository;
            }
        }

        public ISubscriptionRepository SubscriptionRepository
        {
            get
            {
                if (_subscriptionRepository == null)
                    _subscriptionRepository = new SubscriptionRepository(_connection, _transaction);
                return _subscriptionRepository;
            }
        }

        public ITicketRepository TicketRepository
        {
            get
            {
                if (_ticketRepository == null)
                    _ticketRepository = new TicketRepository(_connection, _transaction);
                return _ticketRepository;
            }
        }*/

        public void Dispose()
        {
            try
            {
                if (_transaction != null) _transaction.Dispose();
            }
            finally
            {
                _connection.Dispose();
            }
        }

        public void Commit()
        {
            if (_transaction != null) _transaction.Commit();
        }

        public void RollBack()
        {
            if (_transaction != null) _transaction.Rollback();
        }
    }
}
