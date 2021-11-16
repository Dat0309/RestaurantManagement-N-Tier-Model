using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillDetails
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
    }
}
