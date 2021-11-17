using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using BusenessLogic;

namespace QuanLyDangKyHocPhan
{
    public partial class AddCateForm : Form
    {

        public AddCateForm()
        {
            InitializeComponent();
        }

        private int InsertCate()
        {
            Category cate = new Category();
            cate.ID = 0;
            cate.Name = txtNameCat.Text;
            cate.Type = int.Parse(cbbType.Text == "Đồ ăn" ? "1" : "0");

            CategoryBL catBL = new CategoryBL();
            return catBL.Insert(cate);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int reslut = InsertCate();
            if(reslut > 0)
            {
                MessageBox.Show("Them du lieu hanh cong");
                this.ResetText();
            }
            MessageBox.Show("Adding category failed");
        }

        private void ResetText()
        {
            txtID.ResetText();
            txtNameCat.ResetText();
            cbbType.ResetText();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCateForm_Load(object sender, EventArgs e)
        {
            LoadType();
        }

        private void LoadType()
        {
            string connString = "server=WINDOWS-11\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT Type From Category";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();

            adapter.Fill(dt);
            conn.Close();

            cbbType.DataSource = dt;
            cbbType.DisplayMember = "Type";
        }
    }
}
