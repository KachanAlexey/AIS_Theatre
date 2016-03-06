using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    internal class GenreRepository : BaseRepository<Guid, Genre>
    {
        public GenreRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Genre entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into Genre (id_genre, name_genre) values (@Id, @CName) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.Name},
                        }
                    );

        }

        public override bool Update(Genre entity)
        {
            var res = base.ExecuteNonQuery(
                    "update genre set name_genre = @Name  where id_genre = @Id ",
                    new SqlParameters
                    {
                        {"Id", entity.Id.ToString()},
                        {"Name", entity.Name},
                    }
                );

            return res > 0;
        }
        
        public Genre GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from genre where id_genre = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }
        
        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from genre where id_genre = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public IList<Genre> GetAll()
        {
            return base.ExecuteSelect("Select * from genre");
        }

        protected override Genre DefaultRowMapping(NpgsqlDataReader reader)
        {
            return new Genre(Guid.Parse((string)reader["Id"]), (string)reader["name_genre"]);
        }
    }
}
