using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillDeetailsDA
    {
        public List<BillDetails> GetAll()
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.BillDetails_GetAll;

            SqlDataReader reader = cmd.ExecuteReader();
            List<BillDetails> list = new List<BillDetails>();
            while (reader.Read())
            {
                BillDetails bd = new BillDetails();
                bd.Id = Convert.ToInt32(reader["ID"]);
                bd.InvoiceId = Convert.ToInt32(reader["InvoiceID"]);
                bd.FoodId = Convert.ToInt32(reader["FoodID"]);
                bd.Quantity = Convert.ToInt32(reader["Quantity"]);
                list.Add(bd);
            }

            conn.Close();
            return list;
        }

        public int Insert_Update_Delete(BillDetails billDetails, int action)
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.BillDetails_InsertUpdateDelete;

            SqlParameter IDPara = new SqlParameter("@ID", SqlDbType.Int);
            IDPara.Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters.Add(IDPara).Value = billDetails.Id;

            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = billDetails.InvoiceId;
            cmd.Parameters.Add("@FoodID", SqlDbType.Int).Value = billDetails.FoodId;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = billDetails.Quantity;
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
