using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class SubscriptionRepository : BaseRepository<Guid, Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Subscription entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into subscription (id_subscription, id_season_subscription) values (@Id, @Season) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Season", entity.Season.Id.ToString()}
                        }
                    );

        }

        public override bool Update(Subscription entity)
        {
            var res = base.ExecuteNonQuery(
                    "update subscription set id_season_subscription = @Season where id_subscription = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Season", entity.Season.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public Subscription GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from subscription where id_subscription = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from subscription where id_subscription = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Subscription> GetAll()
        {
            return base.ExecuteSelect("Select * from subscription");
        }

        public List<Subscription> GetBySeason(Season season)
        {
            return base.ExecuteSelect(
                        "Select * from subscription where id_season_subscription = @Season",
                        new SqlParameters
                        {
                            {"Season", season.Id.ToString()},
                        });
        }

        protected override Subscription DefaultRowMapping(NpgsqlDataReader reader)
        {
            SeasonRepository seasonRepository = new SeasonRepository(_connection, _transaction);
            return new Subscription(Guid.Parse((string)reader["id_subscription"]), seasonRepository.GetById(Guid.Parse((string)reader["id_season_subscription"])));
        }
    }
}
