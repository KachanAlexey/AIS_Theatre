using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class SettingRepository : BaseRepository<Guid, Setting>, ISettingRepository
    {
        public SettingRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Setting entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into setting (id_setting, name_setting, premiere_date_setting, id_composition_setting) values (@Id, @Name, @Date, @Composition) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.Name},
                            {"Date", entity.PremiereDate},
                            {"Composition", entity.Composition.Id.ToString()}
                        }
                    );

        }

        public override bool Update(Setting entity)
        {
            var res = base.ExecuteNonQuery(
                    "update setting set name_setting = @Name, premiere_date_setting = @Date, id_composition_setting = @Composition where id_setting = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.Name},
                            {"Date", entity.PremiereDate},
                            {"Composition", entity.Composition.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public Setting GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from setting where id_setting = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from setting where id_setting = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Setting> GetAll()
        {
            return base.ExecuteSelect("Select * from setting");
        }

        public List<Setting> GetByName(string name)
        {
            return base.ExecuteSelect(
                        "Select * from setting where name_setting = @Name",
                        new SqlParameters
                        {
                            {"Name", name},
                        });
        }

        public List<Setting> GetByPremiereDate(string date)
        {
            return base.ExecuteSelect(
                        "Select * from setting where premiere_date_setting = @Date",
                        new SqlParameters
                        {
                            {"Date", date},
                        });
        }

        public List<Setting> GetByComposition(Composition composition)
        {
            return base.ExecuteSelect(
                        "Select * from setting where id_composition_setting = @Composition",
                        new SqlParameters
                        {
                            {"Composition", composition.Id.ToString()},
                        });
        }
        
        protected override Setting DefaultRowMapping(NpgsqlDataReader reader)
        {
            CompositionRepository compositionRepository = new CompositionRepository(_connection, _transaction);
            return new Setting(Guid.Parse((string)reader["id_setting"]), (string)reader["name_setting"], (string)reader["premiere_date_setting"], compositionRepository.GetById(Guid.Parse((string)reader["id_composition_setting"])));
        }
    }
}
