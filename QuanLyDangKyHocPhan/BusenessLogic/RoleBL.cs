using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusenessLogic
{
    public class RoleBL
    {
        RoleDA roleDA = new RoleDA();

        public List<Role> GetAll()
        {
            return roleDA.GetAll();
        }

        public Role GetByID(int ID)
        {
            List<Role> list = GetAll();
            foreach (var item in list)
                if (item.Id == ID)
                    return item;
            return null;
        }

        public List<Role> Find(string key)
        {
            List<Role> list = GetAll();
            List<Role> result = new List<Role>();

            foreach (var item in list)
                if (item.Id.ToString().Contains(key)|| item.RoleName.Contains(key))
                    result.Add(item);
            return result;
        }

        public int Insert(Role role)
        {
            return roleDA.Insert_Update_Delete(role, 0);
        }
        public int Update(Role role)
        {
            return roleDA.Insert_Update_Delete(role, 1);
        }
        public int Delete(Role role)
        {
            return roleDA.Insert_Update_Delete(role, 2);
        }
    }
}
