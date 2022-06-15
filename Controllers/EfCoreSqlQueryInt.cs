using FactoryMethod.Data;
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
    public class EfCoreSqlQueryInt : ICrudableInt
    {
        private string _connnectionString;
        public string Name { get; } = "EfCoreSqlQueryInt";

        public EfCoreSqlQueryInt(string conString)
        {
            _connnectionString = conString;
        }

        public List<Person> Select()
        {
            try
            {
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    return context.person.FromSqlRaw(@"Select * from person").ToList();
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
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    return context.person.FromSqlRaw(@"Select * from person where firstname = '" + firstName + "'").ToList();
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
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    context.Database.ExecuteSqlRaw(@"update person set firstname ='" + firstName + "' where id = " + id.ToString());
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
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    context.Database.ExecuteSqlRaw(@"delete from person where id = " + id);
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
