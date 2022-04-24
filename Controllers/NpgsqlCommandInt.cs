using FactoryMethod.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using FactoryMethod.Interfaces;

namespace FactoryMethod.Controllers
{
    public class NpgsqlCommandInt : ICrudableNpgsqlInt
    {
        private string _connectionString;
        public string Name { get; } = "NpgsqlCommandInt";
        public NpgsqlCommandInt(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Select()
        {
            try
            {
                string cmd = "select id, firstname, lastname, fio, username, password from person";
                NpgsqlDataReader reader;
                DataTable table;

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                    {
                        reader = command.ExecuteReader();
                        table = new DataTable();
                        table.Load(reader);
                        reader.Close();
                    }
                    connection.Close();
                }

                return table;
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor= ConsoleColor.White;
                return new DataTable();
            }

        }
        public DataTable SelectWhere(string firstname)
        {
            try
            {
                string cmd = $"select id, firstname, lastname, fio, username, password from person where firstname = '{firstname}'";
                NpgsqlDataReader reader;
                DataTable table;

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                    {
                        reader = command.ExecuteReader();

                        table = new DataTable();
                        table.Load(reader);

                        reader.Close();
                    }
                    connection.Close();
                }

                return table;
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new DataTable();
            }

        }
        public void UpdateWhere(int id, string firstname)
        {
            try
            {
                string cmd = $"update person set firstname = '{firstname}' where id = {id}";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
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
                string cmd = $"delete from person where id = {id}";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
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
