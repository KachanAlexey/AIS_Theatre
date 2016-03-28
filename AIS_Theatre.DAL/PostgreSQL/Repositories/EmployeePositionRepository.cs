using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    internal class EmployeePositionRepository : BaseRepository<Guid, EmployeePosition>, IEmployeePositionRepository
    {
        public EmployeePositionRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(EmployeePosition entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into employee_position (id_employee_position, name_employee_position) values (@Id, @CName) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.Name},
                        }
                    );

        }

        public override bool Update(EmployeePosition entity)
        {
            var res = base.ExecuteNonQuery(
                    "update employee_position set name_employee_position = @Name  where id_employee_position = @Id ",
                    new SqlParameters
                    {
                        {"Id", entity.Id.ToString()},
                        {"Name", entity.Name},
                    }
                );

            return res > 0;
        }

        public EmployeePosition GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from employee_position where id_employee_position = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from employee_position where id_employee_position = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<EmployeePosition> GetAll()
        {
            return base.ExecuteSelect("select * from employee_position");
        }

        protected override EmployeePosition DefaultRowMapping(NpgsqlDataReader reader)
        {
            return new EmployeePosition(Guid.Parse((string)reader["id_employee_position"]), (string)reader["name_employee_position"]);
        }
    }
}
