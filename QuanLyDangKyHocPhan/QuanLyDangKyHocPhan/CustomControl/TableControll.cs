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
using static QuanLyDangKyHocPhan.TableForm;
using DataAccess;
using BusenessLogic;

namespace QuanLyDangKyHocPhan.CustomControl
{
    public partial class TableControll : UserControl
    {
        private string billID;
        private Table table;
        public Load send;
        public TableControll()
        {
            InitializeComponent();
        }

        public TableControll(Load sender)
        {
            InitializeComponent();
            this.send = sender;
        }
        private int InsertBillsTable()
        {
            Bill bill = new Bill();
            bill.Id = 0;
            bill.Name = "Hoá đơn bàn " + table.Name;
            bill.TableId = table.Id;
            bill.Amount = 0;
            bill.Discount = 0;
            bill.Tax = 0;
            bill.Status = false;
            bill.CheckoutDate = DateTime.Now;
            bill.Account = "dtdat";

            BillBL billBL = new BillBL();
            return billBL.Insert(bill);
        }

        private int UpdateStatusTable(int status)
        {
            Table table = this.table;
            table.Id = this.table.Id;
            table.Status = status;

            TableBL tableBL = new TableBL();
            return tableBL.TableStatus_Update(table);
        }

        private void TableControll_Load(object sender, EventArgs e)
        {

        }

        private void TableControll_Click(object sender, EventArgs e)
        {
            if (btnValid.Visible == false)
            {
                try
                {
                    if (send != null)
                    {
                        UpdateStatusTable(1);
                        InsertBillsTable();
                        btnValid.Visible = true;
                        this.send(this.table, billID);
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SqlError");
                }
                
            }
            else
            {
                try
                {
                    BillForm billForm = new BillForm();
                    billForm.LoadBills(table.Id);
                    //billForm.FormClosing += new FormClosingEventHandler(frmClosed);
                    if(billForm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadStatus(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SqlError");
                }
            }
        }

        //private void frmClosed(object sender, FormClosingEventArgs e)
        //{
        //    LoadStatus(0);
        //}

        public void LoadTableName(string tableName, Table currentTable)
        {
            this.table = currentTable;
            lbNameTable.Text = "Bàn " + tableName;
        }
        public void LoadStatus(int status)
        {
            if (status == 0)
                btnValid.Visible = false;
            else
                btnValid.Visible = true;
        }
    }
}
