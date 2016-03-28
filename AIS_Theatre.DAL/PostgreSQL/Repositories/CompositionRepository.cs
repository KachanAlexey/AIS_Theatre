using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class CompositionRepository : BaseRepository<Guid, Composition>, ICompositionRepository
    {
        public CompositionRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Composition entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into composition (id_composition, name_composition, year_composition, id_author_composition, id_genre_composition) values (@Id, @Name, @Year, @Author, @Genre) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.Name},
                            {"Year", entity.Year},
                            {"Author", entity.Author.Id.ToString()},
                            {"Genre", entity.Genre.Id.ToString()}
                        }
                    );

        }

        public override bool Update(Composition entity)
        {
            var res = base.ExecuteNonQuery(
                    "update composition set name_composition = @Name, year_composition = @Year, id_author_composition = @Author, id_genre_composition = @Genre where id_composition = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.Name},
                            {"Year", entity.Year},
                            {"Author", entity.Author.Id.ToString()},
                            {"Genre", entity.Genre.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public Composition GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from composition where id_composition = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from composition where id_composition = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Composition> GetAll()
        {
            return base.ExecuteSelect("Select * from composition");
        }

        public List<Composition> GetByGenre(Genre genre)
        {
            return base.ExecuteSelect(
                        "Select * from composition where id_genre_composition = @Genre",
                        new SqlParameters
                        {
                            {"Genre", genre.Id.ToString()},
                        });
        }

        public List<Composition> GetByAuthor(Author author)
        {
            return base.ExecuteSelect(
                        "Select * from composition where id_author_composition = @Author",
                        new SqlParameters
                        {
                            {"Author", author.Id.ToString()},
                        });
        }

        public List<Composition> GetByYear(string year)
        {
            return base.ExecuteSelect(
                        "Select * from composition where year_composition = @Year",
                        new SqlParameters
                        {
                            {"Year", year},
                        });
        }

        public List<Composition> GetByName(string name)
        {
            return base.ExecuteSelect(
                        "Select * from composition where name_composition = @Name",
                        new SqlParameters
                        {
                            {"Name", name},
                        });
        }

        protected override Composition DefaultRowMapping(NpgsqlDataReader reader)
        {
            AuthorRepository authorRepository = new AuthorRepository(_connection, _transaction);
            GenreRepository genreRepository = new GenreRepository(_connection, _transaction);
            return new Composition(Guid.Parse((string)reader["id_composition"]), (string)reader["name_composition"], (string)reader["year_composition"], authorRepository.GetById(Guid.Parse((string)reader["id_author_composition"])), genreRepository.GetById(Guid.Parse((string)reader["id_genre_composition"])));
        }
    }
}
