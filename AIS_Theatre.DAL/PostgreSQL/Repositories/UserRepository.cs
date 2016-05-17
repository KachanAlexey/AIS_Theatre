using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class UserRepository : BaseRepository<Guid, User>, IUserRepository
    {
        public UserRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override int Insert(User entity)
        {
            return (int)
                base.ExecuteNonQuery(
                        "insert into user_account (id_user, login_user, password_user) values (@Id, @Login, @Password)",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Login", entity.Login},
                            {"Passowrd", entity.Password},
                        }
                    );

        }

        public override bool Update(User entity)
        {
            var res = base.ExecuteNonQuery(
                    "update user_account set login_user = @Name, password_user = @Password where id_user = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Login", entity.Login},
                            {"Passowrd", entity.Password},
                    }
                );

            return res > 0;
        }

        public User GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from user_account where id_user = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }
        public User GetUser(string login, string password)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from user_account where login_user = @Login and password_user = @Password",
                    new SqlParameters()
                    {
                        {"Login", login},
                        {"Password", password }
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from user_account where id_user = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<User> GetAll()
        {
            return base.ExecuteSelect("select * from user");
        }

        protected override User DefaultRowMapping(NpgsqlDataReader reader)
        {
            return new User(Guid.Parse((string)reader["id_user"]), (string)reader["login_user"], (string)reader["password_user"]);
        }
    }
}