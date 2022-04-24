using FactoryMethod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Interfaces
{
    internal interface ICrudableInt
    {
        public string Name { get; }
        public List<Person> Select();
        public List<Person> SelectWhere(string firstName);
        public void UpdateWhere(int id, string firstName);
        public void DeleteWhere(int id);
    }
}
