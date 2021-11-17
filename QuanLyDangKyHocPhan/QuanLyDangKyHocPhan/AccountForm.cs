using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusenessLogic;
using DataAccess;

namespace QuanLyDangKyHocPhan
{
    public partial class AccountForm : Form
    {
        List<Account> accounts = new List<Account>();
        Account curAcc = new Account();
        public AccountForm()
        {
            InitializeComponent();
        }
        private void LoadAccount()
        {
            AccountBL accountBL = new AccountBL();
            accounts = accountBL.GetAll();

            dgvAccount.DataSource = accounts;
            dgvAccount.Columns[0].ReadOnly = true;
            VietSub();
            for (int i = 0; i < dgvAccount.RowCount - 1; i++)
            {
                MahoaMD5(dgvAccount.Rows[i].Cells["Password"].Value.ToString(), i);
            }
        }

        private void MahoaMD5(string pass, int index)
        {
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            dgvAccount.Rows[index].Cells["Password"].Value = sb.ToString();
        }

        private void dgvAccount_Click(object sender, EventArgs e)
        {
            int index = dgvAccount.CurrentRow.Index;
            curAcc = accounts[index];
            if (index != null)
            {
                txtAccountName.Text = dgvAccount.Rows[index].Cells["AccountName"].Value.ToString();
                txtPass.Text = dgvAccount.Rows[index].Cells["Password"].Value.ToString();
                txtFullName.Text = dgvAccount.Rows[index].Cells["FullName"].Value.ToString();
                txtEmail.Text = dgvAccount.Rows[index].Cells["Email"].Value.ToString();
                mtbPhone.Text = dgvAccount.Rows[index].Cells["Tell"].Value.ToString();

                btnUpdate.Enabled = true;
            }
        }

        private void ResetForm()
        {
            txtAccountName.Text = "";
            txtPass.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            mtbPhone.Text = "";
            btnUpdate.Enabled = false;
        }

        private int InsertAccount()
        {
            if (Validation())
            {
                Account account = new Account();
                account.AccountName = txtAccountName.Text;
                account.Password = txtPass.Text;
                account.FullName = txtFullName.Text;
                account.Email = txtEmail.Text;
                account.Tell = mtbPhone.Text;
                account.DateCreate = DateTime.Now.ToShortDateString();

                AccountBL accountBL = new AccountBL();
                return accountBL.Insert(account);
            }
            else
            {
                MessageBox.Show("Loi", "Vui lòng điền đủ thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int UpdateAccount()
        {
            if (Validation())
            {
                Account acc = curAcc;
                acc.AccountName = txtAccountName.Text;
                acc.Password = txtPass.Text;
                acc.Tell = mtbPhone.Text;
                acc.Email = txtEmail.Text;
                acc.FullName = txtFullName.Text;
                acc.DateCreate = DateTime.Now.ToShortDateString();

                AccountBL accBL = new AccountBL();
                return accBL.Update(acc);
            }
            else
            {
                MessageBox.Show("Loi", "Vui lòng điền đủ thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return -1;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result = InsertAccount();
            if(result > 0)
            {
                MessageBox.Show("Them du lieu hanh cong");
                LoadAccount();
                ResetForm();
            }
            else MessageBox.Show("Loi", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateAccount();
            if (result > 0)
            {
                LoadAccount();
                ResetForm();
                MessageBox.Show("Cap nhat tai khoan thanh cong");
            }
            else MessageBox.Show("Cap nhat du lieu khong thanh cong. vVui long kiem tra lai du lieu nhap");
            
        }

        private void dgvAccount_DoubleClick(object sender, EventArgs e)
        {
            var name = dgvAccount.SelectedRows[0].Cells[0].Value.ToString();
            RoleForm frm = new RoleForm();
            frm.LoadRole(name);
            frm.ShowDialog(this);
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string defaultPass = "123456";
                string connectionString = "server=WINDOWS-11\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConn = new SqlConnection(connectionString);
                SqlCommand sqlComd = sqlConn.CreateCommand();

                sqlComd.CommandText = string.Format("UPDATE Account SET Password = '{1}' WHERE AccountName = '{0}' ",
                    txtAccountName.Text, defaultPass);

                sqlConn.Open();

                sqlComd.ExecuteNonQuery();
                MessageBox.Show("Cap nhat thanh cong");
                LoadAccount();
                ResetForm();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sql Error");
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtAccountName.Text)) return false;
            else if (string.IsNullOrEmpty(txtPass.Text)) return false;
            else if (string.IsNullOrEmpty(txtEmail.Text)) return false;
            else if (string.IsNullOrEmpty(txtFullName.Text)) return false;
            return true;
        }

        private void VietSub()
        {
            dgvAccount.Columns[0].HeaderText = "Tài khoản";
            dgvAccount.Columns[1].HeaderText = "Mật khẩu";
            dgvAccount.Columns[2].HeaderText = "Họ tên";
            dgvAccount.Columns[3].HeaderText = "Email";
            dgvAccount.Columns[4].HeaderText = "Điện thoại";
            dgvAccount.Columns[5].HeaderText = "Ngày tạo";
        }
    }
}
