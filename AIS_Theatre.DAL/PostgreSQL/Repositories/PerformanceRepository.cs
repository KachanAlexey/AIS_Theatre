using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class PerformanceRepository : BaseRepository<Guid, Performance>, IPerformanceRepository
    {
        public PerformanceRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Performance entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into performance (id_performance, date_performance, time_performance, id_setting_performance) values (@Id, @Date, @Time, @Setting) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Date", entity.Date},
                            {"Time", entity.Time},
                            {"Setting", entity.Setting.Id.ToString()}
                        }
                    );

        }

        public override bool Update(Performance entity)
        {
            var res = base.ExecuteNonQuery(
                    "update performance set date_performance = @Date, time_performance = @Time, id_setting_performance = @Setting where id_performance = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Date", entity.Date},
                            {"Time", entity.Time},
                            {"Setting", entity.Setting.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public Performance GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from performance where id_performance = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from performance where id_performance = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Performance> GetAll()
        {
            return base.ExecuteSelect("Select * from performance");
        }

        public List<Performance> GetPerformancesOfActor(Actor actor)
        {
            return base.ExecuteSelect(
                        "select * from actor_performance left join performance on performance.id_performance = actor_performance.id_performance_actor_performance where actor_performance.id_actor_actor_performance = @Actor",
                        new SqlParameters
                        {
                            {"Actor", actor.Id.ToString()}
                        });
        }

        public List<Performance> GetPerformancesOfMusician(Musician musician)
        {
            return base.ExecuteSelect(
                        "select * from musician_performance left join performance on performance.id_performance = musician_performance.id_performance_musician_performance where musician_performance.id_musician_musician_performance = @Musician",
                        new SqlParameters
                        {
                            {"Musician", musician.Id.ToString()}
                        });
        }

        public List<Performance> GetPerformancesOfEmployee(Employee employee)
        {
            return base.ExecuteSelect(
                        "select * from employee_performance left join performance on performance.id_performance = employee_performance.id_performance_employee_performance where employee_performance.id_employee_employee_performance = @Employee",
                        new SqlParameters
                        {
                            {"Employee", employee.Id.ToString()}
                        });
        }

        public List<Performance> GetByDate(string date)
        {
            return base.ExecuteSelect(
                        "Select * from performance where date_performance = @Date",
                        new SqlParameters
                        {
                            {"Date", date},
                        });
        }

        public List<Performance> GetByTime(string time)
        {
            return base.ExecuteSelect(
                        "Select * from performance where time_performance = @Time",
                        new SqlParameters
                        {
                            {"Time", time},
                        });
        }

        public List<Performance> GetBySetting(Setting setting)
        {
            return base.ExecuteSelect(
                        "Select * from performance where id_setting_performance = @Setting",
                        new SqlParameters
                        {
                            {"Setting", setting.Id.ToString()},
                        });
        }

        protected override Performance DefaultRowMapping(NpgsqlDataReader reader)
        {
            SettingRepository settingRepository = new SettingRepository(_connection, _transaction);
            return new Performance(Guid.Parse((string)reader["id_performance"]), (string)reader["date_performance"], (string)reader["time_performance"], settingRepository.GetById(Guid.Parse((string)reader["id_setting_performance"])));
        }
    }
}
