using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class AuthorRepository : BaseRepository<Guid, Author>, IAuthorRepository
    {
        public AuthorRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Author entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into author (id_author, full_name_author, birth_date_author, death_date_author, country_author, id_genre_author) values (@Id, @Name, @BirthDate, @DeathDate, @Country, @Genre) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"DeathDate", entity.DeathDate},
                            {"Country", entity.Country},
                            {"Genre", entity.Genre.Id.ToString()},
                        }
                    );

        }

        public override bool Update(Author entity)
        {
            var res = base.ExecuteNonQuery(
                    "update author set full_name_author=@Name, birth_date_author = @BirthDate, death_date_author = @DeathDate, country_author = @Country, id_genre_author = @Genre  where id_author = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"DeathDate", entity.DeathDate},
                            {"Country", entity.Country},
                            {"Genre", entity.Genre.Id.ToString()},
                    }
                );

            return res > 0;
        }

        public Author GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from author where id_author = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from author where id_author = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Author> GetAll()
        {
            return base.ExecuteSelect("Select * from author");
        }

        public List<Author> GetByGenre(Genre genre)
        {
            return base.ExecuteSelect("Select * from author where id_genre_author = @Genre", 
                new SqlParameters
                {
                    { "Genre", genre.Id.ToString() },
                }
                );
        }

        public List<Author> GetByCountry(string country)
        {
            return base.ExecuteSelect("Select * from author where country_author = @Country",
                new SqlParameters
                {
                    { "Country", country },
                }
                );
        }

        protected override Author DefaultRowMapping(NpgsqlDataReader reader)
        {
            GenreRepository genreRepository = new GenreRepository(_connection, _transaction);
            return new Author(Guid.Parse((string)reader["id_author"]), (string)reader["full_name_author"], (string)reader["birth_date_author"], (string)reader["death_date_author"], (string)reader["country_author"], genreRepository.GetById(Guid.Parse((string)reader["id_genre_author"])));
        }
    }
}
