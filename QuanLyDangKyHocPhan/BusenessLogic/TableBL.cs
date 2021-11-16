using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusenessLogic
{
    public class TableBL
    {
        TableDA tableDA = new TableDA();

        public List<Table> GetAll()
        {
            return tableDA.GetAll();
        }

        public Table GetByID(int ID)
        {
            List<Table> list = GetAll();
            foreach (var item in list)
                if (item.Id == ID)
                    return item;
            return null;
        }

        public List<Table> Find(string key)
        {
            List<Table> list = GetAll();
            List<Table> result = new List<Table>();

            foreach (var item in list)
            {
                if (item.Id.ToString().Contains(key)|| item.Name.Contains(key))
                    result.Add(item);
            }
            return result;
        }

        public int Insert(Table table)
        {
            return tableDA.Insert_Update_Delete(table, 0);
        }

        public int Update(Table table)
        {
            return tableDA.Insert_Update_Delete(table, 1);
        }

        public int Delete(Table table)
        {
            return tableDA.Insert_Update_Delete(table, 2);
        }
    }
}
