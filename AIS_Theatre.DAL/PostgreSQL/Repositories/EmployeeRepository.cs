using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class EmployeeRepository : BaseRepository<Guid, Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Employee entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into employee (id_employee, full_name_employee, birth_date_employee, salary_employee, experience_employee, id_employee_position_employee) values (@Id, @Name, @BirthDate, @Salary, @Experience, @Position) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"Salary", entity.Salary},
                            {"Experience", entity.Experience},
                            {"Position", entity.EmployeePosition.Id.ToString()},
                        }
                    );

        }

        public override bool Update(Employee entity)
        {
            var res = base.ExecuteNonQuery(
                    "update employee set full_name_employee=@Name, birth_date_employee = @BirthDate, salary_employee = @Salary, experience_employee = @Experience, id_employee_position_employee = @Position  where id_employee = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"Salary", entity.Salary},
                            {"Experience", entity.Experience},
                            {"Position", entity.EmployeePosition.Id.ToString()},
                    }
                );

            return res > 0;
        }

        public Employee GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from employee where id_employee = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from employee where id_employee = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Employee> GetAll()
        {
            return base.ExecuteSelect("Select * from employee");
        }

        public List<Employee> GetEmployeesOfPerformance(Performance performance)
        {
            return base.ExecuteSelect(
                        "select * from employee_performance left join employee on employee.id_employee = employee_performance.id_employee_employee_performance where employee_performance.id_performance_employee_performance = @Performance",
                        new SqlParameters
                        {
                            {"Performance", performance.Id.ToString()}
                        });
        }

        public List<Employee> GetByPosition(EmployeePosition position)
        {
            return base.ExecuteSelect(
                        "Select * from employee where id_employee_position_employee = @Position",
                        new SqlParameters
                        {
                            {"Position", position.Id.ToString()},
                        });
        }

        public List<Employee> GetByBirthDate(string birthDate)
        {
            return base.ExecuteSelect(
                        "Select * from employee where birth_date_employee = @BirthDate",
                        new SqlParameters
                        {
                            {"BirthDate", birthDate},
                        });
        }

        public List<Employee> GetByExperience(string experince)
        {
            return base.ExecuteSelect(
                        "Select * from employee where experience_employee = @Experince",
                        new SqlParameters
                        {
                            {"Experince", experince},
                        });
        }

        public List<Employee> GetBySalary(int salary)
        {
            return base.ExecuteSelect(
                        "Select * from employee where salary_employee = @Salary",
                        new SqlParameters
                        {
                            {"Salary", salary},
                        });
        }

        protected override Employee DefaultRowMapping(NpgsqlDataReader reader)
        {
            EmployeePositionRepository employeePositionRepository = new EmployeePositionRepository(_connection, _transaction);
            return new Employee(Guid.Parse((string)reader["id_employee"]), (string)reader["full_name_employee"], (string)reader["birth_date_employee"], (int)reader["salary_employee"], (string)reader["experience_employee"], employeePositionRepository.GetById(Guid.Parse((string)reader["id_employee_position_employee"])));
        }
    }
}
