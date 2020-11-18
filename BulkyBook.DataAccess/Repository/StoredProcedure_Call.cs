﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class StoredProcedure_Call : IStoredProcedure_Call
    {
        private readonly ApplicationDbContext _db;
        private string _connectionString;
        public StoredProcedure_Call(ApplicationDbContext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            sqlConnection.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            var result = SqlMapper.QueryMultiple(sqlConnection, procedureName, param, commandType: CommandType.StoredProcedure);
            var item1 = result.Read<T1>().ToList();
            var item2 = result.Read<T2>().ToList();

            if(item1 != null && item2 != null)
            {
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
            }

            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return (T)sqlConnection.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection.ExecuteScalar<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
