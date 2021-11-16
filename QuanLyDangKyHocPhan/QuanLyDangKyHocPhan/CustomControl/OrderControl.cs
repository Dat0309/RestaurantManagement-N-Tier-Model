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

namespace QuanLyDangKyHocPhan.CustomControl
{
    public partial class OrderControl : UserControl
    {
        int foodId, billId, billDetailId;

        public OrderControl()
        {
            InitializeComponent();
        }

        private void OrderControl_Load(object sender, EventArgs e)
        {

        }

        private int UpDateBillDetail(int quantity)
        {
            BillDetails billDetails = new BillDetails();
            billDetails.Id = billDetailId;
            billDetails.InvoiceId = billId;
            billDetails.Quantity = quantity;

            BillDetailsBL bdBL = new BillDetailsBL();
            return bdBL.Update(billDetails);
        }

        public int GetQuantity()
        {
            return int.Parse(nmrCount.Text);
        }

        public void initUI(string title,string date, int price, int quantity, int foodId, int billId, int billDetailId)
        {
            this.billDetailId = billDetailId;
            this.foodId = foodId;
            this.billId = billId;
            lbTitle.Text = title;
            lbDate.Text = date;
            lbPrice.Text = (price*quantity).ToString();
            nmrCount.Text = quantity.ToString();
        }

        private void nmrCount_ValueChanged(object sender, EventArgs e)
        {
            UpDateBillDetail(int.Parse(nmrCount.Value.ToString()));
        }
    }
}
