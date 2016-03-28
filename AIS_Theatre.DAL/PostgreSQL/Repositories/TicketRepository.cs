using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class TicketRepository : BaseRepository<Guid, Ticket>, ITicketRepository
    {
        public TicketRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Ticket entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into subscription (id_ticket, price_ticket, row_ticket, place_ticket, is_free_ticket, id_performance_ticket, id_subscription_ticket) values (@Id, @Price, @Row, @Place, @IsFree, @Performance, @Subscription) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Price", entity.Price},
                            {"Row", entity.Row},
                            {"IsFree", entity.IsFree},
                            {"Performance", entity.Performance.Id.ToString()},
                            {"Subscription", entity.Subscription.Id.ToString()}
                        }
                    );

        }

        public override bool Update(Ticket entity)
        {
            var res = base.ExecuteNonQuery(
                    "update ticket set price_ticket = @Price, row_ticket = @Row, place_ticket = @Place, is_free_ticket = @IsFree, id_performance_ticket = @Performance, id_subscription_ticket = @Subscription where id_ticket = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Price", entity.Price},
                            {"Row", entity.Row},
                            {"IsFree", entity.IsFree},
                            {"Performance", entity.Performance.Id.ToString()},
                            {"Subscription", entity.Subscription.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public Ticket GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from ticket where id_ticket = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from ticket where id_ticket = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Ticket> GetAll()
        {
            return base.ExecuteSelect("Select * from ticket");
        }

        public List<Ticket> GetByPerformance(Performance performance)
        {
            return base.ExecuteSelect(
                        "Select * from ticket where id_performance_ticket = @Performance",
                        new SqlParameters
                        {
                            {"Performance", performance.Id.ToString()},
                        });
        }

        public List<Ticket> GetBySubscription(Subscription subscription)
        {
            return base.ExecuteSelect(
                        "Select * from ticket where id_subscription_ticket = @Subscription",
                        new SqlParameters
                        {
                            {"Subscription", subscription.Id.ToString()},
                        });
        }

        public List<Ticket> GetFree()
        {
            return base.ExecuteSelect("Select * from ticket where is_free_ticket = true");
        }

        public List<Ticket> GetFreeByPerformance(Performance performance)
        {
            return base.ExecuteSelect(
                        "Select * from ticket where id_performance_ticket = @Performance and is_free_ticket = true",
                        new SqlParameters
                        {
                            {"Performance", performance.Id.ToString()},
                        });
        }

        public List<Ticket> GetByPrice(int price)
        {
            return base.ExecuteSelect(
                        "Select * from ticket where price_ticket = @Price",
                        new SqlParameters
                        {
                            {"Price", price},
                        });
        }

        public List<Ticket> GetByPlaceAndRow(int place, string row)
        {
            return base.ExecuteSelect(
                        "Select * from ticket where row_ticket = @Row and place_ticket = @Place",
                        new SqlParameters
                        {
                            {"Row", row},
                            {"Place", place},
                        });
        }

        public List<Ticket> GetByPlace(int place)
        {
            return base.ExecuteSelect(
                        "Select * from ticket where place_ticket = @Place",
                        new SqlParameters
                        {
                            {"Place", place},
                        });
        }

        public List<Ticket> GetByRow(string row)
        {
            return base.ExecuteSelect(
                        "select * from ticket where row_ticket = @Row",
                        new SqlParameters
                        {
                            {"Row", row},
                        });
        }

        protected override Ticket DefaultRowMapping(NpgsqlDataReader reader)
        {
            PerformanceRepository performanceRepository = new PerformanceRepository(_connection, _transaction);
            Subscription subscription = null;
            if (Guid.Parse((string)reader["id_subscription_ticket"]) != Guid.Empty)
            {
                SubscriptionRepository subscriptionRepository = new SubscriptionRepository(_connection, _transaction);
                subscription = subscriptionRepository.GetById(Guid.Parse((string)reader["id_subscription_ticket"]));
            }
            return new Ticket(Guid.Parse((string)reader["id_subscription"]), (int)reader["price_ticket"], (string)reader["row_ticket"], (int)reader["place_ticket"], (bool)reader["is_free_ticket"], performanceRepository.GetById(Guid.Parse((string)reader["id_performance_ticket"])), subscription);
        }
    }
}
