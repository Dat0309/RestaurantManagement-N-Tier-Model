using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyDangKyHocPhan.Form1;
using BusenessLogic;
using DataAccess;

namespace QuanLyDangKyHocPhan
{
    public partial class TableForm : Form
    {
        public delegate void Load(Table food,string billId);

        public Table curTable;
        public SendFood send;

        public TableForm()
        {
            InitializeComponent();
        }
        
        public TableForm(SendFood sender)
        {
            InitializeComponent();
            this.send = sender;
        }

        private void SetValue(Table value, string billId)
        {
            this.curTable = value;
            this.send(curTable,billId);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        public void initUI(List<Table> tables)
        {
            foreach (Table table in tables)
            {
                var item = new CustomControl.TableControll(SetValue);

                item.LoadTableName(table.Name, table);
                item.LoadStatus(table.Status);

                flpTables.Controls.Add(item);
            }
        }
    }
}
