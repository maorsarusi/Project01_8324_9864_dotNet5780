using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public int HostingUnitKey { get; set; }

        public int GuestRequestKey { get; set; }

        public int OrderKey { get; set; }

        private OrderStatus m_orderStatus = OrderStatus.NotYetAdressed;
        public OrderStatus StatusOrder { get { return m_orderStatus; } set { m_orderStatus = value; } }

        private DateTime m_createDate = DateTime.Today;
        public DateTime CreateDate { get { return m_createDate; } set { m_createDate = value; } }

        public DateTime OrderDate { get; set; }

        public int Fee { get; set; }

        public override string ToString()
        {
            return String.Format("Hosting unit key: {0}\nGuest request key: {1}\nOrder key: {2}\nOrder status: {3}\nCreation date: {4}\nOrder date: {5}\n", HostingUnitKey, GuestRequestKey, OrderKey, StatusOrder, CreateDate, OrderDate);
        }
    }
}
