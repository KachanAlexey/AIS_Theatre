using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class EmployeeInPerformanceRepository : BaseRepository<Guid, EmployeeInPerformance>, IEmployeeInPerformanceRepository
    {
        public EmployeeInPerformanceRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(EmployeeInPerformance entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into employee_performance (id_employee_performance, id_employee_employee_performance, id_performance_employee_performance) values (@Id, @Employee, @Performance) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Employee", entity.Employee.Id.ToString()},
                            {"Performance", entity.Performance.Id.ToString()}
                        }
                    );

        }

        public override bool Update(EmployeeInPerformance entity)
        {
            var res = base.ExecuteNonQuery(
                    "update employee_performance set id_employee_employee_performance = @Employee, id_performance_employee_performance = @Performance where id_employee_performance = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Employee", entity.Employee.Id.ToString()},
                            {"Performance", entity.Performance.Id.ToString()}
                    }
                );

            return res > 0;
        }

        public EmployeeInPerformance GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from employee_performance where id_employee_performance = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from employee_performance where id_employee_performance = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<EmployeeInPerformance> GetAll()
        {
            return base.ExecuteSelect("select * from employee_performance");
        }

        protected override EmployeeInPerformance DefaultRowMapping(NpgsqlDataReader reader)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository(_connection, _transaction);
            PerformanceRepository performanceRepository = new PerformanceRepository(_connection, _transaction);
            return new EmployeeInPerformance(Guid.Parse((string)reader["id_employee_performance"]), employeeRepository.GetById(Guid.Parse((string)reader["id_employee_employee_performance"])), performanceRepository.GetById(Guid.Parse((string)reader["id_performance_employee_performance"])));
        }
    }
}
