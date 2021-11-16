﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusenessLogic
{
    public class AccountBL
    {
        AccountDA accDA = new AccountDA();

        public List<Account> GetAll()
        {
            return accDA.GetAll();
        }

        public Account GetByAccName(string accName)
        {
            List<Account> list = GetAll();
            foreach (Account acc in list)
                if(acc.AccountName == accName)
                    return acc;
            return null;
        }

        public List<Account> Find(string key)
        {
            List<Account> list = GetAll();
            List<Account> result = new List<Account>();
            result.Add(list.Find(x => 
            (x.AccountName.Contains(key))
            || (x.FullName.Contains(key))
            || (x.Email.Contains(key))
            || (x.Tell.Contains(key))));
            return result;
        }

        public int Insert(Account acc)
        {
            return accDA.Insert_Update_Delete(acc, 0);
        }

        public int Update(Account acc)
        {
            return accDA.Insert_Update_Delete(acc, 1);
        }

        public int Delete(Account acc)
        {
            return accDA.Insert_Update_Delete(acc, 2);
        }
    }
}
