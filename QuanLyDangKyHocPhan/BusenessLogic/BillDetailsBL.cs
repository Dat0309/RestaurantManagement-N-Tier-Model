using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusenessLogic
{
    public class BillDetailsBL
    {
        BillDeetailsDA bdDA = new BillDeetailsDA();

        public List<BillDetails> GetAll()
        {
            return bdDA.GetAll();
        }

        public BillDetails GetByID(int ID)
        {
            List<BillDetails> list = GetAll();
            foreach (var bill in list)
                if(bill.Id == ID)
                    return bill;
            return null;
        }

        public int Insert(BillDetails billDetail)
        {
            return bdDA.Insert_Update_Delete(billDetail, 0);
        }

        public int Update(BillDetails bd)
        {
            return bdDA.Insert_Update_Delete(bd, 1);
        }
        public int Detele(BillDetails bd)
        {
            return bdDA.Insert_Update_Delete(bd, 2);
        }
    }
}
