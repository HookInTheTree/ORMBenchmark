using Dapper;
using FactoryMethod.Interfaces;
using FactoryMethod.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Controllers
{
    public class DapperGuid : ICrudableGuid
    {
        private string _connectionString;
        public string Name { get; } = "DapperGuid";
        public DapperGuid(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person2> Select()
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    return db.Query<Person2>("Select * from person2").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new List<Person2>();
            }

        }

        public List<Person2> SelectWhere(string firstName)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    return db.Query<Person2>("Select * from person2 where firstname = '" + firstName + "'").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new List<Person2>();
            }

        }

        public void UpdateWhere(Guid id, string firstName)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    db.Execute("update person2 set firstname = '" + firstName + "' where id = '" + id.ToString() + "'");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        public void DeleteWhere(Guid id)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    db.Execute($"Delete from person2 where id = '{id.ToString()}'");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}
