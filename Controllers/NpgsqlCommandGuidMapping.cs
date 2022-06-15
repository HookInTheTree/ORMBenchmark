using FactoryMethod.Interfaces;
using FactoryMethod.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Controllers
{
    internal class NpgsqlCommandGuidMapping : ICrudableGuid
    {
        private string _connectionString;
        public string Name { get; } = "NpgsqlCommandGuidMapping";

        public NpgsqlCommandGuidMapping(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person2> Select()
        {
            try
            {
                string cmd = "select id, firstname, lastname, fio, username, password from person2";
                NpgsqlDataReader reader;
                var people = new List<Person2>();

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                    {
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int index = 0;
                            people.Add(new Person2()
                            {
                                Id = reader.GetGuid(index++),
                                FirstName = reader.GetString(index++),
                                LastName = reader.GetString(index++),
                                FIO = reader.GetString(index++),
                                UserName = reader.GetString(index++),
                                Password = reader.GetString(index++),
                            });
                        }
                        reader.Close();
                    }
                    connection.Close();
                }

                return people;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new List<Person2>();
            }

        }


        public List<Person2> SelectWhere(string firstname)
        {
            try
            {
                string cmd = $"select id, firstname, lastname, fio, username, password from person2 where firstname = '{firstname}'";
                NpgsqlDataReader reader;
                List<Person2> people = new List<Person2>();

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, connection))
                    {
                        reader = command.ExecuteReader();
                        int index = 0;
                        while (reader.Read())
                        {
                            index = 0;
                            people.Add(new Person2()
                            {
                                Id = reader.GetGuid(index++),
                                FirstName = reader.GetString(index++),
                                LastName = reader.GetString(index++),
                                FIO = reader.GetString(index++),
                                UserName = reader.GetString(index++),
                                Password = reader.GetString(index++)
                            });
                        }
                        reader.Close();
                    }
                    connection.Close();
                }

                return people;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return new List<Person2>();
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
