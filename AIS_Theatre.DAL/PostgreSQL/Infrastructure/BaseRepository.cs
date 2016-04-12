using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public abstract class BaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        internal readonly NpgsqlConnection _connection;
        internal readonly NpgsqlTransaction _transaction;

        protected BaseRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }


        protected T ExecuteScalar<T>(string sql, IDictionary<string, object> parameters = null)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                return (T)command.ExecuteScalar();
            }
        }

        protected int ExecuteNonQuery(string sql, IDictionary<string, object> parameters = null)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                return command.ExecuteNonQuery();
            }
        }

        protected T ExecuteSingleRowSelect<T>(
            string sql,
            Func<NpgsqlDataReader, T> rowMapping,
            IDictionary<string, object> parameters = null
            )
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return rowMapping(reader);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }


        protected List<T> ExecuteSelect<T>(
            string sql,
            Func<NpgsqlDataReader, T> rowMapping,
            IDictionary<string, object> parameters = null
            )
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                using (var reader = command.ExecuteReader())
                {
                    List<T> list = new List<T>(1);
                    while (reader.Read())
                    {
                        list.Add(rowMapping(reader));
                    }

                    return list;
                }
            }
        }


        protected TEntity ExecuteSingleRowSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSingleRowSelect(sql, DefaultRowMapping, sqlParameters);
        }

        protected List<TEntity> ExecuteSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSelect(sql, DefaultRowMapping, sqlParameters);
        }

        private static void FillParameters(IDictionary<string, object> parameters, NpgsqlCommand command)
        {
            if (parameters != null)
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                }
        }


        public int Upsert(TEntity entity)
        {
            if (Object.Equals(entity.Id, default(TKey)))
                return Insert(entity);
            else
            {/*
                if (Update(entity))
                    return entity.Id;
                else
                    return default(int);*/
            }
            return default(int);
        }

        public abstract int Insert(TEntity entity);
        public abstract bool Update(TEntity entity);
        protected abstract TEntity DefaultRowMapping(NpgsqlDataReader reader);

    }
}
