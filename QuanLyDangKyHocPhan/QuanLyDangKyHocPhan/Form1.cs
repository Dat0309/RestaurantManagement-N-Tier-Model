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
using BusenessLogic;
using DataAccess;

namespace QuanLyDangKyHocPhan
{
    public partial class Form1 : Form
    {
        // Truyền, nhận dữ liệu từ form Table
        public delegate void SendFood(Table table,string billId);
        // Truyền, nhận dũe liệu từ form Food
        public delegate void ReceiveFood(Food food);
        List<Bill> bills = new List<Bill>();

        private string curBill,curBillDetail;
        private Table curTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region cac ham xu ly

        // Click in table user control
        private void SetValue(Table value,string billId)
        {
            this.lbNameTable.Text = "Bàn " + value.Name;
            this.curBill = billId;
            this.curTable = value;
        }

        //Create billDetail
        private int InsertBillDetail(string billId, int foodId, int quantity)
        {
            BillDetails bd = new BillDetails();
            bd.Id = 0;
            bd.InvoiceId = int.Parse(billId);
            bd.FoodId = foodId;
            bd.Quantity = quantity;

            BillDetailsBL bdBL = new BillDetailsBL();
            return bdBL.Insert(bd);
        }

        // click in food control
        private void SetFood(Food value)
        {

            var item = new CustomControl.OrderControl();

            InsertBillDetail(curBill, value.ID, item.GetQuantity());
            item.initUI(value.Name, DateTime.Now.ToShortDateString(), value.Price, 1,value.ID,int.Parse(curBill),int.Parse(curBillDetail));
            flOrder.Controls.Add(item);
        }

        // Update status table
        private int UpdateStatusTable(int status)
        {
            Table table = curTable;
            table.Status = status;
            table.Name = curTable.Name;
            table.Capacity = curTable.Capacity;

            TableBL tableBL = new TableBL();
            return tableBL.Update(table);
        }

        //Update status Bills
        private int UpdateStatusBill(bool status)
        {
            Bill bill = new Bill();
            bill.Id = int.Parse(curBill);
            bill.Status = status;

            BillBL billBL = new BillBL();
            return billBL.UpdateStatus(bill);
        }

        // Load Amount of Bills
        private void LoadAmount()
        {
            string connString = "server=WINDOWS-11\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "EXECUTE Amount_Update @id";

            cmd.Parameters.Add("@id", SqlDbType.Int);

            cmd.Parameters["@id"].Value = curBill;
            conn.Open();

            cmd.ExecuteNonQuery();
            cmd.CommandText = $"Select * from Bills where ID = {curBill}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lbSumPrice.Text = (reader["Amount"]).ToString();
                txtDiscount.Text = (reader["Discount"]).ToString();
                txtTax.Text = (reader["Tax"]).ToString();

                int discount = int.Parse(lbSumPrice.Text) * int.Parse(txtDiscount.Text);
                int tax = int.Parse(lbSumPrice.Text) * int.Parse(txtTax.Text);

                txtAmount.Text = (int.Parse(lbSumPrice.Text) - discount - tax).ToString();
            }

            conn.Close();

        }

        // resetform
        private void ResetForm()
        {
            flOrder.Controls.Clear();
            lbSumPrice.Text = "";
            txtAmount.Text = "";
            txtDiscount.Text = "";
            txtTax.Text = "";
            lbNameTable.Text = "New Order";
        }
        #endregion

        // Load listTable onClick
        private void btnListTable_Click(object sender, EventArgs e)
        {
            List<Table> tables = new List<Table>();
            flpFoodList.Controls.Clear();

            TableBL tableBL = new TableBL();
            tables = tableBL.GetAll();
            foreach (var table in tables)
            {
                var item = new CustomControl.TableControll();
                item.LoadTableName(table.Name, table);
                item.LoadStatus(table.Status);

                flpFoodList.Controls.Add(item);
            }
        }

        //Load list Food onClick
        private void btnFood_Click(object sender, EventArgs e)
        {
            List<Food> foods = new List<Food>();
            flpFoodList.Controls.Clear();

            FoodBL foodBL = new FoodBL();
            foods = foodBL.GetAll();

            foreach (var food in foods)
            {
                var item = new CustomControl.DetailFood(SetFood);
                item.LoadFood(food.Name, food.Price, food.Picture, food);

                flpFoodList.Controls.Add(item);
            }
        }

        //Chose table
        private void btnTable_Click(object sender, EventArgs e)
        {
            ResetForm();
            List<Table> tables = new List<Table>();

            TableBL tableBL = new TableBL();
            tables = tableBL.GetAll();

            TableForm frm = new TableForm(SetValue);
            frm.initUI(tables);  
            frm.FormClosed += new FormClosedEventHandler(frmClosed);
            frm.ShowDialog(this);
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            LoadAmount();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            UpdateStatusTable(0);
            UpdateStatusBill(true);
            btnListTable_Click(sender, e);
            ResetForm();
            MessageBox.Show("Thanh toán thành công!!");
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            BillOrderForm frm = new BillOrderForm();
            frm.LoadBills();
            frm.ShowDialog(this);
        }

        private void btnFoodinfo_Click(object sender, EventArgs e)
        {
            FoodInfoForm frm = new FoodInfoForm();
            frm.ShowDialog(this);
        }

        private void gunaGradientTileButton6_Click(object sender, EventArgs e)
        {
            TableInfoForm frm = new TableInfoForm();
            frm.Show(this);
            frm.LoadTable();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            AccountForm frm = new AccountForm();
            frm.ShowDialog(this);
        }

        private void tsmChangePass_Click(object sender, EventArgs e)
        {
            ChangePassForm frm = new ChangePassForm();
            frm.Show(this);

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<Food> foods = new List<Food>();
            flpFoodList.Controls.Clear();

            FoodBL foodBL = new FoodBL();
            foods = foodBL.Find(txtSearch.Text);

            foreach (var food in foods)
            {
                var item = new CustomControl.DetailFood(SetFood);
                item.LoadFood(food.Name, food.Price, food.Picture, food);

                flpFoodList.Controls.Add(item);
            }
        }

        private void frmClosed(object sender, FormClosedEventArgs e)
        {
            btnListTable_Click(sender, e);
        }
    }
}
