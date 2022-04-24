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
    public class EfCoreIntNoTracking : ICrudableInt
    {
        private ApplicationDbContext _context;
        public string Name { get; } = "EfCoreIntNoTracking";
        public EfCoreIntNoTracking(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Person> Select()
        {
            try
            {
                return _context.person.AsNoTracking().ToList();
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
                return _context.person.AsNoTracking().Where(x => x.FIO.Contains(firstName)).ToList();
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
                if (id == 0 || string.IsNullOrEmpty(firstname))
                {
                    return;
                }

                var person = _context.person.AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (person == null)
                {
                    return;
                }

                person.FirstName = firstname;
                _context.person.Update(person);
                _context.SaveChanges();
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }


        }

        public void DeleteWhere(int id)
        {
            try
            {
                var person = _context.person.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (person == null)
                {
                    return;
                }
                _context.person.Remove(person);
                _context.SaveChanges();

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
