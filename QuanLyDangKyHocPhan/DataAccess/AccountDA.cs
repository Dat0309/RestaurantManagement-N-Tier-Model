using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDA
    {
       public List<Account> GetAll()
       {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Account_GetAll;

            SqlDataReader reader = cmd.ExecuteReader();
            List<Account> list = new List<Account>();
            while (reader.Read())
            {
                Account account = new Account();
                account.AccountName = reader["AccountName"].ToString();
                account.Password = reader["Password"].ToString();
                account.FullName = reader["FullName"].ToString();
                account.Email = reader["Email"].ToString();
                account.Tell = reader["Tell"].ToString();
                account.DateCreate = reader["DateCreated"].ToString() == null ? DateTime.Parse((reader["DateCreated"].ToString())).ToShortDateString() :
                    (reader["DateCreated"].ToString());

                list.Add(account);
            }

            conn.Close();
            return list;
       }

        public int Insert_Update_Delete(Account account, int action)
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Category_InsertUpdateDelete;

            cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar, 200).Value = account.AccountName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 200).Value = account.Password;
            cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 200).Value = account.FullName;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 200).Value = account.Email;
            cmd.Parameters.Add("@Tell", SqlDbType.NVarChar, 200).Value = account.Tell;
            cmd.Parameters.Add("@DateCreate", SqlDbType.NVarChar, 200).Value = account.DateCreate;

            cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return 1;
            }
            return 0;

            conn.Close();
        }
    }
}
