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
    public partial class TableInfoForm : Form
    {
        List<Table> tables = new List<Table>();
        Table curTable = new Table();
        public TableInfoForm()
        {
            InitializeComponent();
        }

        public void LoadTable()
        {
            TableBL tableBL = new TableBL();
            tables = tableBL.GetAll();

            dgvTable.DataSource = tables;
            dgvTable.Columns[0].ReadOnly = true;
            dgvTable.Columns[0].HeaderText = "Mã bàn";
            dgvTable.Columns[1].HeaderText = "Tên bàn";
            dgvTable.Columns[2].HeaderText = "Tình trạng";
            dgvTable.Columns[3].HeaderText = "Số chỗ";
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text)) return false;
            else if (string.IsNullOrEmpty(cbbStatus.Text)) return false;
            else if (string.IsNullOrEmpty(txtCapacity.Text)) return false;
            return true;
        }
        private void ResetForm()
        {
            txtName.Text = "";
            cbbStatus.Text = "";
            txtCapacity.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private int InsertTable()
        {
            if (Validation())
            {
                Table table = new Table();
                table.Id = 0;
                table.Name = txtName.Text;
                table.Status = int.Parse(cbbStatus.Text == "Trống" ? "0" : "1");
                table.Capacity = int.Parse(txtCapacity.Text);

                TableBL tableBL = new TableBL();
                return tableBL.Insert(table);
            }
            return -1;
        }

        private int UpdateTable()
        {
            if (Validation())
            {
                Table table = curTable;
                table.Name = txtName.Text;
                table.Status = int.Parse(cbbStatus.Text == "Trống" ? "0" : "1");
                table.Capacity = int.Parse(txtCapacity.Text);

                TableBL tableBL = new TableBL();
                return tableBL.Update(table);

            }
            return -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result = InsertTable();
            if (result > 0)
            {
                LoadTable();
                ResetForm();
                MessageBox.Show("Them tai khoan moi thanh cong");
            }
            else
            {
                MessageBox.Show("Vui long nhap day du thong tin", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateTable();
            if (result > 0)
            {
                MessageBox.Show("Cap nhat du lieu thanh cong");
                LoadTable();
                ResetForm();
            }
            else MessageBox.Show("Cap nhat du lieu khong thanh cong. vVui long kiem tra lai du lieu nhap");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var rowSelect = dgvTable.SelectedRows[0];
            TableBL tableBL = new TableBL();
            tableBL.Delete(curTable);

            dgvTable.Rows.Remove(rowSelect);
            LoadTable();
            ResetForm();
            MessageBox.Show("Da xoa thanh cong");
        }

        private void dgvTable_Click(object sender, EventArgs e)
        {
            int index = dgvTable.CurrentRow.Index;
            curTable = tables[index];
            txtName.Text = dgvTable.Rows[index].Cells["Name"].Value.ToString();
            cbbStatus.Text = dgvTable.Rows[index].Cells["Status"].Value.ToString() == "0" ? "Trống" : "Có người";
            txtCapacity.Text = dgvTable.Rows[index].Cells["Capacity"].Value.ToString();

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void tsmViewBills_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var rowSelect = dgvTable.SelectedRows[0];
            string id = dgvTable.SelectedRows[0].Cells[0].Value.ToString();

            FrmTableBills frm = new FrmTableBills();
            frm.Show(this);
            frm.LoadBills(id);
        }

        private void tsmViewMemoryBills_Click(object sender, EventArgs e)
        {
            BillsMemoryForm frm = new BillsMemoryForm();
            frm.Show(this);
            frm.LoadMemory();
        }
    }
}
