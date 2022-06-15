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
    public class EfCoreSqlQueryGuid : ICrudableGuid
    {
        private string _connnectionString;
        public string Name { get; } = "EfCoreSqlQueryGuid";

        public EfCoreSqlQueryGuid(string conString)
        {
            _connnectionString = conString;
        }

        public List<Person2> Select()
        {
            try
            {
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    return context.person2.FromSqlRaw(@"Select * from person2").AsNoTracking().ToList();
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
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    return context.person2.FromSqlRaw(@"Select * from person2 where firstname = '" + firstName + "'").AsNoTracking().ToList();
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
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    context.Database.ExecuteSqlRaw(@"update person2 set firstname ='" + firstName + "' where id = '" + id.ToString() + "'");
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
                using (var context = ApplicationDbContextFactory.CreateDbContext(_connnectionString))
                {
                    context.Database.ExecuteSqlRaw(@"delete from person2 where id = '" + id.ToString() + "'");
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
