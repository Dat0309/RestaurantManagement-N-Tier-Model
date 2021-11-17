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
    public partial class FoodControlForm : Form
    {
        public FoodControlForm()
        {
            InitializeComponent();
        }
        public void loadAdd()
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
        }
        public void loadUpdate()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
        }
        private void InitValues()
        {
            string connString = "server=WINDOWS-11\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Category";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            conn.Open();

            adapter.Fill(ds, "Category");

            cbbCate.DataSource = ds.Tables["Category"];
            cbbCate.DisplayMember = "Name";
            cbbCate.ValueMember = "ID";

            conn.Close();
        }
        private void ResetText()
        {
            txtID.ResetText();
            txtName.ResetText();
            txtNotes.ResetText();
            txtUnit.ResetText();
            cbbCate.ResetText();
            nmrPrice.ResetText();
            txtPhoto.ResetText();
        }

        private void FoodControlForm_Load(object sender, EventArgs e)
        {
            this.InitValues();
        }
        public void DisplayFoodInfo(DataRowView rowView)
        {
            try
            {
                txtID.Text = rowView["ID"].ToString();
                txtName.Text = rowView["Name"].ToString();
                txtUnit.Text = rowView["Unit"].ToString();
                txtNotes.Text = rowView["Notes"].ToString();
                nmrPrice.Text = rowView["Notes"].ToString();
                txtPhoto.Text = rowView["Picture"].ToString();
                cbbCate.SelectedIndex = -1;

                for (int index = 0; index < cbbCate.Items.Count; index++)
                {
                    var cat = cbbCate.Items[index] as DataRowView;
                    if (cat["ID"].ToString() == rowView["FoodCategoryID"].ToString())
                    {
                        cbbCate.SelectedIndex = index;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                this.Close();
            }
        }

        private int InsertFood()
        {
            string defaultPhoto = "E:\\NAM3\\Restaurant - Managerment - Winform - C -\\QuanLyDangKyHocPhan\\QuanLyDangKyHocPhan\\Resources\\food.png";
            Food food = new Food();

            food.ID = 0;
            food.Name = txtName.Text;
            food.Unit = txtUnit.Text;
            food.FoodCategoryID = int.Parse(cbbCate.SelectedValue.ToString());
            food.Price = int.Parse(nmrPrice.Value.ToString());
            food.Notes = txtNotes.Text;
            food.Picture = txtPhoto.Text == "" ? defaultPhoto : txtPhoto.Text;

            FoodBL foodBL = new FoodBL();
            return foodBL.Insert(food);
        }

        private int UpdateFood()
        {
            string defaultPhoto = "E:\\NAM3\\Restaurant - Managerment - Winform - C -\\QuanLyDangKyHocPhan\\QuanLyDangKyHocPhan\\Resources\\food.png";
            Food food = new Food();
            food.ID = int.Parse(txtID.Text);
            food.Name = txtName.Text;
            food.Unit = txtUnit.Text;
            food.FoodCategoryID = int.Parse(cbbCate.SelectedValue.ToString());
            food.Price = int.Parse(nmrPrice.Value.ToString());
            food.Notes = txtNotes.Text;
            food.Picture = txtPhoto.Text == "" ? defaultPhoto : txtPhoto.Text;

            FoodBL foodBL = new FoodBL();
            return foodBL.Update(food);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result = InsertFood();
            if (result > 0)
            {
                MessageBox.Show("Them du lieu hanh cong");
                this.ResetText();
            }
            else MessageBox.Show("Them du lieu khong thanh cong. Vui long kiem tra lai du lieu nhap");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateFood();
            if (result > 0)
            {
                MessageBox.Show("Cap nhat du lieu thanh cong");
                this.ResetText();
            }
            else MessageBox.Show("Cap nhat du lieu khong thanh cong. vVui long kiem tra lai du lieu nhap");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            AddCateForm frm = new AddCateForm();
            frm.FormClosed += new FormClosedEventHandler(frmCloed);
            frm.Show(this);
        }

        private void frmCloed(object sender, FormClosedEventArgs e)
        {
            int index = cbbCate.SelectedIndex;
            cbbCate.SelectedIndex = -1;
            cbbCate.SelectedIndex = index;
            InitValues();
        }

        private void btnAddPic_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = $"D:";
            DialogResult dlg = openFileDialog1.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                txtPhoto.Text = fileName;
            }
        }
    }
}
