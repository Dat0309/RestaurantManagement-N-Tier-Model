using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDA
    {
        public List<Role> GetAll()
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Role_GetAll;

            SqlDataReader reader = cmd.ExecuteReader();
            List<Role> list = new List<Role>();
            while (reader.Read())
            {
                Role role = new Role();
                role.Id = Convert.ToInt32(reader["ID"]);
                role.RoleName = reader["RoleName"].ToString();
                role.Path = reader["Path"].ToString();
                role.Notes = reader["Notes"].ToString();
                
                list.Add(role);
            }
            conn.Close();
            return list;
        }

        public int Insert_Update_Delete(Role role, int action)
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Role_InsertUpdateDelete;

            SqlParameter IDPara = new SqlParameter("@ID", SqlDbType.Int);
            IDPara.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(IDPara).Value = role.Id;
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 1000).Value = role.RoleName;
            cmd.Parameters.Add("@Path", SqlDbType.NVarChar, 3000).Value = role.Path;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 3000).Value = role.Notes;
            cmd.Parameters.Add("@Action", SqlDbType.Int)
                .Value = action;

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return (int)cmd.Parameters["@ID"].Value;
            }
            return 0;
            conn.Close();
        }
    }
}
