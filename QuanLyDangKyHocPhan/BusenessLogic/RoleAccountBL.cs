using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusenessLogic
{
    public class RoleAccountBL
    {
        RoleAccountDA raDA = new RoleAccountDA();

        public List<RoleAccount> GetAll()
        {
            return raDA.GetAll();
        }

        public int Insert(RoleAccount ra)
        {
            return raDA.Insert_Update_Delete(ra, 0);
        }

        public int Update(RoleAccount ra)
        {
            return raDA.Insert_Update_Delete(ra, 1);
        }

        public int Delete(RoleAccount ra)
        {
            return raDA.Insert_Update_Delete(ra, 2);
        }
    }
}
