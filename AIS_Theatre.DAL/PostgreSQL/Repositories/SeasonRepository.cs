using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    internal class SeasonRepository : BaseRepository<Guid, Season>, ISeasonRepository
    {
        public SeasonRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Season entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into season (id_season, begin_date_season, end_date_season) values (@Id, @BeginDate, @EndDate) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"BeginDate", entity.BeginDate},
                            {"EndDate", entity.EndDate},
                        }
                    );

        }

        public override bool Update(Season entity)
        {
            var res = base.ExecuteNonQuery(
                    "update season set begin_date_season = @BeginDate, end_date_season = @EndDate  where id_season = @Id ",
                    new SqlParameters
                    {
                        {"Id", entity.Id.ToString()},
                        {"BeginDate", entity.BeginDate},
                        {"EndDate", entity.EndDate},
                    }
                );

            return res > 0;
        }

        public Season GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from season where id_season = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from season where id_season = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Season> GetAll()
        {
            return base.ExecuteSelect("select * from season");
        }

        protected override Season DefaultRowMapping(NpgsqlDataReader reader)
        {
            return new Season(Guid.Parse((string)reader["id_season"]), (string)reader["begin_date_season"], (string)reader["end_date_season"]);
        }
    }
}
