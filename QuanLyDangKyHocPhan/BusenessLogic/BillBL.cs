using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusenessLogic
{
    public class BillBL
    {
        BillsDA billDA = new BillsDA();

        public List<Bill> GetAll()
        {
            return billDA.GetAll();
        }

        public Bill GetByID(int ID)
        {
            List<Bill> list = GetAll();
            foreach (var item in list)
                if(item.Id == ID)
                    return item;
            return null;
        }

        public List<Bill> GetByIDTable(int ID)
        {
            List<Bill> list = GetAll();
            List<Bill> result = new List<Bill>();
            foreach(var item in list)
            {
                if(item.TableId == ID)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<Bill> Find(string key)
        {
            List<Bill> bills = GetAll();
            List<Bill> result = new List<Bill>();

            foreach (var bill in bills)
            {
                if (bill.Id.ToString().Contains(key) || bill.Name.Contains(key))
                    result.Add(bill);
            }
            return result;
        }

        public int Insert(Bill bill)
        {
            return billDA.Insert_Update_Delete(bill, 0);
        }

        public int Update(Bill bill)
        {
            return billDA.Insert_Update_Delete(bill, 1);
        }
        public int Delete(Bill bill)
        {
            return billDA.Insert_Update_Delete(bill, 2);
        }
        public int UpdateStatus(Bill bill)
        {
            return billDA.UpdateStatus(bill);
        }
    }
}
