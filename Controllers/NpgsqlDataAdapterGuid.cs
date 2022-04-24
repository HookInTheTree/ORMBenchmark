using FactoryMethod.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Controllers
{
    public class NpgsqlDataAdapterGuid : ICrudableNpgsqlGuid
    {
        private string _connectionString;
        public string Name { get; } = "NpgsqlDataAdapterGuid";
        public NpgsqlDataAdapterGuid(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Select()
        {
            try
            {
                string cmd = "select id, firstname, lastname, fio, username, password from person2";
                DataTable table;

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd, connection))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
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

        public DataTable SelectWhere(string firstname)
        {
            try
            {
                string cmd = $"select id, firstname, lastname, fio, username, password from person2 where firstname = '{firstname}'";
                DataTable table;

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd, connection))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
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
                string cmd = $"update person2 set firstname= '{firstname}' where id='{id.ToString()}'";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                    {
                        adapter.UpdateCommand = connection.CreateCommand();
                        adapter.UpdateCommand.CommandText = cmd;
                        adapter.UpdateCommand.ExecuteNonQuery();
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
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                    {
                        adapter.DeleteCommand = connection.CreateCommand();
                        adapter.DeleteCommand.CommandText = cmd;
                        adapter.DeleteCommand.ExecuteNonQuery();
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
