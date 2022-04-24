using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Interfaces
{
    internal interface ICrudableNpgsqlInt
    {
        public string Name { get; }
        public DataTable Select();
        public DataTable SelectWhere(string firstName);
        public void UpdateWhere(int id, string firstName);
        public void DeleteWhere(int id);
    }
}
