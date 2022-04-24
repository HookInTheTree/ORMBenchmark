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
    public class DapperInt : ICrudableInt
    {
        private string _connectionString;
        public string Name { get; } = "DapperInt";
        public DapperInt(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> Select()
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    return db.Query<Person>("Select * from person").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new List<Person>();
            }

        }

        public List<Person> SelectWhere(string firstName)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    return db.Query<Person>("Select * from person where firstname = '" + firstName + "'").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new List<Person>();
            }

        }

        public void UpdateWhere(int id, string firstName)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    db.Execute("update person set firstname = '" + firstName + "' where id = " + id.ToString());
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

        public void DeleteWhere(int id)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(_connectionString))
                {
                    db.Execute("Delete person where id = " + id.ToString());
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
