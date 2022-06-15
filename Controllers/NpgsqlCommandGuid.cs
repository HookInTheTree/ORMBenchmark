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
    public class NpgsqlCommandGuid : ICrudableNpgsqlGuid
    {
        private string _connectionString;
        public string Name { get; } = "NpgsqlCommandGuid";

        public NpgsqlCommandGuid(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Select()
        {
            try
            {
                string cmd = "select id, firstname, lastname, fio, username, password from person2";
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new DataTable();
            }

        }

        public void Create(List<Person2> persons)
        {
            try
            {
                string cmd = $"insert into person2(id, firstname, lastname, fio, username, password) values";

                foreach (var person in persons)
                {
                    cmd += $"('{person.Id.ToString()}', '{person.FirstName}', '{person.LastName}', '{person.FIO}', '{person.UserName}', '{person.Password}'), ";
                }

                cmd = cmd.Remove(cmd.LastIndexOf(','));
                cmd += ";";

                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public DataTable SelectWhere(string firstname)
        {
            try
            {
                string cmd = $"select id, firstname, lastname, fio, username, password from person2 where firstname = '{firstname}'";
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new DataTable();
            }

        }

        public void UpdateWhere(Guid id, string firstname)
        {
            try
            {
                string cmd = $"update person2 set firstname = '{firstname}' where id = '{id.ToString()}'";

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

        public void DeleteWhere(Guid id)
        {
            try
            {
                string cmd = $"delete from person2 where id = '{id.ToString()}'";

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
