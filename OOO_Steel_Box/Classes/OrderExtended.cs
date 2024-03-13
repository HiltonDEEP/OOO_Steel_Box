using OOO_Steel_Box.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOO_Steel_Box.Classes
{
    internal class OrderExtended
    {
        //данные о заказе
        public Model.Orders order { get; set; }
        public List<Model.PCBuilds> pcBuilds { get; set; }

        //public Model.PCBuilds builds { get; set; }

        public int OrderNumber
        {
            get { return this.order.OrderID; }
        }
        public DateTime OrderDate
        {
            get { return this.order.OrderData; }
        }
        public string UserName
        {
            get { return this.order.Users.UserFullName; }
        }
        public int OrderPrice
        {
            get { return this.order.PCBuilds.PCBuildPrice; }
        }
        public double OrderDiscountPerecent
        {
            get { return (double)this.order.PCBuilds.PCBuildDiscount; }
        }


    }
}
