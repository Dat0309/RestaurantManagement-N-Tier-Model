using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Capacity { get; set; }

        public Table()
        {
        }

        public Table(int id, string name, int status, int capacity)
        {
            Id = id;
            Name = name;
            Status = status;
            Capacity = capacity;
        }
    }
}
