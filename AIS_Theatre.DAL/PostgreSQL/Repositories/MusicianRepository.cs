using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class MusicianRepository : BaseRepository<Guid, Musician>, IMusicianRepository
    {
        public MusicianRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Musician entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into musician (id_musician, full_name_musician, birth_date_musician, salary_musician, experience_musician, musical_instrument_musician) values (@Id, @Name, @BirthDate, @Salary, @Experience, @Instrument) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"Salary", entity.Salary},
                            {"Experience", entity.Experience},
                            {"Instrument", entity.MisucalInstrument},
                        }
                    );

        }

        public override bool Update(Musician entity)
        {
            var res = base.ExecuteNonQuery(
                    "update musician set full_name_musician = @Name, birth_date_musician = @BirthDate, salary_musician = @Salary, experience_musician = @Experience, musical_instrument_musician = @Instrument  where id_musician = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"Salary", entity.Salary},
                            {"Experience", entity.Experience},
                            {"Instrument", entity.MisucalInstrument},
                    }
                );

            return res > 0;
        }

        public Musician GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from musician where id_musician = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from musician where id_musician = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Musician> GetAll()
        {
            return base.ExecuteSelect("Select * from musician");
        }

        public List<Musician> GetMusiciansOfPerformance(Performance performance)
        {
            return base.ExecuteSelect(
                        "select * from musician_performance left join musician on musician.id_musician = musician_performance.id_musician_musician_performance where musician_performance.id_performance_musician_performance = @Performance",
                        new SqlParameters
                        {
                            {"Performance", performance.Id.ToString()}
                        });
        }

        public List<Musician> GetByInstrument(string instrument)
        {
            return base.ExecuteSelect(
                        "Select * from musician where musical_instrument_musician = @Instrument",
                        new SqlParameters
                        {
                            {"Instrument", instrument},
                        });
        }

        public List<Musician> GetByBirthDate(string birthDate)
        {
            return base.ExecuteSelect(
                        "Select * from musician where birth_date_musician = @BirthDate",
                        new SqlParameters
                        {
                            {"BirthDate", birthDate},
                        });
        }

        public List<Musician> GetByExperience(string experince)
        {
            return base.ExecuteSelect(
                        "Select * from musician where experience_musician = @Experince",
                        new SqlParameters
                        {
                            {"Experince", experince},
                        });
        }

        public List<Musician> GetBySalary(int salary)
        {
            return base.ExecuteSelect(
                        "Select * from musician where salary_musician = @Salary",
                        new SqlParameters
                        {
                            {"Salary", salary},
                        });
        }

        protected override Musician DefaultRowMapping(NpgsqlDataReader reader)
        {
            return new Musician(Guid.Parse((string)reader["id_musician"]), (string)reader["full_name_musician"], (string)reader["birth_date_musician"], (int)reader["salary_musician"], (string)reader["experience_musician"], (string)reader["musical_instrument_musician"]);
        }
    }
}
