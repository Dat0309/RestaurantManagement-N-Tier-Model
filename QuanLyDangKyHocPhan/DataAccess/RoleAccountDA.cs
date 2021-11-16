using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleAccountDA
    {
        public List<RoleAccount> GetAll()
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.RoleAcc_GetAll;

            SqlDataReader reader = cmd.ExecuteReader();
            List<RoleAccount> list = new List<RoleAccount>();
            while (reader.Read())
            {
                RoleAccount account = new RoleAccount();
                account.RoleId = Convert.ToInt32(reader["RoleID"]);
                account.AccountName = reader["AccountName"].ToString();
                account.Actived = bool.Parse(reader["Actived"].ToString());
                account.Notes = reader["Notes"].ToString();
                list.Add(account);
            }
            conn.Close();
            return list;
        }

        public int Insert_Update_Delete(RoleAccount roleAccount, int action)
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.RoleAcc_InsertUpdateDelete;

            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleAccount.RoleId;
            cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100).Value = roleAccount.AccountName;
            cmd.Parameters.Add("@Actived", SqlDbType.Bit).Value = roleAccount.Actived;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 3000).Value = roleAccount.Notes;
            cmd.Parameters.Add("@Action", SqlDbType.Int)
                .Value = action;

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
