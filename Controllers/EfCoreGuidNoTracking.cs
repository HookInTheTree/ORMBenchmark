﻿using FactoryMethod.Data;
using FactoryMethod.Interfaces;
using FactoryMethod.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Controllers
{
    public class EfCoreGuidNoTracking : ICrudableGuid
    {
        private string _connectionString;
        public string Name { get; } = "EfCoreGuidNoTracking";

        public EfCoreGuidNoTracking(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person2> Select()
        {
            try
            {
                using(var _context = ApplicationDbContextFactory.CreateDbContext(_connectionString))
                {
                    return _context.person2.AsNoTracking().ToList();
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
                using (var _context = ApplicationDbContextFactory.CreateDbContext(_connectionString))
                {
                    return _context.person2.AsNoTracking().Where(x => x.FIO.Contains(firstName)).ToList();
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
                using (var _context = ApplicationDbContextFactory.CreateDbContext(_connectionString))
                {
                    var person = _context.person2.AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (person == null)
                    {
                        return;
                    }

                    person.FirstName = firstName;
                    _context.person2.Update(person);
                    _context.SaveChanges();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }


        }

        public void DeleteWhere(Guid id)
        {
            try
            {
                using (var _context = ApplicationDbContextFactory.CreateDbContext(_connectionString))
                {
                    var person = _context.person2.AsNoTracking().FirstOrDefault(x => x.Id == id);
                    if (person == null)
                    {
                        return;
                    }
                    _context.person2.Remove(person);
                    _context.SaveChanges();
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
