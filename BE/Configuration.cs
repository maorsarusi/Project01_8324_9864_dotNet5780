using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        private static int m_guestRequestKey = 20000000;
        public static int GuestRequestKey { get { return m_guestRequestKey; } set { m_guestRequestKey = value; } }

        private static int m_hostKey = 30000000;
        public static int HostKey { get { return m_hostKey; } set { m_hostKey = value; } }

        private static int m_hostingUnitKey = 40000000;
        public static int HostingUnitKey { get { return m_hostingUnitKey; } set { m_hostingUnitKey = value; } }

        private static int m_orderKey = 50000000;
        public static int OrderKey { get { return m_orderKey; } set { m_orderKey = value; } }

        private static int m_feePerDay = 10;
        public static int FeePerDay { get { return m_feePerDay; } set { m_feePerDay = value; } }
    }
}
