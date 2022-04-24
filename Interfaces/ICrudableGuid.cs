using FactoryMethod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Interfaces
{
    interface ICrudableGuid
    {
        public string Name { get; }
        public List<Person2> Select();
        public List<Person2> SelectWhere(string firstName);
        public void UpdateWhere(Guid id, string firstName);
        public void DeleteWhere(Guid id);
    }
}
