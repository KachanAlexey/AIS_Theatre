using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class ActorInPerformanceRepository : BaseRepository<Guid, ActorInPerformance>, IActorInPerformanceRepository
    {
        public ActorInPerformanceRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(ActorInPerformance entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into actor_performance (id_actor_performance, id_actor_actor_performance, id_performance_actor_performance) values (@Id, @Actor, @Performance) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Actor", entity.Actor.Id.ToString()},
                            {"Performance", entity.Performance.Id.ToString()}
                        }
                    );

        }

        public override bool Update(ActorInPerformance entity)
        {
            var res = base.ExecuteNonQuery(
                    "update actor_performance set id_actor_actor_performance = @Actor, id_performance_actor_performance = @Performance where id_actor_performance = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Actor", entity.Actor.Id.ToString()},
                            {"Performance", entity.Performance.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public ActorInPerformance GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from actor_performance where id_actor_performance = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from actor_performance where id_actor_performance = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<ActorInPerformance> GetAll()
        {
            return base.ExecuteSelect("select * from actor_performance");
        }

        protected override ActorInPerformance DefaultRowMapping(NpgsqlDataReader reader)
        {
            ActorRepository actorRepository = new ActorRepository(_connection, _transaction);
            PerformanceRepository performanceRepository = new PerformanceRepository(_connection, _transaction);
            return new ActorInPerformance(Guid.Parse((string)reader["id_actor_performance"]), actorRepository.GetById(Guid.Parse((string)reader["id_actor_actor_performance"])), performanceRepository.GetById(Guid.Parse((string)reader["id_performance_actor_performance"])));
        }
    }
}
