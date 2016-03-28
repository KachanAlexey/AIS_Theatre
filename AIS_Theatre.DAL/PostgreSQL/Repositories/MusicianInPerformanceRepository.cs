using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class MusicianInPerformanceRepository : BaseRepository<Guid, MusicianInPerformance>, IMusicianInPerformanceRepository
    {
        public MusicianInPerformanceRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(MusicianInPerformance entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into musician_performance (id_musician_performance, id_musician_musician_performance, id_performance_musician_performance) values (@Id, @Musician, @Performance) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Musician", entity.Musician.Id.ToString()},
                            {"Performance", entity.Performance.Id.ToString()}
                        }
                    );

        }

        public override bool Update(MusicianInPerformance entity)
        {
            var res = base.ExecuteNonQuery(
                    "update musician_performance set id_musician_musician_performance = @Musician, id_performance_musician_performance = @Performance where id_musician_performance = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Musician", entity.Musician.Id.ToString()},
                            {"Performance", entity.Performance.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public MusicianInPerformance GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from musician_performance where id_musician_performance = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from musician_performance where id_musician_performance = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<MusicianInPerformance> GetAll()
        {
            return base.ExecuteSelect("select * from musician_performance");
        }

        protected override MusicianInPerformance DefaultRowMapping(NpgsqlDataReader reader)
        {
            MusicianRepository musicianRepository = new MusicianRepository(_connection, _transaction);
            PerformanceRepository performanceRepository = new PerformanceRepository(_connection, _transaction);
            return new MusicianInPerformance(Guid.Parse((string)reader["id_musician_performance"]), musicianRepository.GetById(Guid.Parse((string)reader["id_musician_musician_performance"])), performanceRepository.GetById(Guid.Parse((string)reader["id_performance_musician_performance"])));
        }
    }
}
