﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillsDA
    {
        public List<Bill> GetAll()
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Bills_GetAll;

            SqlDataReader reader = cmd.ExecuteReader();
            List<Bill> list = new List<Bill>();
            while (reader.Read())
            {
                Bill bill = new Bill();
                bill.Id = Convert.ToInt32(reader["ID"]);
                bill.Name = reader["Name"].ToString();
                bill.TableId = Convert.ToInt32(reader["TableeID"]);
                bill.Amount = Convert.ToInt32(reader["Amount"]);
                bill.Discount = float.Parse((string)reader["Discount"]);
                bill.Tax = float.Parse((string)reader["Tax"]);
                bill.Status = bool.Parse((string)reader["Status"]);
                bill.CheckoutDate = DateTime.Parse((string)reader["CheckoutDate"]);
                bill.Account = reader["Account"].ToString();

                list.Add(bill);
            }
            conn.Close();
            return list;
        }

        // them, xoa, sua theo thu tuc
        public int Insert_Update_Delete(Bill bill, int action)
        {
            SqlConnection conn = new SqlConnection(Ultilities.ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = Ultilities.Category_InsertUpdateDelete;

            SqlParameter IDPara = new SqlParameter("@ID", SqlDbType.Int);
            IDPara.Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters.Add(IDPara).Value = bill.Id;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value = bill.Name;
            cmd.Parameters.Add("@TableID", SqlDbType.Int).Value = bill.TableId;
            cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = bill.Amount;
            cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = bill.Discount;
            cmd.Parameters.Add("@Tax", SqlDbType.Float).Value = bill.Tax;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = bill.Status;
            cmd.Parameters.Add("@CheckoutDate", SqlDbType.SmallDateTime).Value = bill.CheckoutDate;
            cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 100).Value = bill.Account;
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