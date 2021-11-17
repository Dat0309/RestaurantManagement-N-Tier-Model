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
    public partial class BillForm : Form
    {
        List<Bill> bills = new List<Bill>();
        private int tableId;
        private string billId;
        public BillForm()
        {
            InitializeComponent();
        }

        private void BillForm_Load(object sender, EventArgs e)
        {

        }

        private int UpdateStatusTable(int status)
        {
            TableBL tableBL = new TableBL();
            Table table = tableBL.GetByID(tableId);
            table.Id = tableId;
            table.Status = status;

            return tableBL.TableStatus_Update(table);
        }

        //Update status Bills
        private int UpdateStatusBill(bool status)
        {
            Bill bill = new Bill();
            bill.Id = int.Parse(billId);
            bill.Status = status;

            BillBL billBL = new BillBL();
            return billBL.UpdateStatus(bill);
        }
        public void LoadBills(int tableId)
        {
            dgvBills.Columns.Clear();
            this.tableId = tableId;
            
            BillBL billBL = new BillBL();
            bills = billBL.GetByIDTable(tableId);

            dgvBills.DataSource = bills;
            dgvBills.Columns[0].ReadOnly = true;

            dgvBills.Columns[0].HeaderText = "Mã hoá đơn";
            dgvBills.Columns[0].HeaderText = "Tên hoá đơn";
            dgvBills.Columns[0].HeaderText = "Mã bàn ăn";
            dgvBills.Columns[0].HeaderText = "Số tiền";
            dgvBills.Columns[0].HeaderText = "Khuyến mãi";
            dgvBills.Columns[0].HeaderText = "Thuế";
            dgvBills.Columns[0].HeaderText = "Thanh toán";
            dgvBills.Columns[0].HeaderText = "Ngày nhập";
            dgvBills.Columns[0].HeaderText = "Nhân viên";

            //conn.Close();
        }

        private void dgvBills_DoubleClick(object sender, EventArgs e)
        {
            var billID = dgvBills.SelectedRows[0].Cells[0].Value.ToString();

            BillDetailForm frm = new BillDetailForm();
            frm.LoadDetail(billID);
            frm.ShowDialog(this);
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int index = dgvBills.CurrentRow.Index;
            if (index != null)
            {
                billId = dgvBills.Rows[index].Cells[0].Value.ToString();
                UpdateStatusTable(0);
                UpdateStatusBill(true);

                MessageBox.Show("Thanh toán thành công");
                this.LoadBills(tableId);
                DialogResult = DialogResult.OK;
            }
        }

        private void dgvBills_Click(object sender, EventArgs e)
        {
            int index = dgvBills.CurrentRow.Index;
            if (index != null)
            {
                lbSumPrice.Text = dgvBills.Rows[index].Cells["Amount"].Value.ToString();
                txtDiscount.Text = dgvBills.Rows[index].Cells["Discount"].Value.ToString();
                txtTax.Text = dgvBills.Rows[index].Cells["Tax"].Value.ToString();

                int discount = int.Parse(lbSumPrice.Text) * int.Parse(txtDiscount.Text);
                int tax = int.Parse(lbSumPrice.Text) * int.Parse(txtTax.Text);

                txtAmount.Text = (int.Parse(lbSumPrice.Text) - discount - tax).ToString();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            int index = dgvBills.CurrentRow.Index;
            if (index != null)
            {
                billId = dgvBills.Rows[index].Cells[0].Value.ToString();
                UpdateStatusTable(0);
                
            }
        }
    }
}
