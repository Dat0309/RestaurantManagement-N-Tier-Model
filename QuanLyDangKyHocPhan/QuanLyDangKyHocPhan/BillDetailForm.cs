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
    public partial class BillDetailForm : Form
    {
        List<BillDetails> billDetailsList = new List<BillDetails>();
        public BillDetailForm()
        {
            InitializeComponent();
        }

        public void LoadDetail(string billID)
        {
            BillDetailsBL bdBL = new BillDetailsBL();
            billDetailsList = bdBL.GetAll();

            dgvDetailBill.DataSource = billDetailsList;
            dgvDetailBill.Columns[0].ReadOnly = true;

            dgvDetailBill.Columns[0].HeaderText = "Mã";
            dgvDetailBill.Columns[1].HeaderText = "Tên món ăn";
            dgvDetailBill.Columns[2].HeaderText = "Số lượng";
        }
    }
}
