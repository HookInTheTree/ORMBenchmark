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
    internal class NpgsqlCommandIntMapping : ICrudableInt
    {
        private string _connectionString;
        public string Name { get; } = "NpgsqlCommandIntMapping";
        public NpgsqlCommandIntMapping(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> Select()
        {
            try
            {
                string cmd = "select id, firstname, lastname, fio, username, password from person";
                NpgsqlDataReader reader;
                List<Person> people = new List<Person>();

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
                            people.Add(new Person()
                            {
                                Id = reader.GetInt32(index++),
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
                return new List<Person>();
            }

        }
        public List<Person> SelectWhere(string firstname)
        {
            try
            {
                string cmd = $"select id, firstname, lastname, fio, username, password from person where firstname = '{firstname}'";
                NpgsqlDataReader reader;
                List<Person> people = new List<Person>();

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
                            people.Add(new Person()
                            {
                                Id = reader.GetInt32(index++),
                                FirstName = reader.GetString(index++),
                                LastName = reader.GetString(index++),
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
                return new List<Person>();
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
