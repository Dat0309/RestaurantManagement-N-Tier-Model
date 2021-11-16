using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TableDA
    {
        public List<Table> GetAll()
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Table_GetAll;

            SqlDataReader reader = cmd.ExecuteReader();
            List<Table> list = new List<Table>();
            while (reader.Read())
            {
                Table table = new Table();
                table.Id = Convert.ToInt32(reader["ID"]);
                table.Name = reader["Name"].ToString();
                table.Status = Convert.ToInt32(reader["Status"]);
                table.Capacity = Convert.ToInt32(reader["Capacity"]);

                list.Add(table);
            }
            conn.Close();
            return list;
        }

        public int Insert_Update_Delete(Table table, int action)
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Table_InsertUpdateDelete;

            SqlParameter IDPara = new SqlParameter("@ID", SqlDbType.Int);
            IDPara.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(IDPara).Value = table.Id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value = table.Name;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = table.Status;
            cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value = table.Capacity;
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
